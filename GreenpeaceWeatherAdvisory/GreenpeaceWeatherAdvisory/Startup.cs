using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreenpeaceWeatherAdvisory.Startup))]
namespace GreenpeaceWeatherAdvisory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
