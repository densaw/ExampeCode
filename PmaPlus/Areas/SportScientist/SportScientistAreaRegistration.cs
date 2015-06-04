using System.Web.Mvc;

namespace PmaPlus.Areas.SportScientist
{
    public class SportScientistAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SportScientist";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SportScientist_default",
                "SportScientist/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}