using Core.Erp.Bus.Importacion;
using Core.Erp.Info.Importacion;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    [SessionTimeout]
    public class CatalogoImportacionController : Controller
    {
        #region Index /  Metodos

        imp_catalogo_Bus bus_catalogo = new imp_catalogo_Bus();
        public ActionResult Index(int IdCatalogo_tipo = 0)
        {
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_imp(int IdCatalogo_tipo = 0)
        {
            List<imp_catalogo_Info> model = bus_catalogo.get_list(IdCatalogo_tipo);
            return PartialView("_GridViewPartial_catalogo_imp", model);
        }

        private void cargar_combos()
        {
            imp_catalogo_tipo_Bus bus_tipo = new imp_catalogo_tipo_Bus();
            var lst_tipo = bus_tipo.get_list();
            ViewBag.lst_tipo = lst_tipo;
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdCatalogo_tipo = 0)
        {
            imp_catalogo_Info model = new imp_catalogo_Info
            {
                IdCatalogo_tipo = IdCatalogo_tipo
            };
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]

        public ActionResult Nuevo(imp_catalogo_Info model)
        {
            if(!bus_catalogo.guardarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }

        public ActionResult Modificar(int IdCatalogo_tipo = 0, int IdCatalogo = 0)
        {
            imp_catalogo_Info model = bus_catalogo.get_info(IdCatalogo_tipo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(imp_catalogo_Info model)
        {
            if(!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }
        public ActionResult Anular(int IdCatalogo_tipo = 0, int IdCatalogo = 0)
        {
            imp_catalogo_Info model = bus_catalogo.get_info(IdCatalogo_tipo, IdCatalogo_tipo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(imp_catalogo_Info model)
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