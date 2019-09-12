using Models.DbModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPostService
    {
        Task<List<PostViewModel>> GetRandomPost();
    }
}
