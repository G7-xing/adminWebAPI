using Autofac;
using Autofac.Extensions.DependencyInjection;
using EWI_System.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
        //#region 引入Log4net
        //        .ConfigureLogging((context,loggingBuiler)=> {
        //            loggingBuiler.AddFilter("System",LogLevel.Warning);
        //            loggingBuiler.AddFilter("Microsoft", LogLevel.Warning);
        //            loggingBuiler.AddLog4Net();
        //        })
        //#endregion
        #region 引入autofac
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacModuleRegister());
                })
                #endregion
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
