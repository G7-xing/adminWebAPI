using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IDepartmentService
    {
        public List<Department> List();
        bool AddDepartment(Department department);
        Department GetDepartmentInfo(string departmentId);
        bool DeleteDepartment(string departmentId, out string msg);
        bool UpdateDepartment(Department department);
    }
}
