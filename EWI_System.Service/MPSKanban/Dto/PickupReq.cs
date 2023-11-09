using System;
using System.Collections.Generic;
using System.Text;

using EWI_System.Model.Enties;

namespace EWI_System.Service
{
    public class PickupReq : PickOrderReq
    {
        public string ComponentNumber { get; set; }
        public string UniqueId { get; set; }
        public string PickupTime { get; set; }
        public int PickupStatus { get; set; }

    }
}
