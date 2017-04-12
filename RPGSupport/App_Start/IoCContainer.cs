//using Autofac;
//using Autofac.Integration.Mvc;
//using DataAccess;
//using Entities;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.DataProtection;
//using Owin;
//using Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//[assembly: OwinStartupAttribute(typeof(RPGSupport.Startup))]
//namespace RPGSupport.App_Start
//{
//    public class IoCContainer
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            var builder = new ContainerBuilder();

//            //Register Dependecies
//            builder.RegisterType<RPGSupportDb>().AsSelf().InstancePerRequest();
//            builder.RegisterType<ApplicationUserStore>().As<IUserStore<User, int>>().InstancePerRequest();
//            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
//            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
//            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
//            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

//            // BUILD THE CONTAINER
//            var container = builder.Build();

//            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

//            //// REGISTER WITH OWIN
//            app.UseAutofacMiddleware(container);
//            app.UseAutofacMvc();

//            ConfigureAuth(app);
//        }
//    }

//}