using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class RoleReq 
    {
        public string roleId { get; set; }
        public List<string> menuIds { get; set; }
    }
}
