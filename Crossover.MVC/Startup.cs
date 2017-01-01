using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Crossover.MVC.Startup))]
namespace Crossover.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
