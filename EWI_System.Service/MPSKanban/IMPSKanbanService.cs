using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service.MPSKanban.Dto;

using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IMPSKanbanService
    {
        List<TbMpsPickorderdetails> expandRowList(string pickOrderId);
        List<vPackagingUnitInSetup> fetchASMSetupList(AsmSetupReq asmSetupReq, ref int total);
        List<TbMpsPickorders> fetchHistoryList(PickOrderReq pickOrderReq, ref int total);
        List<vCfmPickOrders> fetchPickupList(PickupReq pickupReq, ref int total);
        List<MPSKanbanRes> getDeatils();

        #region SMTMPSPDA
        List<TbMpsPickorders> fetchMPSofLineList(List<string> lineMPSRes);
        List<TbMpsPickorderdetails> fetchMPSDetailById(string pickOrderId);
        bool handleQADAndMPS(MPSPDAReqcs mPSPDA, ref string errMsg);
        #endregion
    }
}
