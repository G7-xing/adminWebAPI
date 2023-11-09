using AutoMapper;
using EWI_System.Common;
using EWI_System.Model;
using EWI_System.Model.Enties;

using Microsoft.Extensions.Logging;

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;

namespace EWI_System.Service
{
    public class MPSKanbanService : IMPSKanbanService
    {
  
        private readonly ISqlSugarClient dbconn;// 主要是ace的库
        private readonly ISqlSugarClient asmDB; // asm贴片机库
		private readonly ILogger<MPSKanbanService> _logger;

        public MPSKanbanService(ISqlSugarClient dbconn, ISqlSugarClient asmDB, ILogger<MPSKanbanService> logger)
        {
            this.dbconn = dbconn.AsTenant().GetConnectionScope("ACE_Traceability_DB"); // ACE_Traceability_DB库 
            this.asmDB = asmDB.AsTenant().GetConnectionScope("SiplaceSetupCenter_DB"); // SiplaceSetupCenter_DB库 贴片机的
            _logger = logger;
        }
        /// <summary>
        /// 带查询的分页
        /// </summary>
        /// <param name="pickOrderReq"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<vCfmPickOrders> fetchPickupList(PickupReq pickupReq, ref int total)
        {
            List<vCfmPickOrders> list = dbconn.Queryable<vCfmPickOrders>()
                .WhereIF(!String.IsNullOrEmpty(pickupReq.ComponentNumber), T => T.ComponentNumber == pickupReq.ComponentNumber.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickupReq.UniqueId), T => T.UniqueId == pickupReq.UniqueId.Trim().ToUpper())
                .WhereIF(pickupReq.PickOrderType != 0, T => T.PickOrderType == pickupReq.PickOrderType)
                .WhereIF(pickupReq.PickupStatus != 0, T => T.PickupStatus == pickupReq.PickupStatus)
                .WhereIF(!String.IsNullOrEmpty(pickupReq.LineName), T => T.Line == pickupReq.LineName.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickupReq.PickupTime), T => T.PickupDateTime >= DateTime.Parse(pickupReq.PickupTime))
                .Where(T=>SqlFunc.Contains(T.PickOrderId,"CFM"))
                .OrderByDescending(T => T.PickOrderId)
                .ToPageList(pickupReq.PageNum, pickupReq.PageSize, ref total);
            return list;
        }
        /// <summary>
        /// 展开行的数据
        /// </summary>
        /// <param name="pickOrderId"></param>
        /// <returns></returns>
        public List<TbMpsPickorderdetails> expandRowList(string pickOrderId)
        {
            return dbconn.Queryable<TbMpsPickorderdetails>().Where(T=>T.PickOrderId==pickOrderId)
				  .OrderBy(T=>T.PartNumber).ToList();
        }

        /// <summary>
        /// 带查询的分页
        /// </summary>
        /// <param name="pickOrderReq"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<TbMpsPickorders> fetchHistoryList(PickOrderReq pickOrderReq, ref int total)
        {
            var list =  dbconn.Queryable<TbMpsPickorders>()
				.WhereIF(!String.IsNullOrEmpty(pickOrderReq.PickOrderNumber),T=>T.PickOrderId==pickOrderReq.PickOrderNumber.Trim().ToUpper())
				.WhereIF(!String.IsNullOrEmpty(pickOrderReq.PcbaPartNumber),T=>T.PartNumber==pickOrderReq.PcbaPartNumber.Trim().ToUpper())
				.WhereIF(pickOrderReq.PickOrderType!=0,T=>T.PickOrderType==pickOrderReq.PickOrderType)
				.WhereIF(pickOrderReq.PickOrderStatus!=0,T=>T.PickStatus== pickOrderReq.PickOrderStatus)
				.WhereIF(!String.IsNullOrEmpty(pickOrderReq.LineName),T=>T.Line==pickOrderReq.LineName.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickOrderReq.CreatedTime), T => T.Created >= DateTime.Parse(pickOrderReq.CreatedTime))
				.OrderByDescending(T=>T.PickOrderId)
				.ToPageList(pickOrderReq.PageNum,pickOrderReq.PageSize, ref total);
			return list;
        }
		/// <summary>
		/// kanban 的sql
		/// </summary>
		/// <returns></returns>
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
        /// <summary>
        /// asm分页查询带条件
        /// </summary>
        /// <param name="asmSetupReq"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<vPackagingUnitInSetup> fetchASMSetupList(AsmSetupReq asmSetupReq, ref int total)
        {
            var list = asmDB.Queryable<vPackagingUnitInSetup>()
               .WhereIF(!String.IsNullOrEmpty(asmSetupReq.AsmUID), T=>T.PU1_PackagingUnitId== asmSetupReq.AsmUID || T.PU2_PackagingUnitId == asmSetupReq.AsmUID || T.PU3_PackagingUnitId == asmSetupReq.AsmUID)
               .WhereIF(!String.IsNullOrEmpty(asmSetupReq.AsmPartNumber), T => T.PU1_SiplaceProComponent == asmSetupReq.AsmPartNumber.Trim().ToUpper())
               .WhereIF(!String.IsNullOrEmpty(asmSetupReq.LineName), T => T.Line == asmSetupReq.LineName.Trim().ToUpper())
               //.OrderByDescending(T => T.)
               .ToPageList(asmSetupReq.PageNum, asmSetupReq.PageSize, ref total);
            foreach (var item in list)
            {
                item.TrackName = item.Station + "__" + item.Location + "_" + item.Track + "_" + item.Division;
            }
            return list;
        }
    }
}
