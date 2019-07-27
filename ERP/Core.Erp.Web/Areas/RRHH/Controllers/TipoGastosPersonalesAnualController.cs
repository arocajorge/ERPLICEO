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
    public class TipoGastosPersonalesAnualController : Controller
    {
        ro_tipo_gastos_personales_maxim_x_anio_Bus bus_gastos = new ro_tipo_gastos_personales_maxim_x_anio_Bus();

        #region Variables
        ro_tipo_gastos_personales_Bus bus_tipo_gasto = new ro_tipo_gastos_personales_Bus();
        #endregion
        public ActionResult Index(string IdTipoGasto)
        {
            ViewBag.IdTipoGasto = IdTipoGasto;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tipo_gastos_personal_anual(string IdTipoGasto)
        {
            try
            {
                cargar_combo();
                int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                List<ro_tipo_gastos_personales_maxim_x_anio_Info> model = bus_gastos.get_list(IdTipoGasto);
                ViewBag.IdTipoGasto = IdTipoGasto;
                return PartialView("_GridViewPartial_tipo_gastos_personal_anual", model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(ro_tipo_gastos_personales_maxim_x_anio_Info info)
        {
            try
            {
                ViewBag.IdTipoGasto = info.IdTipoGasto;
                info.IdUsuario = SessionFixed.IdUsuario;
                if (ModelState.IsValid)
                {
                    var model = bus_gastos.si_existe(info.IdTipoGasto, info.AnioFiscal);
                    if (model != null)
                    {
                        ViewBag.mensaje = "El codigo ya se encuentra registrado para este año fiscal";
                        return View(model);
                    }
                    if (!bus_gastos.guardarDB(info))
                        return View(info);
                    else
                        return RedirectToAction("Index", new { IdTipoGasto = info.IdTipoGasto });
                }
                else
                    return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Nuevo(string IdTipoGasto)
        {
            try
            {
                cargar_combo();
                   ro_tipo_gastos_personales_maxim_x_anio_Info info = new ro_tipo_gastos_personales_maxim_x_anio_Info();
                info.AnioFiscal = DateTime.Now.Year;
                ViewBag.IdTipoGasto = IdTipoGasto;
                return View(info);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Modificar(ro_tipo_gastos_personales_maxim_x_anio_Info info)
        {
            try
            {
                info.IdUsuarioUltMod = SessionFixed.IdUsuario;
                ViewBag.IdTipoGasto = info.IdTipoGasto;

                if (ModelState.IsValid)
                {
                    if (!bus_gastos.modificarDB(info))
                    {
                        cargar_combo();
                        return View(info);
                    }
                    else
                        return RedirectToAction("Index", new { IdTipoGasto =info.IdTipoGasto});
                }
                else
                {
                    cargar_combo();

                    return View(info);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Modificar(int IdGasto)
        {
            try
            {
                cargar_combo();


                return View(bus_gastos.get_info(IdGasto));

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult Anular(ro_tipo_gastos_personales_maxim_x_anio_Info info)
        {
            try
            {
                info.IdUsuarioUltAnu = SessionFixed.IdUsuario;
                ViewBag.IdTipoGasto = info.IdTipoGasto;

                if (!bus_gastos.anularDB(info))
                {
                    cargar_combo();

                    return View(info);
                }
                else
                    return RedirectToAction("Index", new { IdTipoGasto = info.IdTipoGasto });
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Anular(int IdGasto)
        {
            try
            {
                cargar_combo();

                return View(bus_gastos.get_info(IdGasto));

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cargar_combo()
        {
            try
            {
                var list_tipo_gasto = bus_tipo_gasto.get_list(false);
                ViewBag.list_tipo_gasto = list_tipo_gasto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}