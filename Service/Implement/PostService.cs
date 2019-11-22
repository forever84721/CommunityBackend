using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.DbModels;
using Models.ResponseModels;
using Models.ViewModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Implement
{
    public class PostService : BaseService, IPostService
    {
        //private static Semaphore replySemaphore = new Semaphore(1, 1);
        public PostService(CommunityContext dbContext, IOptions<ApplicationSettings> appSettings) : base(dbContext, appSettings)
        {

        }

        public async Task<List<PostViewModel>> GetRandomPost(int UserId)
        {
            using var con = new SqlConnection(AppSettings.IdentityConnection);
            var result = await con.QueryAsync<PostViewModel>(@"
select Post.PostId,
       Post.IssuerId,
       [User].Name,
       Post.PostTime,
       Post.InnerText as Content,
       (select count(1) from [Like] where Post.PostId=[Like].PostId and [Like].UserId=2)+ROUND(RAND(CHECKSUM(NEWID()))*2000,0) as NumOfLike,
       ROUND(RAND(CHECKSUM(NEWID()))*200,0) as NumOfComment,
       ROUND(RAND(CHECKSUM(NEWID()))*100,0) as NumOfShare,
	   isnull((select top 1 LikeType from [Like] where Post.PostId=[Like].PostId and [Like].UserId=@UserId),0) as LikeType
from Post 
left join [User] on Post.IssuerId=[User].UserId
left join [Like] on Post.PostId=[Like].PostId and [Like].UserId=@UserId
where Post.IssuerId in (select FollowUserId from Follow where UserId=@UserId)
order by RAND(CHECKSUM(NEWID()))", new { UserId });
            return result.AsList();
        }

        public async Task<LikePostResult> LikePost(int UserId, long PostId, int LikeType)
        {
            CheckDbContext();
            var result = new LikePostResult();
            try
            {
                //using TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted });
                var likeInstance = await DbContext.Like.SingleOrDefaultAsync(a => a.PostId == PostId && a.UserId == UserId);

                var num = DbContext.Like.Where(a => a.PostId == PostId).Count();
                if (likeInstance == null)
                {
                    Like like = new Like()
                    {
                        UserId = UserId,
                        PostId = PostId,
                        LikeTime = DateTime.Now,
                        LikeType = LikeType
                    };
                    await DbContext.Like.AddAsync(like);
                    num++;
                }
                else
                {
                    if (LikeType == 0)
                    {
                        DbContext.Like.Remove(likeInstance);
                        num--;
                    }
                    else
                    {
                        likeInstance.LikeType = LikeType;
                    }
                }
                result.Data = new LikePostResponseModel(LikeType, num);
                await DbContext.SaveChangesAsync();
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponse<List<ReplyViewModel>>> GetReply(long PostId, int Page)
        {
            try
            {
                using var con = new SqlConnection(AppSettings.IdentityConnection);
                var result = (await con.QueryAsync<ReplyViewModel>(@"
select PostReplyId,IssuerId,[Name],PostTime,InnerText as Content 
from PostReply 
left join [User] on IssuerId=UserId
where ReplyType=1 and TargetId = @PostId
order by PostTime desc
OFFSET (@Page-1)*20 ROWS
FETCH NEXT 20 ROWS ONLY", new { PostId, Page })).ToList();
                return new BaseResponse<List<ReplyViewModel>>(true, "", result);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<ReplyViewModel>>(false, ex.Message, null);
            }
        }
    }
}
