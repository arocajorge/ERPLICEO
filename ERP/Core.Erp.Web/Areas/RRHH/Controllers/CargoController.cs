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
    public class CargoController : Controller
    {
        ro_cargo_Bus bus_cargo = new ro_cargo_Bus();
        int IdEmpresa = 0;
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cargo()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                List<ro_cargo_Info> model = bus_cargo.get_list(IdEmpresa, true);
                return PartialView("_GridViewPartial_cargo", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_cargo_Info info)
        {
            try
            {
                info.IdUsuario = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    info.IdEmpresa =  GetIdEmpresa();
                    if (!bus_cargo.guardarDB(info))
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
                ro_cargo_Info info = new ro_cargo_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_cargo_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    if (!bus_cargo.modificarDB(info))
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
      
        public ActionResult Modificar(int IdCargo=0)
        {
            try
            {
                return View(bus_cargo.get_info(GetIdEmpresa(),IdCargo));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_cargo_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                if (!bus_cargo.anularDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
               

            }
            catch (Exception)
            {

                throw;
            }
        }
       public ActionResult Anular(int IdCargo=0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                return View(bus_cargo.get_info(IdEmpresa, IdCargo));

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
    }

    public class ro_cargo_List
    {
        string Variable = "ro_cargo_Info";
        public List<ro_cargo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_cargo_Info> list = new List<ro_cargo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_cargo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_cargo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}