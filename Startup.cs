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
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWI_System.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            //注册上下文：AOP里面可以获取IOC对象，如果有现成框架比如Furion可以不写这一行
            services.AddHttpContextAccessor();
            //注册SqlSugar
            services.AddSingleton<ISqlSugarClient>(s =>
            {
                SqlSugarScope sqlSugar = new SqlSugarScope(new List<ConnectionConfig>()
                {
                    new ConnectionConfig(){
                        ConfigId="0",
                        DbType = SqlSugar.DbType.SqlServer,
                        ConnectionString = Configuration.GetSection("ConnectionStrings").GetChildren().ToList()[0].Value,
                        IsAutoCloseConnection = true,
                        InitKeyType = InitKeyType.Attribute},
                    new ConnectionConfig(){
                        ConfigId="1",
                        DbType = SqlSugar.DbType.SqlServer,
                        ConnectionString = Configuration.GetSection("ConnectionStrings").GetChildren().ToList()[1].Value,
                        IsAutoCloseConnection = true,
                        InitKeyType = InitKeyType.Attribute },
                },
               db =>
               {
                   //单例参数配置，所有上下文生效
                   db.Aop.OnLogExecuting = (sql, pars) =>
                               {
                                   //获取IOC对象不要求在一个上下文
                                   //vra log=s.GetService<Log>()
                                   Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                                   //获取IOC对象要求在一个上下文
                                   //var appServive = s.GetService<IHttpContextAccessor>();
                                   //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
                               };
               });
                return sqlSugar;
            });
            //services.AddControllers();
            // 设置Json返回的日期格式
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            
            // swagger 配置  其中，WebApplication2是您的API的名称，v1是API的版本号。
            services.AddSwaggerGen(swagger => {
                swagger.SwaggerDoc("V1", new OpenApiInfo { Title = "WebApplication2", Version = "V1" });
            });
       
            // 解决浏览器的同源策略问题--跨域问题
            services.AddCors(option => {
                option.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("X-Pagination"));
            });
            // 注册automapper的配置
            services.AddAutoMapper(typeof(AutoMapperConfigs));
            // 注册jwt服务
            services.Configure<JWTTokenOptions>(Configuration.GetSection("JWTTokenOptions"));
            #region JWT校验

            JWTTokenOptions tokenOptions = new JWTTokenOptions();
            Configuration.Bind("JWTTokenOptions", tokenOptions);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = tokenOptions.Audience,
                        ValidIssuer = tokenOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SigningKey))
                    };

                });
            #endregion

            
        }

  

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                ui.SwaggerEndpoint("/swagger/V1/swagger.json", "WebApplication2_V1");
            });

            app.UseCors("CorsPolicy");
            #region jwt 认证

            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
