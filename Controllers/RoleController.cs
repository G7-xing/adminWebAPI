using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{
    /// <summary>
    /// 菜单接口层
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region 注入服务层
        public IRoleService roleService;
        

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        #endregion

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public R CreateRole(Role role)
        {
            if (string.IsNullOrEmpty(role.RoleName))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (roleService.CreateRole(role))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }

        /// <summary>
        ///获取在库的所有数据----分页
        ///pageNum: 1,
        //pageSize: 10,
        //keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R FetchList(int pageNum, int pageSize, string keyword)
        {
            int total = 0;
            var rolelist = roleService.FetchList(pageNum, pageSize, keyword, ref total);
            return R.OK(rolelist).data("total", total);
        }

        [HttpPost]
        public R UpdateStatus(Role role) 
        {
            if (roleService.UpdateStatus(role))
            {
                return R.OK();
            }
            return R.Error("修改状态失败，请联系管理员");
        }

        [HttpPost]

        public R AllocMenu(RoleReq roleReq)
        {
            if (roleReq.menuIds==null)
            {
                return R.Error("请选择菜单");
            }
            if (roleService.AllocMenu(roleReq))
            {
                return R.OK();
            }
            return R.Error("未知异常，请联系mes");
        }
        [HttpGet]
        public R ListMenuByRole(string roleId) 
        {
            if (roleId==null)
            {
                return R.Error("没有获取到角色id");
            }
            return R.OK(roleService.listMenuByRole(roleId));
        }

        [HttpPost]
        public R UpdateRole(Role role)
        {
            if (string.IsNullOrEmpty(role.RoleName))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (roleService.UpdateRole(role))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }

        /// <summary>
        /// rolelist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R GetAllRoleList() 
        {
            return R.OK(roleService.GetAllRoleList());
        }


    }
}
