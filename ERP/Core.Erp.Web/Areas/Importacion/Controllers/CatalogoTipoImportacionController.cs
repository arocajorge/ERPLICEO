using Core.Erp.Bus.Importacion;
using Core.Erp.Info.Importacion;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoImportacionController : Controller
    {
        #region Index
        imp_catalogo_tipo_Bus bus_catalogo_tipo = new imp_catalogo_tipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cat_tipo_imp()
        {
            List<imp_catalogo_tipo_Info> model = bus_catalogo_tipo.get_list();
            return PartialView("_GridViewPartial_cat_tipo_imp", model);
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            imp_catalogo_tipo_Info model = new imp_catalogo_tipo_Info
            {
                estado = true
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(imp_catalogo_tipo_Info model)
        {
            if(!bus_catalogo_tipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdCatalogo_tipo = 0)
        {
            imp_catalogo_tipo_Info model = bus_catalogo_tipo.get_info(IdCatalogo_tipo);
            if (model == null)
                    return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(imp_catalogo_tipo_Info model)
        {
            if(!bus_catalogo_tipo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdCatalogo_tipo = 0)
        {
            imp_catalogo_tipo_Info model = bus_catalogo_tipo.get_info(IdCatalogo_tipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(imp_catalogo_tipo_Info model)
        {
            if (!bus_catalogo_tipo.anularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}