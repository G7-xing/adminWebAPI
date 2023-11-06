using EWI_System.Model.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWI_System.Service
{
    public class LineService : ILineService
    {

        private readonly ISqlSugarClient dbconn;

        public LineService(ISqlSugarClient dbconn)
        {
            this.dbconn = dbconn;
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool CreateLine(Line line)
        {
            var ds = dbconn.Insertable(line).ExecuteCommand();
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
        public List<LineRes> FetchList(int pageNum, int pageSize, string keyword, ref int totalCount)
        {
            return dbconn.Queryable<Line>()
                                    .WhereIF(!String.IsNullOrEmpty(keyword), it => it.LineName.Contains(keyword))
                                    .LeftJoin<User>((L, U) => L.CreateBy == U.Id.ToString())
                                    .LeftJoin<User>((L, U, U1) => L.UpdateBy == U1.Id.ToString())
                                    .Select((L, U, U1) => new LineRes {
                                        LineId = L.LineId,
                                        LineName = L.LineName,
                                        LineCategory = L.LineCategory,
                                        Status = L.Status,
                                        CreateTime = L.CreateTime,
                                        CreateBy = U.UserName,
                                        UpdateTime = L.UpdateTime,
                                        UpdateBy = U1.UserName
                                    })
                                    .OrderBy(L=>L.LineCategory).OrderBy(L=>L.LineName)
                                    .ToPageList(pageNum, pageSize, ref totalCount);

        }
        /// <summary>
        /// 跟新状态
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        public bool UpdateStatus(Line line)
        {
            return dbconn.Updateable<Line>().SetColumns(r => new Line { Status = line.Status, UpdateBy = line.UpdateBy })
                                                      .Where(r => r.LineId == line.LineId)
                                                      .ExecuteCommandHasChange();
        }

        public bool UpdateLine(Line line)
        {
            return dbconn.Updateable<Line>()
                                                    .SetColumns(r =>
                                                        new Line
                                                        {
                                                            LineName = line.LineName,
                                                            Status = line.Status,
                                                            UpdateBy = line.UpdateBy
                                                        }
                                                       )
                                                      .Where(r => r.LineId == line.LineId)
                                                      .ExecuteCommandHasChange();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>

        public bool DeleteLine(string lineId, out string msg)
        {
            if (!dbconn.Deleteable<Line>().Where(it => it.LineId == lineId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            msg = "";
            return true;

        }

        public List<Line> FetchAllList()
        {
            return dbconn.Queryable<Line>().Select(t => new Line { LineId = t.LineId, LineName = t.LineName }).OrderBy(t=>t.LineName).ToList();
        }
    }
}
