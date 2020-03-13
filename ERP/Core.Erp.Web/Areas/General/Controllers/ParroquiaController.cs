using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.General.Controllers
{
    [SessionTimeout]
    public class ParroquiaController : Controller
    {
        #region Index / Metodos

        tb_parroquia_Bus bus_parroquia = new tb_parroquia_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        tb_provincia_Bus bus_provincia = new tb_provincia_Bus();
        public ActionResult Index(string IdCiudad = "", string IdProvincia = "", string IdPais = "")
        {
            ViewBag.IdPais = IdPais;
            ViewBag.IdProvincia = IdProvincia;
            ViewBag.IdCiudad_canton = IdCiudad;
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial_parroquia(string IdCiudad, string IdProvincia, string IdPais)
        {
            List<tb_parroquia_Info> model = new List<tb_parroquia_Info>();
            model = bus_parroquia.get_list(IdCiudad, true);
            ViewBag.IdCiudad_Canton = IdCiudad;
            ViewBag.IdPais = IdPais;
            ViewBag.IdProvincia = IdProvincia;
            return PartialView("_GridViewPartial_parroquia", model);
        }
        private void cargar_combos()
        {
            var lst_ciudades = bus_ciudad.get_list("",false);
            ViewBag.lst_ciudades = lst_ciudades;
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(string IdCiudad = "", string IdProvincia = "", string IdPais = "")
        {
            tb_parroquia_Info model = new tb_parroquia_Info
            {
                IdCiudad_Canton = IdCiudad,
                IdProvincia=IdProvincia,
                IdPais=IdPais
            };
            ViewBag.IdCiudad = model.IdCiudad_Canton;
            ViewBag.IdProvincia = model.IdProvincia;
            ViewBag.IdPais = model.IdPais;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(tb_parroquia_Info model)
        {
            if (!bus_parroquia.guardarDB(model))
            {
                ViewBag.IdCiudad = model.IdCiudad_Canton;
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCiudad = ViewBag.IdCiudad_Canton, IdProvincia = ViewBag.IdProvincia, IdPais = ViewBag.IdPais });
        }

        //private ActionResult RedirectToAction(string v, dynamic dynamic, object p1, object p2)
        //{
        //    throw new NotImplementedException();
        //}

        public ActionResult Modificar( string IdParroquia = "")
        {
            tb_parroquia_Info model = bus_parroquia.get_info( IdParroquia);
            var info_ciudad = bus_ciudad.get_info(model.IdCiudad_Canton);
            var info_provincia = bus_provincia.get_info(info_ciudad.IdProvincia);
            model.IdPais = info_provincia.IdPais;
            model.IdProvincia = info_ciudad.IdProvincia;
            ViewBag.IdCiudad = model.IdCiudad_Canton;
            ViewBag.IdProvincia = model.IdProvincia;
            ViewBag.IdPais = model.IdPais;

            if (model == null)
            {
                return RedirectToAction("Index", new { IdCiudad = ViewBag.IdCiudad_Canton, IdProvincia = ViewBag.IdProvincia, IdPais = ViewBag.IdPais });
            }

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(tb_parroquia_Info model)
        {
            if (!bus_parroquia.modificarDB(model))
            {
                ViewBag.IdCiudad = model.IdCiudad_Canton;
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCiudad = model.IdCiudad_Canton, IdProvincia = model.IdProvincia, IdPais = model.IdPais });
        }


        public ActionResult Anular( string IdParroquia = "")
        {
            tb_parroquia_Info model = bus_parroquia.get_info( IdParroquia);
            var info_ciudad = bus_ciudad.get_info(model.IdCiudad_Canton);
            var info_provincia = bus_provincia.get_info(info_ciudad.IdProvincia);
            model.IdPais = info_provincia.IdPais;
            model.IdProvincia = info_ciudad.IdProvincia;
            ViewBag.IdCiudad = model.IdCiudad_Canton;
            ViewBag.IdProvincia = model.IdProvincia;
            ViewBag.IdPais = model.IdPais;
            if (model == null)
            {
                return RedirectToAction("Index", new { IdPais = model.IdPais, IdProvincia = model.IdProvincia, IdCiudad = model.IdCiudad_Canton });
            }
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(tb_parroquia_Info model)
        {
            if (!bus_parroquia.anularDB(model))
            {
                ViewBag.IdCiudad = model.IdCiudad_Canton;
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCiudad = model.IdCiudad_Canton, IdProvincia = model.IdProvincia, IdPais = model.IdPais });
        }

        #endregion

        public JsonResult get_lst_ciudad_x_provincia(string IdCiudad)
        {
            try
            {
               var lst_parroquia = bus_parroquia.get_list(IdCiudad, true);
                return Json(lst_parroquia, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
               throw;
            }
        }
    }

    public class tb_parroquia_List
    {
        string Variable = "tb_parroquia_Info";
        public List<tb_parroquia_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<tb_parroquia_Info> list = new List<tb_parroquia_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<tb_parroquia_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<tb_parroquia_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }
    }
}