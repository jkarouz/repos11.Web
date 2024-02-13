using Newtonsoft.Json;
using repos11.Web.App_Start;
using Serilog;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace repos11.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Environment.SetEnvironmentVariable("BASEDIRWEBAPP", AppDomain.CurrentDomain.BaseDirectory);
            Log.Logger = LoggerConfig.CreateLoger();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            Log.Error("Application_Error", ex);
        }

        protected void Application_EndRequest()
        {
            var request = HttpContext.Current.Request;
            if (!request.Path.Contains("Error/NotFound"))
            {
                if (Context.Response.StatusCode == 404)
                {
                    Log.Information("Application_EndRequest executed 404");
                    Response.Clear();
                    Response.Redirect($"~/Error/NotFound?page_not_found_url={request.Path}");
                }
            }

            if (!request.Path.Contains("Error/InternalServerError"))
            {
                if (Context.Response.StatusCode == 500 || Context.Response.StatusCode == 403)
                {
                    Log.Information($"Application_EndRequest executed {Context.Response.StatusCode}");
                    Response.Clear();
                    Response.Redirect("~/Error/InternalServerError");
                }
            }
        }
    }
}
