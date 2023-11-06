using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWI_System.Model;


namespace EWI_System.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        [HttpGet]
        public void InitDataBase()
        {
            //DbContext.InitDataBase();
        }
    }
}
