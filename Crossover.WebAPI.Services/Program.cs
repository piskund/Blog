using System;
using System.Web.Http;

namespace Crossover.WebAPI.Services
{
    public class Program
    {
        private static void Main()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:3000");
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new {id = RouteParameter.Optional});
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Console.ReadLine();
        }
    }
}