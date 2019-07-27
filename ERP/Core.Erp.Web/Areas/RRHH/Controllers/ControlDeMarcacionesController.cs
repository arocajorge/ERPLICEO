using Core.Erp.Bus.General;
using Core.Erp.Bus.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using Core.Erp.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class ControlDeMarcacionesController : Controller
    {
        #region Variables
        ro_SancionesPorMarcaciones_Bus bus_sanciones = new ro_SancionesPorMarcaciones_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_SancionesPorMarcaciones_det_Bus bus_sanciones_det = new ro_SancionesPorMarcaciones_det_Bus();
        ro_SancionesPorMarcaciones_det_Info_list ro_SancionesPorMarcaciones_det_Info_list = new ro_SancionesPorMarcaciones_det_Info_list();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_Parametros_Bus bus_parametro = new ro_Parametros_Bus();
        ro_SancionesPorMarcaciones_x_novedad_Info_list ro_SancionesPorMarcaciones_x_novedad_Info_list = new ro_SancionesPorMarcaciones_x_novedad_Info_list();
        List<ro_SancionesPorMarcaciones_det_Info> lst_grabar = new List<ro_SancionesPorMarcaciones_det_Info>();

        #endregion

        #region Vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            cargar_combos_consulta();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_consulta();

            return View(model);

        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_control_marcaciones(DateTime? Fecha_ini, DateTime? Fecha_fin, decimal? IdSucursal = 0)
        {
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_sanciones.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_control_marcaciones", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_control_marcaciones_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info();
            model.detalle = ro_SancionesPorMarcaciones_det_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_control_marcaciones_det", model);


        }
        #endregion

        #region acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                FechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                FechaFin = DateTime.Now.AddMonths(1).AddDays(-1),
                FechaNovedades=DateTime.Now,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),



            };
            ro_SancionesPorMarcaciones_det_Info_list.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(0);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_SancionesPorMarcaciones_Info model)
        {

            var parametros = bus_parametro.get_info(model.IdEmpresa);
            if (parametros == null)
                parametros = new ro_Parametros_Info();
            model.detalle = ro_SancionesPorMarcaciones_det_Info_list.get_list(model.IdTransaccionSession);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle de marcaciones";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }



            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            //model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_sanciones.guardarDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAjuste = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_SancionesPorMarcaciones_Info model = bus_sanciones.get_info(IdEmpresa, IdAjuste);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_sanciones_det.get_list(IdEmpresa, IdAjuste);
            ro_SancionesPorMarcaciones_det_Info_list.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdNomina_Tipo);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_SancionesPorMarcaciones_Info model)
        {
            model.detalle = ro_SancionesPorMarcaciones_det_Info_list.get_list(model.IdTransaccionSession);
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle de marcaciones";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_sanciones.modificarDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            return RedirectToAction("Index");

        }

        public ActionResult Anular(int IdEmpresa = 0, int IdTransaccion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_SancionesPorMarcaciones_Info model = bus_sanciones.get_info(IdEmpresa, IdTransaccion);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_sanciones_det.get_list(IdEmpresa, IdTransaccion);
            ro_SancionesPorMarcaciones_det_Info_list.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdNomina_Tipo);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_SancionesPorMarcaciones_Info model)
        {
            model.detalle = ro_SancionesPorMarcaciones_det_Info_list.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.Fecha_UltAnu = DateTime.Now;
            if (!bus_sanciones.anularDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            return RedirectToAction("Index");
        }



        public JsonResult get_marcaciones( DateTime FechaInicio , DateTime FechaFin, int IdNomina_Tipo = 0, decimal IdTransaccionSession = 0)
        {
             int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
             var lista=    bus_sanciones_det.get_list(IdEmpresa, IdNomina_Tipo, FechaInicio, FechaFin);
             ro_SancionesPorMarcaciones_det_Info_list.set_list(lista, IdTransaccionSession);
             return Json("", JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GuardarMarcaciones(int IdEmpresa, int IdNomina_Tipo, int IdNomina_TipoLiqui,
            DateTime? FechaInicio, DateTime? FechaFin, DateTime? FechaNovedades, string Observacion=""

            , string Ids = "", decimal IdTransaccionSession = 0)
        {
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info();
            if (Ids != null)
            {
                var lst = ro_SancionesPorMarcaciones_det_Info_list.get_list(IdTransaccionSession);
                string[] array = Ids.Split(',');
                var output = array.GroupBy(q => q).ToList();
                foreach (var item in output)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        var info_add = lst.Where(q => q.Secuencia.ToString() == item.Key).FirstOrDefault();
                        lst_grabar.Add(info_add);
                    }
                }
                model.detalle = new List<ro_SancionesPorMarcaciones_det_Info>();
                model.detalle = lst_grabar;
                model.IdEmpresa = IdEmpresa;
                model.IdNomina_Tipo = IdNomina_Tipo;
                model.IdNomina_TipoLiqui = IdNomina_TipoLiqui;
                model.FechaFin =Convert.ToDateTime( FechaFin);
                model.FechaInicio =Convert.ToDateTime( FechaInicio);
                model.FechaNovedades =Convert.ToDateTime( FechaNovedades);
                model.Observacion = Observacion;
                model.IdUsuario = SessionFixed.IdUsuario;
                bus_sanciones.guardarDB(model);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarMarcaciones(int IdEmpresa,int IdAjuste, int IdNomina_Tipo, int IdNomina_TipoLiqui,
            DateTime? FechaInicio, DateTime? FechaFin, DateTime? FechaNovedades, string Observacion = ""

            , string Ids = "", decimal IdTransaccionSession = 0)
        {
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info();
            if (Ids != null)
            {
                var lst = ro_SancionesPorMarcaciones_det_Info_list.get_list(IdTransaccionSession);
                string[] array = Ids.Split(',');
                var output = array.GroupBy(q => q).ToList();
                foreach (var item in output)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        var info_add = lst.Where(q => q.Secuencia.ToString() == item.Key).FirstOrDefault();
                        lst_grabar.Add(info_add);
                    }
                }
                model.detalle = new List<ro_SancionesPorMarcaciones_det_Info>();
                model.detalle = lst_grabar;
                model.IdEmpresa = IdEmpresa;
                model.IdAjuste = IdAjuste;
                model.IdNomina_Tipo = IdNomina_Tipo;
                model.IdNomina_TipoLiqui = IdNomina_TipoLiqui;
                model.FechaFin = Convert.ToDateTime(FechaFin);
                model.FechaInicio = Convert.ToDateTime(FechaInicio);
                model.FechaNovedades = Convert.ToDateTime(FechaNovedades);
                model.Observacion = Observacion;
                model.IdUsuario = SessionFixed.IdUsuario;
                bus_sanciones.modificarDB(model);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AnularMarcaciones(int IdEmpresa, int IdAjuste, int IdNomina_Tipo, int IdNomina_TipoLiqui,
           DateTime? FechaInicio, DateTime? FechaFin, DateTime? FechaNovedades, string Observacion = ""

           , string Ids = "", decimal IdTransaccionSession = 0)
        {
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info();
            if (Ids != null)
            {
                var lst = ro_SancionesPorMarcaciones_det_Info_list.get_list(IdTransaccionSession);
                string[] array = Ids.Split(',');
                var output = array.GroupBy(q => q).ToList();
                foreach (var item in output)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        var info_add = lst.Where(q => q.Secuencia.ToString() == item.Key).FirstOrDefault();
                        lst_grabar.Add(info_add);
                    }
                }
                model.detalle = new List<ro_SancionesPorMarcaciones_det_Info>();
                model.detalle = lst_grabar;
                model.IdEmpresa = IdEmpresa;
                model.IdAjuste = IdAjuste;
                model.IdNomina_Tipo = IdNomina_Tipo;
                model.IdNomina_TipoLiqui = IdNomina_TipoLiqui;
                model.FechaFin = Convert.ToDateTime(FechaFin);
                model.FechaInicio = Convert.ToDateTime(FechaInicio);
                model.FechaNovedades = Convert.ToDateTime(FechaNovedades);
                model.Observacion = Observacion;
                model.IdUsuario = SessionFixed.IdUsuario;
                bus_sanciones.anularDB(model);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region cargar combos

        private void cargar_combos(int IdNomina_Tipo)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina_Tipo);



            List<ro_periodo_x_ro_Nomina_TipoLiqui_Info> lst_periodos = new List<ro_periodo_x_ro_Nomina_TipoLiqui_Info>();
            ViewBag.lst_periodos = lst_periodos;
        }


        #endregion
        private void cargar_combos_consulta()
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_SancionesPorMarcaciones_det_Info info_det)
        {
            if (ModelState.IsValid)
                ro_SancionesPorMarcaciones_det_Info_list.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info();
            model.detalle = ro_SancionesPorMarcaciones_det_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_control_marcaciones_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_SancionesPorMarcaciones_det_Info info_det)
        {
            ro_SancionesPorMarcaciones_det_Info_list.DeleteRow(Convert.ToInt32(info_det.Secuencia), Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_SancionesPorMarcaciones_Info model = new ro_SancionesPorMarcaciones_Info();
            model.detalle = ro_SancionesPorMarcaciones_det_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_control_marcaciones_det", model);
        }
        #endregion
        
    }

    public class ro_SancionesPorMarcaciones_det_Info_list
    {
        string variable = "ro_SancionesPorMarcaciones_det_Info";
        public List<ro_SancionesPorMarcaciones_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_SancionesPorMarcaciones_det_Info> list = new List<ro_SancionesPorMarcaciones_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_SancionesPorMarcaciones_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_SancionesPorMarcaciones_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(ro_SancionesPorMarcaciones_det_Info info_det, decimal IdTransaccionSession)
        {
            ro_SancionesPorMarcaciones_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.Minutos = info_det.Minutos;
        }


        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_SancionesPorMarcaciones_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

    public class ro_SancionesPorMarcaciones_x_novedad_Info_list
    {
        string variable = "ro_SancionesPorMarcaciones_x_novedad_Info";
        public List<ro_SancionesPorMarcaciones_x_novedad_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_SancionesPorMarcaciones_x_novedad_Info> list = new List<ro_SancionesPorMarcaciones_x_novedad_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_SancionesPorMarcaciones_x_novedad_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_SancionesPorMarcaciones_x_novedad_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(ro_SancionesPorMarcaciones_x_novedad_Info info_det, decimal IdTransaccionSession)
        {
            ro_SancionesPorMarcaciones_x_novedad_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpleado == info_det.IdEmpleado).First();
            //  edited_info.Valor = info_det.Valor;
        }


        public void DeleteRow(decimal IdEmpleado, decimal IdTransaccionSession)
        {
            List<ro_SancionesPorMarcaciones_x_novedad_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdEmpleado == IdEmpleado).First());
        }
    }
}