using Service.Implement;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewCommunity;
using Service.ServiceInterface;
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
            var postViewModelList = await postService.GetRandomPost(2,1).ConfigureAwait(false);
            Assert.IsNotNull(postViewModelList);
        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(2)]
        public async Task LikePostTestAsync(int status)
        {
            var likePostResult = await postService.LikePost(2, 9, status).ConfigureAwait(false);
            Assert.IsNotNull(likePostResult);
            Assert.IsTrue(likePostResult.Success);
            Assert.AreEqual(status, likePostResult.Data);
        }
        [TestMethod()]
        [DataRow(3)]
        public async Task LikePostTestAsync2(int status)
        {
            var likePostResult = await postService.LikePost(2, 9, status).ConfigureAwait(false);
            Assert.IsNotNull(likePostResult);
            Assert.IsTrue(likePostResult.Success);
            Assert.AreEqual(status, likePostResult.Data);
        }
    }
}