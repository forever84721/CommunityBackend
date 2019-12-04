using Models.DbModels;
using Models.ResponseModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPostService
    {
        Task<List<PostViewModel>> GetRandomPost(int UserId);
        Task<LikePostResult> LikePost(int UserId, long PostId, int LikeType);
        Task<BaseResponse<List<ReplyViewModel>>> GetReply(long PostId,int Page);
        Task<BaseResponse<int>> Reply(int UserId, int ReplyType, long TargetId, string Content);
    }
}
