﻿using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{
    /// <summary>
    /// 口层
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class LineController : ControllerBase
    {
        #region 注入服务层
        /// <summary>
        /// 服务层
        /// </summary>
        public ILineService lineService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lineService"></param>
        public LineController(ILineService lineService)
        {
            this.lineService = lineService;
        }
        #endregion

        /// <summary>
        /// 增加角色
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        [HttpPost]
        public R CreateLine(Line line)
        {
            if (string.IsNullOrEmpty(line.LineName))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (lineService.CreateLine(line))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }

        /// <summary>
        ///获取在库的所有数据
        ///pageNum: 1,
        ///pageSize: 10,
        ///keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R FetchList(int pageNum, int pageSize, string keyword)
        {
            int total = 0;
            var Linelist = lineService.FetchList(pageNum, pageSize, keyword, ref total);
            return R.OK(Linelist).data("total", total);
        }
        /// <summary>
        /// all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R FetchAllList()
        {
            return R.OK(lineService.FetchAllList());
        }
        /// <summary>
        /// 根据分类获取线体信息
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        public R getLineListByCategory(string category)
        {
            return R.OK(lineService.getLineListByCategory(category));
        }
        /// <summary>
        /// updateStatus
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        [HttpPost]
        public R UpdateStatus(Line line) 
        {
            if (lineService.UpdateStatus(line))
            {
                return R.OK();
            }
            return R.Error("修改状态失败，请联系管理员");
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        [HttpPost]
        public R UpdateLine(Line line)
        {
            if (string.IsNullOrEmpty(line.LineName))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (lineService.UpdateLine(line))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        [HttpGet]
        public R DeleteLine(string lineId)
        {
            string msg;
            if (lineService.DeleteLine(lineId, out msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }

    }
}
