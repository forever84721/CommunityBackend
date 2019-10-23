using Microsoft.AspNetCore.Http;
using Models.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCommunity.Middleware
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
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await context.Response
                    .WriteAsync(JsonConvert.SerializeObject(new BaseResponse<object>(false, ex.ToString(), null))).ConfigureAwait(false);
            }
        }
    }
}
