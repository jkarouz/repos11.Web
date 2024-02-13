using repos11.BusinessLogic.Systems.Interfaces;
using repos11.Web.Utilities;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace repos11.Web.Models
{
    public class AppInfo : IAppInfo
    {
        public AppInfo()
        {
            var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(MvcApplication).Assembly.Location);
            var titleAttr = typeof(MvcApplication).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

            ProductName = versionInfo.ProductName;
            ProductVersion = versionInfo.ProductVersion;
            CompanyName = versionInfo.CompanyName;
            LegalCopyright = versionInfo.LegalCopyright;
            LegalTrademarks = versionInfo.LegalTrademarks;
            Comments = versionInfo.Comments;
            Title = "ASP .NET Framework";
            LoginName = "Anonymous";
            IsActionDefinedAuthorize = false;
            IsActionDefinedAnonymouse = false;
            IsActionRegistered = false;

            try
            {
                if (titleAttr.Any())
                {
                    Title = (titleAttr.FirstOrDefault() as AssemblyTitleAttribute).Title;
                }
            }
            catch (Exception) { }
        }

        public string Title { get; private set; }
        public string ProductName { get; private set; }
        public string ProductVersion { get; private set; }
        public string CompanyName { get; private set; }
        public string FileVersion { get; private set; }
        public string LegalCopyright { get; private set; }
        public string LegalTrademarks { get; private set; }
        public bool IsDevelopment
        {
            get
            {
                try
                {
                    string IsDev = ConfigurationManager.AppSettings["IsDev"];

                    return Convert.ToBoolean(IsDev);
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }
        public string Comments { get; private set; }
        public string LoginName { get; set; }
        public bool IsActionDefinedAuthorize { get; set; } = false;
        public bool IsActionDefinedAnonymouse { get; set; } = false;
        public bool IsActionRegistered { get; set; } = false;

        public void BuildViewData(ActionExecutingContext filterContext)
        {
            var rd = filterContext.RequestContext.RouteData;
            var controller = rd.GetRequiredString("controller");
            var action = rd.GetRequiredString("action");

            IsActionDefinedAuthorize = filterContext.ActionDescriptor.IsDefined(typeof(ApplicationAuthorize), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(ApplicationAuthorize), inherit: true);

            IsActionDefinedAnonymouse = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (action != "action")
            {
                var _actionsBL = DependencyResolver.Current.GetService<ISystemActionsBusinessLogic>();
                var dataAct = _actionsBL.Get(controller, action);
                IsActionRegistered = (dataAct != null);
            }

            LoginName = filterContext.HttpContext.User.Identity.IsAuthenticated ? IdenityInfo.FullName : "Anonymous";

            filterContext.Controller.ViewData["AppInfo"] = this;
        }

        public void BuildViewData(AuthorizationContext filterContext)
        {
            var rd = filterContext.RequestContext.RouteData;
            var controller = rd.GetRequiredString("controller");
            var action = rd.GetRequiredString("action");

            IsActionDefinedAuthorize = filterContext.ActionDescriptor.IsDefined(typeof(ApplicationAuthorize), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(ApplicationAuthorize), inherit: true);

            IsActionDefinedAnonymouse = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (action != "action")
            {
                var _actionsBL = DependencyResolver.Current.GetService<ISystemActionsBusinessLogic>();
                var dataAct = _actionsBL.Get(controller, action);
                IsActionRegistered = (dataAct != null);
            }

            LoginName = filterContext.HttpContext.User.Identity.IsAuthenticated ? IdenityInfo.FullName : "Anonymous";

            filterContext.Controller.ViewData["AppInfo"] = this;
        }
    }

    public interface IAppInfo
    {
        string ProductName { get; }
        string ProductVersion { get; }
        string CompanyName { get; }
        string FileVersion { get; }
        string LegalCopyright { get; }
        string LegalTrademarks { get; }
        bool IsDevelopment { get; }
        string Comments { get; }
        string LoginName { get; set; }
        bool IsActionDefinedAuthorize { get; set; }
        bool IsActionDefinedAnonymouse { get; set; }
        bool IsActionRegistered { get; set; }

        void BuildViewData(ActionExecutingContext filterContext);
        void BuildViewData(AuthorizationContext filterContext);
    }
}