using System.Web.Mvc;

namespace PmaPlus.Areas.WelfareOfficer
{
    public class WelfareOfficerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WelfareOfficer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WelfareOfficer_default",
                "WelfareOfficer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}