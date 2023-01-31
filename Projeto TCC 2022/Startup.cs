using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projeto_TCC_2022.Startup))]
namespace Projeto_TCC_2022
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}