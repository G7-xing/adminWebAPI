using System;
using System.Collections.Generic;
using System.Text;

using EWI_System.Model.Enties;

namespace EWI_System.Service
{
    public class PickOrderReq
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }

        public string PickOrderNumber { get; set; }
        public string PcbaPartNumber { get; set; }
        public string CreatedTime { get; set; }
        public int PickOrderType { get; set; }
        public int PickOrderStatus { get; set; }
        public string LineName { get; set; }

    }
}
