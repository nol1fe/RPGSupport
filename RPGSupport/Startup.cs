using Autofac;
using Autofac.Integration.Mvc;
using DataAccess;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using Entities;
using Interfaces.Repositories;
using Interfaces.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using Services;
using System.Web;
using System.Web.Mvc;


[assembly: OwinStartupAttribute(typeof(RPGSupport.Startup))]
namespace RPGSupport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //Register Identity
            builder.RegisterType<RPGSupportDb>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<User, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            //
            builder.RegisterType<RepositoryBase>().As<IRepositoryBase>().InstancePerRequest();
            builder.RegisterType<Repository<User>>().As<IRepository<User>>().InstancePerRequest();

            //
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ////// REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}
