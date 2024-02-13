using repos11.Web.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace repos11.Web.Utilities
{
    public class ApplicationAuthorize : AuthorizeAttribute
    {
        public IAppInfo AppInfo { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            AppInfo.BuildViewData(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
                filterContext.Controller.ViewData["AppInfo"] = AppInfo;
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new ResponseLoadResult
                        {
                            data = default,
                            exception = new Exception("Unauthorized"),
                            isError = true,
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    AppInfo.BuildViewData(filterContext);

                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Account/Unauthorized.cshtml",
                        ViewData = filterContext.Controller.ViewData
                    };
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorizeResult = base.AuthorizeCore(httpContext);

            if (httpContext.User.Identity.IsAuthenticated)
            {
                var rd = httpContext.Request.RequestContext.RouteData;
                var ctrl = rd.GetRequiredString("controller");
                var act = rd.GetRequiredString("action");

                if (act != "RegisterActions")
                {
                    var permissionActions = IdenityInfo.PermissionActions.Where(w => w.Controller == ctrl && w.Action == act).FirstOrDefault();

                    return authorizeResult && (permissionActions != null);
                }
            }

            return authorizeResult;
        }
    }
}