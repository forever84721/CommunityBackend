using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Common;

namespace Community.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ApplicationSettings AppSettings;
        public BaseController(IOptions<ApplicationSettings> appSettings)
        {
            this.AppSettings = appSettings.Value;
        }
    }
}