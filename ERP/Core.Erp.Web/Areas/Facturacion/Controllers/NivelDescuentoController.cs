using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    public class NivelDescuentoController : Controller
    {
        fa_NivelDescuento_Bus bus_nivel = new fa_NivelDescuento_Bus();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridViewPartial_nivel_dscto_fa()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_nivel.GetList(IdEmpresa, true);
            return PartialView("_GridViewPartial_nivel_dscto_fa", model);
        }
        #region Acciones

        public ActionResult Nuevo()
        {
            fa_NivelDescuento_Info model = new fa_NivelDescuento_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_NivelDescuento_Info model)
        {
            if (!bus_nivel.GuardarDB(model))
            {
                return View(model);

            }
            return RedirectToAction("Index");

        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdNivel = 0)
        {
            fa_NivelDescuento_Info model = bus_nivel.GetInfo(IdEmpresa, IdNivel);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_NivelDescuento_Info model)
        {
            if (!bus_nivel.ModificarDB(model))

            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(int IdEmpresa = 0, int IdNivel = 0)
        {
            fa_NivelDescuento_Info model = bus_nivel.GetInfo(IdEmpresa, IdNivel);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_NivelDescuento_Info model)
        {
            if (!bus_nivel.AnularDB(model))
            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        #endregion

    }
}