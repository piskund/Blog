using System.Web.Http;
using System.Web.Http.Dispatcher;
using Crossover.Core.Abstractions;
using Crossover.WebApi.Selfhosting.DI;
using Microsoft.Practices.Unity;
using Owin;

namespace Crossover.WebApi.Selfhosting
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IPostRepository, FakePostRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(unityContainer);
            config.Services.Replace(typeof(IHttpControllerActivator), new UnityControllerActivator(unityContainer, GlobalConfiguration.Configuration.Services.GetHttpControllerActivator()));
            appBuilder.UseWebApi(config);
        }
    }
}