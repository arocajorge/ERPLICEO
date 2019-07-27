using System.Web.Mvc;

namespace Core.Erp.Web.Areas.ActivoFijo
{
    public class ActivoFijoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ActivoFijo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ActivoFijo_default",
                "ActivoFijo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}