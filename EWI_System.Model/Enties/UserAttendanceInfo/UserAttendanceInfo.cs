using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 员工考勤详细记录信息表
    ///</summary>
    [SugarTable("UserAttendanceInfo")]
    public class UserAttendanceInfo
    {
        /// <summary>
        /// 唯一标识 
        /// 默认值: (newid())
        ///</summary>
        [SugarColumn(ColumnName = "AttendanceId", IsPrimaryKey = true, InsertSql = "newid()")]
        public string AttendanceId { get; set; }
        /// <summary>
        /// 缺勤日期（yyyy-MM-dd） 
        ///</summary>
        [SugarColumn(ColumnName = "AbsenceDate")]
        public string AbsenceDate { get; set; }
        /// <summary>
        /// 用户id 
        ///</summary>
        [SugarColumn(ColumnName = "UserId")]
        public string UserId { get; set; }
        /// <summary>
        /// 缺勤类型：V-休假，S-病假，H-半天假，E-出差，N-正常 
        ///</summary>
        [SugarColumn(ColumnName = "AbsenceType")]
        public string AbsenceType { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
        [SugarColumn(ColumnName = "CreateBy")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
        [SugarColumn(ColumnName = "UpdateBy")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 排序备用 
        /// 默认值: ((0))
        ///</summary>
        [SugarColumn(ColumnName = "Sort",InsertSql ="0")]
        public int Sort { get; set; }
        /// <summary>
        /// 0-启用，1-删除 
        /// 默认值: ((0))
        ///</summary>
        [SugarColumn(ColumnName = "Status", InsertSql = "0")]
        public int Status { get; set; }
    }
}
