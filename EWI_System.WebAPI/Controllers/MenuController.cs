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
    public class MenuController : ControllerBase
    {
        #region 注入服务层
        public IMenuService menuService;
        

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }
        #endregion

        /// <summary>
        ///获取在库的所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R List(string addFlag) 
        {
            return R.OK(menuService.List(addFlag));
        }
        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public R AddMenu(Menu Menu)
        {
            if (string.IsNullOrEmpty(Menu.MenuName)|| string.IsNullOrEmpty(Menu.ParentName))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (menuService.AddMenu(Menu))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        [HttpPost]
        public R UpdateMenu(Menu Menu)
        {
            if (string.IsNullOrEmpty(Menu.MenuName) || string.IsNullOrEmpty(Menu.ParentName))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (menuService.UpdateMenu(Menu))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");

            
        }
        /// <summary>
        /// 获取某个菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R GetMenuInfo(string MenuId)
        {
            return R.OK(menuService.GetMenuInfo(MenuId));
        }
        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="MenuId"></param>
        /// <returns></returns>
        [HttpGet]
        public R DeleteMenu(string menuId)
        {
            string msg;
            if (menuService.DeleteMenu(menuId,out msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }

        
    }
}
