using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SafaGames.Startup))]
namespace SafaGames
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
