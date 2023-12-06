using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 小车、magazine、库位关系对应表
    ///</summary>
    [SugarTable("WipInfoRelation")]
    public class WipInfoRelation
    {
        /// <summary>
        /// 1 
        ///</summary>
        [SugarColumn(ColumnName = "WipInfoRelationId", IsPrimaryKey = true,IsIdentity =true)]
        public int WipInfoRelationId { get; set; }
        /// <summary>
        /// m---car的info 
        ///</summary>
        [SugarColumn(ColumnName = "WipMagazineCarInfoId")]
        public string WipMagazineCarInfoId { get; set; }
        /// <summary>
        /// car-laotion的info 
        ///</summary>
        [SugarColumn(ColumnName = "WipCarLocationInfoId")]
        public string WipCarLocationInfoId { get; set; }
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

        public static implicit operator List<object>(WipInfoRelation v)
        {
            throw new NotImplementedException();
        }
    }
}
