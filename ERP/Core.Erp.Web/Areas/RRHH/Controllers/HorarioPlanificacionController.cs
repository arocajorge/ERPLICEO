using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Bus.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Info.General;
using DevExpress.Web;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class HorarioPlanificacionController : Controller
    {
        #region variables
        ro_horario_planificacion_Bus bus_planificacion = new ro_horario_planificacion_Bus();
        ro_horario_planificacion_det_Bus bus_planificacion_det = new ro_horario_planificacion_det_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_horario_Bus bus_horario = new ro_horario_Bus();
        ro_horario_planificacion_det_lst lst_planificacion_det = new ro_horario_planificacion_det_lst();
        List<ro_horario_planificacion_Info> lst_detalle = new List<ro_horario_planificacion_Info>();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_division_Bus bus_division = new ro_division_Bus();
        ro_area_Bus bus_area = new ro_area_Bus();
        ro_departamento_Bus bus_departamento = new ro_departamento_Bus();
        ro_cargo_Bus bus_cargo = new ro_cargo_Bus();
        ro_horario_planificacion_Info model = new ro_horario_planificacion_Info();
        int IdEmpresa = 0;
        #endregion
        #region Combo bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_Planificacion()
        {
            ro_horario_planificacion_det_Info model = new ro_horario_planificacion_det_Info();
            return PartialView("_CmbEmpleado_Planificacion", model);
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
        #region Acciones
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                fecha_ini = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                fecha_fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);

        }

        [ValidateInput(false)]      
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cargar_combos();
            ro_horario_planificacion_Info model = new ro_horario_planificacion_Info
            {

                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(30),
                lst_planificacion_det = new List<ro_horario_planificacion_det_Info>(),
                IdHorario = 1

            };
            lst_planificacion_det.set_list(model.lst_planificacion_det, model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ro_horario_planificacion_Info model)
        {
            if (model == null)
                return View(model);
            model.IdEmpresa=Convert.ToInt32( Session["IdEmpresa"].ToString());
            model.lst_planificacion_det = lst_planificacion_det.get_list(model.IdTransaccionSession);
            if (model.lst_planificacion_det == null || model.lst_planificacion_det.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificacion";
                cargar_combos();
                return View(model);
            }
            if (bus_planificacion.guardarDB(model))
                return RedirectToAction("Index");
            else return View(model);
        }
        public ActionResult Modificar(int IdPlanificacion)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cargar_combos();
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model = bus_planificacion.get_info(IdEmpresa, IdPlanificacion);

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_planificacion_det = bus_planificacion_det.get_list(IdEmpresa, IdPlanificacion);
            lst_planificacion_det.set_list(model.lst_planificacion_det, model.IdTransaccionSession);
            return View(bus_planificacion.get_info(IdEmpresa, IdPlanificacion));
        }
        [HttpPost]
        public ActionResult Modificar(ro_horario_planificacion_Info model)
        {
            model.lst_planificacion_det = lst_planificacion_det.get_list(model.IdTransaccionSession);
            if (model.lst_planificacion_det == null || model.lst_planificacion_det.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificacion";
                cargar_combos();
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_planificacion.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Anular(int IdPlanificacion)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cargar_combos();
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model = bus_planificacion.get_info(IdEmpresa, IdPlanificacion);
            lst_planificacion_det.set_list(model.lst_planificacion_det, model.IdTransaccionSession);
            return View(bus_planificacion.get_info(IdEmpresa, IdPlanificacion));
        }
        [HttpPost]
        public ActionResult Anular(ro_horario_planificacion_Info model)
        {
            model.lst_planificacion_det = lst_planificacion_det.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuario = Session["IdUsuario"].ToString();
            if (!bus_planificacion.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Detalle

        public JsonResult Consultar(DateTime fi, DateTime ff, int IdNomina=0, int IdSucursal=0, int IdDivision=0, int IdArea=0, int IdDepartamento=0, int IdCargo=0,decimal IdEmpleado = 0 ,int IdHorario=0, decimal IdTransaccionSession = 0)
        {
            IdEmpresa =Convert.ToInt32( Session["IdEmpresa"].ToString());
            var model = bus_planificacion.get_list(IdEmpresa, IdNomina,IdSucursal,IdDivision,IdArea,IdDepartamento,IdCargo, IdEmpleado, fi,ff,IdHorario);
            lst_planificacion_det.set_list(model.lst_planificacion_det, IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
    }


    [ValidateInput(false)]
        public ActionResult GridViewPartial_horario_planificacion(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(Fecha_fin);

            lst_detalle = bus_planificacion.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_horario_planificacion", lst_detalle);
        }

        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_horario_planificacion_det_Info info_det)
        {
            if (ModelState.IsValid)
                lst_planificacion_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_horario_planificacion_Info model = new ro_horario_planificacion_Info();
            model.lst_planificacion_det = lst_planificacion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_horario_planificacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_horario_planificacion_det_Info info_det)
        {
            if (ModelState.IsValid)
                lst_planificacion_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_horario_planificacion_Info model = new ro_horario_planificacion_Info();
            model.lst_planificacion_det = lst_planificacion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_horario_planificacion_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            lst_planificacion_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_horario_planificacion_Info model = new ro_horario_planificacion_Info();
            model.lst_planificacion_det = lst_planificacion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_horario_planificacion_det", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_horario_planificacion_det()
        {
            cargar_combos_detalle();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ro_horario_planificacion_Info model = new ro_horario_planificacion_Info();
            model.lst_planificacion_det = lst_planificacion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_horario_planificacion_det", model);
        }
        #endregion
        #region Combos
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ViewBag.lst_horario = bus_horario.get_list(IdEmpresa, false);
        }
        private void cargar_combos()
        {
            IdEmpresa = Convert.ToInt32(Session["IdEmpresa"].ToString());
            ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_division = bus_division.get_list(IdEmpresa, false);
            ViewBag.lst_area = bus_area.get_list(IdEmpresa, false);
            ViewBag.lst_departamento = bus_departamento.get_list(IdEmpresa, false);
            ViewBag.lst_cargo = bus_cargo.get_list(IdEmpresa, false);
            ViewBag.lst_horario = bus_horario.get_list(IdEmpresa, false);

        }

        #endregion

    }

    public class ro_horario_planificacion_det_lst
    {
        string Variable = "ro_horario_planificacion_det_Info";

        public List<ro_horario_planificacion_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_horario_planificacion_det_Info> list = new List<ro_horario_planificacion_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_horario_planificacion_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_horario_planificacion_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_horario_planificacion_det_Info info_det, decimal IdTransaccionSession)
        {
            List<ro_horario_planificacion_det_Info> list = get_list(IdTransaccionSession);
            list.Add(info_det);
        }

        public void UpdateRow(ro_horario_planificacion_det_Info info_det, decimal IdTransaccionSession)
        {

            ro_horario_planificacion_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdHorario = info_det.IdHorario;
        }

        public void DeleteRow( int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_horario_planificacion_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}