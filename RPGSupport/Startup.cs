using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Entities;
using Interfaces.Repositories;
using Interfaces.Services;
using Interfaces.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using Services;
using Services.Entity;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System;

[assembly: OwinStartupAttribute(typeof(RPGSupport.Startup))]
namespace RPGSupport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureAutoFac(app);
        }

    }
    public partial class Startup
    {
        public void ConfigureAutoFac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //REGISTER DBCONTEXT
            builder.RegisterType<RPGSupportDb>().AsSelf().InstancePerRequest();

            //REGISTER IDENTITY

            builder.RegisterType<ApplicationUserStore>().As<IUserStore<User, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            //REPOSITORIES
            builder.RegisterType<Repository<User>>().As<IRepository<User>>().InstancePerRequest();

            //UNIT OF WORK
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            //SERVICES
            builder.RegisterType<EntityService<User>>().As<IEntityService<User>>().InstancePerRequest();

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register Web API controllers
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ////// REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
