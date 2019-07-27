using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class AreaController : Controller
    {

        ro_area_Bus bus_area = new ro_area_Bus();
        int IdEmpresa = 0;
        List<ro_division_Info> lista_division = new List<ro_division_Info>();
        ro_division_Bus bus_division = new ro_division_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_area()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                List<ro_area_Info> model = bus_area.get_list(IdEmpresa, true);
                return PartialView("_GridViewPartial_area", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_area_Info info)
        {
            try
            {

                info.IdUsuario = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_area.guardarDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo()
        {
            try
            {
                cargar_combos();
                ro_area_Info info = new ro_area_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_area_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    if (!bus_area.modificarDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(int IdArea = 0)
        {
            try
            {
                cargar_combos();
                IdEmpresa = GetIdEmpresa();
                return View(bus_area.get_info(IdEmpresa, IdArea));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_area_Info info)
        {
            try
            {
                info.IdUsuario = SessionFixed.IdUsuario;
                if (!bus_area.anularDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");              

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdArea = 0)
        {
            try
            {
                cargar_combos();
                IdEmpresa = GetIdEmpresa();
                return View(bus_area.get_info(IdEmpresa, IdArea));

            }
            catch (Exception)
            {

                throw;
            }
        }
        private int GetIdEmpresa()
        {
            try
            {
                if (Session["IdEmpresa"] != null)
                    return Convert.ToInt32(Session["IdEmpresa"]);
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combos()
        {
            try
            {
                lista_division = bus_division.get_list(GetIdEmpresa(), false);
                ViewBag.lista_division = lista_division;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult get_areas(int IdDivision)
        {
            try
            {
                List<ro_area_Info> lst_areas = new List<ro_area_Info>();
                lst_areas = bus_area.get_list(GetIdEmpresa(), IdDivision);
                return Json(lst_areas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ro_area_List
    {
        string Variable = "ro_area_Info";
        public List<ro_area_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_area_Info> list = new List<ro_area_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_area_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_area_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}