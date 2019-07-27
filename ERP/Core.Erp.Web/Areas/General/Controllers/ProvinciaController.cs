using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class ProvinciaController : Controller
    {
        #region Index/ Metodo

        tb_provincia_Bus bus_provincia = new tb_provincia_Bus();
        tb_pais_Bus bus_pais = new tb_pais_Bus();
        tb_region_Bus bus_region = new tb_region_Bus();
        public ActionResult Index(string IdPais = "")
        {
            ViewBag.IdPais = IdPais;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_provincia(string IdPais = "")
        {
            List<tb_provincia_Info> model = new List<tb_provincia_Info>();
            model = bus_provincia.get_list(IdPais, true);
            ViewBag.IdPais = IdPais;
            return PartialView("_GridViewPartial_provincia", model);
        }

        private void cargar_combos(string IdPais)
        {
            
            var lst_pais = bus_pais.get_list(false);
            ViewBag.lst_pais = lst_pais;
            var lst_region = bus_region.get_list(IdPais,false);
            ViewBag.lst_region = lst_region;
        }
        #endregion

        #region Acciones

        public ActionResult Nuevo(string IdPais)
        {
            tb_provincia_Info model = new tb_provincia_Info
            {
                IdPais = IdPais
            };
            ViewBag.IdPais = IdPais;
            cargar_combos(IdPais);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_provincia_Info model)
        {
            if (!bus_provincia.guardarDB(model))
            {
                ViewBag.IdPais = model.IdPais;
                cargar_combos(model.IdPais);
                return View(model);
            }
            return RedirectToAction("Index", new { IdPais = model.IdPais });
        }

        public ActionResult Modificar( string IdProvincia = "")
        {
            tb_provincia_Info model = bus_provincia.get_info( IdProvincia);
            if (model == null)
            {
                ViewBag.IdPais = model.IdPais;
                return RedirectToAction("Index",new { IdPais = model.IdPais });
            }
            cargar_combos(model.IdPais);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_provincia_Info model)
        {
            if (!bus_provincia.modificarDB(model))
            {
                ViewBag.IdPais = model.IdPais;
                cargar_combos(model.IdPais);
                return View(model);
            }
            return RedirectToAction("Index", new { IdPais = model.IdPais });
        }

        public ActionResult Anular( string IdProvincia = "")
        {
            tb_provincia_Info model = bus_provincia.get_info( IdProvincia);
            if (model == null)
            {
                ViewBag.IdPais = model.IdPais;
                return RedirectToAction("Index", new { IdPais = model.IdPais });
            }
            cargar_combos(model.IdPais);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_provincia_Info model)
        {
            if (!bus_provincia.anularDB(model))
            {
                ViewBag.IdPais = model.IdPais;
                cargar_combos(model.IdPais);
                return View(model);
            }
            return RedirectToAction("Index", new { IdPais = model.IdPais });
        }
        #endregion

        #region Json
        public JsonResult get_lst_provincia_pais(string IdPais)
        {
            try
            {
                List<tb_provincia_Info> lst_provincia =new List<tb_provincia_Info>();

                lst_provincia = bus_provincia.get_list(IdPais, true);
                return Json(lst_provincia, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

    }
}