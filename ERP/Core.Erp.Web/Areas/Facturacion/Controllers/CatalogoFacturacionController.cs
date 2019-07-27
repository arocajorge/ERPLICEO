using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class CatalogoFacturacionController : Controller
    {
        #region Index

        fa_catalogo_Bus bus_catalogo = new fa_catalogo_Bus();

        public ActionResult Index(int IdCatalogo_tipo = 0)
        {
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            return View();
        }
        public ActionResult GridViewPartial_catalogo_fa(int IdCatalogo_tipo = 0)
        {
            List<fa_catalogo_Info> model = bus_catalogo.get_list(IdCatalogo_tipo, true);
            return PartialView("_GridViewPartial_catalogo_fa", model);
        }
        private void cargar_combos()
        {
            fa_catalogo_tipo_Bus bus_tipo = new fa_catalogo_tipo_Bus();
            var lst_tipo = bus_tipo.get_list(false);
            ViewBag.lst_tipo = lst_tipo;
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdCatalogo_tipo = 0)
        {
            fa_catalogo_Info model = new fa_catalogo_Info
            {
                IdCatalogo_tipo = IdCatalogo_tipo
            };
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }
       [HttpPost]
        public ActionResult Nuevo(fa_catalogo_Info model)
        {
            if (bus_catalogo.validar_existe_IdCatalogo(model.IdCatalogo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }

            if (!bus_catalogo.guardarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }
        public ActionResult Modificar(int IdCatalogo_tipo = 0, string IdCatalogo = "")
        {
            fa_catalogo_Info model = bus_catalogo.get_info(IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(fa_catalogo_Info model)
        {
            if (!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }
        public ActionResult Anular(int IdCatalogo_tipo = 0, string IdCatalogo = "")
        {
            fa_catalogo_Info model = bus_catalogo.get_info(IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(fa_catalogo_Info model)
        {
            if (!bus_catalogo.anularDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }
        #endregion
    }
}