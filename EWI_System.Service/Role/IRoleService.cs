using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IRoleService
    {
        bool CreateRole(Role role);
        List<Role> FetchList(int pageNum, int pageSize, string keyword, ref int total);
        bool UpdateStatus(Role role);
        bool AllocMenu(RoleReq roleReq);
        List<RoleMenuRelation> listMenuByRole(string roleId);
        bool UpdateRole(Role role);
        List<RoleRes> GetAllRoleList();

        List<Menu> GetMenuListByUserId(string userId);
        String GetRoleByUserId(string userId);
    }
}
