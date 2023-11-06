using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWI_System.Service
{
    public class EWIFileService : IEWIFileService
    {

        private readonly ISqlSugarClient dbconn;

        public EWIFileService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CreateEWIFile(EWIFileInfo file)
        {
            var ds = dbconn.Insertable(file).ExecuteCommand();
            if (ds == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<EWIFileInfo> FetchList(int pageNum, int pageSize, string keyword, ref int totalCount)
        {
            return dbconn.Queryable<EWIFileInfo>()
                                    .WhereIF(!String.IsNullOrEmpty(keyword), it => it.FileName.Contains(keyword))
                                    .LeftJoin<User>((F, U) => F.CreateBy == U.Id.ToString())
                                    .LeftJoin<User>((F, U, U1) => F.UpdateBy == U1.Id.ToString())
                                    .Select((F, U, U1) => new EWIFileInfo
                                    {
                                        FileId = F.FileId,
                                        FileURL = F.FileURL,
                                        Remark = F.Remark,
                                        LineId = F.LineId,
                                        FileName = F.FileName,
                                        Status = F.Status,
                                        CreateTime = F.CreateTime,
                                        CreateBy = U.UserName,
                                        UpdateTime = F.UpdateTime,
                                        UpdateBy = U1.UserName
                                    })
                                    .OrderBy(F => F.FileName)
                                    .ToPageList(pageNum, pageSize, ref totalCount);

        }
        ///// <summary>
        ///// 跟新状态
        ///// </summary>
        ///// <param name="Line"></param>
        ///// <returns></returns>
        //public bool UpdateStatus(Line line)
        //{
        //    return dbconn.Updateable<Line>().SetColumns(r => new Line { Status = line.Status, UpdateBy = line.UpdateBy })
        //                                              .Where(r => r.LineId == line.LineId)
        //                                              .ExecuteCommandHasChange();
        //}

        public bool UpdateEWIFile(EWIFileInfo file)
        {
            return dbconn.Updateable<EWIFileInfo>()
                                                    .SetColumns(r =>
                                                        new EWIFileInfo
                                                        {
                                                            FileURL = file.FileURL,
                                                            Remark = file.Remark,
                                                            LineId = file.LineId,
                                                            FileName = file.FileName,
                                                            Status = file.Status,
                                                            UpdateBy = file.UpdateBy
                                                        }
                                                       )
                                                      .Where(r => r.FileId == file.FileId)
                                                      .ExecuteCommandHasChange();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool DeleteEWIFile(string fileId, out string msg)
        {
            if (!dbconn.Deleteable<EWIFileInfo>().Where(it => it.FileId == fileId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            msg = "";
            return true;

        }

        public List<EWIFileInfoRes> GetFileListByLineId(string lineId)
        {
            return dbconn.Queryable<EWIFileInfo>().Where(t => t.LineId == lineId)
                                                  .Select(t => new EWIFileInfoRes 
                                                  { 
                                                      FileId = t.FileId, 
                                                      FileName = t.FileName
                                                  })
                                                  .OrderBy(t => t.FileName)
                                                  .ToList();
        }

        public string GetFileURL(string fileId)
        {
            var rlt = dbconn.Queryable<EWIFileInfo>().First(t => t.FileId == fileId);
            if (rlt!=null)
            {
                return rlt.FileURL.ToString();
            }
            return null;                                 
                                                  
        }
    }
}
