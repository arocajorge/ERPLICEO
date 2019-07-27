using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoController : Controller
    {
        #region Index

        tb_CatalogoTipo_Bus bus_catalogo_tipo = new tb_CatalogoTipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_tipo()
        {
            List<tb_CatalogoTipo_Info> model = new List<tb_CatalogoTipo_Info>();
            model = bus_catalogo_tipo.get_list();
            return PartialView("_GridViewPartial_catalogo_tipo", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            tb_CatalogoTipo_Info model = new tb_CatalogoTipo_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(tb_CatalogoTipo_Info model)
        {
            if (!bus_catalogo_tipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdTipoCatalogo = 0)
        {
            tb_CatalogoTipo_Info model = bus_catalogo_tipo.get_info(IdTipoCatalogo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_CatalogoTipo_Info model)
        {
            if (!bus_catalogo_tipo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}
