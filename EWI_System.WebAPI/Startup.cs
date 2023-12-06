using DotLiquid.Util;

using EWI_System.Model;
using EWI_System.Service;
using EWI_System.Service.Config;
using EWI_System.Service.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTaste;

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace EWI_System.WebAPI
{
    /// <summary>
    /// 启动
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 参数
        /// </summary>
        public IConfiguration Configuration { get; }
        

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            
            //注册上下文：AOP里面可以获取IOC对象，如果有现成框架比如Furion可以不写这一行
            services.AddHttpContextAccessor();
            //注册SqlSugar
            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SnowFlakeSingle.WorkId = 6789;
                SqlSugarScope sqlSugar = new SqlSugarScope(new List<ConnectionConfig>()
                {
                    new ConnectionConfig(){
                        ConfigId="System_DB",
                        DbType = SqlSugar.DbType.SqlServer,
                        ConnectionString = Configuration["ConnectionStrings:System_DB"],
                        IsAutoCloseConnection = true,
                        InitKeyType = InitKeyType.Attribute},
                    new ConnectionConfig(){
                        ConfigId="ACE_Traceability_DB",
                        DbType = SqlSugar.DbType.SqlServer,
                        ConnectionString = Configuration["ConnectionStrings:ACE_Traceability_DB"],
                        IsAutoCloseConnection = true,
                        InitKeyType = InitKeyType.Attribute },
                    new ConnectionConfig(){
                        ConfigId="SiplaceSetupCenter_DB",
                        DbType = SqlSugar.DbType.SqlServer,
                        ConnectionString = Configuration["ConnectionStrings:SiplaceSetupCenter_DB"],
                        IsAutoCloseConnection = true,
                        InitKeyType = InitKeyType.Attribute },
                },
               db =>
               {
                   //单例参数配置，所有上下文生效 打印一下sql方便调试
                   db.GetConnection("System_DB").Aop.OnLogExecuting = (sql, pars) => Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                   db.GetConnection("ACE_Traceability_DB").Aop.OnLogExecuting = (sql, pars) =>
                   {
                       //技巧：AOP中获取IOC对象
                       //var serviceBuilder = services.BuildServiceProvider();
                       //var log= serviceBuilder.GetService<ILogger<WeatherForecastController>>()
                       Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                   };
                   db.GetConnection("SiplaceSetupCenter_DB").Aop.OnLogExecuting = (sql, pars) => Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

               }) ;
                return sqlSugar;
            });
            // 解决浏览器的同源策略问题--跨域问题
            services.AddCors(option => {
                option.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            //services.AddControllers();
            // 设置Json返回的日期格式
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            // swagger 配置  其中，WebApplication2是您的API的名称，v1是API的版本号。
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "VueAdmin API",
                    Version = "V1"
                });
                options.DocInclusionPredicate((docName, description) => true);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat="JWT",
                    Scheme = "Bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = string.Join(Environment.NewLine + Environment.NewLine, "将 JWT 放到这里", "格式：Bearer[空格]JWT", "注意是 Bearer[空格] 开头！！！！！！！！", "例如：Bearer 12345abcdef")
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                var xmlComments = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var xmlComment in xmlComments)
                {
                    options.IncludeXmlComments(xmlComment);
                }

                options.AddEnumsWithValuesFixFilters( o =>
                {
                    o.IncludeDescriptions = true;
                });
            });    
           
            // 注册automapper的配置
            services.AddAutoMapper(typeof(AutoMapperConfigs));
            // 注册jwt服务
            services.Configure<JWTTokenOptions>(Configuration.GetSection("JWTTokenOptions"));
            #region JWT校验

            JWTTokenOptions tokenOptions = new JWTTokenOptions();
            Configuration.Bind("JWTTokenOptions", tokenOptions);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SigningKey)),
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                };
            });
            #endregion


        }

  

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // log4net注册
            loggerFactory.AddLog4Net();
            // 启用swagger中间件
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/V1/swagger.json", "VueAdmin API");
            });
            app.UseRouting();

            app.UseCors("CorsPolicy");
            #region jwt 认证

            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers().RequireCors("CorsPolicy");
            });
        }
    }
}
