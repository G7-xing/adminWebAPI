using AutoMapper;
using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWI_System.Service
{
    public class RoleService : IRoleService
    {

        private readonly ISqlSugarClient dbconn;
        private readonly IMapper mapper; //映射对象

        public RoleService(ISqlSugarClient dbconn,IMapper mapper)
        {
            this.dbconn = dbconn;
            this.mapper = mapper;
        }
        /// <summary>
        /// 增加角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool CreateRole(Role role)
        {
            var ds = dbconn.Insertable(role).ExecuteCommand();
            if (ds == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Role> FetchList(int pageNum, int pageSize, string keyword, ref int totalCount)
        {
            return dbconn.Queryable<Role>()
                                    .WhereIF(!String.IsNullOrEmpty(keyword),it=>it.RoleName.Contains(keyword))
                                    .ToPageList(pageNum, pageSize, ref totalCount);

        }
        /// <summary>
        /// 跟新状态
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateStatus(Role role)
        {
            return dbconn.Updateable<Role>().SetColumns(r => new Role { Status = role.Status, UpdateBy = role.UpdateBy })
                                                      .Where(r => r.Id == role.Id)
                                                      .ExecuteCommandHasChange();
        }

        public bool AllocMenu(RoleReq roleReq)
        {
            try
            {
                // 先删除原有的关系
                if (dbconn.Deleteable<RoleMenuRelation>().Where(it => it.RoleId == roleReq.roleId).ExecuteCommandHasChange()
                     || dbconn.Queryable<RoleMenuRelation>().Count(it=>it.RoleId == roleReq.roleId)==0)
                {
                    var sd = roleReq.menuIds.Select(s => new RoleMenuRelation { RoleId = roleReq.roleId, MenuId = s }).ToList();
                    if (dbconn.Insertable(sd).ExecuteCommand() >= 0)
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

        public List<RoleMenuRelation> listMenuByRole(string roleId)
        {
            var list = dbconn.Queryable<RoleMenuRelation>().Where(r => r.RoleId == roleId).ToList();
            return list;
        }

        public bool UpdateRole(Role role)
        {
            return dbconn.Updateable<Role>()
                                                    .SetColumns(r =>
                                                        new Role
                                                        {
                                                            RoleName = role.RoleName,
                                                            RoleDesc = role.RoleDesc,
                                                            Status = role.Status,
                                                            UpdateBy = role.UpdateBy
                                                        }
                                                       )
                                                      .Where(r => r.Id == role.Id)
                                                      .ExecuteCommandHasChange();
        }

        public List<RoleRes> GetAllRoleList()
        {
            return mapper.Map<List<RoleRes>>(dbconn.Queryable<Role>().ToList());
        }

        /// <summary>
        /// 获取相应的菜单数据 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Menu> GetMenuListByUserId(string userId)
        {
            try
            {
                return dbconn.Queryable<UserRoleRelation>().LeftJoin<Role>((URR, R) => R.Id == URR.RoleId)
                                                    .LeftJoin<RoleMenuRelation>((URR, R, RMR) => RMR.RoleId == R.Id)
                                                    .LeftJoin<Menu>((URR, R, RMR, M) => M.Id == RMR.MenuId)
                                                    .Where(URR => URR.UserId == userId)
                                                    .Select((URR, R, RMR, M) => M).ToList();
                 
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 对应的人对应的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public String GetRoleByUserId(string userId)
        {
            var roleName = dbconn.Queryable<UserRoleRelation>()
                     .LeftJoin<Role>((UR, R) => R.Id == UR.RoleId)
                     .Where((UR, R) => UR.UserId == userId)
                     .Select((UR, R) => R.RoleName).ToList();
            if (roleName.Count==0)
            {
                return "NPPI";
            }
            return roleName[0];
        }
    }
}
