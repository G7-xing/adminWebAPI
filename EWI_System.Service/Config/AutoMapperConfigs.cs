using AutoMapper;
using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service.Config
{
    public class AutoMapperConfigs : Profile
    {
        /// <summary>
        /// 配置函数构造映射关系
        /// </summary>
        public AutoMapperConfigs() {
            // 从A=>b的映射
            CreateMap<User,LoginRes>();
            CreateMap<Role, RoleRes>();
        }
    }
}
