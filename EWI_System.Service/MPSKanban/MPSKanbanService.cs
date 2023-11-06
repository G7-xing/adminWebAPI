using AutoMapper;
using EWI_System.Common;
using EWI_System.Model;
using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace EWI_System.Service
{
    public class MPSKanbanService : IMPSKanbanService
    {
  
        private readonly ISqlSugarClient dbconn;

        public MPSKanbanService( ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn.AsTenant().GetConnectionScope(1); // 另外一个库
        }

        public List<MPSKanbanRes> getDeatils()
        {
            string sql = @"select
								[PickOrderId],
								[PartNumber],
								[Line],
								[PickStatus],
								po.[WorkOrder],
								[PickOrderType],
								[Created],
								[Required],
								DATEDIFF(MI, [Created], getdate()) ExpiredMinutes
							from
								[Tb_MPS_PickOrders] po
							LEFT join [Tb_MPS_SmtSetup] ss on
								po.Line = ss.CfmSmtLineName
							where
								PickStatus = 10
								and PlantSite = 'CFM-E'
							order by
								PickOrderType DESC,
								PickStatus ASC,
								Created ASC";
            var list = dbconn.Ado.SqlQuery<MPSKanbanRes>(sql);
           
            return list;
        }
  
    }
}
