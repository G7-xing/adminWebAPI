using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class MenuService : IMenuService
    {
        private readonly ISqlSugarClient dbconn;

        public MenuService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn;
        }
        /// <summary>
        /// 增加菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool AddMenu(Menu menu)
        {
            var ds = dbconn.Insertable(menu).ExecuteCommand();
            if (ds==1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteMenu(string menuId, out string msg)
        {
           
            // 是否存在下级菜单
            var isExist = dbconn.Queryable<Menu>().Where(it => it.ParentId == menuId).Any();
            if (isExist)
            {
                msg = "该菜单还有下级菜单，不允许删除,请注意";
                return false;
            }
            // 该菜单是最底层的
            if (!dbconn.Deleteable<Menu>().Where(it => it.Id.ToString() == menuId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            msg = "";
            return true;

        }
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Menu GetMenuInfo(string menuId)
        {
            var info = dbconn.Queryable<Menu>().First(t=>t.Id.ToString() == menuId);
            if (info!=null)
            {
                return info;
            }
            return new Menu();
        }

        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        public List<Menu> List(string addFlag)
        {
            return  dbconn.Queryable<Menu>()
                .WhereIF(addFlag==null,t => t.ParentId != "0")
                .ToList();
            
        }
        /// <summary>
        /// 更新菜单数据
        /// </summary>
        /// <param name="Menu"></param>
        /// <returns></returns>
        public bool UpdateMenu(Menu obj)
        {

            var flag = dbconn.Updateable<Menu>()
                .SetColumns(it => new Menu()
                {
                    MenuName = obj.MenuName,
                    ParentId = obj.ParentId,
                    ParentName = obj.ParentName,
                    Status = obj.Status,
                    MenuURL = obj.MenuURL,
                    MenuLevel = obj.MenuLevel,
                    UpdateBy = obj.CreateBy,
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    Name = obj.Name
                }).Where(it=>it.Id == obj.Id).ExecuteCommand();
            if (flag !=1)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
