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
    [Route("[controller]/[action]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        #region 注入服务层
        public IAttendanceService attendanceService;
        

        public AttendanceController(IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
            
        }
        #endregion

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="Attendance"></param>
        /// <returns></returns>
        [HttpPost]
        public R CreateAttendance(UserAttendanceInfo attendanceInfo)
        {
            if (string.IsNullOrEmpty(attendanceInfo.UserId))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (attendanceService.CreateAttendance(attendanceInfo))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }

        /// <summary>
        ///获取在库的所有数据AttendanceReq
        ///pageNum: 1,
        ///pageSize: 10,
        ///keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public R FetchList(AttendanceReq req)
        {
            string msg = "";
            var attendancelist = attendanceService.FetchList(req.YearOfMonth, req.DepartmentId,ref  msg);
            if (attendancelist.Count==0)
            {
                return R.Error(msg);
            }
            return R.OK(attendancelist);
        }

        [HttpGet]
        public R DeleteAttendance(string attendanceDate, string userId)
        {
            string msg;
            if (attendanceService.DeleteAttendance(attendanceDate, userId, out msg))
            {
                return R.OK();
            };
            return R.Error(msg);
            //return R.Error(msg);
        }
        [HttpPost]
        public R UpdateAttendance(UserAttendanceInfo attendanceInfo)
        {
            if (string.IsNullOrEmpty(attendanceInfo.AbsenceDate))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (attendanceService.UpdateAttendance(attendanceInfo))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }



    }
}
