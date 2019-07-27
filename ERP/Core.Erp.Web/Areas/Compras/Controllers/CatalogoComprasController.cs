using Core.Erp.Bus.Compras;
using Core.Erp.Info.Compras;
using Core.Erp.Web.Helps;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    [SessionTimeout]
    public class CatalogoComprasController : Controller
    {
        #region Index
        com_catalogo_Bus bus_catalogo = new com_catalogo_Bus();
        public ActionResult Index(string IdCatalogocompra_tipo = "")
        {
            ViewBag.IdCatalogocompra_tipo = IdCatalogocompra_tipo;
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial_cat_compras(string IdCatalogocompra_tipo = "")
        {
            var model = bus_catalogo.get_list(IdCatalogocompra_tipo, true);
            ViewBag.IdCatalogocompra_tipo = IdCatalogocompra_tipo;
            return PartialView("_GridViewPartial_cat_compras", model);
        }
        private void cargar_combos()
        {
            com_catalogo_tipo_Bus bus_catalogotipo = new com_catalogo_tipo_Bus();
            var lst_catalogo_tipo = bus_catalogotipo.get_list(false);
            ViewBag.lst_tipos = lst_catalogo_tipo;
        }

        #endregion

        #region Acciones

        public ActionResult Nuevo(string IdCatalogocompra_tipo = "")
        {
            com_catalogo_Info model = new com_catalogo_Info
            {
                IdCatalogocompra_tipo = IdCatalogocompra_tipo
            };
            ViewBag.IdCatalogocompra_tipo = IdCatalogocompra_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(com_catalogo_Info model)
        {
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (bus_catalogo.validar_existe_IdCatalogo(model.IdCatalogocompra))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogocompra_tipo = model.IdCatalogocompra_tipo;
                cargar_combos();
                return View(model);
            }

            if (!bus_catalogo.guardarDB(model))
            {
                ViewBag.IdCatalogocompra_tipo = model.IdCatalogocompra_tipo;
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", new { IdCatalogocompra_tipo = model.IdCatalogocompra_tipo });
        }

        public ActionResult Modificar(string IdCatalogocompra_tipo = "", string IdCatalogocompra = "")
        {
            com_catalogo_Info model = bus_catalogo.get_info(IdCatalogocompra_tipo, IdCatalogocompra);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogocompra_tipo = IdCatalogocompra_tipo });
            ViewBag.IdCatalogocompra_tipo = IdCatalogocompra_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(com_catalogo_Info model)
        {
            if (!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdCatalogocompra_tipo = model.IdCatalogocompra_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogocompra_tipo = model.IdCatalogocompra_tipo });

        }
        public ActionResult Anular(string IdCatalogocompra_tipo = "", string IdCatalogocompra = "")
        {
            com_catalogo_Info model = bus_catalogo.get_info(IdCatalogocompra_tipo, IdCatalogocompra);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogocompra_tipo = IdCatalogocompra_tipo });
            ViewBag.IdCatalogocompra_tipo = IdCatalogocompra_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(com_catalogo_Info model)
        {
            if (!bus_catalogo.anularDB(model))
            {
                ViewBag.IdCatalogocompra_tipo = model.IdCatalogocompra_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogocompra_tipo = model.IdCatalogocompra_tipo });

        }
        #endregion
    }
}