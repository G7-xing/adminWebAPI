using EWI_System.Model;
using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IEWIFileService
    {
        bool CreateEWIFile(EWIFileInfo file);
        List<EWIFileInfo> FetchList(int pageNum, int pageSize, string keyword, ref int total);
        //bool UpdateStatus(Line role);
        bool UpdateEWIFile(EWIFileInfo file);
        List<EWIFileInfoRes> GetFileListByLineId(string lineId);
        string GetFileURL(string fileId);
        public bool DeleteEWIFile(string fileId, out string msg);
        //List<Line> FetchAllList();
    }
}
