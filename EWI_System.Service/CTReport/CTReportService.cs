using DotLiquid;

using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWI_System.Service
{
    public class CTReportService : ICTReportService
    {

        private readonly ISqlSugarClient dbconn;

        public CTReportService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn.AsTenant().GetConnectionScope(0);
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="cTReport"></param>
        /// <returns></returns>
        public bool CreateCTReport(CTReport cTReport)
        {
            var ds = dbconn.Insertable(cTReport).ExecuteCommand();
            if (ds == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 查看detail
        /// </summary>
        /// <param name="cTRepeortId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<CtreportDetail> eyeDetailData(string CTReportId)
        {
            return dbconn.Queryable<CtreportDetail>().Where(c=>c.CtreportId == CTReportId).ToList();
        }

        /// <summary>
        /// fetchList
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="listQuery"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<CTReportRes> FetchList(int pageNum, int pageSize, ListQuery listQuery, ref int total)
        {
            try
            {

                List<CTReportRes> list = dbconn.Queryable<CTReport>().WhereIF(listQuery.When.Length != 0, C => SqlFunc.Between(C.When, listQuery.When[0], listQuery.When[1]))
                                                   .WhereIF(!String.IsNullOrEmpty(listQuery.ShippingNo),C=>C.ShippingNo==listQuery.ShippingNo)
                                                   .WhereIF(!String.IsNullOrEmpty(listQuery.LineId),C=>C.LineId == listQuery.LineId)
                                                   .LeftJoin<User>((C, U) => C.CreateBy == U.Id.ToString())
                                                   .LeftJoin<User>((C, U, U1) => C.UpdateBy == U1.Id.ToString())
                                                   .LeftJoin<Line>((C, U, U1, L) => C.LineId == L.LineId)
                                                   .OrderBy(C => C.When)
                                                   .Select((C, U, U1, L) => new CTReportRes
                                                   {
                                                       CTReport = C,
                                                       LineName = L.LineName
                                                   })
                                                   .ToPageList(pageNum, pageSize, ref total);
                foreach (var item in list)
                {
                    item.CTReport.ResultBalancerateline = (Double.Parse(item.CTReport.ResultBalancerateline) * 100.00).ToString();
                    item.CTReport.ResultBalancerateop = (Double.Parse(item.CTReport.ResultBalancerateop) * 100.00).ToString();
                }
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
        /// <summary>
        /// 保存detail数据
        /// </summary>
        /// <param name="cTDetaildataReq"></param>
        /// <returns></returns>
        public bool saveDetailData(CTDetaildataReq cTDetaildataReq)
        {
            // 先查CTReport的数据
            CTReport cTReport = dbconn.Queryable<CTReport>().Where(C => C.CTRepeortId == cTDetaildataReq.ctRepeortId).First();
            if (cTReport==null) return false;
            decimal maxS_CT = 0;
            decimal maxM_CT = 0;
            decimal sumS_CT = 0;
            decimal sumM_CT = 0;
            int totalS_CT = 0;
            int totalM_CT = 0;
            // 将数据封装
            foreach (var item in cTDetaildataReq.ctreportDetails)
            {
                // 封装detail的数据--必填数据
                item.CreateBy = cTDetaildataReq.userId;
                item.CtreportId = cTDetaildataReq.ctRepeortId;
                // 计算CTReport的数据依据每一个值
                if (!(item.StationCT == null || item.StationCT == 0))
                {
                    ++totalS_CT;
                    sumS_CT = (decimal)(sumS_CT + item.StationCT);
                }
                if (!(item.ManCT == null || item.ManCT == 0))
                {
                    ++totalM_CT;
                    sumM_CT = (decimal)(sumM_CT + item.ManCT);
                }
                if (item.StationCT > maxS_CT)
                {
                    maxS_CT = (decimal)item.StationCT; // maxS_CT
                    cTReport.ResultNeckstation = item.StationL1; // 最大的那个station
                }
                if (item.ManCT > maxM_CT)
                {
                    maxM_CT = (decimal)item.ManCT; // maxM_CT
                }
            }
            cTReport.ResultCt = maxS_CT.ToString();
            cTReport.ResultBalancerateline = (sumS_CT / maxS_CT / totalS_CT).ToString("0.0000");
            cTReport.ResultBalancerateop = (sumM_CT / maxM_CT / totalM_CT).ToString("0.0000");
            // 将数据存入detail 表中同时更新report的haveDetail字段值---是一个事务，进行事务处理:语法糖--自动异常--没有全局异常处理机制
            var result = dbconn.AsTenant().UseTran(() =>
            {
                dbconn.Insertable(cTDetaildataReq.ctreportDetails).ExecuteCommand();
                dbconn.Updateable<CTReport>().SetColumns(it => new CTReport 
                                                        {
                                                            haveDetail="yes",
                                                            ResultCt= cTReport.ResultCt,
                                                            ResultBalancerateop= cTReport.ResultBalancerateop,
                                                            ResultBalancerateline= cTReport.ResultBalancerateline,
                                                            ResultNeckstation= cTReport.ResultNeckstation,
                                                            UpdateBy = cTDetaildataReq.userId,
                                                            UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                         })
                                             .Where(CT=>CT.CTRepeortId==cTDetaildataReq.ctRepeortId).ExecuteCommand();
                return true;
            });
            
            return result.IsSuccess;
        }

        public bool UpdateCTReport(CTReport report)
        {
            return dbconn.Updateable<CTReport>().SetColumns(r =>new CTReport {   platform = report.platform, 
                When = report.When,Why = report.Why, Who = report.Who, How = report.How,Comments = report.Comments,
                ConnPlatesNum = report.ConnPlatesNum, Customer = report.Customer, LineId = report.LineId,
                OPNum = report.OPNum,Project = report.Project,ShippingNo = report.ShippingNo,TaktTime = report.TaktTime,
                UpdateBy = report.UpdateBy,UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")})
                                                          .Where(r => r.CTRepeortId == report.CTRepeortId)
                                                          .ExecuteCommandHasChange();
        }


        //    /// <summary>
        //    /// 删除
        //    /// </summary>
        //    /// <param name="AttendanceId"></param>
        //    /// <param name="msg"></param>
        //    /// <returns></returns>

        //    public bool DeleteAttendance(string attendanceDate, string userId, out string msg)
        //    {
        //        if (!dbconn.Deleteable<UserAttendanceInfo>().Where(it => it.AbsenceDate == attendanceDate).Where(it=>it.UserId==userId).ExecuteCommandHasChange())
        //        {
        //            msg = "执行语句有异常";
        //            return false;
        //        }
        //        msg = "";
        //        return true;
        //    }
    }
}
