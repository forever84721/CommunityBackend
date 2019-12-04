using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ResponseModels
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = false;
            Msg = null;
        }
        public BaseResponse(bool success, string msg)
        {
            Success = success;
            Msg = msg;
        }
        public bool Success { get; set; }
        public string Msg { get; set; }
    }
    public class BaseResponse<T>: BaseResponse
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
        public T Data { get; set; }
    }
}
