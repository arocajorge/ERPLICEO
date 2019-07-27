using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class EmpleadoTituloController : Controller
    {
        ro_empleado_x_titulos_Bus bus_cargo = new ro_empleado_x_titulos_Bus();
        ro_catalogo_Bus bus_catalogo = new ro_catalogo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        int IdEmpresa = 0;
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_empleado_titulo()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                List<ro_empleado_x_titulos_Info> model = bus_cargo.get_list(IdEmpresa);
                return PartialView("_GridViewPartial_empleado_titulo", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_empleado_x_titulos_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    info.IdUsuario = Session["IdUsuario"].ToString();
                    if (!bus_cargo.guardarDB(info))
                    {
                        cargar_combos();
                        return View(info);
                    }
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
                ro_empleado_x_titulos_Info info = new ro_empleado_x_titulos_Info();
                cargar_combos();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_empleado_x_titulos_Info info)
        {
            try
            {
                info.IdEmpresa = GetIdEmpresa();
                if (ModelState.IsValid)
                {
                    if (!bus_cargo.modificarDB(info))
                    {
                        cargar_combos();
                        return View(info);
                    }
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

        public ActionResult Modificar(decimal IdEmpleado, int Secuencia = 0)
        {
            try
            {
                cargar_combos();
                return View(bus_cargo.get_info(IdEmpresa, IdEmpleado, Secuencia));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_empleado_x_titulos_Info info)
        {
            try
            {
                info.IdEmpresa = GetIdEmpresa();
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
        public ActionResult Anular(decimal IdEmpleado, int Secuencia = 0)
        {
            try
            {
                cargar_combos();
                IdEmpresa = GetIdEmpresa();
                return View(bus_cargo.get_info(IdEmpresa, IdEmpleado, Secuencia));

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
                IdEmpresa = GetIdEmpresa();
                ViewBag.lst_empleado = bus_empleado.get_list_combo(IdEmpresa);
                ViewBag.lst_institucion = bus_catalogo.get_list_x_tipo(5);
                ViewBag.lst_titulo = bus_catalogo.get_list_x_tipo(4);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}