using Newtonsoft.Json;
using repos11.BusinessLogic.Dtos.Systems;
using repos11.BusinessLogic.Systems.Interfaces;
using repos11.BusinessLogic.UserManagement.Interfaces;
using repos11.Web.Models;
using repos11.Web.Utilities;
using Serilog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace repos11.Web.Controllers
{
    public class HomeController : BaseAppController
    {
        IUsersBusinessLogic _userBL;
        ISystemActionsBusinessLogic _actionsBL;
        ILogger _logger;

        public HomeController(IUsersBusinessLogic userBL, ISystemActionsBusinessLogic actionsBL, ILogger logger) : base(logger)
        {
            _userBL = userBL;
            _actionsBL = actionsBL;
        }

        [Description("View page home")]
        public ActionResult Index()
        {
            return View();
        }

        [ApplicationAuthorize(Roles = "Administrator")]
        public async Task<ActionResult> RegisterActions()
        {
            return await ResponeJson(async () =>
            {
                var items = new List<SystemActionsDto>();
                var assembly = typeof(BaseAppController).Assembly;
                var controllers = assembly.GetTypes()
                 .Where(t => t.IsSubclassOf(typeof(BaseAppController)))
                 .ToList();

                foreach (var ctrl in controllers)
                {
                    var actions = ctrl.GetMethods().Where(w => w.IsPublic && w.Module.ToString().Equals(assembly.ManifestModule.Name)).ToList();
                    foreach (var act in actions)
                    {
                        if (act.Name != "RegisterActions")
                        {
                            var descriptionAttr = act.GetCustomAttribute<DescriptionAttribute>();
                            items.Add(new SystemActionsDto { Controller = ctrl.Name.Replace("Controller", ""), Action = act.Name, Description = (descriptionAttr != null) ? descriptionAttr.Description : null });
                        }
                    }
                }

                await _actionsBL.SaveBatch(items, IdenityInfo.UserId);
                return "All actions registered.";
            }, "Register actions");
        }
    }
}