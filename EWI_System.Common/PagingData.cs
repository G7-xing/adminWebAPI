using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EWI_System.Common
{
    public class PagingData<T> where T : class
    {
        public int RecordCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public List<T> DataList { get; set; }

        public string SearchString { get; set; }
    }
}
