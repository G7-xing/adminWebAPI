using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{
    /// <summary>
    /// 部门接口层
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region 注入服务层
        public IDepartmentService departmentService;
        

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        #endregion

        /// <summary>
        ///获取在库的所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R List() 
        {
            return R.OK(departmentService.List());
        }
        /// <summary>
        /// 增加部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public R AddDepartment(Department department)
        {
            if (string.IsNullOrEmpty(department.DepartmentName)|| string.IsNullOrEmpty(department.ParentName))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (departmentService.AddDepartment(department))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public R UpdateDepartment(Department department)
        {
            if (string.IsNullOrEmpty(department.DepartmentName) || string.IsNullOrEmpty(department.ParentName))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (departmentService.UpdateDepartment(department))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");

            
        }
        /// <summary>
        /// 获取某个部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R GetDepartmentInfo(string departmentId)
        {
            return R.OK(departmentService.GetDepartmentInfo(departmentId));
        }
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet]
        public R DeleteDepartment(string departmentId)
        {
            string msg;
            if (departmentService.DeleteDepartment(departmentId,out msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }

        
    }
}
