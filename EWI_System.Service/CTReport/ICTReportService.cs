using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface ICTReportService 
    {
        bool CreateCTReport(CTReport cTReport);
        bool UpdateCTReport(CTReport report);
        List<CtreportDetail> eyeDetailData(string CTReportId);
        List<CTReportRes> FetchList(int pageNum, int pageSize, ListQuery listQuery, ref int total);
        bool saveDetailData(CTDetaildataReq cTDetaildataReq);

        //bool DeleteAttendance(string attendanceDate, string userId, out string msg);
        //List<Attendance> FetchAllList();
    }
}
