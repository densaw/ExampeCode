using System.Web.Mvc;

namespace PmaPlus.Areas.HeadOfEducation
{
    public class HeadOfEducationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HeadOfEducation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HeadOfEducation_default",
                "HeadOfEducation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}