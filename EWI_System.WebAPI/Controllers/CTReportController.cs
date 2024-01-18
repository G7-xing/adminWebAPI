using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;

using Microsoft.AspNetCore.Mvc;


namespace EWI_System.WebAPI.Controllers
{
    /// <summary>
    /// CT报表的controller
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class CTReportController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        #region 注入服务层
        public ICTReportService CTReportService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CTReportService"></param>
        public CTReportController(ICTReportService CTReportService)
        {
            this.CTReportService = CTReportService;
        }
        #endregion

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="cTReport"></param>
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
            if (CTReportlist.Count < 0)
            {
                return R.Error("查询失败");
            }           
            return R.OK(CTReportlist).data("total", total);
        }
        /// <summary>
        /// 删除数据及detail也需要删除
        /// </summary>
        /// <param name="CTReportId"></param>
        /// <returns></returns>
        [HttpGet]
        public R deleteCTReport(string CTReportId)
        {
            if (string.IsNullOrEmpty(CTReportId))
            {
                return R.Error("检查请求数据");
            }
            return CTReportService.deleteCTReport(CTReportId)?R.OK(): R.Error("删除异常，请联系IT");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CTReportId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
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
        /// <param name="cTDetaildataReq"></param>
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
