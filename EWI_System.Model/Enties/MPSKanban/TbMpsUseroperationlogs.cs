using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// SMTMPSPDA操作日志表
    ///</summary>
    [SugarTable("Tb_MPS_Useroperationlogs")]
    public class TbMpsUseroperationlogs
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "ID", IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Pickorderid")]
        public string Pickorderid { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PartNo")]
        public string PartNo { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "UniqueId")]
        public string UniqueId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Staffid")]
        public string Staffid { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "QADuser")]
        public string QADuser { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Barcode")]
        public string Barcode { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "QADreturndata")]
        public string QADreturndata { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Mark")]
        public string Mark { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Date")]
        public string Date { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Type")]
        public string Type { get; set; }
    }
}
