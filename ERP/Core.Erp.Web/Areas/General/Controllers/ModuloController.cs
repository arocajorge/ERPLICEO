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

    public class ModuloController : Controller
    {
        #region Index

        tb_modulo_Bus bus_modulo = new tb_modulo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_modulo()
        {
            List<tb_modulo_Info> model = new List<tb_modulo_Info>();
            model = bus_modulo.get_list();
            return PartialView("_GridViewPartial_modulo", model);
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo()
        {
            tb_modulo_Info model = new tb_modulo_Info();
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(tb_modulo_Info model)
        {
            if (bus_modulo.validar_existe_CodModulo(model.CodModulo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                return View(model);
            }
            if (!bus_modulo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string CodModulo = "")
        {
            tb_modulo_Info model = bus_modulo.get_info(CodModulo);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_modulo_Info model)
        {
            if (!bus_modulo.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        
    }
}