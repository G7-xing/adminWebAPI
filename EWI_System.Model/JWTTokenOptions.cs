using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Model
{
    public class JWTTokenOptions
    {
        /// <summary>
        /// 颁发给谁
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 谁颁发的
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 令牌签名
        /// </summary>
        public string SigningKey { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpireSeconds { get; set; }
    }
}
