using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class LoginRes : User
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
