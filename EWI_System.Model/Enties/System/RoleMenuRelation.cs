using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 角色-菜单对应关系表
    ///</summary>
    [SugarTable("Role_Menu_Relation")]
    public class RoleMenuRelation
    {
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true ,IsIdentity = true  )]
         public long Id { get; set; }
        /// <summary>
        /// 菜单id 
        ///</summary>
         [SugarColumn(ColumnName="MenuId"    )]
         public string MenuId { get; set; }
        /// <summary>
        /// 角色id 
        ///</summary>
         [SugarColumn(ColumnName="RoleId"    )]
         public string RoleId { get; set; }
    }
}
