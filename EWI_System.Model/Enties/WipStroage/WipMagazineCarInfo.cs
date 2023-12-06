using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 小车与magazine关联明细表
    ///</summary>
    [SugarTable("WipMagazineCarInfo")]
    public class WipMagazineCarInfo
    {
        /// <summary>
        /// 唯一标识 
        ///</summary>
        [SugarColumn(ColumnName = "WipMagazineCarInfoId", IsPrimaryKey = true)]
        public Guid WipMagazineCarInfoId  { get; set; }
        /// <summary>
        /// 车辆编号 
        ///</summary>
        [SugarColumn(ColumnName = "WipCarNo")]
        public string WipCarNo { get; set; }
        /// <summary>
        /// magazine编号 
        ///</summary>
        [SugarColumn(ColumnName = "MagazineNo")]
        public string MagazineNo { get; set; }
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
        /// 更新人 
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 是否删除；0----未删除；1----已删除 
        ///</summary>
        [SugarColumn(ColumnName = "Deleted",InsertSql ="0")]
        public int Deleted { get; set; }
    }
}
