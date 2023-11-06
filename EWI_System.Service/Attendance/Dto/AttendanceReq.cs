using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class AttendanceReq
    {
       public string YearOfMonth { get; set; }
       public string[] DepartmentId { get; set; }
    }
}

