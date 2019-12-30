using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.DbModels;
using Service.Common;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class UserService : BaseService, IUserService
    {
        public UserService(CommunityContext dbContext, IOptions<ApplicationSettings> appSettings) : base(dbContext, appSettings)
        {

        }

        public Task<User> Login(string Email, string Password)
        {
            CheckDbContext();
            var encryption = Utility.PasswordEncoding(Password);
            //var a = DbContext.User.Single(u => u.Email == Email);
            return DbContext.User.SingleOrDefaultAsync(u => u.Email == Email && u.Password == encryption);
            //return await DbContext.User.Where(u => u.Email.Equals(Email, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(encryption, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
