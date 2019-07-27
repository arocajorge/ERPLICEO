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
using Core.Erp.Bus.Banco;
using Core.Erp.Bus.General;
using Core.Erp.Bus.CuentasPorPagar;
namespace Core.Erp.Web.Areas.RRHH.Controllers
{
    public class PagosEnChequesController : Controller
    {
        #region Variables
        ro_NominasPagosCheques_Bus bus_archivo = new ro_NominasPagosCheques_Bus();
        ro_nomina_tipo_Bus bus_nomina = new ro_nomina_tipo_Bus();
        ro_Nomina_Tipoliquiliqui_Bus bus_nomina_tipo = new ro_Nomina_Tipoliquiliqui_Bus();
        ro_NominasPagosCheques_det_Bus bus_pago_detalle = new ro_NominasPagosCheques_det_Bus();
        ro_NominasPagosCheques_det_Info_list ro_NominasPagosCheques_det_Info_list = new ro_NominasPagosCheques_det_Info_list();
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_Parametros_Bus bus_parametro = new ro_Parametros_Bus();
        cp_orden_pago_tipo_x_empresa_Bus bus_tipo_op = new cp_orden_pago_tipo_x_empresa_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
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
        public ActionResult GridViewPartial_pagos_cheques(DateTime? Fecha_ini, DateTime? Fecha_fin, decimal? IdSucursal = 0)
        {
            ViewBag.Fecha_ini = Fecha_ini == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_archivo.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, true);
            return PartialView("_GridViewPartial_pagos_cheques", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_pagos_cheques_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ro_NominasPagosCheques_Info model = new ro_NominasPagosCheques_Info();
            model.detalle = ro_NominasPagosCheques_det_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_pagos_cheques_det", model);


        }
        #endregion

        #region acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdNomina_Tipo_Tipo = 0, int IdNomina_Tipo_TipoLiqui = 0, int IdPeriodo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_NominasPagosCheques_Info model = new ro_NominasPagosCheques_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdNomina_Tipo = IdNomina_Tipo_Tipo,
                IdNomina_TipoLiqui = IdNomina_Tipo_TipoLiqui,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),



            };
            ro_NominasPagosCheques_det_Info_list.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(0);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ro_NominasPagosCheques_Info model)
        {

            var parametros = bus_parametro.get_info(model.IdEmpresa);
            if (parametros == null)
                parametros = new ro_Parametros_Info();
            var tipo_op = bus_tipo_op.get_info(model.IdEmpresa, parametros.IdTipo_op_sueldo_por_pagar);
            model.detalle = ro_NominasPagosCheques_det_Info_list.get_list(model.IdTransaccionSession);
            if(tipo_op==null)
            {
                ViewBag.mensaje = "No existe parametros para las ordenes de pagos";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            else
            {
                if(tipo_op.IdCtaCble==null)
                {
                    ViewBag.mensaje = "No existe cuenta contable en tipo de orden de pago";
                    cargar_combos(model.IdNomina_Tipo);
                    return View(model);

                }
            }
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para el pago";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            else
            {
                foreach (var item in model.detalle)
                {
                    if( item.IdCtaCble_Emplea==null || item.IdCtaCble_Emplea=="" )
                    {
                        ViewBag.mensaje = "El empleado "+ item.pe_nombreCompleto+" no tiene cuenta contable";
                        cargar_combos(model.IdNomina_Tipo);
                        return View(model);
                    }
                }
            }



            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_archivo.guardarDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTransaccion = model.IdTransaccion, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdTransaccion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_NominasPagosCheques_Info model = bus_archivo.get_info(IdEmpresa, IdTransaccion);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_pago_detalle.get_list(IdEmpresa, IdTransaccion);
            ro_NominasPagosCheques_det_Info_list.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdNomina_Tipo);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ro_NominasPagosCheques_Info model)
        {
            model.detalle = ro_NominasPagosCheques_det_Info_list.get_list(model.IdTransaccionSession);
            if (model.detalle == null || model.detalle.Count() == 0)
            {
                ViewBag.mensaje = "No existe detalle para el arhivo";
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioAnu = Session["IdUsuario"].ToString();
            if (!bus_archivo.modificarDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTransaccion = model.IdTransaccion, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdTransaccion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ro_NominasPagosCheques_Info model = bus_archivo.get_info(IdEmpresa, IdTransaccion);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            if (model == null)
                return RedirectToAction("Index");
            model.detalle = bus_pago_detalle.get_list(IdEmpresa, IdTransaccion);
            ro_NominasPagosCheques_det_Info_list.set_list(model.detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdNomina_Tipo);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ro_NominasPagosCheques_Info model)
        {
            model.detalle = ro_NominasPagosCheques_det_Info_list.get_list(model.IdTransaccionSession);

            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            model.IdUsuarioAnu = Session["IdUsuario"].ToString();
            model.FechaAnu = DateTime.Now;
            if (!bus_archivo.anularDB(model))
            {
                cargar_combos(model.IdNomina_Tipo);
                return View(model);
            }
            return RedirectToAction("Index");
        }


   
        public JsonResult CargarEmpleados( int IdNomina_Tipo = 0, int IdNomina_TipoLiqui = 0, int IdPeriodo = 0, decimal IdTransaccionSession = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            string TipoCuenta = cl_enumeradores.eTipoCuentaRRHH.CHE+"," + cl_enumeradores.eTipoCuentaRRHH.EFE;
            var detalle = bus_pago_detalle.get_list(IdEmpresa, IdNomina_Tipo, IdNomina_TipoLiqui, IdPeriodo, TipoCuenta);
            ro_NominasPagosCheques_det_Info_list.set_list(detalle, IdTransaccionSession);
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
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }
        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ro_NominasPagosCheques_det_Info info_det)
        {
            if (ModelState.IsValid)
                ro_NominasPagosCheques_det_Info_list.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_NominasPagosCheques_Info model = new ro_NominasPagosCheques_Info();
            model.detalle = ro_NominasPagosCheques_det_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_transferencia_det", model);
        }

        public ActionResult EditingDelete([ModelBinder(typeof(DevExpressEditorsBinder))] ro_NominasPagosCheques_det_Info info_det)
        {
            ro_NominasPagosCheques_det_Info_list.DeleteRow(Convert.ToDecimal( info_det.IdEmpleado), Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ro_NominasPagosCheques_Info model = new ro_NominasPagosCheques_Info();
            model.detalle = ro_NominasPagosCheques_det_Info_list.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_archivo_transferencia_det", model);
        }
        #endregion



    }


    public class ro_NominasPagosCheques_det_Info_list
    {
        string variable = "ro_NominasPagosCheques_det_Info";
        public List<ro_NominasPagosCheques_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ro_NominasPagosCheques_det_Info> list = new List<ro_NominasPagosCheques_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ro_NominasPagosCheques_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ro_NominasPagosCheques_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(ro_NominasPagosCheques_det_Info info_det, decimal IdTransaccionSession)
        {
            ro_NominasPagosCheques_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpleado == info_det.IdEmpleado).First();
            edited_info.Valor = info_det.Valor;
        }


        public void DeleteRow(decimal IdEmpleado, decimal IdTransaccionSession)
        {
            List<ro_NominasPagosCheques_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdEmpleado == IdEmpleado).First());
        }
    }
}