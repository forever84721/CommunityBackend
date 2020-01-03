using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.RequestModels;
using Models.ResponseModels;
using Models.ViewModels;
using NewCommunity.Common;
using NewCommunity.Hubs;
using Service.ServiceInterface;

namespace NewCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly IHubContext<ChatHub, IChatHub> chatHubContext;
        public PostController(IOptions<ApplicationSettings> appSettings, IPostService postService, IHubContext<ChatHub, IChatHub> chatHubContext) : base(appSettings)
        {
            this.postService = postService;
            this.chatHubContext = chatHubContext;
        }
        [HttpGet("[action]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<暫止>")]
        public BaseResponse<object> Test()
        {
            return new BaseResponse<object>(true, "Test", "1234");
        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet("[action]/{Page}")]
        public async Task<BaseResponse<List<PostViewModel>>> GetRandomPost(int Page)
        {
            try
            {
                //var chathub = GlobalHost.ConnectionManager.GetHubContext<Hub<IChatHub>>("ChatHub") as IHubContext<IChatHub>;
                await chatHubContext.Clients.All.ServerMessage(new MessageInfo() { IssuerId = 0, Name = "Test", PostTime = Utils.GetDateTimeWithoutMillisecond(), Content = "RandomPost" }).ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
            //var options = new JsonSerializerOptions
            //{
            //    AllowTrailingCommas = true
            //};
            //JsonSerializer.Deserialize<BaseResponse<int>>("", options);
            //var asd= JsonSerializer.Parse<BaseResponse<int>>("", options);
            try
            {
                var UserId = int.Parse(User.Claims.Where(c => c.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value, CultureInfo.CurrentCulture);
                var data = await postService.GetRandomPost(UserId, Page).ConfigureAwait(false);
                BaseResponse<List<PostViewModel>> baseResponse = new BaseResponse<List<PostViewModel>>
                {
                    Data = data,
                    Msg = "",
                    Success = true
                };
                return baseResponse;
            }
            catch (Exception ex)
            {
                BaseResponse<List<PostViewModel>> baseResponse = new BaseResponse<List<PostViewModel>>
                {
                    Data = new List<PostViewModel>(),
                    Msg = "",
                    Success = false
                };
                return baseResponse;
            }
        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("[action]")]
        public async Task<LikePostResult> LikePost(LikePostRequestModel model)
        {
            var UserId = int.Parse(User.Claims.Where(c => c.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value, CultureInfo.CurrentCulture);
            if (model == null)
            {
                return new LikePostResult(false, "model null", new LikePostResponseModel(0, 0));
            }
            return await postService.LikePost(UserId, model.PostId, model.LikeType).ConfigureAwait(false);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("[action]")]
        public async Task<BaseResponse<List<ReplyViewModel>>> GetReply(GetReplyRequestModel model)
        {
            if (model == null)
            {
                return new BaseResponse<List<ReplyViewModel>>(false, "model null", null);
            }
            var response = await postService.GetReply(model.PostId, model.Page).ConfigureAwait(false);
            return response;
        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("[action]")]
        public async Task<BaseResponse> Reply(ReplyRequestModel model)
        {
            if (model == null)
            {
                return new BaseResponse(false, "model null");
            }
            var UserId = int.Parse(User.Claims.Where(c => c.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value, CultureInfo.CurrentCulture);
            var response = await postService.Reply(UserId, model.ReplyType, model.TargetId, model.Content).ConfigureAwait(false);
            return response;
        }
    }
}