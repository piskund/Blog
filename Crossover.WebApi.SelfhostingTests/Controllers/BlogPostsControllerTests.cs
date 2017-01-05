using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using Crossover.Common.POCO;
using Crossover.Core.Abstractions;
using Crossover.WebApi.Selfhosting.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Crossover.WebApi.SelfhostingTests.Controllers
{
    [TestClass]
    public class BlogPostsControllerTests
    {
        [TestMethod]
        public void Get_ReturnsAllPosts()
        {
            // Arrange
            var repositoryMock = new Mock<IPostRepository>();
            var controller = new BlogPostsController(repositoryMock.Object);

            // Act
            controller.Get();

            // Assert
            repositoryMock.Verify(r => r.GetAll());
        }

        [TestMethod]
        public void GetById_ReturnsBlogPost()
        {
            // Arrange
            var repositoryMock = new Mock<IPostRepository>();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var id = fixture.Freeze<Guid>();
            var controller = new BlogPostsController(repositoryMock.Object);

            // Act
            controller.Get(id);

            // Assert
            repositoryMock.Verify(r => r.Get(id));
        }

        [TestMethod]
        public void Post_CreatesPost()
        {
            // Arrange
            var repositoryMock = new Mock<IPostRepository>();
            var controller = new BlogPostsController(repositoryMock.Object);
            SetupControllerForTests(controller);
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var post = fixture.Freeze<Post>();

            // Act
            var response = controller.Post(post);

            // Assert
            repositoryMock.Verify(r => r.Create(post));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
        }

        [TestMethod]
        public void Put_UpdatesPost()
        {
            // Arrange
            var repositoryMock = new Mock<IPostRepository>();
            var controller = new BlogPostsController(repositoryMock.Object);
            SetupControllerForTests(controller);
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var post = fixture.Freeze<Post>();

            // Act
            var response = controller.Put(post.Id, post);

            // Assert
            repositoryMock.Verify(r => r.Update(post));
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            Assert.IsNotNull(response.Headers.Location);
        }

        [TestMethod]
        public void Delete_DeletesPost()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var id = fixture.Create<Guid>();
            var repositoryMock = fixture.Create<Mock<IPostRepository>>();
            var requestMock = fixture.Create<Mock<HttpRequestMessage>>();

            var controller = new BlogPostsController(repositoryMock.Object)
            {
                Request = requestMock.Object
            };

            // Act
            var response = controller.Delete(id);

            // Assert
            repositoryMock.Verify(r => r.Delete(id));
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        private static void SetupControllerForTests(ApiController controller)
        {
            var config = new HttpConfiguration();
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "blogposts" } });
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/products");
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Url = new UrlHelper(controller.Request);
        }
    }
}