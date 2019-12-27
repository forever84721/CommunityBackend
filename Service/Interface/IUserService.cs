using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterface
{
    public interface IUserService
    {
        Task<User> Login(string Email,string Password);
    }
}
