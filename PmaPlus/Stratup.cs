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
using PmaPlus.Model;

[assembly: OwinStartup(typeof(PmaPlus.Startup))]
namespace PmaPlus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            // REGISTER DEPENDENCIES
            //builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<MyUserStore>().As<IUserStore<User, int>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            app.UseAutofacWebApi(new HttpConfiguration());

            ConfigureAuth(app);
        }

    }
}