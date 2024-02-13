using repos11.Web.Models;
using System.Web.Mvc;

namespace repos11.Web.Utilities
{
    public class GlobalActionFilter : ActionFilterAttribute
    {
        public IAppInfo AppInfo { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            AppInfo.BuildViewData(filterContext);
        }
    }
}