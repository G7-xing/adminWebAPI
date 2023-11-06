using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// CTReport报表主数据表
    ///</summary>
    [SugarTable("CTReport")]
    public class CTReport
    {
        /// <summary>
        /// 唯一标识 
        /// 默认值: (newid())
        ///</summary>
        [SugarColumn(ColumnName = "CTRepeortId", IsPrimaryKey = true, InsertSql = "newid()")]
        public string CTRepeortId { get; set; }
        /// <summary>
        /// 日期 yyyy-mm-dd 
        ///</summary>
        [SugarColumn(ColumnName = "when")]
        public string When { get; set; }
        /// <summary>
        /// 人员 
        ///</summary>
        [SugarColumn(ColumnName = "who")]
        public string Who { get; set; }
        /// <summary>
        /// why 
        ///</summary>
        [SugarColumn(ColumnName = "why")]
        public string Why { get; set; }
        /// <summary>
        /// how 
        ///</summary>
        [SugarColumn(ColumnName = "how")]
        public string How { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "shippingNo")]
        public string ShippingNo { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "customer")]
        public string Customer { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "project")]
        public string Project { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "lineId")]
        public string LineId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "connPlatesNum")]
        public int? ConnPlatesNum { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "OPNum")]
        public int? OPNum { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "taktTime")]
        public string TaktTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "result_CT")]
        public string ResultCt { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "result_neckStation")]
        public string ResultNeckstation { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "result_balanceRateLine")]
        public string ResultBalancerateline { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "result_balanceRateOP")]
        public string ResultBalancerateop { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Comments")]
        public string Comments { get; set; }
        /// <summary>
        /// 创建时间 
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 创建人 
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
        /// yes--已经有detail数据，no--是没有
        ///</summary>
        [SugarColumn(ColumnName = "haveDetail")]
        public string haveDetail { get; set; }
        /// <summary>
        /// 平台
        ///</summary>
        [SugarColumn(ColumnName = "platform")]
        public string platform { get; set; }
    }
}
