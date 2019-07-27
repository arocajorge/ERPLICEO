using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.General;
using Core.Erp.Bus.General;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class CiudadController : Controller
    {
        #region Inex

        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        tb_provincia_Bus bus_provincia = new tb_provincia_Bus();

        public ActionResult Index(string IdPais = "", string IdProvincia = "")
        {
            ViewBag.IdPais = IdPais;
            ViewBag.IdProvincia = IdProvincia;
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial_Ciudad(string IdProvincia="")
        {
            var model = bus_ciudad.get_list(IdProvincia, true);
            ViewBag.IdProvincia = IdProvincia;
            return PartialView("_GridViewPartial_Ciudad", model);
        }
        private void cargar_combos()
        {
         var  lst_provincia = bus_provincia.get_list("1", false);
            ViewBag.lst_provincia = lst_provincia;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(string IdProvincia)
        {
            tb_ciudad_Info model = new tb_ciudad_Info
            {
                IdProvincia = IdProvincia
            };
            ViewBag.IdProvincia = IdProvincia;
            ViewBag.IdPais = model.IdPais;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_ciudad_Info model)
        {
            if (!bus_ciudad.guardarDB(model))
            {
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdProvincia = model.IdProvincia, IdPais = model.IdPais, } );
        }

        public ActionResult Modificar( string IdCiudad="")
        {
            tb_ciudad_Info model = bus_ciudad.get_info( IdCiudad);
            if (model == null)
            {
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                return RedirectToAction("Index", new { IdProvincia = model.IdProvincia, IdPais = model.IdPais, });
            }
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_ciudad_Info model)
        {
            if (!bus_ciudad.modificarDB(model))
            {
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", ViewBag.IdProvincia = model.IdProvincia, ViewBag.IdPais = model.IdPais );
        }

        public ActionResult Anular( string IdCiudad="")
        {
            tb_ciudad_Info model = bus_ciudad.get_info( IdCiudad);
            if (model == null)
            {
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                return RedirectToAction("Index", new { IdProvincia = model.IdProvincia, IdPais = model.IdPais, });
            }
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_ciudad_Info model)
        {
            if (!bus_ciudad.anularDB(model))
            {
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", ViewBag.IdProvincia = model.IdProvincia, ViewBag.IdPais = model.IdPais);
        }

        public JsonResult get_lst_ciudad_x_provincia(string IdProvincia)
        {
            try
            {
                List<tb_ciudad_Info> lst_ciudad = new List<tb_ciudad_Info>();
                lst_ciudad = bus_ciudad.get_list(IdProvincia, true);
                return Json(lst_ciudad, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}