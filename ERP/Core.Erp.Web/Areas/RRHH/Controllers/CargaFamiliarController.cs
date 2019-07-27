using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Bus.General;
namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class CargaFamiliarController : Controller
    {
        ro_cargaFamiliar_Bus bus_cargo = new ro_cargaFamiliar_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        tb_Catalogo_Bus bus_catalogo_general = new tb_Catalogo_Bus();
        int IdEmpresa = 0;
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cargas(decimal IdEmpleado = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                ViewBag.IdEmpleado = IdEmpleado;
                bus_cargo = new ro_cargaFamiliar_Bus();
                List<ro_cargaFamiliar_Info> model = bus_cargo.get_list(IdEmpresa, IdEmpleado);
                return PartialView("_GridViewPartial_cargas", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_cargaFamiliar_Info info)
        {
            try
            {

                ViewBag.IdEmpleado = info.IdEmpleado;
                IdEmpresa = GetIdEmpresa();
                info.IdEmpresa = IdEmpresa;
                if (ModelState.IsValid)
                {
                    if (!bus_cargo.guardarDB(info))
                    {
                        cargar_combos();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index", new { IdEmpleado = info.IdEmpleado });
                    
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo(int IdEmpleado = 0)
        {
            try
            {
                ro_cargaFamiliar_Info model = new ro_cargaFamiliar_Info
                {
                    IdEmpleado = IdEmpleado
                };
                ViewBag.IdEmpleado = IdEmpleado;
                cargar_combos();
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_cargaFamiliar_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();                   
                    if (!bus_cargo.modificarDB(info))
                    {
                        cargar_combos();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index", new { IdEmpleado = info.IdEmpleado });
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Modificar(int IdEmpleado, int IdCargaFamiliar = 0)
        {
            try
            {
                cargar_combos();
                IdEmpresa = GetIdEmpresa();
                ViewBag.IdEmpleado = IdEmpleado;
                return View(bus_cargo.get_info(IdEmpresa, IdEmpleado, IdCargaFamiliar));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_cargaFamiliar_Info info)
        {
            try
            {
                if (!bus_cargo.anularDB(info))
                {
                    cargar_combos();
                    return View(info);
                }
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdEmpleado, int IdCargaFamiliar = 0)
        {
            try
            {
                cargar_combos();
                IdEmpresa = GetIdEmpresa();
                ViewBag.IdEmpleado = IdEmpleado;
                return View(bus_cargo.get_info(IdEmpresa, IdEmpleado, IdCargaFamiliar));

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
                
                ViewBag.lst_tipo_familiar = bus_catalogo.get_list_x_tipo(3);
                ViewBag.lst_sexo = bus_catalogo_general.get_list(1, false);

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

    public class ro_cargaFamiliar_List
    {
        string Variable = "ro_cargaFamiliar_Info";
        public List<ro_cargaFamiliar_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_cargaFamiliar_Info> list = new List<ro_cargaFamiliar_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_cargaFamiliar_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_cargaFamiliar_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}