using System;
using System.Collections.Generic;
using System.Text;

using SqlSugar;

namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 视图
    ///</summary>
    [SugarTable("v_PackagingUnitInSetup")]
    public class vPackagingUnitInSetup
    {
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Setup")]
        public string Setup { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Line")]
        public string Line { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Station")]
        public string Station { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Location")]
        public byte? Location { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "TableId")]
        public string TableId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Track")]
        public int? Track { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "FeederId")]
        public string FeederId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Tower")]
        public int? Tower { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Level")]
        public int? Level { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "Division")]
        public int? Division { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_PackagingUnitId")]
        public string PU1_PackagingUnitId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_SiplaceProComponent")]
        public string PU1_SiplaceProComponent { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Component")]
        public string PU1_Component { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_OriginalQuantity")]
        public int? PU1_OriginalQuantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Quantity")]
        public int? PU1_Quantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Manufacturer")]
        public string PU1_Manufacturer { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_ManufactureDate")]
        public DateTime? PU1_ManufactureDate { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_ExpiryDate")]
        public DateTime? PU1_ExpiryDate { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Supplier")]
        public string PU1_Supplier { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Batch")]
        public string PU1_Batch { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Serial")]
        public string PU1_Serial { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_MsdLevel")]
        public string PU1_MsdLevel { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_MsdLive")]
        public int? PU1_MsdLive { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Extra1")]
        public string PU1_Extra1 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Extra2")]
        public string PU1_Extra2 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU1_Extra3")]
        public string PU1_Extra3 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_PackagingUnitId")]
        public string PU2_PackagingUnitId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_SiplaceProComponent")]
        public string PU2_SiplaceProComponent { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Component")]
        public string PU2_Component { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_OriginalQuantity")]
        public int? PU2_OriginalQuantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Quantity")]
        public int? PU2_Quantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Manufacturer")]
        public string PU2_Manufacturer { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_ManufactureDate")]
        public DateTime? PU2_ManufactureDate { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_ExpiryDate")]
        public DateTime? PU2_ExpiryDate { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Supplier")]
        public string PU2_Supplier { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Batch")]
        public string PU2_Batch { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Serial")]
        public string PU2_Serial { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_MsdLevel")]
        public string PU2_MsdLevel { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_MsdLive")]
        public int? PU2_MsdLive { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Extra1")]
        public string PU2_Extra1 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Extra2")]
        public string PU2_Extra2 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU2_Extra3")]
        public string PU2_Extra3 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_PackagingUnitId")]
        public string PU3_PackagingUnitId { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_SiplaceProComponent")]
        public string PU3_SiplaceProComponent { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Component")]
        public string PU3_Component { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_OriginalQuantity")]
        public int? PU3_OriginalQuantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Quantity")]
        public int? PU3_Quantity { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Manufacturer")]
        public string PU3_Manufacturer { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_ManufactureDate")]
        public DateTime? PU3_ManufactureDate { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_ExpiryDate")]
        public DateTime? PU3_ExpiryDate { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Supplier")]
        public string PU3_Supplier { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Batch")]
        public string PU3_Batch { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Serial")]
        public string PU3_Serial { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_MsdLevel")]
        public string PU3_MsdLevel { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_MsdLive")]
        public int? PU3_MsdLive { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Extra1")]
        public string PU3_Extra1 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Extra2")]
        public string PU3_Extra2 { get; set; }
        /// <summary>
        ///  
        ///</summary>
        [SugarColumn(ColumnName = "PU3_Extra3")]
        public string PU3_Extra3 { get; set; }


        [SugarColumn(IsIgnore =true)]
        public string TrackName { get; set; }
    }
}
