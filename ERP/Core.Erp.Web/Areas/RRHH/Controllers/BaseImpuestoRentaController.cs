using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class BaseImpuestoRentaController : Controller
    {
        ro_tabla_Impu_Renta_Bus bus_division = new ro_tabla_Impu_Renta_Bus();
        // GET: RRHH/Division
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_base_impuesto_renta()
        {
            try
            {
                List<ro_tabla_Impu_Renta_Info> model = bus_division.get_list();
                return PartialView("_GridViewPartial_base_impuesto_renta", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_tabla_Impu_Renta_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                    if (!bus_division.guardarDB(info))
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
                ro_tabla_Impu_Renta_Info info = new ro_tabla_Impu_Renta_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_tabla_Impu_Renta_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!bus_division.modificarDB(info))
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
        public ActionResult Modificar(int AnioFiscal, int Secuencia = 0)
        {
            try
            {

                return View(bus_division.get_info( AnioFiscal, Secuencia));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Anular(ro_tabla_Impu_Renta_Info info)
        {
            try
            {

                if (!bus_division.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int AnioFiscal, int Secuencia = 0)
        {
            try
            {

                return View(bus_division.get_info(AnioFiscal, Secuencia));

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}