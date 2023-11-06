using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Model
{
    /// <summary>
    /// 远程服务的相关配置信息model
    /// </summary>
    public class RemoteFileServiceInfo
    {
        
        public string IP_Path { get; set; }
        
        public string Account { get; set; }
        
        public string Password { get; set; }
    }
}
