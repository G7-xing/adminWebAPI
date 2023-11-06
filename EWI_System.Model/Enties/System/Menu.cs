using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 菜单表
    ///</summary>
    [SugarTable("Menu")]
    public class Menu
    {
        /// <summary>
        /// 唯一id 
        /// 默认值: (newid())
        ///</summary>
         [SugarColumn(ColumnName="Id", IsPrimaryKey = true, InsertSql = "newid()")]
         public string Id { get; set; }
        /// <summary>
        /// 父级菜单id 
        ///</summary>
         [SugarColumn(ColumnName="ParentId"    )]
         public string ParentId { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
         [SugarColumn(ColumnName="CreateBy"    )]
         public string CreateBy { get; set; }
        /// <summary>
        /// 菜单名称 
        ///</summary>
         [SugarColumn(ColumnName="MenuName"    )]
         public string MenuName { get; set; }
        /// <summary>
        /// 菜单层级数 
        ///</summary>
         [SugarColumn(ColumnName="MenuLevel"    )]
         public int MenuLevel { get; set; }
        /// <summary>
        /// 前端文件名称或地址 
        ///</summary>
         [SugarColumn(ColumnName="MenuURL"    )]
         public string MenuURL { get; set; }
        /// <summary>
        /// 0-禁用，1-启用 
        /// 默认值: ((1))
        ///</summary>
         [SugarColumn(ColumnName="Status" ,InsertSql = "1"   )]
         public int Status { get; set; }
        /// <summary>
        /// 排序序号 
        ///</summary>
         [SugarColumn(ColumnName="Sort"    )]
         public int Sort { get; set; }
        /// <summary>
        /// 更新时间 
        ///</summary>
         [SugarColumn(ColumnName="UpdateTime" , InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string UpdateTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
         [SugarColumn(ColumnName="UpdateBy"    )]
         public string UpdateBy { get; set; }
        /// <summary>
        /// 上级菜单名 
        ///</summary>
        [SugarColumn(ColumnName = "ParentName")]
        public string ParentName { get; set; }
        /// <summary>
        /// 前端路由的名称 
        ///</summary>
        [SugarColumn(ColumnName = "Name")]
        public string Name { get; set; }
    }
}
