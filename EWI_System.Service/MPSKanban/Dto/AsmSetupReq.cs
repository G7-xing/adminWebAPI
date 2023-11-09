using System;
using System.Collections.Generic;
using System.Text;

using EWI_System.Model.Enties;

namespace EWI_System.Service
{
    public class AsmSetupReq
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string LineName { get; set; }
        public string AsmUID { get; set; }
        public string AsmPartNumber { get; set; }
    }
}
