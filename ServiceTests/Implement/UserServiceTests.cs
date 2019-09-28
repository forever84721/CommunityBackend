using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Common;
using NewCommunity;
using Service.Implement;
using Service.Interface;
using ServiceTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implement.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private DependencyResolverHelpercs _serviceProvider;
        public UserServiceTests()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelpercs(webHost);
        }

        [TestMethod()]
        public async System.Threading.Tasks.Task LoginTestAsync()
        {
            var userService = _serviceProvider.GetService<IUserService>();

            var asd = await userService.Login("Jay","asdf");
            Assert.IsNotNull(asd);
        }
    }
}