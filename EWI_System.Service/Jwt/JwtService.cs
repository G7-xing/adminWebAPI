using EWI_System.Model;
using EWI_System.Model.Enties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EWI_System.Service.Jwt
{
    public class JwtService : IJwtService
    {

        #region 注入JWTTokenOptions
        private readonly JWTTokenOptions _JWTTokenOptions;
        public IConfiguration Configuration { get; }

        public JwtService(IOptionsMonitor<JWTTokenOptions> jWTTokenOptions, IConfiguration configuration)
        {
            _JWTTokenOptions = jWTTokenOptions.CurrentValue;
            Configuration = configuration;
        }
        #endregion

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public string GenerateToken(User user)
        {
            #region 有效载荷,有多少写多少尽量避免铭感信息
            var claims = new[] {
                new Claim("UserName",user.UserName),
                new Claim("UserId",user.Id.ToString()),
                new Claim("NickName",user.NickName),
                new Claim("DepartmentId",user.DepartmentId),
            };
            #endregion

            // 对我们的设置的key加密
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTTokenOptions.SigningKey));
            // 选个算法
            SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer:_JWTTokenOptions.Issuer,
                audience:_JWTTokenOptions.Audience,
                claims:claims,
                expires: DateTime.Now.Add(TimeSpan.FromDays(_JWTTokenOptions.ExpireDays)),//天数有效
                signingCredentials: credentials
                );
            string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
            return returnToken;
        }

        public LoginRes verityToken(string token)
        {
            try
            {
                JWTTokenOptions tokenOptions = new JWTTokenOptions();
                Configuration.Bind("JWTTokenOptions", tokenOptions);
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = tokenOptions.Audience,
                    ValidIssuer = tokenOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SigningKey))
                };
                var context = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var identity = jwtToken.Claims.First(c => c.Type == "UserName").Value;
                return new LoginRes()
                {
                    UserName = jwtToken.Claims.First(c => c.Type == "UserName").Value,
                    UserId = jwtToken.Claims.First(c => c.Type == "UserId").Value,
                    NickName = jwtToken.Claims.First(c => c.Type == "NickName").Value,
                    DepartmentId = jwtToken.Claims.First(c => c.Type == "DepartmentId").Value,
                };
            }
            catch (SecurityTokenExpiredException ex)
            {

                return new LoginRes();
            }           
        }
    }
}
