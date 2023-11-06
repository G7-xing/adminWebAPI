using DotLiquid.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Model
{

    public enum ResultCode { 
        SUCCESS= 20000,
        ERROR = 10000
    }
    public class R
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public Object Data { get; set; }
        public Dictionary<string,Object> DynamicData { get; set; }
        public string Msg { get; set; }

        public R()
        {
            this.DynamicData = new Dictionary<string, object>();
        }

        public static  R OK() {
            var r = new R();
            r.Success = true;
            r.Code = (int)ResultCode.SUCCESS;
            return r;
        }

        public static R OK(Object data)
        {
            var r = new R();
            r.Success = true;
            r.Code = (int)ResultCode.SUCCESS;
            r.Data = data;
            return r;
        }
        public static  R Error(string msg)
        {
            var r = new R();
            r.Code = (int)ResultCode.ERROR;
            r.Msg = msg;
            r.DynamicData = default;
            return r;
        }

 
        public R code(int code)
        {
            this.Code = Code;
            return this;
        }
        public R message(string message)
        {
            this.Msg = message;
            return this;
        }
        public R data(string key,Object value)
        {

            this.DynamicData.Add(key,value);
            return this;
        }
        public R data(Dictionary<string,Object> map)
        {
            this.DynamicData = map;
            return this;
        }

    }
}
