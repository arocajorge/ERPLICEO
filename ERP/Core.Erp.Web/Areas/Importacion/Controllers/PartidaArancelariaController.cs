using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Importacion;
using Core.Erp.Bus.Importacion;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    public class PartidaArancelariaController : Controller
    {
        imp_partida_arancelaria_Bus bus_arancel = new imp_partida_arancelaria_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_partida_arancelaria()
        {
            var model = bus_arancel.get_list(true);
            return PartialView("_GridViewPartial_partida_arancelaria", model);
        }

        public ActionResult Nuevo()
        {
            imp_partida_arancelaria_Info model = new imp_partida_arancelaria_Info();
            return View(model);

        }

        [HttpPost]
        public ActionResult Nuevo(imp_partida_arancelaria_Info model)
        {
            if (bus_arancel.validar_si_existe_codigo(model.CodigoPartidaArancelaria))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }

            if (!bus_arancel.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar (decimal IdArancel = 0)
        {
            imp_partida_arancelaria_Info model = bus_arancel.get_info(IdArancel);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar (imp_partida_arancelaria_Info model)
        {
            if (!bus_arancel.modificarDB(model))
                {
                return View(model);
                }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(decimal IdArancel = 0)
        {
            imp_partida_arancelaria_Info model = bus_arancel.get_info(IdArancel);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(imp_partida_arancelaria_Info model)
        {
            if (!bus_arancel.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}