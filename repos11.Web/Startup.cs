using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using repos11.BusinessLogic.Extension;
using repos11.Web.App_Start;
using repos11.Web.Controllers;
using repos11.Web.Models;
using Serilog;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(repos11.Web.Startup))]

namespace repos11.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegisterComponents(app);
            ConfigureAuthDevelopment(app);
        }

        private void RegisterComponents(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            var logger = LoggerConfig.CreateLoger();
            builder.Register<ILogger>((c, p) =>
            {
                return logger;
            }).SingleInstance();

            builder.Register(c => new AppInfo())
                .As<IAppInfo>().SingleInstance();

            //register Business Logic
            builder.RegisterBusinessLogic();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
