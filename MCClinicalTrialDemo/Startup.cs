using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MCClinicalTrialDemo.Startup))]
namespace MCClinicalTrialDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
