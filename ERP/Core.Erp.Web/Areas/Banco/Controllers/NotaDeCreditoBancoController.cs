using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Contabilidad;
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
    public class NotaDeCreditoBancoController : Controller
    {
        #region Variables
        ba_Cbte_Ban_Bus bus_cbteban = new ba_Cbte_Ban_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ba_tipo_nota_Bus bus_tipo_nota = new ba_tipo_nota_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        ct_cbtecble_det_List List_ct = new ct_cbtecble_det_List();
        ct_cbtecble_det_Bus bus_det_ct = new ct_cbtecble_det_Bus();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        ba_TipoFlujo_Bus bus_tipo = new ba_TipoFlujo_Bus();
        ba_Banco_Flujo_Det_NotaCredito_List List_Banco_Flujo_Det = new ba_Banco_Flujo_Det_NotaCredito_List();
        ba_Cbte_Ban_x_ba_TipoFlujo_Bus bus_Cbte_Ban_x_ba_TipoFlujo = new ba_Cbte_Ban_x_ba_TipoFlujo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion
        #region Metodos ComboBox bajo demanda flujo
        public ActionResult CmbFlujo_ND_C()
        {
            decimal model = new decimal();
            return PartialView("_CmbFlujo_ND_C", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demandaFlujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoIngEgr.ING.ToString());
        }
        public ba_TipoFlujo_Info get_info_bajo_demandaFlujo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Metodos ComboBox bajo demanda flujo
        public ActionResult CmbFlujo_NotaCredito()
        {
            ba_Cbte_Ban_Info model = new ba_Cbte_Ban_Info();
            return PartialView("_CmbFlujo_NotaCredito", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demandaFlujo_NC(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public ba_TipoFlujo_Info get_info_bajo_demandaFlujo_NC(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Metodos ComboBox tipo de nota
        public ActionResult CmbTipoNota()
        {
            ba_Cbte_Ban_Info model = new ba_Cbte_Ban_Info();
            return PartialView("_CmbTipoNota", model);
        }
        public List<ba_tipo_nota_Info> get_list_bajo_TipoNota(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo_nota.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),cl_enumeradores.eTipoCbteBancario.NCBA.ToString());
        }
        public ba_tipo_nota_Info get_info_bajo_demanda_TipoNota(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo_nota.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Detalle del diario
        [ValidateInput(false)]
        public ActionResult GridViewPartial_comprobante_detalle_Credito()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_comprobante_detalle_Credito", model);
        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                List_ct.AddRow(info_det,Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_comprobante_detalle_Credito", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                List_ct.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_comprobante_detalle_Credito", model);
        }
        public ActionResult EditingDelete(int secuencia)
        {
            List_ct.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_comprobante_detalle_Credito", model);
        }
        #endregion
        #region Detalle Flujo
        [ValidateInput(false)]
        public ActionResult GridViewPartial_flujo_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = List_Banco_Flujo_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewFlujo([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det)
        {
            if (ModelState.IsValid)
                List_Banco_Flujo_Det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Banco_Flujo_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateFlujo([ModelBinder(typeof(DevExpressEditorsBinder))] ba_Cbte_Ban_x_ba_TipoFlujo_Info info_det)
        {
            if (ModelState.IsValid)
                List_Banco_Flujo_Det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Banco_Flujo_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_flujo_det", model);
        }
        public ActionResult EditingDeleteFlujo(int Secuencia)
        {
            List_Banco_Flujo_Det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_Banco_Flujo_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_flujo_det", model);
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
            ba_Cbte_Ban_Info model = new ba_Cbte_Ban_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                CodTipoCbteBan = cl_enumeradores.eTipoCbteBancario.NCBA.ToString(),
                cb_Fecha = DateTime.Now.Date,
                lst_det_ct = new List<ct_cbtecble_det_Info>(),
                IdBanco = 1,
                IdTipoNota = null,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                list_det = new List<ba_Cbte_Ban_x_ba_TipoFlujo_Info>()
            };
            SessionFixed.TipoPersona = model.IdTipo_Persona;
            List_ct.set_list(model.lst_det_ct, model.IdTransaccionSession);
            List_Banco_Flujo_Det.set_list(model.list_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ba_Cbte_Ban_Info model)
        {
            model.list_det = List_Banco_Flujo_Det.get_list(model.IdTransaccionSession);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_cbteban.guardarDB(model, cl_enumeradores.eTipoCbteBancario.NCBA))
            {
                ViewBag.mensaje = "No se pudo guardar el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipocbte = model.IdTipocbte, IdCbteCble = model.IdCbteCble, Exito = true });
        }
        [HttpPost]
        public ActionResult Modificar(ba_Cbte_Ban_Info model)
        {
            model.list_det = List_Banco_Flujo_Det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_cbteban.modificarDB(model, cl_enumeradores.eTipoCbteBancario.NCBA))
            {
                ViewBag.mensaje = "No se pudo modificar el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

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
            ba_Cbte_Ban_Info model = bus_cbteban.get_info(IdEmpresa, IdTipocbte, IdCbteCble);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.list_det = bus_Cbte_Ban_x_ba_TipoFlujo.GetList(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_Banco_Flujo_Det.set_list(model.list_det, model.IdTransaccionSession);
            model.lst_det_ct = bus_det_ct.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ct.set_list(model.lst_det_ct,model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdSucursal);
            SessionFixed.TipoPersona = model.IdTipo_Persona;

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa,model.cb_Fecha,cl_enumeradores.eModulo.BANCO,model.IdSucursal,ref mensaje))
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
            model.list_det = bus_Cbte_Ban_x_ba_TipoFlujo.GetList(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_Banco_Flujo_Det.set_list(model.list_det, model.IdTransaccionSession);
            model.lst_det_ct = bus_det_ct.get_list(model.IdEmpresa, model.IdTipocbte, model.IdCbteCble);
            List_ct.set_list(model.lst_det_ct,model.IdTransaccionSession);
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
            {   ViewBag.mensaje = "No se pudo anular el registro";
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;
            var lst_tipo_nota = bus_tipo_nota.get_list(IdEmpresa, cl_enumeradores.eTipoCbteBancario.NCBA.ToString(), false);
            ViewBag.lst_tipo_nota = lst_tipo_nota;
            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;
        }
        private bool validar(ba_Cbte_Ban_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.BANCO, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cb_Fecha, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            i_validar.lst_det_ct = List_ct.get_list(i_validar.IdTransaccionSession);

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
            if (Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0, por favor verifique";
                return false;
            }
            if (i_validar.lst_det_ct.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber, por favor verifique";
                return false;
            }
            if (i_validar.list_det.Count() > 0)
            {
                if (Math.Round(i_validar.list_det.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero) != i_validar.cb_Valor)
                {
                    mensaje = "La suma de los detalles del flujo debe ser igual a el valor del documento";
                    return false;
                }
            }
            var cta = bus_banco_cuenta.get_info(i_validar.IdEmpresa, i_validar.IdBanco);
            if(cta==null)
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
            i_validar.IdUsuarioUltMod = SessionFixed.IdUsuario;
            i_validar.cb_Valor = Math.Round(i_validar.lst_det_ct.Sum(q => q.dc_Valor_debe), 2, MidpointRounding.AwayFromZero);
            return true;
        }
        #endregion
        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
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
        public ActionResult GridViewPartial_CreditoBanco(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_cbteban.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin, IdSucursal, cl_enumeradores.eTipoCbteBancario.NCBA.ToString(), true);
            return PartialView("_GridViewPartial_CreditoBanco", model);
        }
        #endregion
        #region Json
        public JsonResult armar_diario(int IdEmpresa = 0, int IdBanco = 0, int IdTipoNota = 0, decimal IdTransaccionSession = 0, double Valor = 0)
        {
            List_ct.set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);
            var bco = bus_banco_cuenta.get_info(IdEmpresa, IdBanco);
            var tipo_nota = bus_tipo_nota.get_info(IdEmpresa, IdTipoNota);
            //Debe
            if (bco != null)
            {
                List_ct.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = bco.IdCtaCble,
                    dc_Valor = Math.Round(Valor, 2, MidpointRounding.AwayFromZero),
                    dc_Valor_debe = Math.Round(Valor, 2, MidpointRounding.AwayFromZero),
                    dc_para_conciliar = true
                },IdTransaccionSession);
            }            
            //Haber
            if (tipo_nota != null && !string.IsNullOrEmpty(tipo_nota.IdCtaCble))
            {
                List_ct.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = tipo_nota.IdCtaCble,
                    dc_Valor = Math.Round(Valor, 2, MidpointRounding.AwayFromZero) *-1,
                    dc_Valor_haber = Math.Round(Valor, 2, MidpointRounding.AwayFromZero),
                    dc_para_conciliar = false
                }, IdTransaccionSession);
            }
            else
            {
                List_ct.AddRow(new ct_cbtecble_det_Info
                {
                    dc_Valor = Math.Round(Valor, 2, MidpointRounding.AwayFromZero)*-1,
                    dc_Valor_haber = Math.Round(Valor, 2, MidpointRounding.AwayFromZero),
                    IdCtaCble = null,
                }, IdTransaccionSession);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
    public class ba_Banco_Flujo_Det_NotaCredito_List
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