using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface ILineService
    {
        bool CreateLine(Line line);
        List<LineRes> FetchList(int pageNum, int pageSize, string keyword, ref int total);
        bool UpdateStatus(Line role);
        bool UpdateLine(Line role);
        bool DeleteLine(string lineId, out string msg);
        List<Line> FetchAllList();
    }
}
