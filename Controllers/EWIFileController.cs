using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;

namespace EWI_System.WebAPI.Controllers
{
    /// <summary>
    /// 口层
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class EWIFileController : ControllerBase
    {
        #region 注入服务层
        public IEWIFileService eWIFileService;
        private ILogger<EWIFileController> logger;
        public IConfiguration configuration { get; }

        public EWIFileController(IEWIFileService EWIFileService, ILogger<EWIFileController> logger, IConfiguration configuration)
        {
            this.eWIFileService = EWIFileService;
            this.logger = logger;
            this.configuration = configuration;
        }
        #endregion
        /// <summary>
        /// 待完善
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult  ViewFile(string fileId)
        {

            byte[] fileBytes = null;
            bool status = false;
            // 获取远程地址，远程服务器账号密码
            RemoteFileServiceInfo remoteFileServiceInfo = new RemoteFileServiceInfo();
            configuration.Bind("RemoteFileServiceInfo", remoteFileServiceInfo);
            // 获取该文件的地址
            string URLByFilePath = eWIFileService.GetFileURL(fileId);
            if (URLByFilePath == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            // 连接远程共享文件的代码
            try
            {
                //连接共享文件夹
                status = connectState(remoteFileServiceInfo.IP_Path, remoteFileServiceInfo.Account, remoteFileServiceInfo.Password);
                if (status)
                {
                    //共享文件夹的目录
                    DirectoryInfo theFolder = new DirectoryInfo(remoteFileServiceInfo.IP_Path + URLByFilePath);
                    fileBytes = System.IO.File.ReadAllBytes(theFolder.ToString());
     
                    return  File(fileBytes, "application/pdf", "remote.pdf");
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (IOException io)
            {
                logger.LogError("获取文件错误" + io);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex) 
            {
                logger.LogError("其他错误" + ex);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        [HttpPost]
        public R CreateEWIFile(EWIFileInfo file)
        {
            if (string.IsNullOrEmpty(file.FileName) && string.IsNullOrEmpty(file.FileURL))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (eWIFileService.CreateEWIFile(file))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }

        /// <summary>
        ///获取在库的所有数据
        ///pageNum: 1,
        //pageSize: 10,
        //keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R FetchList(int pageNum, int pageSize, string keyword)
        {
            int total = 0;
            var Linelist = eWIFileService.FetchList(pageNum, pageSize, keyword, ref total);
            return R.OK(Linelist).data("total", total);
        }
        [HttpGet]
        public R GetFileListByLineId(string lineId)
        {
            return R.OK(eWIFileService.GetFileListByLineId(lineId));
        }

        //[HttpPost]
        //public R UpdateStatus(Line line) 
        //{
        //    if (lineService.UpdateStatus(line))
        //    {
        //        return R.OK();
        //    }
        //    return R.Error("修改状态失败，请联系管理员");
        //}

        [HttpPost]
        public R UpdateEWIFile(EWIFileInfo file)
        {
            if (string.IsNullOrEmpty(file.FileName) && string.IsNullOrEmpty(file.FileURL)&& string.IsNullOrEmpty(file.FileId))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (eWIFileService.UpdateEWIFile(file))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }

        [HttpGet]
        public R DeleteEWIFile(string fileId)
        {
            string msg;
            if (eWIFileService.DeleteEWIFile(fileId, out msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }
        /// <summary>
        /// 远程共享文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                // net use \\192.168.30.240\public "123456" /user:admin
                string dosLine = @"net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
    }
}
