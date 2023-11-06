using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IMenuService
    {
        public List<Menu> List(string addFlag);
        bool AddMenu(Menu menu);
        Menu GetMenuInfo(string menuId);
        bool DeleteMenu(string menuId, out string msg);
        bool UpdateMenu(Menu menu);
    }
}
