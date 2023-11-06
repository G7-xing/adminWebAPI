using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IUserService
    {
        public List<UserRes> List(int pageNum, int pageSize, string keyword,ref int totalCount);
        bool CreateUser(User User);
        public UserRoleRelation GetRoleByUser(string UserId);
        bool DeleteUser(string UserId, out string msg);
        bool UpdateUser(User User);
        bool AllocRole(UserRoleRelation obj);
    }
}
