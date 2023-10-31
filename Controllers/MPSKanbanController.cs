using EWI_System.Model;
using EWI_System.Service;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{
    
    [Route("[controller]/[action]")]
    [ApiController]
    public class MPSKanbanController : ControllerBase
    {
        
        private readonly ILogger<MPSKanbanController> _logger;
        public IMPSKanbanService _IMPSkanbanService;

        public MPSKanbanController(ILogger<MPSKanbanController> logger, IMPSKanbanService kanbanService)
        {
            _logger = logger;
            _IMPSkanbanService = kanbanService;
        }

        [HttpGet]
        public R getDeatils()
        {
            var list = _IMPSkanbanService.getDeatils();
            return R.OK(list);
        }
    }
}
