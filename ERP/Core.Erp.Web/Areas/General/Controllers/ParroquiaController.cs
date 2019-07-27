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

        public ActionResult Index(string IdPais, string IdProvincia = "",string IdCiudad = "")
        {
            ViewBag.IdPais = IdPais;
            ViewBag.IdProvincia = IdProvincia;
            ViewBag.IdCiudad_canton = IdCiudad;
            return View();
        }
        
        [ValidateInput(false)]
        public ActionResult GridViewPartial_parroquia(string IdCiudad)
        {
            List<tb_parroquia_Info> model = new List<tb_parroquia_Info>();
            model = bus_parroquia.get_list(IdCiudad, true);
            ViewBag.IdCiudad_Canton = IdCiudad;
            return PartialView("_GridViewPartial_parroquia", model);
        }
        private void cargar_combos()
        {
            var lst_ciudades = bus_ciudad.get_list("",false);
            ViewBag.lst_ciudades = lst_ciudades;
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(string IdCiudad = "")
        {
            tb_parroquia_Info model = new tb_parroquia_Info
            {
                IdCiudad_Canton = IdCiudad
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
            return RedirectToAction("Index", ViewBag.IdPais = model.IdPais, ViewBag.IdProvincia = model.IdProvincia, ViewBag.IdCiudad = model.IdCiudad_Canton );
        }

        private ActionResult RedirectToAction(string v, dynamic dynamic, object p1, object p2)
        {
            throw new NotImplementedException();
        }

        public ActionResult Modificar( string IdParroquia = "")
        {
            tb_parroquia_Info model = bus_parroquia.get_info( IdParroquia);
            if (model == null)
            {
                ViewBag.IdCiudad = model.IdCiudad_Canton;
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
                return RedirectToAction("Index", new { IdPais = model.IdPais, IdProvincia = model.IdProvincia, IdCiudad = model.IdCiudad_Canton });
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
            return RedirectToAction("Index", ViewBag.IdPais = model.IdPais, ViewBag.IdProvincia = model.IdProvincia, ViewBag.IdCiudad = model.IdCiudad_Canton);
        }


        public ActionResult Anular( string IdParroquia = "")
        {
            tb_parroquia_Info model = bus_parroquia.get_info( IdParroquia);
            if (model == null)
            {
                ViewBag.IdCiudad = model.IdCiudad_Canton;
                ViewBag.IdProvincia = model.IdProvincia;
                ViewBag.IdPais = model.IdPais;
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
            return RedirectToAction("Index", ViewBag.IdPais = model.IdPais, ViewBag.IdProvincia = model.IdProvincia, ViewBag.IdCiudad = model.IdCiudad_Canton);
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