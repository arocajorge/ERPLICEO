using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorCobrar
{
    public class CuentasPorCobrarAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CuentasPorCobrar";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CuentasPorCobrar_default",
                "CuentasPorCobrar/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}