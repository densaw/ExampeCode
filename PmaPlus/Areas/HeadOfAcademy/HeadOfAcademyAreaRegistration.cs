using System.Web.Mvc;

namespace PmaPlus.Areas.HeadOfAcademy
{
    public class HeadOfAcademyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HeadOfAcademy";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HeadOfAcademy_default",
                "HeadOfAcademy/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}