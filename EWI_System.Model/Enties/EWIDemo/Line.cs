using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace EWI_System.Model.Enties
{
    /// <summary>
    /// 线体表
    ///</summary>
    [SugarTable("Line")]
    public class Line
    {
        /// <summary>
        ///  
        /// 默认值: (newid())
        ///</summary>
         [SugarColumn(ColumnName="LineId" ,IsPrimaryKey = true,  InsertSql = "newid()")]
         public string LineId { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="LineName"    )]
         public string LineName { get; set; }
        /// <summary>
        /// 状态，1-正常，0禁用 
        /// 默认值: ((1))
        ///</summary>
         [SugarColumn(ColumnName="Status"    )]
         public int Status { get; set; }
        /// <summary>
        ///  
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="CreateTime", InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string CreateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="CreateBy"    )]
         public string CreateBy { get; set; }
        /// <summary>
        ///  
        /// 默认值: (CONVERT([nvarchar],getdate(),(20)))
        ///</summary>
         [SugarColumn(ColumnName="UpdateTime" , InsertSql = "CONVERT([nvarchar],getdate(),(20))")]
         public string UpdateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="UpdateBy"    )]
         public string UpdateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(ColumnName = "LineCategory")]
        public string LineCategory { get; set; }
    }
}
