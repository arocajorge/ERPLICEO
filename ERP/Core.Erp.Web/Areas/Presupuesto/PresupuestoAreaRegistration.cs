using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Presupuesto
{
    public class PresupuestoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Presupuesto";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Presupuesto_default",
                "Presupuesto/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}