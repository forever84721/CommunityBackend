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

        public async Task<User> Login(string Email, string Password)
        {
            CheckDbContext();
            var encryption = Utility.PasswordEncoding(Password);
            return await DbContext.User.Where(u => u.Email.Equals(Email, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(encryption, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
