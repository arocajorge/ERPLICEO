using Core.Erp.Web.Helps;
using Core.Erp.Web.Reportes.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Reportes.Controllers
{
    public class PresupuestoReportesController : Controller
    {
        // GET: Reportes/PresupuestoReportes
        public ActionResult PRE_001(int IdEmpresa = 0, decimal IdPresupuesto = 0)
        {
            PRE_001_Rpt model = new PRE_001_Rpt();
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdPresupuesto.Value = IdPresupuesto;

            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            return View(model);
        }
    }
}