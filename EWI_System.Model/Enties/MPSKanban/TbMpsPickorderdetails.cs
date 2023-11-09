using System;
using System.Collections.Generic;
using System.Text;

using SqlSugar;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("Tb_MPS_PickOrderDetails")]
    public class TbMpsPickorderdetails
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Id")]
        public int? Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickOrderId")]
        public string PickOrderId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Inventory")]
        public string Inventory { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "BatchNo")]
        public string BatchNo { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PartNumber")]
        public string PartNumber { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "UniqueId")]
        public string UniqueId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickStatus")]
        public int? PickStatus { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "RequiredQuantity")]
        public int? RequiredQuantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Track")]
        public string Track { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PickupDateTime")]
        public DateTime? PickupDateTime { get; set; }
        /// <summary>
        ///  
        /// 默认值: ((0))
        ///</summary>
        [SugarColumn(ColumnName = "SmtInlineSetupFlag")]
        public int? SmtInlineSetupFlag { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ClosedReason")]
        public string ClosedReason { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "SmtSide")]
        public string SmtSide { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "SmtMachine")]
        public string SmtMachine { get; set; }
    }
}
