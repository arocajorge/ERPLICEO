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
    public class DepartamentoController : Controller
    {
        // GET: RRHH/Departamento
        int IdEmpresa = 0;

        ro_departamento_Bus bus_departamento = new ro_departamento_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_departamento()
        {
            try
            {
                List<ro_departamento_Info> model = bus_departamento.get_list(GetIdEmpresa(), true);
                return PartialView("_GridViewPartial_departamento", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_departamento_Info info)
        {
            try
            {
                info.IdUsuario = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_departamento.guardarDB(info))
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
                ro_departamento_Info info = new ro_departamento_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_departamento_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    if (!bus_departamento.modificarDB(info))
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

        public ActionResult Modificar(int IdDepartamento = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                return View(bus_departamento.get_info(IdEmpresa, IdDepartamento));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_departamento_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;

                if (!bus_departamento.anularDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
              
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdDepartamento = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                return View(bus_departamento.get_info(IdEmpresa, IdDepartamento));

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

    public class ro_departamento_List
    {
        string Variable = "ro_departamento_Info";
        public List<ro_departamento_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_departamento_Info> list = new List<ro_departamento_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_departamento_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_departamento_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}