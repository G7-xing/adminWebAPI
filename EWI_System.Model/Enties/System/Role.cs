using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 角色表
    ///</summary>
    [SugarTable("Role")]
    public class Role
    {
        /// <summary>
        ///  
        /// 默认值: (newid())
        ///</summary>
         [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true ,InsertSql ="newid()"  )]
         public string Id { get; set; }
        /// <summary>
        /// 角色名称 
        ///</summary>
         [SugarColumn(ColumnName="RoleName"    )]
         public string RoleName { get; set; }
        /// <summary>
        /// 角色描述 
        ///</summary>
         [SugarColumn(ColumnName="RoleDesc"    )]
         public string RoleDesc { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="CreateTime"  ,InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string CreateTime { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
         [SugarColumn(ColumnName="CreateBy"    )]
         public string CreateBy { get; set; }
        /// <summary>
        /// 0-禁用，1-启用 
        /// 默认值: ((1))
        ///</summary>
         [SugarColumn(ColumnName="Status"  ,InsertSql ="1"  )]
         public int Status { get; set; }
        /// <summary>
        /// 排序 
        ///</summary>
         [SugarColumn(ColumnName="Sort"    )]
         public int Sort { get; set; }
        /// <summary>
        /// 更新人 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="UpdateTime" ,InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string UpdateTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
         [SugarColumn(ColumnName="UpdateBy" )]
         public string UpdateBy { get; set; }
    }
}
