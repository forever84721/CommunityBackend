using Community.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Service
{
    public interface IPostService
    {
        Task<PostViewModel> GetAllPost();
    }
}
