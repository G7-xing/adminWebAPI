using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class UserService : IUserService
    {
        private readonly ISqlSugarClient dbconn;

        public UserService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn;
        }
        /// <summary>
        /// 用户与角色配置
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool AllocRole(UserRoleRelation obj)
        {
            try
            {
                // 先删除原有的关系
                if (dbconn.Deleteable<UserRoleRelation>().Where(it => it.UserId == obj.UserId).ExecuteCommandHasChange()
                     || dbconn.Queryable<UserRoleRelation>().Count(it => it.UserId == obj.UserId) == 0)
                {
                    //var sd = roleReq.menuIds.Select(s => new UserRoleRelation { RoleId = roleReq.roleId, MenuId = s }).ToList();
                    if (dbconn.Insertable(obj).ExecuteCommand() >= 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 增加User信息
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool CreateUser(User User)
        {
            var ds = dbconn.Insertable(User).ExecuteCommand();
            if (ds==1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除User信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteUser(string UserId, out string msg)
        {
           
            // 是否存在下级User
            var isExist = dbconn.Queryable<User>().Any();
            if (isExist)
            {
                msg = "该User还有下级User，不允许删除,请注意";
                return false;
            }
            // 该User是最底层的
            if (!dbconn.Deleteable<User>().Where(it => it.Id.ToString() == UserId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            msg = "";
            return true;

        }
        /// <summary>
        /// 获取User信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserRoleRelation GetRoleByUser(string UserId)
        {
            var info = dbconn.Queryable<UserRoleRelation>().First(t=>t.UserId == UserId);
            if (info!=null)
            {
                return info;
            }
            return null ;
        }

        /// <summary>
        /// 获取User数据
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<UserRes> List(int pageNum, int pageSize, string keyword,ref int totalCount)
        {

          var page = dbconn.Queryable<User>()
                                     .LeftJoin<Department>((u, d) => u.DepartmentId == d.DepartmentId)
                                     .Select((u, d) => new UserRes 
                                     {
                                         Id = u.Id,
                                         UserName = u.UserName,
                                         Password = u.Password,
                                         NickName = u.NickName,
                                         Email = u.Email,
                                         CreateTime = u.CreateTime,
                                         UpdateTime = u.UpdateTime,
                                         CreateBy = u.CreateBy,
                                         UpdateBy = u.UpdateBy,
                                         DepartmentId = u.DepartmentId,
                                         Status = u.Status,
                                         DepartmentName = d.DepartmentName 
                                     })
                                     .ToPageList(pageNum, pageSize,ref totalCount);

         return  page;
            
        }
        /// <summary>
        /// 更新User数据
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool UpdateUser(User obj)
        {

            var flag = dbconn.Updateable<User>()
                .SetColumns(it => new User()
                {
                    UserName = obj.UserName,
                    NickName = obj.NickName,
                    Email =obj.Email,
                    Password = obj.Password,
                    DepartmentId = obj.DepartmentId,
                    Status = obj.Status,
                    UpdateBy = obj.CreateBy,
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
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

        //public UserRoleRelation GetRoleByUser(string UserId)
        //{
        //    return dbconn.Queryable<UserRoleRelation>().Where(i => i.UserId == UserId);
        //}
    }
}
