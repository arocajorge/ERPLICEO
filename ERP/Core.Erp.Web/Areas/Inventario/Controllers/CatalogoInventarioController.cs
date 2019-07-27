using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class CatalogoInventarioController : Controller
    {
        #region Variables
        in_Catalogo_Bus bus_catalogo = new in_Catalogo_Bus();
        in_CatalogoTipo_Bus bus_catalogo_tipo = new in_CatalogoTipo_Bus();
        #endregion
        #region Index
        public ActionResult Index(int IdCatalogo_tipo = 0)
        {
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_inventario(int IdCatalogo_tipo = 0)
        {
            List<in_Catalogo_Info> model = new List<in_Catalogo_Info>();
            model = bus_catalogo.get_list(IdCatalogo_tipo, true);
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            return PartialView("_GridViewPartial_catalogo_inventario", model);
        }

        private void cargar_combos()
        {
            var lst_catalogo_tipo = bus_catalogo_tipo.get_list(false);
            ViewBag.lst_tipos = lst_catalogo_tipo;
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdCatalogo_tipo = 0)
        {
            in_Catalogo_Info model = new in_Catalogo_Info
            {
                IdCatalogo_tipo = IdCatalogo_tipo
            };
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_Catalogo_Info model)
        {
            if (bus_catalogo.validar_existe_IdCatalogo(model.IdCatalogo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                return View(model);
            }

            if (!bus_catalogo.guardarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                return View(model);
            }

            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }
        public ActionResult Modificar(string IdCatalogo = "", int IdCatalogo_tipo = 0)
        {
            in_Catalogo_Info model = bus_catalogo.get_info(IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_Catalogo_Info model)
        {
            if (!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });

        }

        public ActionResult Anular(string IdCatalogo = "", int IdCatalogo_tipo = 0)
        {
            in_Catalogo_Info model = bus_catalogo.get_info(IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdTipoCatalogo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_Catalogo_Info model)
        {
            if (!bus_catalogo.anularDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });

        }

        #endregion
    }
}