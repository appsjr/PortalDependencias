using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pendencias.Startup))]
namespace Pendencias
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
