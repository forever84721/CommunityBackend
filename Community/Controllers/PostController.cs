using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("[action]")]
        public BaseResponse<object> Test()
        {
            return new BaseResponse<object>(true, "Test", null);
        }

    }
}