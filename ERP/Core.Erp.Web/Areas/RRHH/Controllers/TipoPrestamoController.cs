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
    public class TipoPrestamoController : Controller
    {
        ro_tipo_prestamo_Bus bus_tipo_prestamo = new ro_tipo_prestamo_Bus();
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_prestamo()
        {
            try
            {
                List<ro_tipo_prestamo_Info> model = bus_tipo_prestamo.get_list(GetIdEmpresa(), true);
                return PartialView("_GridViewPartial_tipo_prestamo", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_tipo_prestamo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.IdEmpresa = GetIdEmpresa();
                    if (!bus_tipo_prestamo.guardarDB(info))
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
                ro_tipo_prestamo_Info info = new ro_tipo_prestamo_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_tipo_prestamo_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!bus_tipo_prestamo.modificarDB(info))
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
        public ActionResult Modificar(int IdTipoPrestamo = 0)
        {
            try
            {

                return View(bus_tipo_prestamo.get_info(GetIdEmpresa(), IdTipoPrestamo));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Anular(ro_tipo_prestamo_Info info)
        {
            try
            {
                    if (!bus_tipo_prestamo.anularDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdTipoPrestamo = 0)
        {
            try
            {

                return View(bus_tipo_prestamo.get_info(GetIdEmpresa(), IdTipoPrestamo));

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