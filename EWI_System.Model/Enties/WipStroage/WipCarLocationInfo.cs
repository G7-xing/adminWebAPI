using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 小车与库位联系信息表
    ///</summary>
    [SugarTable("WipCarLocationInfo")]
    public class WipCarLocationInfo
    {
        /// <summary>
        /// 唯一标识 
        ///</summary>
        [SugarColumn(ColumnName = "WipCarLocationInfoId", IsPrimaryKey = true)]
        public Guid WipCarLocationInfoId { get; set; }
        /// <summary>
        /// 小车编号 
        ///</summary>
        [SugarColumn(ColumnName = "WipCarNo")]
        public string WipCarNo { get; set; }
        /// <summary>
        /// 库位编号 
        ///</summary>
        [SugarColumn(ColumnName = "LocationNo")]
        public string LocationNo { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
        [SugarColumn(ColumnName = "CreateBy")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间 
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
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 是否删除；0----未删除；1----已删除 
        ///</summary>
        [SugarColumn(ColumnName = "Deleted", InsertSql = "0")]
        public int Deleted { get; set; }
    }
}