using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoInventarioController : Controller
    {
        #region Index
        in_CatalogoTipo_Bus bus_catalogotipo = new in_CatalogoTipo_Bus();      
            public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogotipo_inventario()
        {
            List<in_CatalogoTipo_Info> model = new List<in_CatalogoTipo_Info>();
            model = bus_catalogotipo.get_list(false);
            return PartialView("_GridViewPartial_catalogotipo_inventario", model);
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            in_CatalogoTipo_Info model = new in_CatalogoTipo_Info();
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(in_CatalogoTipo_Info model)
        {
            if (!bus_catalogotipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdCatalogo_tipo = 0)
        {
            in_CatalogoTipo_Info model = bus_catalogotipo.get_info(IdCatalogo_tipo);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_CatalogoTipo_Info model)
        {
            if (!bus_catalogotipo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdCatalogo_tipo = 0)
        {
            in_CatalogoTipo_Info model = bus_catalogotipo.get_info(IdCatalogo_tipo);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_CatalogoTipo_Info model)
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