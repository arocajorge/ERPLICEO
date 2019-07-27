using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PermisoEmpleadoController : Controller
    {
        ro_permiso_x_empleado_Bus bus_permiso = new ro_permiso_x_empleado_Bus();
        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_permiso()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_permiso", model);
        }
        public ActionResult CmbEmpleado_aprueba_permiso()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_aprueba_permiso", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        #endregion
        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_permiso_emp()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_permiso.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_permiso_emp", model);
        }
        private void cargar_combos()
        {
            var list_tipo_permiso = from cl_enumeradores.eTipoPermisoRRHH s in Enum.GetValues(typeof(cl_enumeradores.eTipoPermisoRRHH))
                           select s ;
            ViewBag.list_tipo_permiso = list_tipo_permiso;
        }

        #endregion
        #region Acciones
        public ActionResult Nuevo()
        {
            ro_permiso_x_empleado_Info model = new ro_permiso_x_empleado_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                HoraRegreso = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                HoraSalida = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                FechaInicio = DateTime.Now.Date,
                FechaFin = DateTime.Now.Date
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ro_permiso_x_empleado_Info model)
        {
            if (!bus_permiso.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdPermiso = 0)
        {
            ro_permiso_x_empleado_Info model = bus_permiso.get_info(IdEmpresa, IdPermiso);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ro_permiso_x_empleado_Info model)
        {
            if (!bus_permiso.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdPermiso = 0)
        {
            ro_permiso_x_empleado_Info model = bus_permiso.get_info(IdEmpresa, IdPermiso);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_permiso_x_empleado_Info model)
        {
            if (!bus_permiso.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }

        #endregion
    }
}