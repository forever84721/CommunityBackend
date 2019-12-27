using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.DbModels;
using Models.ResponseModels;
using Service.ServiceInterface;

namespace NewCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService userService;

        public AuthController(IOptions<ApplicationSettings> appSettings, IUserService userService) : base(appSettings)
        {
            this.userService = userService;
        }
        [HttpPost("[action]")]
        public async Task<BaseResponse<string>> Login(User LoginInfo)
        {
            BaseResponse<string> response = new BaseResponse<string>
            {
                Data = "",
                Msg = "",
                Success = true
            };
#pragma warning disable CA1062 // 必須驗證公用方法的引數
            var user = await userService.Login(LoginInfo.Email, LoginInfo.Password).ConfigureAwait(false);
#pragma warning restore CA1062 // 必須驗證公用方法的引數
            if (user == null)
            {
                response.Success = false;
                response.Msg = "Not found user";
                return response;
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JWT_Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Claims = new Dictionary<string, object> { { "UserId", user.UserId.ToString() }, { "Email", user.Email }, { "Name", user.Name } },
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",user.UserId.ToString(CultureInfo.CurrentCulture)),
                    new Claim("Email",user.Email),
                    new Claim("Name",user.Name),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            response.Data = token;
            return response;
        }
    }
}