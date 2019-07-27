using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Produccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    public class ProduccionReportesController : Controller
    {
        public ActionResult PRO_001(int IdEmpresa = 0, decimal IdFabricacion = 0)
        {
            PRO_001_Rpt model = new PRO_001_Rpt();
            model.p_IdEmpresa.Value = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdFabricacion.Value = IdFabricacion;

            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            return View(model);
        }

    }
}