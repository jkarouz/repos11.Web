using Serilog;
using Serilog.Core;
using SerilogWeb.Classic;
using System;
using System.Diagnostics;
using System.Web;

namespace repos11.Web.App_Start
{
    public class LoggerConfig
    {
        public static Logger CreateLoger()
        {
            var logConfig = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.WithProcessId()
                    .Enrich.WithProcessName()
                    .Destructure.ByTransforming<HttpRequest>(
                            r => new { RawUrl = r.RawUrl, Method = r.HttpMethod })
                    .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            SerilogWebClassic.Configure(cfg => cfg
                .EnableFormDataLogging()
                .Enable()
                .UseDefaultLogger()
            );

            return logConfig;
        }
    }
}