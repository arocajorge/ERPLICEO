using Core.Erp.Bus.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using Core.Erp.Web.Helps;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.ActivoFijo.Controllers
{
    [SessionTimeout]

    public class CatalogoAFController : Controller
    {
        #region Index

        Af_Catalogo_Bus bus_catalogo = new Af_Catalogo_Bus();
        public ActionResult Index(string IdTipoCatalogo = "")
        {
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_af(string IdTipoCatalogo = "")
        {
            List<Af_Catalogo_Info> model = new List<Af_Catalogo_Info>();
            model = bus_catalogo.get_list(IdTipoCatalogo, true);
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            return PartialView("_GridViewPartial_catalogo_af", model);
        }
        private void cargar_combos()
        {
            Af_CatalogoTipo_Bus bus_catalogotipo = new Af_CatalogoTipo_Bus();
            var lst_catalogo_tipo = bus_catalogotipo.get_list();
            ViewBag.lst_tipos = lst_catalogo_tipo;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(string IdTipoCatalogo = "")
        {
            Af_Catalogo_Info model = new Af_Catalogo_Info
            {
                IdTipoCatalogo = IdTipoCatalogo
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(Af_Catalogo_Info model)
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
            Af_Catalogo_Info model = bus_catalogo.get_info(IdTipoCatalogo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdTipoCatalogo = IdTipoCatalogo });
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(Af_Catalogo_Info model)
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
            Af_Catalogo_Info model = bus_catalogo.get_info(IdTipoCatalogo, IdCatalogo);
            if (model == null)
                return RedirectToAction("Index", new { IdTipoCatalogo = IdTipoCatalogo });
            ViewBag.IdTipoCatalogo = IdTipoCatalogo;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(Af_Catalogo_Info model)
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

    public class Af_Catalogo_List
    {
        string Variable = "Af_Catalogo_Info";
        public List<Af_Catalogo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<Af_Catalogo_Info> list = new List<Af_Catalogo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<Af_Catalogo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<Af_Catalogo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}