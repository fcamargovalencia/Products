using Microsoft.ApplicationInsights.Extensibility;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Products.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DisableApplicationInsightsOnDebug();
        }

        private static void DisableApplicationInsightsOnDebug()
        {
            bool disable;
            string disableAiTelemetry = ConfigurationManager.AppSettings["DisableAITelemetry"];
            bool.TryParse(disableAiTelemetry, out disable);
            TelemetryConfiguration.Active.DisableTelemetry = disable;
        }
    }
}
