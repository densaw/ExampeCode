using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using PmaPlus.Controllers;
using PmaPlus.Data;
using PmaPlus.Data.Infrastructure;
using PmaPlus.Model;
using PmaPlus.Data.Repository;
using PmaPlus.Model.Models;
using PmaPlus.Services;

[assembly: OwinStartup(typeof(PmaPlus.Startup))]
namespace PmaPlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            
            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterApiControllers(typeof(DashboardController).Assembly);
            builder.RegisterControllers(typeof(HomeController).Assembly);


            // REGISTER DEPENDENCIES
            //builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<MyUserStore>().As<IUserStore<User, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerHttpRequest();
           
            
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
           .Where(t => t.Name.EndsWith("Repository"))
           .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(PlayerServices).Assembly)
           .Where(t => t.Name.EndsWith("Services"))
           .AsSelf().InstancePerHttpRequest();
           
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();


            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            app.UseAutofacWebApi(new HttpConfiguration());

            ConfigureAuth(app);
        }

    }
}