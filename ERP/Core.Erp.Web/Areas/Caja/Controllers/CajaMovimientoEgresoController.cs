using Core.Erp.Bus.Caja;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorCobrar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Caja;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Areas.Banco.Controllers;
using Core.Erp.Web.Areas.Contabilidad.Controllers;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Caja.Controllers
{
    [SessionTimeout]
    public class CajaMovimientoEgresoController : Controller
    {
        #region  Variables
        caj_Caja_Movimiento_Bus bus_caja_mov = new caj_Caja_Movimiento_Bus();
        caj_Caja_Movimiento_det_Bus bus_caja_mov_det = new caj_Caja_Movimiento_det_Bus();
        ct_cbtecble_det_Bus bus_comprobante_detalle = new ct_cbtecble_det_Bus();
        ct_cbtecble_det_List list_ct_cbtecble_det = new ct_cbtecble_det_List();
        caj_parametro_Bus bus_caj_param = new caj_parametro_Bus();
        caj_Caja_Bus bus_caja = new caj_Caja_Bus();
        caj_Caja_Movimiento_Tipo_Bus bus_tipo = new caj_Caja_Movimiento_Tipo_Bus();
        string mensaje = string.Empty;
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        cxc_cobro_tipo_Bus bus_cobro = new cxc_cobro_tipo_Bus();
        cp_orden_pago_cancelaciones_Bus bus_cancelaciones = new cp_orden_pago_cancelaciones_Bus();
        cp_orden_pago_cancelaciones_PorCruzar ListPorCruzar = new cp_orden_pago_cancelaciones_PorCruzar();
        cp_orden_pago_cancelaciones_List List_op = new cp_orden_pago_cancelaciones_List();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Combo box bajo demanda
        public ActionResult CmbPersona_MovimientoEgreso()
        {
            SessionFixed.TipoPersona = Request.Params["IdTipoPersona"] != null ? Request.Params["IdTipoPersona"].ToString() : "PERSONA";
            decimal model = new decimal();
            return PartialView("_CmbPersona_MovimientoEgreso", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.TipoPersona);
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), SessionFixed.TipoPersona);
        }

        public ActionResult CmbTipoMovimiento_Egreso()
        {
            decimal model = new decimal();
            return PartialView("_CmbTipoMovimiento_Egreso", model);
        }
        public List<caj_Caja_Movimiento_Tipo_Info> get_list_bajo_demanda_TipoMovimiento_Egreso(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), "-");
        }
        public caj_Caja_Movimiento_Tipo_Info get_info_bajo_demanda_TipoMovimiento_Egreso(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_caja_Info model = new cl_filtros_caja_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdCaja = string.IsNullOrEmpty(SessionFixed.IdCaja) ? 0 : Convert.ToInt32(SessionFixed.IdCaja)
            };
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_caja_Info model)
        {
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }
        public ActionResult GridViewPartial_movimiento_egresos(DateTime? fecha_ini, DateTime? fecha_fin, int IdEmpresa = 0, int IdCaja = 0)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdCaja = IdCaja;
            List<caj_Caja_Movimiento_Info> model = bus_caja_mov.get_list(IdEmpresa, IdCaja, "-", true, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_movimiento_egresos", model);
        }
        public void CargarCombosConsulta(int IdEmpresa)
        {
            var lst_caja = bus_caja.get_list(IdEmpresa, false);
            lst_caja.Add(new caj_Caja_Info
            {
                IdCaja = 0,
                ca_Descripcion = "Todos"
            });
            ViewBag.lst_caja = lst_caja;
        }

        #endregion

        #region Metodos

        private bool validar(caj_Caja_Movimiento_Info i_validar, ref string msg)
        {
            if (i_validar.lst_ct_cbtecble_det.Count == 0)
            {
                mensaje = "Debe ingresar registros en el detalle, por favor verifique";
                return false;
            }

            foreach (var item in i_validar.lst_ct_cbtecble_det)
            {
                if (string.IsNullOrEmpty(item.IdCtaCble))
                {
                    mensaje = "Faltan cuentas contables, por favor verifique";
                    return false;
                }
            }
            if (Math.Round(i_validar.lst_ct_cbtecble_det.Sum(q => q.dc_Valor),2,MidpointRounding.AwayFromZero) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0, por favor verifique";
                return false;
            }
            if (i_validar.lst_ct_cbtecble_det.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber, por favor verifique";
                return false;
            }

            var persona = bus_persona.get_info(i_validar.IdEmpresa, i_validar.IdTipo_Persona, i_validar.IdEntidad);
            if (persona == null)
            {
                msg = "La persona seleccionada no corresponde al tipo asignado";
                return false;
            }
            i_validar.IdPersona = persona.IdPersona;

            if (Math.Round(i_validar.info_caj_Caja_Movimiento_det.cr_Valor, 2, MidpointRounding.AwayFromZero) != Math.Round(i_validar.lst_ct_cbtecble_det.Sum(q => q.dc_Valor_debe), 2, MidpointRounding.AwayFromZero))
            {
                msg = "Los valores ingresados no concuerdan con el valor del diario";
                return false;
            }

            i_validar.cm_valor = i_validar.info_caj_Caja_Movimiento_det.cr_Valor;
            i_validar.lst_det_canc_op = List_op.get_list(i_validar.IdTransaccionSession);
            return true;
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_tipo = bus_tipo.get_list(IdEmpresa,"-", false,true);
            ViewBag.lst_tipo = lst_tipo;

            var lst_caja = bus_caja.get_list(IdEmpresa, false);
            ViewBag.lst_caja = lst_caja;

            var lst_cobro = bus_cobro.get_list(false);
            ViewBag.lst_cobro = lst_cobro;

            Dictionary<string, string> lst_tipo_personas = new Dictionary<string, string>();
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.PERSONA.ToString(), "Persona");
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.PROVEE.ToString(), "Proveedor");
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.EMPLEA.ToString(), "Empleado");
            lst_tipo_personas.Add(cl_enumeradores.eTipoPersona.CLIENTE.ToString(), "Cliente");
            ViewBag.lst_tipo_personas = lst_tipo_personas;
        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            caj_Caja_Movimiento_Info model = new caj_Caja_Movimiento_Info
            {
                IdEmpresa = IdEmpresa,
                IdTipo_Persona = "PERSONA",
                info_caj_Caja_Movimiento_det = new caj_Caja_Movimiento_det_Info
                {
                    IdCobro_tipo = "EFEC"
                },
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                cm_fecha = DateTime.Now,
                IdCaja = string.IsNullOrEmpty(SessionFixed.IdCaja) ? 0 : Convert.ToInt32(SessionFixed.IdCaja)
            };
            model.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();
            list_ct_cbtecble_det.set_list(model.lst_ct_cbtecble_det,model.IdTransaccionSession);
            cargar_combos_detalle();
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(caj_Caja_Movimiento_Info model)
        {
            #region Validaciones
            model.lst_ct_cbtecble_det = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario;
            caj_parametro_Info i_parametro = bus_caj_param.get_info(model.IdEmpresa);
            if (i_parametro == null)
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = "Debe ingresar los parámetros para usar el módulo";
                return View(model);
            }
            model.IdTipocbte = i_parametro.IdTipoCbteCble_MoviCaja_Egr;
            model.cm_Signo = "-";
            #endregion

            #region guardar
            if (!bus_caja_mov.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            #endregion

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipocbte = 0, decimal IdCbteCble = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            caj_Caja_Movimiento_Info model = bus_caja_mov.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model == null)
                return RedirectToAction("Index");
            SessionFixed.TipoPersona = model.IdTipo_Persona;
            model.info_caj_Caja_Movimiento_det = bus_caja_mov_det.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model.info_caj_Caja_Movimiento_det == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_ct_cbtecble_det = bus_comprobante_detalle.get_list(IdEmpresa, IdTipocbte, IdCbteCble);
            list_ct_cbtecble_det.set_list(model.lst_ct_cbtecble_det,model.IdTransaccionSession);

            model.lst_det_canc_op = bus_cancelaciones.get_list_x_pago(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble, SessionFixed.IdUsuario);
            List_op.set_list(model.lst_det_canc_op, model.IdTransaccionSession);

            SessionFixed.TipoPersona = model.IdTipo_Persona;

            if (!bus_caja_mov.ValidarMovimientoModificar(IdEmpresa,IdTipocbte,IdCbteCble,"-"))
            {
                ViewBag.mensaje = "El movimiento de caja no puede ser modificado";
                ViewBag.NoMostrarBotones = true;
            }else
                ViewBag.NoMostrarBotones = false;

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cm_fecha, cl_enumeradores.eModulo.CAJA, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(caj_Caja_Movimiento_Info model)
        {
            model.lst_ct_cbtecble_det = list_ct_cbtecble_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_caja_mov.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0 , int IdTipocbte = 0, decimal IdCbteCble = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            caj_Caja_Movimiento_Info model = bus_caja_mov.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model == null)
                return RedirectToAction("Index");
            SessionFixed.TipoPersona = model.IdTipo_Persona;
            model.info_caj_Caja_Movimiento_det = bus_caja_mov_det.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model.info_caj_Caja_Movimiento_det == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_ct_cbtecble_det = bus_comprobante_detalle.get_list(IdEmpresa, IdTipocbte, IdCbteCble);
            list_ct_cbtecble_det.set_list(model.lst_ct_cbtecble_det,model.IdTransaccionSession);
            cargar_combos(IdEmpresa);

            if (!bus_caja_mov.ValidarMovimientoModificar(IdEmpresa, IdTipocbte, IdCbteCble, "-"))
            {
                ViewBag.mensaje = "El movimiento de caja no puede ser anulado";
                ViewBag.NoMostrarBotones = true;
            }
            else
                ViewBag.NoMostrarBotones = false;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cm_fecha, cl_enumeradores.eModulo.CAJA, 0, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(caj_Caja_Movimiento_Info model)
        {
            model.IdUsuario_Anu = SessionFixed.IdUsuario;
            if (!bus_caja_mov.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Json
        public JsonResult GetListPorCruzar(int IdEmpresa = 0, decimal IdTransaccionSession = 0, string IdTipoPersona = "", decimal IdEntidad = 0, int IdCaja = 0)
        {
            var i_caja = bus_caja.get_info(IdEmpresa, IdCaja);
            if (i_caja != null)
            {
                var lst = bus_cancelaciones.get_list_con_saldo(IdEmpresa, 0, IdTipoPersona, IdEntidad, "APRO", SessionFixed.IdUsuario, false, i_caja.IdSucursal);
                ListPorCruzar.set_list(lst, IdTransaccionSession);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult armar_diario(int IdEmpresa = 0, int IdCaja = 0, int IdTipoMovi = 0, double valor = 0, decimal IdTransaccionSession = 0)
        {
            var i_caja = bus_caja.get_info(IdEmpresa, IdCaja);
            var i_tipo_movi = bus_tipo.get_info(IdEmpresa, IdTipoMovi);

            list_ct_cbtecble_det.set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);
            var ListOp = List_op.get_list(IdTransaccionSession);
            if (ListOp.Count > 0)
            {
                list_ct_cbtecble_det.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = i_caja == null ? null : i_caja.IdCtaCble,
                    dc_Valor = Math.Round(ListOp.Sum(q => q.MontoAplicado), 2, MidpointRounding.AwayFromZero) * -1,
                    dc_Valor_haber = Math.Round(ListOp.Sum(q => q.MontoAplicado), 2, MidpointRounding.AwayFromZero)
                }, IdTransaccionSession);
            }
            else
                list_ct_cbtecble_det.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = i_caja == null ? null : i_caja.IdCtaCble,
                dc_Valor = Math.Round(Math.Abs(valor), 2, MidpointRounding.AwayFromZero) * -1,
                dc_Valor_haber = Math.Round(Math.Abs(valor), 2, MidpointRounding.AwayFromZero)
            }, IdTransaccionSession);

            //Haber            
            if (ListOp.Count > 0)
            {
                foreach (var item in ListOp)
                {
                    list_ct_cbtecble_det.AddRow(new ct_cbtecble_det_Info
                    {
                        IdCtaCble = item.IdCtaCble,
                        dc_Valor = Math.Round(Math.Abs(item.MontoAplicado), 2, MidpointRounding.AwayFromZero),
                        dc_Valor_debe = Math.Round(Math.Abs(item.MontoAplicado), 2, MidpointRounding.AwayFromZero)
                    }, IdTransaccionSession);
                }
            }
            else
            list_ct_cbtecble_det.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = i_tipo_movi == null ? "" : i_tipo_movi.IdCtaCble,
                dc_Valor = Math.Round(Math.Abs(valor),2,MidpointRounding.AwayFromZero),
                dc_Valor_debe = Math.Round(Math.Abs(valor),2,MidpointRounding.AwayFromZero)
            }, IdTransaccionSession);


            return Json(new { EsOp = ListOp.Count > 0 ? "S" : "N", Valor = Math.Round(ListOp.Sum(q=>q.MontoAplicado),2,MidpointRounding.AwayFromZero)}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalles
        public ActionResult GridViewPartial_egreso_op_x_cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListPorCruzar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_egreso_op_x_cruzar", model);
        }

        public ActionResult GridViewPartial_egreso_op()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_op.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_egreso_op", model);
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
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_pago_cancelaciones_Info info_det)
        {
            List_op.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_op.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_egreso_op", model);
        }
        public ActionResult EditingDelete(decimal IdOrdenPago_op)
        {
            decimal IdTransaccionSession = Convert.ToDecimal(string.IsNullOrEmpty(SessionFixed.IdTransaccionSessionActual) ? "0" : SessionFixed.IdTransaccionSessionActual);
            List_op.DeleteRow(IdOrdenPago_op, IdTransaccionSession);
            var model = List_op.get_list(IdTransaccionSession);
            return PartialView("_GridViewPartial_egreso_op", model);
        }
        #endregion
    }
}