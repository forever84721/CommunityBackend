using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Common;

namespace NewCommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal readonly ApplicationSettings AppSettings;
        public BaseController(IOptions<ApplicationSettings> appSettings)
        {
            this.AppSettings = appSettings?.Value;
        }
    }
}