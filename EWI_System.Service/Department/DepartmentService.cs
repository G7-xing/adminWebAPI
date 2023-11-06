using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ISqlSugarClient dbconn;

        public DepartmentService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn;
        }
        /// <summary>
        /// 增加部门信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public bool AddDepartment(Department department)
        {
            var ds = dbconn.Insertable(department).ExecuteCommand();
            if (ds==1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteDepartment(string departmentId, out string msg)
        {
           
            // 是否存在下级部门
            var isExist = dbconn.Queryable<Department>().Where(it => it.ParentId == departmentId).Any();
            if (isExist)
            {
                msg = "该部门还有下级部门，不允许删除,请注意";
                return false;
            }
            // 该部门是最底层的
            if (!dbconn.Deleteable<Department>().Where(it => it.DepartmentId == departmentId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            msg = "";
            return true;

        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public Department GetDepartmentInfo(string departmentId)
        {
            var info = dbconn.Queryable<Department>().First(t=>t.DepartmentId == departmentId);
            if (info!=null)
            {
                return info;
            }
            return new Department();
        }

        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <returns></returns>
        public List<Department> List()
        {
            return  dbconn.Queryable<Department>().ToList();
            
        }
        /// <summary>
        /// 更新部门数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public bool UpdateDepartment(Department obj)
        {

            var flag = dbconn.Updateable<Department>()
                .SetColumns(it => new Department()
                {
                    DepartmentName = obj.DepartmentName,
                    ParentId = obj.ParentId,
                    ParentName = obj.ParentName,
                    Status = obj.Status,
                    DepartmentLevel = obj.DepartmentLevel,
                    UpdateBy = obj.CreateBy,
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                }).Where(it=>it.DepartmentId == obj.DepartmentId).ExecuteCommand();
            if (flag !=1)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }
}
