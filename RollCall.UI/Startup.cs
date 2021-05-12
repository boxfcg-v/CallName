using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RollCall.UI.Startup))]
namespace RollCall.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
