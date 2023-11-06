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
        /// 获取方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R getDeatils()
        {
            var list = _IMPSkanbanService.getDeatils();
            return R.OK(list);
        }
    }
}
