using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Compras;
using Core.Erp.Info.Compras;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoComprasController : Controller
    {
        #region Index
        com_catalogo_tipo_Bus bus_catalogotipo = new com_catalogo_tipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catipocompras()
        {
            var model = bus_catalogotipo.get_list(true);
            return PartialView("_GridViewPartial_catipocompras", model);
        }

        #endregion

        #region Acciones

        public ActionResult Nuevo()
        {
            com_catalogo_tipo_Info model = new com_catalogo_tipo_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(com_catalogo_tipo_Info model)
        {
            if (bus_catalogotipo.validar_existe_IdCatalogotipo(model.IdCatalogocompra_tipo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogocompra_tipo = model.IdCatalogocompra_tipo;
                return View(model);
            }
            if (!bus_catalogotipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(string IdCatalogocompra_tipo = "")
        {
            com_catalogo_tipo_Info model = bus_catalogotipo.get_info(IdCatalogocompra_tipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(com_catalogo_tipo_Info model)
        {
            if (!bus_catalogotipo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(string IdCatalogocompra_tipo = "")
        {
            com_catalogo_tipo_Info model = bus_catalogotipo.get_info(IdCatalogocompra_tipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(com_catalogo_tipo_Info model)
        {
            if (!bus_catalogotipo.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

    }
}