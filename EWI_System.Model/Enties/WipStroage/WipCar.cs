using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 装magazine的车表
    ///</summary>
    [SugarTable("WipCar")]
    public class WipCar
    {
        /// <summary>
        /// 唯一标识 
        ///</summary>
        [SugarColumn(ColumnName = "WipCarId", IsPrimaryKey = true,IsIdentity =true)]
        public int wipCarId { get; set; }
        /// <summary>
        /// 车编号 
        ///</summary>
        [SugarColumn(ColumnName = "WipCarNo")]
        public string wipCarNo { get; set; }
        /// <summary>
        /// 创建人 
        ///</summary>
        [SugarColumn(ColumnName = "CreateBy")]
        public string createBy { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string createTime { get; set; }
        /// <summary>
        /// 更新人 
        ///</summary>
        [SugarColumn(ColumnName = "UpdateBy")]
        public string updateBy { get; set; }
        /// <summary>
        /// 更新人 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string updateTime { get; set; }
        /// <summary>
        /// 排序 
        /// 默认值: ((0))
        ///</summary>
        [SugarColumn(ColumnName = "Sort", InsertSql = "0")]
        public int? sort { get; set; }
        /// <summary>
        /// 该条记录状态 
        /// 默认值: ((1))
        ///</summary>
        [SugarColumn(ColumnName = "Status", InsertSql = "1")]
        public int? status { get; set; }
        ///// <summary>
        ///// 表没有的
        ///// </summary>
        //[SugarColumn(IsIgnore =true)]
        //public string userName { get; set; }
    }
}
