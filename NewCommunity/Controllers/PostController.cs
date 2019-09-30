using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.RequestModels;
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
        [Authorize]
        [HttpGet("[action]")]
        public async Task<BaseResponse<List<PostViewModel>>> GetRandomPost()
        {
            var UserId = int.Parse(User.Claims.Where(c => c.Type.Equals("UserId")).FirstOrDefault().Value);
            var data = await postService.GetRandomPost(UserId);
            BaseResponse<List<PostViewModel>> baseResponse = new BaseResponse<List<PostViewModel>>
            {
                Data = data,
                Msg = "",
                Success = true
            };
            return baseResponse;
        }
        [HttpPost("[action]")]
        public async Task<LikePostResult> LikePost(LikePostRequestModel model)
        {
            var UserId = int.Parse(User.Claims.Where(c => c.Type.Equals("UserId")).FirstOrDefault().Value);
            return await postService.LikePost(UserId, model.PostId, model.LikeType);
        }
        
    }
}