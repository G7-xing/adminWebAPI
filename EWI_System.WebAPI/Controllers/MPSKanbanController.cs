using EWI_System.Model;
using EWI_System.Service;
using EWI_System.Service.MPSKanban.Dto;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{   /// <summary>
    /// MPS
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class MPSKanbanController : ControllerBase
    {
        
        private readonly ILogger<MPSKanbanController> _logger;
        /// <summary>
        /// 服务接口
        /// </summary>
        public IMPSKanbanService _IMPSkanbanService; 
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="kanbanService"></param>
        public MPSKanbanController(ILogger<MPSKanbanController> logger, IMPSKanbanService kanbanService)
        {
            _logger = logger;
            _IMPSkanbanService = kanbanService;
        }
        /// <summary>
        /// 获取MPS看板的数据方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R getDeatils()
        {
            var list = _IMPSkanbanService.getDeatils();
            return R.OK(list);
        }
        /// <summary>
        /// 订单查询的分页加过滤条件
        /// </summary>
        /// <param name="pickOrderReq"></param>
        /// <returns></returns>
        [HttpPost]
        public R fetchHistoryList(PickOrderReq pickOrderReq) 
        {
            int total = 0;
            
            var historyList = _IMPSkanbanService.fetchHistoryList(pickOrderReq,ref total);
            if (historyList.Count < 0)
            {
                return R.Error("查询失败");
            }
            return R.OK(historyList).data("total", total);
        }
        /// <summary>
        /// 对展开行的api
        /// </summary>
        /// <param name="pickOrderId"></param>
        /// <returns></returns>
        [HttpGet]
        public R expandRowList(string pickOrderId) 
        {
            var rowList = _IMPSkanbanService.expandRowList(pickOrderId);
            return R.OK(rowList);
        }

        /// <summary>
        /// 配料查询的分页加过滤条件
        /// </summary>
        /// <param name="pickupReq"></param>
        /// <returns></returns>
        [HttpPost]
        public R fetchPickupList(PickupReq pickupReq)
        {
            int total = 0;
            var pickupList = _IMPSkanbanService.fetchPickupList(pickupReq, ref total);
            if (pickupList.Count < 0)
            {
                return R.Error("查询失败");
            }
            return R.OK(pickupList).data("total", total);
        }

        /// <summary>
        /// asm上料查询的分页加过滤条件
        /// </summary>
        /// <param name="asmSetupReq"></param>
        /// <returns></returns>
        [HttpPost]
        public R fetchASMSetupList(AsmSetupReq asmSetupReq)
        {
            int total = 0;
            var asmList = _IMPSkanbanService.fetchASMSetupList(asmSetupReq, ref total);
            if (asmList.Count < 0)
            {
                return R.Error("查询失败");
            }
            return R.OK(asmList).data("total", total);
        }
        #region SMTMPSPDA 
        /// <summary>
        /// 获取MPSofline 信息
        /// </summary>
        /// <param name="lineMPSRes"></param>
        /// <returns></returns>
        [HttpPost]
        public R fetchMPSofLineList(List<string> lineMPSRes)
        {
            if (lineMPSRes.Count < 0)
            {
                return R.Error("提交数据为空，请注意！");
            }
            var MPSofLineList = _IMPSkanbanService.fetchMPSofLineList(lineMPSRes);
            if (MPSofLineList.Count < 0)
            {
                return R.Error("查询失败");
            }
            return R.OK(MPSofLineList);
        }
        /// <summary>
        /// 依据id查detail数据
        /// </summary>
        /// <param name="pickOrderId"></param>
        /// <returns></returns>
        [HttpGet]
        public R fetchMPSDetailById(string pickOrderId)
        {
            if (string.IsNullOrEmpty(pickOrderId))
            {
                return R.Error("提交数据为空，请注意！");
            }
            var MPSDetailList = _IMPSkanbanService.fetchMPSDetailById(pickOrderId);
            if (MPSDetailList.Count < 0)
            {
                return R.Error("查询失败");
            }
            return R.OK(MPSDetailList);
        }

        [HttpPost]
        public R handleQADAndMPS(MPSPDAReqcs mPSPDA)
        {
            if (string.IsNullOrEmpty(mPSPDA.uniqueId))
            {
                return R.Error("提交数据为空，请注意！");
            }
            string errMsg = string.Empty;
            return _IMPSkanbanService.handleQADAndMPS(mPSPDA, ref  errMsg) ? R.OK().message(mPSPDA.uniqueId+ ":QAD移库成功") : 
                R.Error(mPSPDA.uniqueId + ":QAD返回失败"+ errMsg); 
        }
        #endregion


    }
}
