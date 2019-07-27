using Core.Erp.Bus.Facturacion;
using Core.Erp.Info.Facturacion;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoFacturacionController : Controller
    {
        #region Index
        fa_catalogo_tipo_Bus bus_fa_catalogotipo = new fa_catalogo_tipo_Bus();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridViewPartial_catalogotipo_fa()
        {
            List<fa_catalogo_tipo_Info> model = bus_fa_catalogotipo.get_list(true);
            return PartialView("_GridViewPartial_catalogotipo_fa", model);
        }

        #endregion
        #region Acciones

        public ActionResult Nuevo()
        {
            fa_catalogo_tipo_Info model = new fa_catalogo_tipo_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_catalogo_tipo_Info model)
        {
            if (!bus_fa_catalogotipo.guardarDB(model))
            {
                return View(model);

            }
            return RedirectToAction("Index");

        }

        public ActionResult Modificar(int IdCatalogo_tipo = 0)
        {
            fa_catalogo_tipo_Info model = bus_fa_catalogotipo.get_info(IdCatalogo_tipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_catalogo_tipo_Info model)
        {
            if (!bus_fa_catalogotipo.modificarDB(model))

            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(int IdCatalogo_tipo = 0)
        {
            fa_catalogo_tipo_Info model = bus_fa_catalogotipo.get_info(IdCatalogo_tipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_catalogo_tipo_Info model)
        {
            if (!bus_fa_catalogotipo.anularDB(model))
            {
                return View(model);

            }
            return RedirectToAction("Index");

        }
        #endregion
    }
}