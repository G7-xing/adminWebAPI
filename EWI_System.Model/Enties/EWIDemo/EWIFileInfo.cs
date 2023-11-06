using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// ewi文件信息表
    ///</summary>
    [SugarTable("EWIFileInfo")]
    public class EWIFileInfo
    {
        /// <summary>
        /// 唯一标识 
        /// 默认值: (newid())
        ///</summary>
         [SugarColumn(ColumnName= "FileId" , IsPrimaryKey = true, InsertSql = "newid()")]
         public string FileId { get; set; }
        /// <summary>
        /// 文件的地址 
        ///</summary>
         [SugarColumn(ColumnName="FileURL"    )]
         public string FileURL { get; set; }
        /// <summary>
        /// 文件关联的线体 
        ///</summary>
         [SugarColumn(ColumnName="LineId"    )]
         public string LineId { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="CreateBy")]
         public string CreateBy { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="UpdateTime" , InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string UpdateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="UpdateBy" ) ]
         public string UpdateBy { get; set; }
        /// <summary>
        /// 1-yes,0-no 
        /// 默认值: ((1))
        ///</summary>
         [SugarColumn(ColumnName="Status" ,InsertSql ="1"   )]
         public int Status { get; set; }
        /// <summary>
        ///  
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string CreateTime { get; set; }
        /// <summary>
        /// 文件名 
        ///</summary>
         [SugarColumn(ColumnName="FileName"    )]
         public string FileName { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
         [SugarColumn(ColumnName="Remark"    )]
         public string Remark { get; set; }
    }
}
