using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("CTReport_Detail")]
    public class CtreportDetail
    {
        /// <summary>
        ///  
        /// 默认值: (newid())
        ///</summary>
        [SugarColumn(ColumnName = "CTReport_Detail_Id", IsPrimaryKey = true, InsertSql = "newid()")]
        public string CtreportDetailId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "stationL1")]
        public string StationL1 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "manOrMachineL2")]
        public string ManormachineL2 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "typeL3")]
        public string TypeL3 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num1")]
        public decimal? Num1 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num2")]
        public decimal? Num2 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num3")]
        public decimal? Num3 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num4")]
        public decimal? Num4 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num5")]
        public decimal? Num5 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num6")]
        public decimal? Num6 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num7")]
        public decimal? Num7 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num8")]
        public decimal? Num8 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num9")]
        public decimal? Num9 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num10")]
        public decimal? Num10 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num11")]
        public decimal? Num11 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num12")]
        public decimal? Num12 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num13")]
        public decimal? Num13 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num14")]
        public decimal? Num14 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num15")]
        public decimal? Num15 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num16")]
        public decimal? Num16 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num17")]
        public decimal? Num17 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num18")]
        public decimal? Num18 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num19")]
        public decimal? Num19 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "num20")]
        public decimal? Num20 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "averageValue")]
        public decimal? AverageValue { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "allowance")]
        public decimal? Allowance { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "connectPlates")]
        public decimal? ConnectPlates { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "manOrMachineNums")]
        public decimal? ManOrMachineNums { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "standardCT")]
        public decimal? StandardCT { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "equipmentCT")]
        public decimal? EquipmentCT { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "manCT")]
        public decimal? ManCT { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "apportionTime")]
        public decimal? ApportionTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "stationCT")]
        public decimal? StationCT { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "remak")]
        public string Remak { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "CTReport_Id")]
        public string CtreportId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "CreateBy")]
        public string CreateBy { get; set; }
        /// <summary>
        ///  
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string CreateTime { get; set; }
        /// <summary>
        ///  
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
        [SugarColumn(ColumnName = "UpdateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
        public string UpdateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "UpdateBy")]
        public string UpdateBy { get; set; }
    }
}
