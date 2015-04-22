using System.Web.Mvc;

namespace PmaPlus.Areas.SysAdmin
{
    public class SysAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SysAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SysAdmin_default",
                "SysAdmin/{controller}/{action}/{id}",
                new { action = "Dashbord", controller = "Home", id = UrlParameter.Optional }
            );
        }
    }
}