using Core.Erp.Bus.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorCobrar.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoCXCController : Controller
    {
        #region Index

        cxc_CatalogoTipo_Bus bus_catalogotipo = new cxc_CatalogoTipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogotipocxc()
        {
            List<cxc_CatalogoTipo_Info> model = new List<cxc_CatalogoTipo_Info>();
                model = bus_catalogotipo.get_list();
            return PartialView("_GridViewPartial_catalogotipocxc", model);
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo ()
        {
            cxc_CatalogoTipo_Info model = new cxc_CatalogoTipo_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cxc_CatalogoTipo_Info model)
        {
            if (bus_catalogotipo.validar_existe_IdCatalogotipo(model.IdCatalogo_tipo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                return View(model);
            }
            if (!bus_catalogotipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string IdCatalogo_tipo = "")
        {
            cxc_CatalogoTipo_Info model = bus_catalogotipo.get_info(IdCatalogo_tipo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cxc_CatalogoTipo_Info model)
        {
            if (!bus_catalogotipo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}