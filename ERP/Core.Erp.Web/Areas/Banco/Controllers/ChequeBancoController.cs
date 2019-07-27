using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Areas.Contabilidad.Controllers;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class ChequeBancoController : Controller
    {
        #region Variables
        ba_Cbte_Ban_Bus bus_cbteban = new ba_Cbte_Ban_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ba_tipo_nota_Bus bus_tipo_nota = new ba_tipo_nota_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        cp_orden_pago_cancelaciones_Bus bus_cancelaciones = new cp_orden_pago_cancelaciones_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        
        ct_cbtecble_det_List List_ct = new ct_cbtecble_det_List();
        ba_Talonario_cheques_x_banco_Bus bus_talonario = new ba_Talonario_cheques_x_banco_Bus();
        ct_cbtecble_det_Bus bus_det_ct = new ct_cbtecble_det_Bus();
        cl_funciones funciones = new cl_funciones();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        cp_orden_pago_cancelaciones_PorCruzar ListPorCruzar = new cp_orden_pago_cancelaciones_PorCruzar();
        cp_orden_pago_cancelaciones_List List_op = new cp_orden_pago_cancelaciones_List();
        cp_SolicitudPago_Bus bus_solicitud = new cp_SolicitudPago_Bus();
        ba_parametros_Bus bus_param = new ba_parametros_Bus();
        ba_Banco_Flujo_Det_List_Cheque List_Flujo = new ba_Banco_Flujo_Det_List_Cheque();
        ba_Cbte_Ban_x_ba_TipoFlujo_Bus bus_flujo = new ba_Cbte_Ban_x_ba_TipoFlujo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
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
        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "Todos"
            });
            ViewBag.lst_sucursal = lst_sucursal;
        }
        public ActionResult GridViewPartial_cheque(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_cbteban.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, IdSucursal, cl_enumeradores.eTipoCbteBancario.CHEQ.ToString(), true);
            return PartialView("_GridViewPartial_cheque", model);
        }
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbPersona_ChequeBanco()
        {
            SessionFixed.TipoPersona = Request.Params["IdTipoPersona"] != null ? Request.Params["IdTipoPersona"].ToString() : SessionFixed.TipoPersona;
            decimal model = new decimal();
            return PartialView("_CmbPersona_ChequeBanco", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.TipoPersona);
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.TipoPersona);
        }
        #endregion

        #region Metodos ComboBox bajo demanda flujo
        ba_TipoFlujo_Bus bus_tipo = new ba_TipoFlujo_Bus();
        public ActionResult CmbFlujo_Cheque()
        {
            decimal model = new decimal();
            return PartialView("_CmbFlujo_Cheque", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demandaFlujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ba_TipoFlujo_Info get_info_bajo_demandaFlujo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_ciudad = bus_ciudad.get_list("", false);
            ViewBag.lst_ciudad = lst_ciudad;

            Dictionary<string, string> lst_tipo_personas = new Dictionary<string, string>();
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.PERSONA.ToString(), "Persona");
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.PROVEE.ToString(), "Proveedor");
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.EMPLEA.ToString(), "Empleado");
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.CLIENTE.ToString(), "Cliente");
            ViewBag.lst_tipo_personas = lst_tipo_personas;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;
        }
        private bool validar(ba_Cbte_Ban_Info i_validar, ref string msg)
        {
            i_validar.lst_det_canc_op = List_op.get_list(i_validar.IdTransaccionSession);
            i_validar.lst_det_ct = List_ct.get_list(i_validar.IdTransaccionSession);
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.BANCO,i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (i_validar.IdEntidad == 0)
            {
                msg = "Seleccione el beneficiario";
                return false;
            }
            if (i_validar.lst_det_canc_op.Count == 0)
            {
                msg = "Seleccione las órdenes de pago a ser canceladas";
                return false;
            }
            if (i_validar.lst_det_ct.Count == 0)
            {
                msg = "El detalle del diario se encuentra vacío";
                return false;
            }

            foreach (var item in i_validar.lst_det_ct)
            {
                if (string.IsNullOrEmpty(item.IdCtaCble))
                {
                    mensaje = "Faltan cuentas contables, por favor verifique";
                    return false;
                }
            }

            if (Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor),2,MidpointRounding.AwayFromZero) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0, por favor verifique";
                return false;
            }
            if (i_validar.lst_det_ct.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber, por favor verifique";
                return false;
            }

            var persona = bus_persona.get_info(i_validar.IdEmpresa, i_validar.IdTipo_Persona, Convert.ToDecimal(i_validar.IdEntidad));
            if (persona == null)
            {
                msg = "La persona seleccionada no corresponde al tipo asignado";
                return false;
            }
            i_validar.IdPersona = persona.IdPersona;
            i_validar.IdPersona_Girado_a = persona.IdPersona;

            if (Math.Round(i_validar.lst_det_canc_op.Sum(q=>q.MontoAplicado), 2, MidpointRounding.AwayFromZero) != Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor_debe), 2, MidpointRounding.AwayFromZero))
            {
                msg = "Los valores ingresados no concuerdan con el valor del diario";
                return false;
            }

            if (i_validar.list_det.Count() > 0)
            {
                if (Math.Round(i_validar.list_det.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero) != Convert.ToDouble(i_validar.cb_Valor))
                {
                    mensaje = "La suma de los detalles del flujo debe ser igual a el valor del documento";
                    return false;
                }
            }
            var cta = bus_banco_cuenta.get_info(i_validar.IdEmpresa, i_validar.IdBanco);
            if (cta == null)
            {
                mensaje = "Selecciona la cuenta bancaria";
                return false;
            }
            if (cta.EsFlujoObligatorio)
            {
                if (i_validar.list_det.Count == 0)
                {
                    mensaje = "Falta distribución de flujo";
                    return false;
                }
            }
            i_validar.IdPeriodo = Convert.ToInt32(i_validar.cb_Fecha.ToString("yyyyMM"));
            i_validar.IdUsuario = SessionFixed.IdUsuario;
            i_validar.IdUsuarioUltMod = SessionFixed.IdUsuario;
            i_validar.IdUsuario_Anu = SessionFixed.IdUsuario;
            i_validar.cb_Valor = Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor_debe), 2, MidpointRounding.AwayFromZero);
            i_validar.ValorEnLetras = funciones.NumeroALetras(i_validar.cb_Valor.ToString());

            var param = bus_param.get_info(i_validar.IdEmpresa);
            if (!(param.PermitirSobreGiro))
            {
                var Valor = Math.Round(i_validar.lst_det_ct.Where(q => q.IdCtaCble == cta.IdCtaCble).Sum(q => q.dc_Valor),2,MidpointRounding.AwayFromZero);
                if (!bus_banco_cuenta.ValidarSaldoCuenta(i_validar.IdEmpresa,cta.IdCtaCble,Valor))
                {
                    mensaje = "No se puede guardar la transacción por sobre giro en la cuenta";
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Cbte_Ban_Info model = new ba_Cbte_Ban_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                CodTipoCbteBan = cl_enumeradores.eTipoCbteBancario.CHEQ.ToString(),
                cb_ciudadChq = "09",
                IdTipo_Persona = cl_enumeradores.eTipoPersona.PROVEE.ToString(),
                cb_Fecha = DateTime.Now.Date,
                IdEntidad = 0,
                lst_det_canc_op = new List<cp_orden_pago_cancelaciones_Info>(),
                lst_det_ct = new List<ct_cbtecble_det_Info>(),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                list_det = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>()
            };
            SessionFixed.TipoPersona = model.IdTipo_Persona;
            List_ct.set_list(model.lst_det_ct,model.IdTransaccionSession);
            List_op.set_list(model.lst_det_canc_op, model.IdTransaccionSession);
            List_Flujo.set_list(model.list_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa,model.IdSucursal);

            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ba_Cbte_Ban_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.list_det = List_Flujo.get_list(model.IdTransaccionSession);
            if (!validar(model,ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_cbteban.guardarDB(model, cl_enumeradores.eTipoCbteBancario.CHEQ))
            {
                ViewBag.mensaje = "No se pudo guardar el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true});
        }

        [HttpPost]
        public ActionResult Modificar(ba_Cbte_Ban_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            model.list_det = List_Flujo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }            
            
            if (!bus_cbteban.modificarDB(model, cl_enumeradores.eTipoCbteBancario.CHEQ))
            {
                ViewBag.mensaje = "No se pudo modificar el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdTipocbte = 0, decimal IdCbteCble = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Cbte_Ban_Info model = bus_cbteban.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if(model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
           model.list_det = bus_flujo.GetList(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_Flujo.set_list(model.list_det, model.IdTransaccionSession);
            model.lst_det_ct = bus_det_ct.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ct.set_list(model.lst_det_ct,model.IdTransaccionSession);
            model.lst_det_canc_op = bus_cancelaciones.get_list_x_pago(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble, SessionFixed.IdUsuario);
            List_op.set_list(model.lst_det_canc_op,model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdSucursal);
            SessionFixed.TipoPersona = model.IdTipo_Persona;

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cb_Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0 , int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_Cbte_Ban_Info model = bus_cbteban.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.list_det = bus_flujo.GetList(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_Flujo.set_list(model.list_det, model.IdTransaccionSession);
            model.lst_det_ct = bus_det_ct.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ct.set_list(model.lst_det_ct, model.IdTransaccionSession);
            model.lst_det_canc_op = bus_cancelaciones.get_list_x_pago(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble, SessionFixed.IdUsuario);
            List_op.set_list(model.lst_det_canc_op,model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdSucursal);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cb_Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ba_Cbte_Ban_Info model)
        {
            model.IdUsuario_Anu = SessionFixed.IdUsuario;
            if (!bus_cbteban.anularDB(model))
            {
                ViewBag.mensaje = "No se pudo anular el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Detalle

        public ActionResult GridViewPartial_cheque_op_x_cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListPorCruzar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cheque_op_x_cruzar",model);
        }

        public ActionResult GridViewPartial_cheque_op()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_op.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cheque_op", model);
        }

        public JsonResult ImportarSolicitud(int IdEmpresa = 0, decimal IdSolicitudPago = 0)
        {
            string GiradoA = string.Empty;
            string Observacion = "Canc./";

            var solicitud = bus_solicitud.GetInfo(IdEmpresa, IdSolicitudPago);
            if (solicitud != null)
            {
                GiradoA = solicitud.GiradoA;
                Observacion = solicitud.Concepto;
            }

            return Json(new { GiradoA = GiradoA, Observacion = Observacion }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {
            string GiradoA = string.Empty;
            string Observacion = "Canc./";
            
            if (IDs != "")
            {
                var lst_x_cruzar = ListPorCruzar.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst_x_cruzar.Where(q => q.IdOrdenPago_op == Convert.ToInt32(item)).FirstOrDefault();
                    if (info_det != null)
                    {
                        GiradoA = info_det.pe_nombreCompleto;
                        Observacion += info_det.Referencia + "/ ";
                        List_op.AddRow(info_det, IdTransaccionSession);
                    }
                }
            }
            var model = List_op.get_list(IdTransaccionSession);
            return Json(new { GiradoA = GiradoA, Observacion = Observacion }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateFactura([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_pago_cancelaciones_Info info_det)
        {
            List_op.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_op.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cheque_op", model);
        }

        public ActionResult EditingDeleteFactura(decimal IdOrdenPago_op)
        {
            decimal IdTransaccionSession = Convert.ToDecimal(string.IsNullOrEmpty(SessionFixed.IdTransaccionSessionActual) ? "0" : SessionFixed.IdTransaccionSessionActual);
            List_op.DeleteRow(IdOrdenPago_op,IdTransaccionSession);            
            var model = List_op.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_cheque_op", model);
        }
        #endregion

        #region Json

        public JsonResult GetListPorCruzar(int IdEmpresa = 0, decimal IdTransaccionSession = 0, string IdTipoPersona = "", decimal IdEntidad = 0, int IdSucursal = 0)
        {
            var lst = bus_cancelaciones.get_list_con_saldo(IdEmpresa, 0, IdTipoPersona, IdEntidad, "APRO", SessionFixed.IdUsuario, false, IdSucursal);
            ListPorCruzar.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult armar_diario(int IdEmpresa = 0, int IdBanco = 0, decimal IdTransaccionSession = 0)
        {
            List_ct.set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);
            var bco = bus_banco_cuenta.get_info(IdEmpresa, IdBanco);
            var lst_op = List_op.get_list(IdTransaccionSession);

            foreach (var item in lst_op)
            {
                //Debe
                List_ct.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = item.IdCtaCble,
                    dc_Valor = Math.Round(item.MontoAplicado, 2, MidpointRounding.AwayFromZero),
                    dc_Valor_debe = Math.Round(item.MontoAplicado, 2, MidpointRounding.AwayFromZero)
                },IdTransaccionSession);
            }
            List_ct.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = bco.IdCtaCble,
                dc_Valor = Math.Round(lst_op.Sum(q => q.MontoAplicado), 2, MidpointRounding.AwayFromZero) * -1,
                dc_Valor_haber = Math.Round(lst_op.Sum(q => q.MontoAplicado), 2, MidpointRounding.AwayFromZero),
                dc_para_conciliar = true
            }, IdTransaccionSession);
            
            return Json(Math.Round(lst_op.Sum(q => q.MontoAplicado), 2, MidpointRounding.AwayFromZero), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNumCheque(int IdEmpresa = 0, int IdBanco = 0)
        {
            var error = "";
            var resultado = bus_talonario.get_ult_NumCheque_no_usado(IdEmpresa, IdBanco);
            #region Alerta
            var param = bus_param.get_info(IdEmpresa);
            if (param != null && param.CantidadChequesAlerta != null && param.CantidadChequesAlerta > 0)
            {
                var lista = bus_talonario.GetCantidadTalonariosDisponibles(IdEmpresa, IdBanco);

                if (lista <= param.CantidadChequesAlerta)
                {
                    error = "Cantidad de cheques disponibles: "+lista;
                }
            }
            #endregion
            return Json(new {NumCheque= resultado , Error = error }, JsonRequestBehavior.AllowGet);
        }
        public void vaciar_detalle(decimal IdTransaccionSession = 0)
        {
            List_op.set_list(new List<cp_orden_pago_cancelaciones_Info>(), IdTransaccionSession);
            List_ct.set_list(new List<ct_cbtecble_det_Info>(),IdTransaccionSession);
            List_Flujo.set_list(new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>(), IdTransaccionSession);
        }
        #endregion
    }
    public class cp_orden_pago_cancelaciones_List
    {
        public List<cp_orden_pago_cancelaciones_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info"+ IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_pago_cancelaciones_Info> list = new List<cp_orden_pago_cancelaciones_Info>();

                HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info"+ IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_pago_cancelaciones_Info>)HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info" + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_pago_cancelaciones_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info" + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_orden_pago_cancelaciones_Info info_det, decimal IdTransaccionSession)
        {
            List<cp_orden_pago_cancelaciones_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            if (list.Where(q => q.IdOrdenPago_op == info_det.IdOrdenPago_op).Count() == 0)
                list.Add(info_det);
        }
        public void UpdateRow(cp_orden_pago_cancelaciones_Info info_det, decimal IdTransaccionSession)
        {
            cp_orden_pago_cancelaciones_Info info = get_list(IdTransaccionSession).Where(q=>q.IdOrdenPago_op == info_det.IdOrdenPago_op).FirstOrDefault();
            if(info != null)
            {
                info.MontoAplicado = info_det.MontoAplicado;
            }
        }

        public void DeleteRow(decimal IdOrdenPago_op, decimal IdTransaccionSession)
        {
            List<cp_orden_pago_cancelaciones_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdOrdenPago_op == IdOrdenPago_op).First());
        }
    }
    public class cp_orden_pago_cancelaciones_PorCruzar
    {
        string Variable = "cp_orden_pago_cancelaciones_PorCruzar";
        public List<cp_orden_pago_cancelaciones_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_pago_cancelaciones_Info> list = new List<cp_orden_pago_cancelaciones_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_pago_cancelaciones_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_pago_cancelaciones_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    public class ba_Banco_Flujo_Det_List_Cheque
    {
        string Variable = "ba_Cbte_Ban_x_ba_TipoFlujo_Info";
        public List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;


            list.Add(info_det);
        }

        public void UpdateRow(ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det, decimal IdTransaccionSession)
        {
            ba_Cbte_Ban_x_ba_TipoFlujo_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdTipocbte = info_det.IdTipocbte;
            edited_info.IdCbteCble = info_det.IdCbteCble;
            edited_info.Porcentaje = info_det.Porcentaje;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_Cbte_Ban_x_ba_TipoFlujo_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}