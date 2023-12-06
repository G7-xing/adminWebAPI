using System;
using System.Collections.Generic;
using System.Linq;

using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("Tb_Magazine_Magazine")]
    public class TbMagazine
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "MagazineNo")]
        public string MagazineNo { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PurchaseOrder")]
        public string PurchaseOrder { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Buyer")]
        public string Buyer { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PurchaseDate")]
        public DateTime? PurchaseDate { get; set; }
        /// <summary>
        ///  
        /// 默认值: ((90))
        ///</summary>
        [SugarColumn(ColumnName = "ValidDays")]
        public int? ValidDays { get; set; }
    }
}
