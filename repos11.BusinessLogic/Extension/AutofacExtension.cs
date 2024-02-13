using Autofac;
using repos11.Repository.Entity;
using System;
using System.Reflection;

namespace repos11.BusinessLogic.Extension
{
    public static class AutofacExtension
    {
        public static void RegisterBusinessLogic(this ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var assemblyRepo = Assembly.GetAssembly(typeof(Repository.Repository<>));
            var assemblyBL = Assembly.GetAssembly(typeof(IBusinessLogic<>));

            //register DbContext
            builder
                .RegisterType<ApplicationDbContext>()
                //.WithParameter("options", DbContextOptionsFactory.Get())
                .InstancePerLifetimeScope();

            //register Repository
            builder.RegisterAssemblyTypes(assemblyRepo)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces();

            //register Business Logic
            builder.RegisterAssemblyTypes(assemblyBL)
               .Where(t => t.Name.EndsWith("BusinessLogic"))
               .AsImplementedInterfaces();
        }
    }
}
