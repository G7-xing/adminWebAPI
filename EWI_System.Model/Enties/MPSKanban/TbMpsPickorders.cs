using System;
using System.Collections.Generic;
using System.Text;

using SqlSugar;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("Tb_MPS_PickOrders")]
    public class TbMpsPickorders
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
    }
}
