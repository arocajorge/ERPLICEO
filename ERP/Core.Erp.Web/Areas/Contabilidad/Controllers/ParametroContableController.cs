using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    [SessionTimeout]
    public class ParametroContableController : Controller
    {
        ct_parametro_Bus bus_parametro = new ct_parametro_Bus();

        private void cargar_combos()
        {
            ct_cbtecble_tipo_Bus bus_comprobante = new ct_cbtecble_tipo_Bus();
            var lst_parametro = bus_comprobante.get_list(Convert.ToInt32(SessionFixed.IdEmpresa), false);
            ViewBag.lst_parametrocontable = lst_parametro;
            
        }
        public ActionResult Index()
        {
            ct_parametro_Info model = bus_parametro.get_info(Convert.ToInt32(Session["IdEmpresa"]));
            if (model == null)
                model = new ct_parametro_Info();
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ct_parametro_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (bus_parametro.guardarDB(model))
            { 
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_parametro()
        {
            ct_parametro_Info model = new ct_parametro_Info();
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            return PartialView("_GridViewPartial_parametro", model);
        }

    }
}