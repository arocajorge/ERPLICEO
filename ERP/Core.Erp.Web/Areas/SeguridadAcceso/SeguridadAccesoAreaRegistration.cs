using System.Web.Mvc;

namespace Core.Erp.Web.Areas.SeguridadAcceso
{
    public class SeguridadAccesoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SeguridadAcceso";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SeguridadAcceso_default",
                "SeguridadAcceso/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}