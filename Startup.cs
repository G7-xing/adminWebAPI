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
            
            //ע�������ģ�AOP������Ի�ȡIOC����������ֳɿ�ܱ���Furion���Բ�д��һ��
            services.AddHttpContextAccessor();
            //ע��SqlSugar
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
                   //�����������ã�������������Ч
                   db.Aop.OnLogExecuting = (sql, pars) =>
                               {
                                   //��ȡIOC����Ҫ����һ��������
                                   //vra log=s.GetService<Log>()
                                   Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                                   //��ȡIOC����Ҫ����һ��������
                                   //var appServive = s.GetService<IHttpContextAccessor>();
                                   //var log= appServive?.HttpContext?.RequestServices.GetService<Log>();
                               };
               });
                return sqlSugar;
            });
            //services.AddControllers();
            // ����Json���ص����ڸ�ʽ
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            
            // swagger ����  ���У�WebApplication2������API�����ƣ�v1��API�İ汾�š�
            services.AddSwaggerGen(swagger => {
                swagger.SwaggerDoc("V1", new OpenApiInfo { Title = "WebApplication2", Version = "V1" });
            });
       
            // ����������ͬԴ��������--��������
            services.AddCors(option => {
                option.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("X-Pagination"));
            });
            // ע��automapper������
            services.AddAutoMapper(typeof(AutoMapperConfigs));
            // ע��jwt����
            services.Configure<JWTTokenOptions>(Configuration.GetSection("JWTTokenOptions"));
            #region JWTУ��

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

            // log4netע��
            loggerFactory.AddLog4Net();
            
            // ����swagger�м��
            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("/swagger/V1/swagger.json", "WebApplication2_V1");
            });

            app.UseCors("CorsPolicy");
            #region jwt ��֤

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
