using Microsoft.Owin;

[assembly: OwinStartupAttribute(typeof(Products.Backend.Startup))]
namespace Products.Backend
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
