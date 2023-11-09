using EWI_System.Model;
using EWI_System.Service;

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

    }
}
