using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// wip库位信息表
    ///</summary>
    [SugarTable("WipStorageLocation")]
    public class WipStorageLocation
    {
        /// <summary>
        /// 唯一标识 
        ///</summary>
        [SugarColumn(ColumnName = "WipStorageLocationId", IsPrimaryKey = true, IsIdentity = true)]
        public int WipStorageLocationId { get; set; }
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
        [SugarColumn(ColumnName = "Status", InsertSql = "1")]
        public int? Status { get; set; }
        /// <summary>
        /// 库位区域信息表id 
        ///</summary>
        [SugarColumn(ColumnName = "WipLocationAreaId")]
        public string WipLocationAreaId { get; set; }

        /// <summary>
        /// 区域名称 
        ///</summary>
        [SugarColumn(IsIgnore =true)]
        public string AreaName { get; set; }
        /// <summary>
        /// 区域对应的线体 
        ///</summary>
        [SugarColumn(IsIgnore = true)]
        public string AreaLineName { get; set; }
    }
}

