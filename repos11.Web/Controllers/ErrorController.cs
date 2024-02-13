using repos11.Web.Models;
using repos11.Web.Utilities;
using Serilog;
using System;
using System.Web.Mvc;

namespace repos11.Web.Controllers
{
    [GlobalActionFilter]
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        ILogger _logger;

        public ErrorController(ILogger logger)
        {
            _logger = logger;
        }

        public ActionResult NotFound(string page_not_found_url = null)
        {
            ViewBag.PageNotFoundUrl = page_not_found_url;

            if (Request.IsAjaxRequest())
            {
                _logger.Error("Request ajax not found");
                return Json(new ResponseLoadResult
                {
                    data = default,
                    exception = new Exception("404 Not Found"),
                    isError = true
                }, JsonRequestBehavior.AllowGet);
            }

            _logger.Error("Page not found");
            return View();
        }

        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}