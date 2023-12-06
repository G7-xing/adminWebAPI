using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service.MPSKanban.Dto
{
    public class MPSPDAReqcs
    {
        public string pickOrderId { get; set; }
        public string rawPartNumber { get; set; }
        public string uniqueId { get; set; }
        public string barcode { get; set; }
        public string userid { get; set; }
    }
}
