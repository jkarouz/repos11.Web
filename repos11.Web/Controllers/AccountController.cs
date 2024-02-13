using Microsoft.Owin.Security;
using repos11.BusinessLogic.UserManagement.Interfaces;
using repos11.Web.Models;
using repos11.Web.Utilities;
using repos11.Web.Utilities.Security;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace repos11.Web.Controllers
{
    [GlobalActionFilter]
    public class AccountController : Controller
    {
        IUsersBusinessLogic _userBL;
        ILogger _logger;

        public AccountController(IUsersBusinessLogic userBL, ILogger logger)
        {
            _userBL = userBL;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (User.Identity.IsAuthenticated)
                return await Task.FromResult(RedirectToLocal(returnUrl));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string username, string returnUrl)
        {
            var authService = new BasicAuthentication(_userBL, _logger);

            var authenticationResult = await authService.SignIn(username);

            if (authenticationResult.IsSuccess)
                return await Task.FromResult(RedirectToLocal(returnUrl));

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(ConstansAuthentication.DeafultCookie);
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            if (Request.IsAjaxRequest())
            {
                _logger.Error("Request ajax Unauthorized");
                return Json(new ResponseLoadResult
                {
                    data = default,
                    exception = new Exception("401 Unauthorized"),
                    isError = true
                }, JsonRequestBehavior.AllowGet);
            }

            _logger.Error("Page Unauthorized");
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            HandleExceptions handleExceptions = new HandleExceptions();

            handleExceptions.OnExceptionHandle(filterContext, _logger);
        }
    }
}