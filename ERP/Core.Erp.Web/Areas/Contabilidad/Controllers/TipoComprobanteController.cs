using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    [SessionTimeout]
    public class TipoComprobanteController : Controller
    {
        #region Index
        ct_cbtecble_tipo_Bus bus_comprobante_tipo = new ct_cbtecble_tipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_comprobante_tipo()
        {
            List<ct_cbtecble_tipo_Info> model = new List<ct_cbtecble_tipo_Info>();
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model = bus_comprobante_tipo.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_comprobante_tipo", model);
        }
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_cbtecble_tipo_Bus bus_tipo = new ct_cbtecble_tipo_Bus();
            var lst_tipo = bus_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_tipo = lst_tipo;
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo()
        {
            ct_cbtecble_tipo_Info model = new ct_cbtecble_tipo_Info
            {
                IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]),
                IdTipoCbte_Anul = 1,
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ct_cbtecble_tipo_Info model)
        {
            if (!bus_comprobante_tipo.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdTipoCbte = 0)
        {
            ct_cbtecble_tipo_Info model = bus_comprobante_tipo.get_info(IdTipoCbte);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_cbtecble_tipo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_comprobante_tipo.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdTipoCbte = 0)
        {
            ct_cbtecble_tipo_Info model = bus_comprobante_tipo.get_info(IdTipoCbte);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_cbtecble_tipo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_comprobante_tipo.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}