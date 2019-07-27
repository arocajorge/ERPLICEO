using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Contabilidad.Controllers
{
    public class CierrePorModuloPorSucursalController : Controller
    {
        // GET: Contabilidad/CierrePorModuloPorSucursal
        #region Variables
        ct_CierrePorModuloPorSucursal_Bus bus_CierreModulo = new ct_CierrePorModuloPorSucursal_Bus();
        tb_sucursal_Bus bus_Sucursal = new tb_sucursal_Bus();
        tb_modulo_Bus bus_modulo = new tb_modulo_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
            };

            cargar_filtros(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            cargar_filtros(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CierrePorModuloPorSucursal(int IdSucursal=0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);

            List<ct_CierrePorModuloPorSucursal_Info> model = bus_CierreModulo.GetList(IdEmpresa, IdSucursal, true);
            return PartialView("_GridViewPartial_CierrePorModuloPorSucursal", model);
        }
        #endregion

        #region Metodos
        private void cargar_filtros(int IdEmpresa)
        {
            try
            {
                var lst_Sucursal = bus_Sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
                ViewBag.lst_Sucursal = lst_Sucursal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void cargar_combos(int IdEmpresa)
        {
            try
            {
                var lst_Sucursal = bus_Sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
                ViewBag.lst_Sucursal = lst_Sucursal;

                var lst_Modulo = bus_modulo.get_list();
                ViewBag.lst_Modulo = lst_Modulo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            ct_CierrePorModuloPorSucursal_Info model = new ct_CierrePorModuloPorSucursal_Info();
            model.FechaIni = DateTime.Now;
            model.FechaFin = DateTime.Now;
            cargar_combos(IdEmpresa);
            return View(model);

        }

        [HttpPost]
        public ActionResult Nuevo(ct_CierrePorModuloPorSucursal_Info model)
        {
            if (!bus_CierreModulo.GuardarBD(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdCierre = 0)
        {
            ct_CierrePorModuloPorSucursal_Info model = bus_CierreModulo.GetInfo(IdEmpresa, IdCierre);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ct_CierrePorModuloPorSucursal_Info model)
        {
            if (!bus_CierreModulo.ModificarBD(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdCierre = 0)
        {
            ct_CierrePorModuloPorSucursal_Info model = bus_CierreModulo.GetInfo(IdEmpresa, IdCierre);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ct_CierrePorModuloPorSucursal_Info model)
        {
            if (!bus_CierreModulo.AnularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}