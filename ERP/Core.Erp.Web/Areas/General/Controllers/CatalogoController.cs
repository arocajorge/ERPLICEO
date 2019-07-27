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
    public class CatalogoController : Controller
    {
        #region Variables
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        tb_CatalogoTipo_Bus bus_catalogo_tipo = new tb_CatalogoTipo_Bus();

        #endregion

        #region Index

        public ActionResult Index(int IdTipoCatalogo = 0)
        {
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo(int IdTipoCatalogo = 0)
        {
            List<tb_Catalogo_Info> model = new List<tb_Catalogo_Info>();            
            model = bus_catalogo.get_list( IdTipoCatalogo, true);
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            return PartialView("_GridViewPartial_catalogo", model);
        }

        private void cargar_combos()
        {
            var lst_catalogo_tipo = bus_catalogo_tipo.get_list();
            ViewBag.lst_tipos = lst_catalogo_tipo;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdTipoCatalogo = 0)
        {
            tb_Catalogo_Info model = new tb_Catalogo_Info
            {
                IdTipoCatalogo = IdTipoCatalogo
            };
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(tb_Catalogo_Info model)
        {
            if (bus_catalogo.validar_existe_CodCatalogo(model.CodCatalogo))
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

        public ActionResult Modificar(string CodCatalogo = "", int IdTipoCatalogo = 0)
        {
            tb_Catalogo_Info model = bus_catalogo.get_info(CodCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdTipoCatalogo = IdTipoCatalogo });
            ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(tb_Catalogo_Info model)
        {
            if (!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdTipoCatalogo = model.IdTipoCatalogo });

        }

        public ActionResult Anular(string CodCatalogo = "", int IdTipoCatalogo = 0)
        {
            tb_Catalogo_Info model = bus_catalogo.get_info(CodCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdTipoCatalogo = IdTipoCatalogo });
            ViewBag.IdTipoCatalogo = model.IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_Catalogo_Info model)
        {
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