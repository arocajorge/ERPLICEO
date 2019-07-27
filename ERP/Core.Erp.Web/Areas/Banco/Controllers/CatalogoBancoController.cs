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
    public class CatalogoBancoController : Controller
    {
        #region Index

        ba_Catalogo_Bus bus_catalogo = new ba_Catalogo_Bus();

        public ActionResult Index(string IdTipoCatalogo = "")
        {
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_banco(string IdTipoCatalogo = "")
        {
            List<ba_Catalogo_Info> model = new List<ba_Catalogo_Info>();
            model = bus_catalogo.get_list(IdTipoCatalogo, true);
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            return PartialView("_GridViewPartial_catalogo_banco", model);
        }
        private void cargar_combos()
        {
            ba_CatalogoTipo_Bus bus_catalogotipo = new ba_CatalogoTipo_Bus();
            var lst_catalogo_tipo = bus_catalogotipo.get_list();
            ViewBag.lst_tipos = lst_catalogo_tipo;
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo(string IdTipoCatalogo = "")
        {
            ba_Catalogo_Info model = new ba_Catalogo_Info
            {
                IdTipoCatalogo = IdTipoCatalogo
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_Catalogo_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            if (bus_catalogo.validar_existe_IdCatalogo(model.IdCatalogo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
                cargar_combos();
                return View(model);
            }

            if (!bus_catalogo.guardarDB(model))
            {
                ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", new { IdTipoCatalogo = model.IdTipoCatalogo });
        }
        public ActionResult Modificar(string IdTipoCatalogo = "", string IdCatalogo = "")
        {
            ba_Catalogo_Info model = bus_catalogo.get_info(IdTipoCatalogo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdTipoCatalogo });
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ba_Catalogo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdTipoCatalogo = model.IdTipoCatalogo });

        }
        public ActionResult Anular(string IdTipoCatalogo = "", string IdCatalogo = "")
        {
            ba_Catalogo_Info model = bus_catalogo.get_info(IdTipoCatalogo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdTipoCatalogo });
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ba_Catalogo_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_catalogo.anularDB(model))
            {
                ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdTipoCatalogo = model.IdTipoCatalogo });

        }
        #endregion

    }
}