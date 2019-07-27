using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.RRHH;
using Core.Erp.Bus.RRHH;
using DevExpress.Web.Mvc;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Bus.Contabilidad;

namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class EmpleadoNovedadController : Controller
    {
        #region Variables
        ro_empleado_novedad_Bus bus_novedad = new ro_empleado_novedad_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_empleado_novedad_det_Bus bus_novedad_detalle_bus = new ro_empleado_novedad_det_Bus();
        ro_empleado_novedad_det_lst ro_empleado_novedad_det_lst = new ro_empleado_novedad_det_lst();
        ro_rubro_tipo_Bus bus_rubro = new ro_rubro_tipo_Bus();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        List<ro_rubro_tipo_Info> lst_rubros = new List<ro_rubro_tipo_Info>();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ro_jornada_Bus bus_jornada = new ro_jornada_Bus();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        int IdEmpresa = 0;
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbEmpleado_novedades()
        {
            decimal model = new decimal();
            return PartialView("_CmbEmpleado_novedades", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.EMPLEA.ToString());
        }

        
        public ActionResult CmbRubro_EmpNov()
        {
            ro_empleado_novedad_det_Info model = new  ro_empleado_novedad_det_Info();
            return PartialView("_CmbRubro_EmpNov", model);
        }
        public List<ro_rubro_tipo_Info> get_list_bajo_demanda_rubro(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_rubro.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_rubro_tipo_Info get_info_bajo_demanda_rubro(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_rubro.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }


        public ActionResult CmbSucursal()
        {
            int model = new int();
            return PartialView("_CmbSucursal", model);
        }
        public List<tb_sucursal_Info> get_list_bajo_demanda_sucursal(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_sucursal.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public tb_sucursal_Info get_info_bajo_demanda_sucursal(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_sucursal.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }


        public ActionResult CmbJornada()
        {
            int model = new int();
            return PartialView("_CmbJornada", model);
        }
        public List<ro_jornada_Info> get_list_bajo_demanda_jornada(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_jornada.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ro_jornada_Info get_info_bajo_demanda_jornada(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_jornada.get_info_bajo_demanda(Convert.ToInt32(SessionFixed.IdEmpresa), args);
        }
        #endregion

        #region Vistas
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
        public ActionResult GridViewPartial_empleado_novedad(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;

            var model = bus_novedad.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, ViewBag.IdSucursal);
            return PartialView("_GridViewPartial_empleado_novedad", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_empleado_novedad_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_empleado_novedad_Info model = new ro_empleado_novedad_Info();
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_empleado_novedad_det", model);
        }
        #endregion

        #region acciones
        public ActionResult Nuevo()
        {
            ro_empleado_novedad_Info model = new ro_empleado_novedad_Info
            {
                IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]),
                Fecha = DateTime.Now,
                    IdNomina_Tipo = 1,
                    IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                    lst_novedad_det = new List<ro_empleado_novedad_det_Info>()
            };
            model.lst_novedad_det = new List<ro_empleado_novedad_det_Info>();
            ro_empleado_novedad_det_lst.set_list(model.lst_novedad_det, model.IdTransaccionSession);
            cargar_combos(0);
            cargar_combos_detalle();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_empleado_novedad_Info model)
        {
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(model.IdTransaccionSession);
            if (model.lst_novedad_det == null || model.lst_novedad_det.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la novedad";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }

            foreach (var item in model.lst_novedad_det)
            {
                item.Valor = Math.Round(item.Valor, 2);
                lst_rubros = Session["rubros"] as List<ro_rubro_tipo_Info>;
                if (lst_rubros.Count() > 0)
                {
                    if (lst_rubros.Where(v => v.IdRubro == item.IdRubro && v.rub_acumula_descuento==true && v.ru_tipo=="E").Count() >0)
                    {
                        double sueldo = 0;
                        double valor_acumulado = 0;
                        DateTime fechai = new DateTime(item.FechaPago.Year, item.FechaPago.Month, 1);
                        DateTime fechaf = fechai.AddMonths(1).AddDays(-1);
                        valor_acumulado = (double)bus_novedad_detalle_bus.get_valor_acumulado_del_mes_x_rubro(model.IdEmpresa, model.IdEmpleado, item.IdRubro, fechai, fechaf);
                        sueldo = (double)bus_contrato.get_sueldo_actual(model.IdEmpresa, model.IdEmpleado);
                        if ((valor_acumulado + item.Valor) > (sueldo) * 0.10)
                        {
                            ViewBag.mensaje = "Ha excedido el valor de multa permitido por la ley";
                            cargar_combos(model.IdNomina_Tipo);
                            return View(model);
                        }

                    }
                }
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_novedad.guardarDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpleado = model.IdEmpleado, IdNovedad = model.IdNovedad, Exito = true });
        }

        public ActionResult Modificar(int IdEmpleado, decimal IdNovedad, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_empleado_novedad_Info model = bus_novedad.get_info(IdEmpresa, IdEmpleado, IdNovedad);
            model.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            if (model == null)
                return RedirectToAction("Index");
            model.lst_novedad_det = bus_novedad_detalle_bus.get_list(IdEmpresa, IdEmpleado, IdNovedad);
            ro_empleado_novedad_det_lst.set_list(model.lst_novedad_det, model.IdTransaccionSession);
            cargar_combos_detalle();
            cargar_combos(model.IdNomina_Tipo);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.RRHH, Convert.ToInt32(model.IdSucursal), ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_empleado_novedad_Info model)
        {
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(model.IdTransaccionSession);

            foreach (var item in model.lst_novedad_det)
            {
                item.Valor = Math.Round(item.Valor, 2);
                lst_rubros = Session["rubros"] as List<ro_rubro_tipo_Info>;
                if (lst_rubros.Count() > 0)
                {
                    if (lst_rubros.Where(v => v.IdRubro == item.IdRubro && v.rub_acumula_descuento == true && v.ru_tipo == "E").Count() > 0)
                    {
                        double sueldo = 0;
                        double valor_acumulado = 0;
                        DateTime fechai = new DateTime(item.FechaPago.Year, item.FechaPago.Month, 1);
                        DateTime fechaf = fechai.AddMonths(1).AddDays(-1);
                        valor_acumulado = (double)bus_novedad_detalle_bus.get_valor_acumulado_del_mes_x_rubro(model.IdEmpresa, model.IdEmpleado, item.IdRubro, fechai, fechaf);
                        sueldo = (double)bus_contrato.get_sueldo_actual(model.IdEmpresa, model.IdEmpleado);
                        if ((valor_acumulado + item.Valor) > (sueldo) * 0.10)
                        {
                            ViewBag.mensaje = "Ha excedido el valor de multa permitido por la ley";
                            cargar_combos(model.IdNomina_Tipo);
                            return View(model);
                        }

                    }
                }
            }
            if (model.lst_novedad_det == null || model.lst_novedad_det.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para la planificación";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_novedad.modificarDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            
            return RedirectToAction("Modificar", new { IdEmpleado = model.IdEmpleado, IdNovedad = model.IdNovedad, Exito = true });
        }

        public ActionResult Anular(int IdEmpleado, decimal IdNovedad)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ro_empleado_novedad_Info model = bus_novedad.get_info(IdEmpresa, IdEmpleado, IdNovedad);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_novedad_det = bus_novedad_detalle_bus.get_list(IdEmpresa, IdEmpleado, IdNovedad);
            ro_empleado_novedad_det_lst.set_list(model.lst_novedad_det, model.IdTransaccionSession);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.RRHH, Convert.ToInt32(model.IdSucursal), ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos(model.IdNomina_Tipo);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_empleado_novedad_Info model)
        {
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.Fecha_UltAnu = DateTime.Now;
            if (!bus_novedad.anularDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region cargar combos

        private void cargar_combos(int IdNomina)
        {
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.lst_nomina = bus_nomina.get_list(IdEmpresa, false);
            ViewBag.lst_nomina_tipo = bus_nomina_tipo.get_list(IdEmpresa, IdNomina);
        }
        #endregion

        #region funciones del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_novedad_det_Info info_det)
        {
            if (ModelState.IsValid)
                ro_empleado_novedad_det_lst.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_empleado_novedad_Info model = new ro_empleado_novedad_Info();
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_empleado_novedad_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_novedad_det_Info info_det)
        {
            if (ModelState.IsValid)
                ro_empleado_novedad_det_lst.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_empleado_novedad_Info model = new ro_empleado_novedad_Info();
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_empleado_novedad_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_empleado_novedad_det_Info info_det)
        {
            ro_empleado_novedad_det_lst.DeleteRow(info_det.Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_empleado_novedad_Info model = new ro_empleado_novedad_Info();
            model.lst_novedad_det = ro_empleado_novedad_det_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_empleado_novedad_det", model);
        }
        #endregion

        #region Json
        public JsonResult getTipoNominaEmpleado(int IdEmpresa = 0, int IdEmpleado = 0)
        {
            var IdNomina_Tipo = 0;

            var info_empleado = bus_contrato.get_info_contrato_empleado(IdEmpresa, IdEmpleado);
            IdNomina_Tipo = info_empleado== null ? 0 : Convert.ToInt32(info_empleado.IdNomina);
       
            return Json(IdNomina_Tipo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            lst_rubros= bus_rubro.get_list_rub_concepto(IdEmpresa);
            ViewBag.lst_rubro = lst_rubros;
            Session["rubros"]=lst_rubros;
        }

       
    }

    public class ro_empleado_novedad_det_lst
    {
        string Variable = "ro_empleado_novedad_det_Info";
        public List<ro_empleado_novedad_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_empleado_novedad_det_Info> list = new List<ro_empleado_novedad_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_empleado_novedad_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_empleado_novedad_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ro_empleado_novedad_det_Info info_det, decimal IdTransaccionSession)
        {
            ro_rubro_tipo_Bus bus_rub = new ro_rubro_tipo_Bus();
            var info_rubro = bus_rub.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdRubro);
            List<ro_empleado_novedad_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.ru_descripcion = info_rubro.ru_descripcion;

            list.Add(info_det);
        }

        public void UpdateRow(ro_empleado_novedad_det_Info info_det, decimal IdTransaccionSession)
        {
            ro_rubro_tipo_Bus bus_rub = new ro_rubro_tipo_Bus();
            var info_rubro = bus_rub.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdRubro);
            ro_empleado_novedad_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdNovedad = info_det.IdNovedad;
            edited_info.IdRubro = info_det.IdRubro;
            edited_info.CantidadHoras = info_det.CantidadHoras;
            edited_info.Valor = info_det.Valor;
            edited_info.Observacion = info_det.Observacion;
            edited_info.ru_descripcion = info_rubro.ru_descripcion;
            edited_info.FechaPago = info_det.FechaPago;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ro_empleado_novedad_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}
