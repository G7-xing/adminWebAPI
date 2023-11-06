using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 部门表
    ///</summary>
    [SugarTable("Department")]
    public class Department
    {
        /// <summary>
        /// 部门id 
        /// 默认值: (newid())
        ///</summary>
        [SugarColumn(ColumnName = "DepartmentId", IsPrimaryKey = true,InsertSql = "newid()")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 部门名称 
        ///</summary>
        [SugarColumn(ColumnName = "DepartmentName")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 上级部门id 
        ///</summary>
        [SugarColumn(ColumnName = "ParentId")]
        public string ParentId { get; set; }
        /// <summary>
        /// 部门等级 
        ///</summary>
        [SugarColumn(ColumnName = "DepartmentLevel")]
        public int DepartmentLevel { get; set; }
        /// <summary>
        /// 0-禁止，1-启用 
        /// 默认值: ((1))
        ///</summary>
        [SugarColumn(ColumnName = "Status", InsertSql = "1")]
        public int Status { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
        [SugarColumn(ColumnName = "CreateBy")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 更新时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
        [SugarColumn(ColumnName = "UpdateBy")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 父级名称 
        ///</summary>
        [SugarColumn(ColumnName = "ParentName")]
        public string ParentName { get; set; }
    }
}
