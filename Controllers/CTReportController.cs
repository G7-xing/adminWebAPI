using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;

using Microsoft.AspNetCore.Mvc;


namespace EWI_System.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CTReportController : ControllerBase
    {

        #region 注入服务层
        public ICTReportService CTReportService;


        public CTReportController(ICTReportService CTReportService)
        {
            this.CTReportService = CTReportService;
        }
        #endregion

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="CTReport"></param>
        /// <returns></returns>
        [HttpPost]
        public R CreateCTReport(CTReport cTReport)
        {
            if (string.IsNullOrEmpty(cTReport.When))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (CTReportService.CreateCTReport(cTReport))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }

        /// <summary>
        ///获取在库的所有数据及分页
        ///pageNum: 1,
        ///pageSize: 10,
        ///keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public R FetchList(CTReportReq cTReportReq)
        {
            int total = 0;
            if (cTReportReq.ListQuery.When ==null)
            {
                cTReportReq.ListQuery.When = Array.Empty<string>();
            }
            var CTReportlist = CTReportService.FetchList(cTReportReq.PageNum, cTReportReq.PageSize, cTReportReq.ListQuery, ref total);
            if (CTReportlist.Count == 0)
            {
                return R.Error("查询失败");
            }
            return R.OK(CTReportlist).data("total", total);
        }

        [HttpGet]
        public R DeleteAttendance(string attendanceDate, string userId)
        {
            
            return R.Error("");
            //return R.Error(msg);
        }
        [HttpGet]
        public R eyeDetailData(string CTReportId)
        {
            if (string.IsNullOrEmpty(CTReportId)) 
            {
                return R.Error("检查请求数据");
            }
            var detailList = CTReportService.eyeDetailData(CTReportId);
            return R.OK(detailList);
        }
        //
        [HttpPost]
        public R UpdateCTReport(CTReport report)
        {
            if (string.IsNullOrEmpty(report.CTRepeortId))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (CTReportService.UpdateCTReport(report))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }
        /// <summary>
        /// 保存ct的detail
        /// </summary>
        /// <param name="ctreportDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public R saveDetailData(CTDetaildataReq cTDetaildataReq)
        {
            if (string.IsNullOrEmpty(cTDetaildataReq.ctRepeortId))
            {
                return R.Error("保存失败，请检查提交的数据");
            }
            if (CTReportService.saveDetailData(cTDetaildataReq))
            {
                return R.OK();
            };
            return R.Error("保存失败，请检查提交的数据");
        }
    }
}
