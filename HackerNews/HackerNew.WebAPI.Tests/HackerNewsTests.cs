using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using HackerNew.WebAPI.Controllers;
using HackerNews.Service;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
namespace HackerNew.WebAPI.Tests
{
    [TestClass]
    public class HackerNewsTests
    {
        [TestMethod]
        public void StoryController_BestStories_ReturnsResults()
        {
            JObject o = new JObject{
                {"Test1","T1" },
                { "Test2","T2"} 
            };
            var restServiceStub = new Mock<IHackerNewsRestService>();
            restServiceStub.Setup(r => r.getBestStories()).Returns(new JArray(o));

            StoryController controller = new StoryController(restServiceStub.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            System.Threading.CancellationToken token = new System.Threading.CancellationToken();
            var result = controller.getBestStories().ExecuteAsync(token).Result;
          
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
