using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LogWebApi.Model
{
    public class ResponseModel<T>
    {
        private HttpStatusCode _resultCode = HttpStatusCode.OK;
        private string _message = "请求成功";        
        private T _data = default(T);
        /// <summary>
        /// 返回码
        /// </summary>
        public HttpStatusCode ResultCode
        {
            get { return this._resultCode; }
            set { this._resultCode = value; }
        }
        /// <summary>
        /// 结果说明
        /// </summary>
        public string Message
        {
            get { return this._message; }
            set { this._message = value; }
        }        
        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Data
        {
            get { return this._data; }
            set { this._data = value; }
        }
    }
}
