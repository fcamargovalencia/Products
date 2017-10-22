using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Products.Backend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Models.DataContextLocal, Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
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
