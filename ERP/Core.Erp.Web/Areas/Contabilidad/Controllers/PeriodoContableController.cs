using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    [SessionTimeout]
    public class PeriodoContableController : Controller
    {
        #region Variables
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();
        tb_mes_Bus bus_mes = new tb_mes_Bus();

        #endregion
        #region Index
        public ActionResult Index(int IdanioFiscal = 0)
        {
            ViewBag.IdanioFiscal = IdanioFiscal;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_periodocontable()
        {
            List<ct_periodo_Info> model = new List<ct_periodo_Info>();
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model = bus_periodo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_periodocontable", model);
        }

        private void cargar_combos()
        {
            var lst_anio = bus_anio.get_list(false);
            ViewBag.lst_anio_fiscal = lst_anio;

            var lst_mes = bus_mes.get_list();
            ViewBag.lst_Mes = lst_mes;
        }

        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdPeriodo = 0)
        {
            cargar_combos();
            ct_periodo_Info model = new ct_periodo_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ct_periodo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_periodo.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdPeriodo = 0)
        {
            ct_periodo_Info model = bus_periodo.get_info(Convert.ToInt32(Session["IdEmpresa"]), IdPeriodo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_periodo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_periodo.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdPeriodo = 0)
        {
            ct_periodo_Info model = bus_periodo.get_info(Convert.ToInt32(Session["IdEmpresa"]), IdPeriodo);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_periodo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_periodo.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}