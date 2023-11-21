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
        /// <summary>
        /// 服务
        /// </summary>
        public IUserService userService;
        
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        #endregion

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
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

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public R AllocRole(UserAllocRole obj)
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
        /// <summary>
        /// 检验旧密码
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        public R checkOldPassword(User User)
        {
            if (string.IsNullOrEmpty(User.Id))
            {
                return R.Error("检验失败，请检查提交的数据");
            }
            if (userService.checkOldPassword(User))
            {
                return R.OK();
            };
            return R.Error("检验失败，原密码不正确");


        }
        /// <summary>
        /// 检验旧密码
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        public R updateUserPwd(User User)
        {
            if (string.IsNullOrEmpty(User.Id))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (userService.updateUserPwd(User))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");


        }

    }
}
