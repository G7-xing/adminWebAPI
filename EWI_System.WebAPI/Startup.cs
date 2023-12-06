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
    /// ����
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// ����
        /// </summary>
        public IConfiguration Configuration { get; }
        

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            
            //ע�������ģ�AOP������Ի�ȡIOC����������ֳɿ�ܱ���Furion���Բ�д��һ��
            services.AddHttpContextAccessor();
            //ע��SqlSugar
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
                   //�����������ã�������������Ч ��ӡһ��sql�������
                   db.GetConnection("System_DB").Aop.OnLogExecuting = (sql, pars) => Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                   db.GetConnection("ACE_Traceability_DB").Aop.OnLogExecuting = (sql, pars) =>
                   {
                       //���ɣ�AOP�л�ȡIOC����
                       //var serviceBuilder = services.BuildServiceProvider();
                       //var log= serviceBuilder.GetService<ILogger<WeatherForecastController>>()
                       Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                   };
                   db.GetConnection("SiplaceSetupCenter_DB").Aop.OnLogExecuting = (sql, pars) => Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

               }) ;
                return sqlSugar;
            });
            // ����������ͬԴ��������--��������
            services.AddCors(option => {
                option.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            //services.AddControllers();
            // ����Json���ص����ڸ�ʽ
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            // swagger ����  ���У�WebApplication2������API�����ƣ�v1��API�İ汾�š�
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
                    Description = string.Join(Environment.NewLine + Environment.NewLine, "�� JWT �ŵ�����", "��ʽ��Bearer[�ո�]JWT", "ע���� Bearer[�ո�] ��ͷ����������������", "���磺Bearer 12345abcdef")
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
           
            // ע��automapper������
            services.AddAutoMapper(typeof(AutoMapperConfigs));
            // ע��jwt����
            services.Configure<JWTTokenOptions>(Configuration.GetSection("JWTTokenOptions"));
            #region JWTУ��

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

            // log4netע��
            loggerFactory.AddLog4Net();
            // ����swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/V1/swagger.json", "VueAdmin API");
            });
            app.UseRouting();

            app.UseCors("CorsPolicy");
            #region jwt ��֤

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
