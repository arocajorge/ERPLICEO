using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class CatalogoTipoRRHHController : Controller
    {
        ro_catalogoTipo_Bus bus_catalogoTipo = new ro_catalogoTipo_Bus();
        int IdEmpresa = 0;
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CatalogoTipo()
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                List<ro_catalogoTipo_Info> model = bus_catalogoTipo.get_list(true);
                return PartialView("_GridViewPartial_CatalogoTipo", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Nuevo(ro_catalogoTipo_Info info)
        {
            try
            {
                info.IdUsuario = SessionFixed.IdUsuario; if (ModelState.IsValid)
                {
                    info.IdUsuario = Session["IdUsuario"].ToString();
                    if (!bus_catalogoTipo.guardarDB(info))
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
                ro_catalogoTipo_Info info = new ro_catalogoTipo_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult Modificar(ro_catalogoTipo_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                    if (ModelState.IsValid)
                {
                    info.IdUsuarioUltMod = Session["IdUsuario"].ToString();
                    info.Fecha_UltMod = DateTime.Now;
                    if (!bus_catalogoTipo.modificarDB(info))
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

        public ActionResult Modificar(int IdTipoCatalogo = 0)
        {
            try
            {
                return View(bus_catalogoTipo.get_info( IdTipoCatalogo));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]

        public ActionResult Anular(ro_catalogoTipo_Info info)
        {
            try
            {
                info.IdUsuarioUltAnu = SessionFixed.IdUsuario;
                info.Fecha_UltAnu = DateTime.Now;
                if (!bus_catalogoTipo.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");


            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdTipoCatalogo = 0)
        {
            try
            {
                IdEmpresa = GetIdEmpresa();
                return View(bus_catalogoTipo.get_info(IdTipoCatalogo));

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
}