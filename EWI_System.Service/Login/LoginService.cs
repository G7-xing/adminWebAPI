using AutoMapper;
using EWI_System.Common;
using EWI_System.Model;
using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace EWI_System.Service
{
    public class LoginService : ILoginService
    {
        #region 注入automapper ,相当于spring中的@Autowired,注入，构造器注入的方式
        private readonly IMapper _mapper;
        private readonly ISqlSugarClient dbconn;

        public LoginService(IMapper mapper, ISqlSugarClient dbconn)
        {
            _mapper = mapper;
            this.dbconn = dbconn;
        }
        #endregion
        public User Login(LoginReq req)
        {
            var user = dbconn.Queryable<User>().First(p => p.UserName == req.UserName && p.Password == req.Password);
            if (user != null)
            {
                return user;
            }
            return new User();
        }
        



        //public List<LoginRes> GetLoginUser(LoginReq req)
        //{
        //    //List<Users> res = new List<Users>();
        //    //var res = DbContext.dbconn.Queryable<Users>().WhereIF(req.UserName != null,x => x.UserName == req.UserName).ToList();
        //    //return _mapper.Map<List<LoginRes>>(res);
        //    var user = DbContext.dbconn.Queryable<Users>().First(p => p.UserName == req.UserName && p.Password == req.Password);
        //    if (user!= null)
        //    {
        //        return null;
        //    }
        //    return null;
        //}

        //public R<LoginRes> Login(LoginReq req)
        //{
        //    var user = DbContext.dbconn.Queryable<Users>().First(p => p.UserName == req.UserName && p.Password == req.Password); ;
        //    if (user != null)
        //    {

        //        return null;
        //    }
        //    return null;
        //}

        //public LoginRes Login()
        //{
        //    throw new NotImplementedException();
        //}


    }
}
