using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 库位区域信息表（不做前台crud，只做数据记录）
    ///</summary>
    [SugarTable("WipLocationArea")]
    public class WipLocationArea
    {
        /// <summary>
        /// 唯一标识 
        ///</summary>
        [SugarColumn(ColumnName = "WipLocationAreaId", IsPrimaryKey = true)]
        public string WipLocationAreaId { get; set; }
        /// <summary>
        /// 区域名称 
        ///</summary>
        [SugarColumn(ColumnName = "AreaName")]
        public string AreaName { get; set; }
        /// <summary>
        /// 区域对应的线体 
        ///</summary>
        [SugarColumn(ColumnName = "AreaLineName")]
        public string AreaLineName { get; set; }
        /// <summary>
        /// 区域库位的数量 
        ///</summary>
        [SugarColumn(ColumnName = "LocationQTY")]
        public int LocationQTY { get; set; }
        /// <summary>
        /// 区域类型，L-线体区域，W工作台区域，S-售后件区域，B--备用区域 
        ///</summary>
        [SugarColumn(ColumnName = "AreaType")]
        public string AreaType { get; set; }
        /// <summary>
        /// 排序 
        ///</summary>
        [SugarColumn(ColumnName = "Sort")]
        public int? Sort { get; set; }
        /// <summary>
        /// 是否删除；0----未删除；1----已删除 
        ///</summary>
        [SugarColumn(ColumnName = "Deleted",InsertSql ="0")]
        public int Deleted { get; set; }
    }
}