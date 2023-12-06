using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// Magazine属性表
    ///</summary>
    [SugarTable("WipMagazine")]
    public class WipMagazine
    {
        /// <summary>
        /// 唯一标识 
        ///</summary>
        [SugarColumn(ColumnName = "MagazineId", IsPrimaryKey = true)]
        public int MagazineId { get; set; }
        /// <summary>
        /// Magazine编号 
        ///</summary>
        [SugarColumn(ColumnName = "MagazineNo")]
        public string MagazineNo { get; set; }
        /// <summary>
        /// Magazine类型--试制--量产 
        ///</summary>
        [SugarColumn(ColumnName = "MagazineType")]
        public string MagazineType { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
        [SugarColumn(ColumnName = "CreateBy")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime",InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
        [SugarColumn(ColumnName = "UpdateBy")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新人 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 排序 
        /// 默认值: ((0))
        ///</summary>
        [SugarColumn(ColumnName = "Sort", InsertSql = "0")]
        public int? Sort { get; set; }
        /// <summary>
        /// 该条记录状态 
        /// 默认值: ((0))
        ///</summary>
        [SugarColumn(ColumnName = "Status", InsertSql = "0")]
        public int? Status { get; set; }
    }
}
