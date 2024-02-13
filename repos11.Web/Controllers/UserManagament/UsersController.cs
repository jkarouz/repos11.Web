using repos11.BusinessLogic.Extension;
using repos11.BusinessLogic.UserManagement.Interfaces;
using Serilog;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace repos11.Web.Controllers.UserManagament
{
    [RoutePrefix("Users")]
    public class UsersController : BaseAppController
    {
        IUsersBusinessLogic _userBL;
        ILogger _logger;

        public UsersController(IUsersBusinessLogic userBL, ILogger logger) : base(logger)
        {
            _userBL = userBL;
            _logger = logger;
        }

        [Route]
        [Description("View data users")]
        public ActionResult Index()
        {
            return View("~/Views/UserManagament/Users/Index.cshtml");
        }

        [Route("Get")]
        [HttpGet]
        [Description("Get data users")]
        public async Task<ActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            return await ResponeJson(async () => await _userBL.GetAll(loadOptions), "get master users");
        }
    }
}