using Microsoft.Extensions.Options;
using Models.Common;
using Models.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implement
{
    public class BaseService
    {
        protected CommunityContext DbContext;
        protected readonly ApplicationSettings AppSettings;
        public BaseService(CommunityContext dbContext, IOptions<ApplicationSettings> appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings.Value;
        }
    }
}
