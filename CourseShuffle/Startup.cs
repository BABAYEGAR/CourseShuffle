using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseShuffle.Startup))]
namespace CourseShuffle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
