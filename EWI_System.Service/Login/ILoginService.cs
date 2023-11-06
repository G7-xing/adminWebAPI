using EWI_System.Model;
using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface ILoginService
    {
        public User Login(LoginReq req);
    }
}
