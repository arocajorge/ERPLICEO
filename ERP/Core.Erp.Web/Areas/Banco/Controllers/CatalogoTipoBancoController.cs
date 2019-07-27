using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.Banco;
using Core.Erp.Info.Banco;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class CatalogoTipoBancoController : Controller
    {
        #region Index
        ba_CatalogoTipo_Bus bus_catalogotipo = new ba_CatalogoTipo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogotipo_banco()
        {
            List<ba_CatalogoTipo_Info> model = new List<ba_CatalogoTipo_Info>();
            model = bus_catalogotipo.get_list();
            return PartialView("_GridViewPartial_catalogotipo_banco", model);
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            ba_CatalogoTipo_Info model = new ba_CatalogoTipo_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ba_CatalogoTipo_Info model)
        {
            if (bus_catalogotipo.validar_existe_IdTipoCatalogo(model.IdTipoCatalogo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
                return View(model);
            }
            if (!bus_catalogotipo.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string IdTipoCatalogo = "")
        {
            ba_CatalogoTipo_Info model = bus_catalogotipo.get_info(IdTipoCatalogo);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_CatalogoTipo_Info model)
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