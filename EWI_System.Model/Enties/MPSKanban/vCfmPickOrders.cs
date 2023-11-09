using System;
using System.Collections.Generic;
using System.Text;

using SqlSugar;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 数据源导入
    ///</summary>
    [SugarTable("vCfmPickOrders")]
    public class vCfmPickOrders
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickOrderId")]
        public string PickOrderId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PartNumber")]
        public string PartNumber { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Line")]
        public string Line { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickStatus")]
        public int? PickStatus { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "WorkOrder")]
        public string WorkOrder { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickOrderType")]
        public int? PickOrderType { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Created")]
        public DateTime? Created { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Required")]
        public DateTime? Required { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ManualCloseTime")]
        public DateTime? ManualCloseTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ManualCloseReason")]
        public string ManualCloseReason { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "BatchNo")]
        public string BatchNo { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ComponentNumber")]
        public string ComponentNumber { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Inventory")]
        public string Inventory { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "UniqueId")]
        public string UniqueId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickupDateTime")]
        public DateTime? PickupDateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Track")]
        public string Track { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickupStatus")]
        public int? PickupStatus { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "SmtInlineSetupFlag")]
        public int? SmtInlineSetupFlag { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Id")]
        public int? Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ClosedReason")]
        public string ClosedReason { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "SmtMachine")]
        public string SmtMachine { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "SmtSide")]
        public string SmtSide { get; set; }
    }
}
