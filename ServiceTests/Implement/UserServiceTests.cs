using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewCommunity;
using Service.ServiceInterface;
using ServiceTests;

namespace Service.Implement.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private readonly DependencyResolverHelpercs _serviceProvider;
        private readonly IUserService userService;
        
        public UserServiceTests()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelpercs(webHost);

            userService = _serviceProvider.GetService<IUserService>();
        }

        [TestMethod()]
        public async System.Threading.Tasks.Task LoginTestAsync()
        {
            var user = await userService.Login("Jay","asdf").ConfigureAwait(false);
            Assert.IsNotNull(user);
        }
    }
}