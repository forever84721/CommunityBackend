using Service.Implement;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewCommunity;
using Service.Interface;
using ServiceTests;
using System.Threading.Tasks;

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
            postService = _serviceProvider.GetService<IPostService>();
        }
        [TestMethod()]
        public async Task GetRandomPostTestAsync()
        {
            var postViewModelList = await postService.GetRandomPost(2);
            Assert.IsNotNull(postViewModelList);
        }

        [TestMethod()]
        public async Task LikePostTestAsync()
        {
            var likePostResult = await postService.LikePost(2, 9, 1);
            Assert.IsNotNull(likePostResult);
            Assert.IsTrue(likePostResult.Success);
            Assert.AreEqual(1, likePostResult.Data);
        }
    }
}