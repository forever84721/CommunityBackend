using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResponseModels
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Success = false;
            Msg = null;
            Data = default;
        }
        public BaseResponse(bool success, string msg, T data)
        {
            Success = success;
            Msg = msg;
            Data = data;
        }
        public bool Success { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
}
