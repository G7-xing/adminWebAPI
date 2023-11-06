using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class CTReportReq
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public ListQuery ListQuery { get; set; }
    }
    public class ListQuery
    {
        public string[] When { get; set; }
        public string ShippingNo { get; set; }
        public string LineId { get; set; }
    }

    public class CTDetaildataReq 
    { 
        public string ctRepeortId { get; set; }
        public string userId { get; set; }

        public List<CtreportDetail> ctreportDetails = new List<CtreportDetail>();
    }
}

