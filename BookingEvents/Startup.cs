using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookingEvents.Startup))]
namespace BookingEvents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
