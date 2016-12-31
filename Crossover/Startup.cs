using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Crossover.Startup))]
namespace Crossover
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
