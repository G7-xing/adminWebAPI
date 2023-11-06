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
    public class UserController : ControllerBase
    {
        #region 注入服务层
        public IUserService userService;
        

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        #endregion

        /// <summary>
        ///获取在库的所有数据
        ///pageNum: 1,
        //pageSize: 10,
         //keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R List(int pageNum, int pageSize , string keyword) 
        {
            int total = 0;
            var userlist = userService.List(pageNum, pageSize, keyword,ref total);
            return R.OK(userlist).data("total",total);
        }
        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public R CreateUser(User User)
        {
            if (string.IsNullOrEmpty(User.UserName)|| string.IsNullOrEmpty(User.NickName))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (userService.CreateUser(User))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }
        /// <summary>
        /// 用户对应的角色关系表数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public R GetRoleByUser(string UserId)
        {
            return R.OK(userService.GetRoleByUser(UserId));
        }


        [HttpPost]
        public R AllocRole(UserRoleRelation obj)
        {
            if (userService.AllocRole(obj))
            {
                return R.OK();
            };
            return R.Error("分配失败，请检查提交的数据");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        public R UpdateUser(User User)
        {
            if (string.IsNullOrEmpty(User.UserName) || string.IsNullOrEmpty(User.Id))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (userService.UpdateUser(User))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");

            
        }
        
        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        public R DeleteUser(string UserId)
        {
            string msg;
            if (userService.DeleteUser(UserId,out msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }

        
    }
}
