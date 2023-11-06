using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 用户-角色对应表
    ///</summary>
    [SugarTable("User_Role_Relation")]
    public class UserRoleRelation
    {
        /// <summary>
        /// 唯一id 
        ///</summary>
         [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public long Id { get; set; }
        /// <summary>
        /// 用户id 
        ///</summary>
         [SugarColumn(ColumnName="UserId"    )]
         public string UserId { get; set; }
        /// <summary>
        /// 角色id 
        ///</summary>
         [SugarColumn(ColumnName="RoleId"    )]
         public string RoleId { get; set; }
    }
}
