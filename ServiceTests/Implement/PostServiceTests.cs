using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewCommunity;
using Service.Interface;
using ServiceTests;

namespace Service.Implement.Tests
{
    [TestClass()]
    public class PostServiceTests
    {
        private readonly DependencyResolverHelpercs _serviceProvider;
        private readonly IPostService postService;
        public PostServiceTests()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelpercs(webHost);
            postService= _serviceProvider.GetService<IPostService>();
        }
        [TestMethod()]
        public async System.Threading.Tasks.Task GetRandomPostTestAsync()
        {
            var postViewModelList = await postService.GetRandomPost();
            Assert.IsNotNull(postViewModelList);
        }
    }
}