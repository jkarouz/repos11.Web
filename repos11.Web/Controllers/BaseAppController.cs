using DevExtreme.AspNet.Data.ResponseModel;
using Newtonsoft.Json;
using repos11.Web.Models;
using repos11.Web.Utilities;
using Serilog;
using SerilogTimings;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace repos11.Web.Controllers
{
    [GlobalActionFilter]
    [ApplicationAuthorize]
    public class BaseAppController : Controller
    {
        ILogger _logger;

        public BaseAppController(ILogger logger)
        {
            _logger = logger;
        }

        protected async Task<JsonResult> ResponeJson<TResult>(Func<Task<TResult>> method, string logMsg = "")
        {
            try
            {
                using (var op = Operation.Begin(logMsg))
                {
                    var data = await method();
                    op.Complete();

                    _logger.Debug(logMsg + " value {@data}", data);

                    return Json(new ResponseResult<TResult>
                    {
                        data = data
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(logMsg, ex);
                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

                var result = JsonConvert.SerializeObject(new ResponseResult<TResult>
                {
                    data = default,
                    exception = ex,
                    isError = true
                }, jss);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        protected async Task<JsonResult> ResponeJson(Func<Task<LoadResult>> method, string logMsg = "")
        {
            try
            {
                using (var op = Operation.Begin(logMsg))
                {
                    var result = await method();
                    op.Complete();

                    _logger.Debug(logMsg + " value {@data}", result.data);

                    return Json(new ResponseLoadResult
                    {
                        data = result.data,
                        totalCount = result.totalCount,
                        groupCount = result.groupCount,
                        summary = result.summary
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(logMsg, ex);
                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

                var result = JsonConvert.SerializeObject(new ResponseLoadResult
                {
                    data = default,
                    exception = ex,
                    isError = true
                }, jss);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            HandleExceptions handleExceptions = new HandleExceptions();

            handleExceptions.OnExceptionHandle(filterContext, _logger);
        }
    }
}