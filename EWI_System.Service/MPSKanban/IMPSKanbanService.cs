using EWI_System.Model;
using EWI_System.Model.Enties;
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
    }
}
