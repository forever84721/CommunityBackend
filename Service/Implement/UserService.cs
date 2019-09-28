using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.DbModels;
using Service.Common;
using Service.Interface;
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
            if (DbContext.IsDead)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CommunityContext>();
                optionsBuilder.UseSqlServer(AppSettings.IdentityConnection);
                DbContext = new CommunityContext(optionsBuilder.Options);
            }
            var encryption = Utility.PasswordEncoding(Password);
            return await DbContext.User.Where(u => u.Email.Equals(Email) && u.Password.Equals(encryption)).FirstOrDefaultAsync();
        }
    }
}
