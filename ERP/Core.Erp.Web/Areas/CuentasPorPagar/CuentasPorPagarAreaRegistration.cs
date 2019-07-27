using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorPagar
{
    public class CuentasPorPagarAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CuentasPorPagar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CuentasPorPagar_default",
                "CuentasPorPagar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}