using repos11.Web.Models;
using Serilog;
using System.Web.Mvc;

namespace repos11.Web.Controllers
{
    public class HandleExceptions : Controller
    {
        public ExceptionContext OnExceptionHandle(ExceptionContext filterContext, ILogger logger)
        {
            filterContext.ExceptionHandled = true;
            var exception = filterContext.Exception;

            logger.Error($"Controller OnException", exception);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = Json(new ResponseLoadResult
                {
                    data = default,
                    exception = exception,
                    isError = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                filterContext.Controller.ViewData["exception"] = exception;
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/InternalServerError.cshtml",
                    ViewData = filterContext.Controller.ViewData
                };
            }

            return filterContext;
        }
    }
}