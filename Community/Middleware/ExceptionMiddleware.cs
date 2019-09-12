using Microsoft.AspNetCore.Http;
using Models.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Community.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await context.Response
                    .WriteAsync(JsonConvert.SerializeObject(new BaseResponse<object>(false, ex.ToString(), null)));
            }
        }
    }
}
