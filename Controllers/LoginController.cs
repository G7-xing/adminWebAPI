using EWI_System.Common;
using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;
using EWI_System.Service.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // 注入服务层
        public ILoginService _loginService;
        public IJwtService _jwtService;
        public IRoleService _roleService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, IJwtService jwtService,IRoleService roleService, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _jwtService = jwtService;
            _roleService = roleService;
            _logger = logger;
        }
        /// <summary>
        /// login
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public R Login(LoginReq req)
        {
            //var result = _loginService.GetLoginUser(req);
            //ApiResult apiResult = new ApiResult() { IsSuccess = true};
            //apiResult.Result = result;
            if (string.IsNullOrEmpty(req.UserName) || string.IsNullOrEmpty(req.Password))
            {
                return R.Error("请输入账号与密码");
            }
            else
            {
                User loginres = _loginService.Login(req);
                if (string.IsNullOrEmpty(loginres.UserName))
                {
                    return R.Error("账号不存在，用户名或密码错误");
                }
                else
                {
                    var token = _jwtService.GenerateToken(loginres);
                    _logger.LogWarning("测试登录成功后的warning"+token);
                    return R.OK().data("token",token);
                }
                
            }
        }
        /// <summary>
        /// 解析jwt
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public R Info(string token) {
            var s = _jwtService.verityToken(token);
            if (s.UserId == null)
            {
                return R.Error("登录时间过期，请重新登录");
            }
            var menusList = _roleService.GetMenuListByUserId(s.UserId);
            s.RoleName = _roleService.GetRoleByUserId(s.UserId);
            return R.OK(s).data("menus", menusList);
        }
        [HttpPost]
        public R LoginOut() {
            return R.OK();
        }
    }
}
