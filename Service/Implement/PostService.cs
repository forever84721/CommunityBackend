using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.DbModels;
using Models.ViewModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class PostService : BaseService, IPostService
    {
        public PostService(CommunityContext dbContext, IOptions<ApplicationSettings> appSettings) : base(dbContext, appSettings)
        {

        }

        public async Task<List<PostViewModel>> GetRandomPost()
        {
            using (var con = new SqlConnection(AppSettings.IdentityConnection))
            {
                var result = await con.QueryAsync<PostViewModel>(@"
select Post.PostId,
       Post.IssuerId,
       [User].Name,
       Post.PostTime,
       Post.InnerText as Content,
       ROUND(RAND(CHECKSUM(NEWID()))*100,0) as NumOfLike,
       ROUND(RAND(CHECKSUM(NEWID()))*100,0) as NumOfComment,
       ROUND(RAND(CHECKSUM(NEWID()))*100,0) as NumOfShare
from Post 
left join [User] on Post.IssuerId=[User].UserId");
                return result.AsList();
            }
        }
    }
}
