using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 用户表
    ///</summary>
    [SugarTable("User")]
    public class User
    {
        /// <summary>
        /// 唯一标识 
        /// 默认值: (newid())
        ///</summary>
         [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true ,InsertSql = "newid()")]
         public string Id { get; set; }
        /// <summary>
        /// 用户名称 
        ///</summary>
         [SugarColumn(ColumnName="UserName"    )]
         public string UserName { get; set; }
        /// <summary>
        /// 用户密码 
        ///</summary>
         [SugarColumn(ColumnName="Password"    )]
         public string Password { get; set; }
        /// <summary>
        /// 昵称 
        ///</summary>
         [SugarColumn(ColumnName="NickName"    )]
         public string NickName { get; set; }
        /// <summary>
        /// 邮件 
        ///</summary>
         [SugarColumn(ColumnName="Email"    )]
         public string Email { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="CreateTime" ,InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string CreateTime { get; set; }
        /// <summary>
        /// 更新时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string UpdateTime { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
         [SugarColumn(ColumnName="CreateBy"    )]
         public string CreateBy { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
         [SugarColumn(ColumnName="UpdateBy"    )]
         public string UpdateBy { get; set; }

        /// <summary>
        /// 部门Id 
        ///</summary>
        [SugarColumn(ColumnName = "DepartmentId")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 状态 
        ///</summary>
        [SugarColumn(ColumnName = "Status",InsertSql ="1")]
        public int Status { get; set; }
    }
}
