using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(nocf_entries.Startup))]
namespace nocf_entries
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
