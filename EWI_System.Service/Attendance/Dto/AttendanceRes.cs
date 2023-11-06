using EWI_System.Model.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public class AttendanceRes
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double Days { get; set; }

        public List<Data> Datalist = new List<Data>();

        public int sort = 0;

    }
    public class Data 
    {
        public string AttendanceDate { get; set; }
        public string Type { get; set; }
    }
}

