using Core.Erp.Bus.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorCobrar.Controllers
{
    [SessionTimeout]
    public class CatalogoCXCController : Controller
    {
        #region Index

        cxc_Catalogo_Bus bus_catalogo = new cxc_Catalogo_Bus();

        public ActionResult Index(string IdCatalogo_tipo = "")
        {
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogocxc(string IdCatalogo_tipo = "")
        {
            List<cxc_Catalogo_Info> model = new List<cxc_Catalogo_Info>();
            model = bus_catalogo.get_list(IdCatalogo_tipo, true);
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            return PartialView("_GridViewPartial_catalogocxc", model);
        }
        private void cargar_combos()
        {
            cxc_CatalogoTipo_Bus bus_catalogotipo = new cxc_CatalogoTipo_Bus();
            var lst_catalogo_tipo = bus_catalogotipo.get_list();
            ViewBag.lst_tipos = lst_catalogo_tipo;

            Dictionary<int, string> lst_orden = new Dictionary<int, string>();
            lst_orden.Add(1, "1");
            lst_orden.Add(2, "2");
            lst_orden.Add(3, "3");
            lst_orden.Add(4, "4");
            lst_orden.Add(5, "5");
            ViewBag.lst_orden = lst_orden;
        }
        #endregion
        #region Acciones

        public ActionResult Nuevo(string IdCatalogo_tipo = "")
        {
            cxc_Catalogo_Info model = new cxc_Catalogo_Info
            {
                IdCatalogo_tipo = IdCatalogo_tipo
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cxc_Catalogo_Info model)
        {
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (bus_catalogo.validar_existe_IdCatalogo(model.IdCatalogo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }

            if (!bus_catalogo.guardarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });
        }

        public ActionResult Modificar(string IdCatalogo_tipo = "", string IdCatalogo = "")
        {
            cxc_Catalogo_Info model = bus_catalogo.get_info(IdCatalogo_tipo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cxc_Catalogo_Info model)
        {
            if (!bus_catalogo.modificarDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });

        }

        public ActionResult Anular(string IdCatalogo_tipo = "", string IdCatalogo = "")
        {
            cxc_Catalogo_Info model = bus_catalogo.get_info(IdCatalogo_tipo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogo_tipo = IdCatalogo_tipo });
            ViewBag.IdCatalogo_tipo = IdCatalogo_tipo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_Catalogo_Info model)
        {
            if (!bus_catalogo.anularDB(model))
            {
                ViewBag.IdCatalogo_tipo = model.IdCatalogo_tipo;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogo_tipo = model.IdCatalogo_tipo });

        }
        #endregion
    }
}
