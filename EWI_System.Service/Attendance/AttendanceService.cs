using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWI_System.Service
{
    public class AttendanceService : IAttendanceService
    {

        private readonly ISqlSugarClient dbconn;

        public AttendanceService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn;
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="AttendanceReq"></param>
        /// <returns></returns>
        public bool CreateAttendance(UserAttendanceInfo attendanceInfo)
        {
            var ds = dbconn.AsTenant().GetConnectionScope(0).Insertable(attendanceInfo).ExecuteCommand();
            if (ds == 1)
            {
                return true;
            }
            return false;
        }
       
        public List<AttendanceRes> FetchList(string yearOfMonth, string[] departmentIds,ref string msg)
        {
            try
            {
                List<AttendanceRes> list = new List<AttendanceRes>();
                List<User> userList = new List<User>();
                // 先依据部门获取到该部门下的所有user

                foreach (var id in departmentIds)
                {
                    userList = dbconn.Queryable<User>().Where(u => u.DepartmentId == id).ToList();
                    if (userList.Count == 0)
                    {
                        msg = "没有用户，请注意";
                        return list;
                    }
                    // user 与 ym 获取到具体记录
                    foreach (User user in userList)
                    {
                        AttendanceRes res = new AttendanceRes();
                        res.UserId = user.Id;
                        res.UserName = user.UserName;
                        List<UserAttendanceInfo> attendanceInfos = new List<UserAttendanceInfo>();
                        attendanceInfos = dbconn.Queryable<UserAttendanceInfo>().Where(A => A.UserId == user.Id).Where(A => A.AbsenceDate.Contains(yearOfMonth)).ToList();
                        if (attendanceInfos.Count == 0)
                        {
                            res.Days = 0;
                            res.Datalist = null;
                            list.Add(res);
                        }
                        else
                        {
                            double total = 0;
                            //Data data = new Data();
                            foreach (UserAttendanceInfo item in attendanceInfos)
                            {
                                Data data = new Data();
                                // 依据结果计算出返回的days
                                data.Type = item.AbsenceType;
                                data.AttendanceDate = item.AbsenceDate;

                                if (item.AbsenceType != "H"  /*item.AbsenceType!= "N"*/) // N 不会添加的
                                {
                                    ++total;
                                }
                                else
                                {
                                    total += 0.5;
                                }
                                res.Datalist.Add(data);
                            }
                            res.Days = total;
                            list.Add(res);
                            //res.Datalist.Add(data);
                        }
                    }   
                }
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }


        public bool UpdateAttendance(UserAttendanceInfo attendanceInfo)
        {
            return dbconn.Updateable<UserAttendanceInfo>()
                                                    .SetColumns(r =>
                                                        new UserAttendanceInfo
                                                        {
                                                            AbsenceType = attendanceInfo.AbsenceType,
                                                            UpdateBy = attendanceInfo.UpdateBy,
                                                            UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                        }
                                                       )
                                                      .Where(r => r.AbsenceDate == attendanceInfo.AbsenceDate)
                                                      .Where(r => r.UserId == attendanceInfo.UserId)
                                                      .ExecuteCommandHasChange();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="AttendanceId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool DeleteAttendance(string attendanceDate, string userId, out string msg)
        {
            if (!dbconn.Deleteable<UserAttendanceInfo>().Where(it => it.AbsenceDate == attendanceDate).Where(it=>it.UserId==userId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            msg = "";
            return true;
        }
    }
}
