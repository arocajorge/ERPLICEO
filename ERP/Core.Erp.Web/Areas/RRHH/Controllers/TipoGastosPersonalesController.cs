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
    public class TipoGastosPersonalesController : Controller
    {
        ro_tipo_gastos_personales_Bus bus_gastos = new ro_tipo_gastos_personales_Bus();
        // GET: RRHH/Division
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_gastos_personales()
        {
            try
            {
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_tipo_gastos_personales_Info> model = bus_gastos.get_list(true);
                return PartialView("_GridViewPartial_tipo_gastos_personales", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_tipo_gastos_personales_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = bus_gastos.get_info(info.IdTipoGasto);
                    if(model!=null){
                        ViewBag.mensaje = "El codigo ya se encuentra registrado";
                        return View(model);
                    }
                    if (!bus_gastos.guardarDB(info))
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
                ro_tipo_gastos_personales_Info info = new ro_tipo_gastos_personales_Info();
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_tipo_gastos_personales_Info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!bus_gastos.modificarDB(info))
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
        public ActionResult Modificar(string IdTipoGasto )
        {
            try
            {

                return View(bus_gastos.get_info(IdTipoGasto));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Anular(ro_tipo_gastos_personales_Info info)
        {
            try
            {

                if (!bus_gastos.anularDB(info))
                    return View(info);
                else
                    return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(string IdTipoGasto )
        {
            try
            {

                return View(bus_gastos.get_info(IdTipoGasto));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}