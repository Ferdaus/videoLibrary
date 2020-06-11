using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(videoLibrary.Startup))]
namespace videoLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
