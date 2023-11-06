using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IAttendanceService
    {
        bool CreateAttendance(UserAttendanceInfo attendanceInfo);
        List<AttendanceRes> FetchList(string yearOfMonth, string[] departmentId,ref string msg);
        //bool UpdateStatus(Attendance role);
        bool UpdateAttendance(UserAttendanceInfo attendanceInfo);
        bool DeleteAttendance(string attendanceDate, string userId, out string msg);
        //List<Attendance> FetchAllList();
    }
}
