using System;
using System.Collections.Generic;
using System.Text;

using EWI_System.Model.Enties;

namespace EWI_System.Service
{
    public class MPSKanbanRes : TbMpsPickorders
    {
        public int ExpiredMinutes {get;set;}
    }
}
