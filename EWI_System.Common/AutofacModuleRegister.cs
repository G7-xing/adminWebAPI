using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EWI_System.Common
{
    public class AutofacModuleRegister : Autofac.Module
    {
        /// <summary>
        /// 重写 autofac管道中的Load方法，在这里注册注入，自动注入服务的配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            // 程序集注入业务服务
            var IAppServices = Assembly.Load("EWI_System.Service");
            var AppServices = Assembly.Load("EWI_System.Service");
            // 根据名称约定 （服务层与实现层均以Service结尾),实现服务接口和服务实现的依赖注册
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
                .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            
        }
    }
}
