using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.ResponseModels;
using Models.ViewModels;
using Service.Interface;

namespace NewCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostService postService;
        public PostController(IOptions<ApplicationSettings> appSettings, IPostService postService) : base(appSettings)
        {
            this.postService = postService;
        }
        [HttpGet("[action]")]
        public BaseResponse<object> Test()
        {
            return new BaseResponse<object>(true, "Test", "123");
        }
        [HttpGet("[action]")]
        public async Task<BaseResponse<List<PostViewModel>>> GetRandomPost()
        {
            var data = await postService.GetRandomPost();
            BaseResponse<List<PostViewModel>> baseResponse = new BaseResponse<List<PostViewModel>>
            {
                Data = data,
                Msg = "",
                Success = true
            };
            return baseResponse;
        }
    }
}