using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Caja;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorCobrar;
using Core.Erp.Bus.General;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorCobrar.Controllers
{
    [SessionTimeout]
    public class CobranzaController : Controller
    {
        #region Variables

        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        cxc_cobro_Bus bus_cobro = new cxc_cobro_Bus();
        caj_Caja_Bus bus_caja = new caj_Caja_Bus();
        caj_parametro_Bus bus_param_caja = new caj_parametro_Bus();
        cxc_cobro_tipo_Bus bus_cobro_tipo = new cxc_cobro_tipo_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        tb_banco_Bus bus_banco = new tb_banco_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        cxc_cobro_det_Bus bus_det = new cxc_cobro_det_Bus();
        cxc_cobro_det_List list_det = new cxc_cobro_det_List();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        cxc_cobro_det_x_cruzar_List List_x_Cruzar = new cxc_cobro_det_x_cruzar_List();
        List<cxc_cobro_det_Info> ListaDetalleXCruzar = new List<cxc_cobro_det_Info>();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbCliente_Cobranza()
        {
            decimal model = new decimal();
            return PartialView("_CmbCliente_Cobranza", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
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

        #endregion

        #region Metodos
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
        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_caja = bus_caja.get_list(IdEmpresa, false);
            ViewBag.lst_caja = lst_caja;

            var lst_cobro_tipo = bus_cobro_tipo.get_list(false);
            ViewBag.lst_cobro_tipo = lst_cobro_tipo;

            var lst_banco = bus_banco.get_list(false);
            ViewBag.lst_banco = lst_banco;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa,IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;
        }

        private bool validar(cxc_cobro_Info i_validar, ref string msg)
        {
            i_validar.IdEntidad = i_validar.IdCliente;
            if (i_validar.cr_TotalCobro == 0)
            {
                msg = "No ha seleccionado documentos para realizar la cobranza";
                return false;
            }
            if (Math.Round(i_validar.cr_saldo,2,MidpointRounding.AwayFromZero) < 0)
            {
                msg = "El valor aplicado a los documentos es mayor al total a cobrar";
                return false;
            }
            i_validar.lst_det = list_det.get_list(i_validar.IdTransaccionSession);
            if (i_validar.lst_det.Count == 0)
            {
                msg = "No ha seleccionado documentos para realizar la cobranza";
                return false;
            }
            if (i_validar.lst_det.Where(q=>q.dc_ValorPago == 0).Count() > 0)
            {
                msg = "Existen documentos con valor aplicado 0";
                return false;
            }
            if (i_validar.IdCobro > 0 && i_validar.lst_det.Where(q => Math.Round(q.dc_ValorPago,2,MidpointRounding.AwayFromZero) > Math.Round((double)q.Saldo,2,MidpointRounding.AwayFromZero)).Count() > 0)
            {
                msg = "Existen documentos cuyo valor aplicado es mayor al saldo de la factura";
                return false;
            }
            string observacion = "Canc./ ";
            foreach (var item in i_validar.lst_det)
            {
                observacion += item.vt_NumDocumento+ "/";
            }
            i_validar.cr_observacion = observacion;
            i_validar.cr_fechaCobro = i_validar.cr_fecha;
            i_validar.cr_fechaDocu = i_validar.cr_fecha;

            if (!string.IsNullOrEmpty(i_validar.IdCobro_tipo))
                i_validar.lst_det.ForEach(q => q.IdCobro_tipo_det = i_validar.IdCobro_tipo);

            
            
            switch (i_validar.IdCobro_tipo)
            {
                case "DEPO":
                    if (i_validar.IdBanco == null)
                    {
                        msg = "El campo cuenta bancaria es obligatorio para depositos";
                        return false;
                    }
                    break;
                case "CHQF":
                    if (string.IsNullOrEmpty(i_validar.cr_Banco))
                    {
                        msg = "El campo banco es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(i_validar.cr_cuenta))
                    {
                        msg = "El campo cuenta es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(i_validar.cr_NumDocumento))
                    {
                        msg = "El campo # cheque es obligatorio para cheques";
                        return false;
                    }
                    i_validar.IdBanco = null;
                    break;

                case "CHQV":
                    if (string.IsNullOrEmpty(i_validar.cr_Banco))
                    {
                        msg = "El campo banco es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(i_validar.cr_cuenta))
                    {
                        msg = "El campo cuenta es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(i_validar.cr_NumDocumento))
                    {
                        msg = "El campo # cheque es obligatorio para cheques";
                        return false;
                    }
                    i_validar.IdBanco = null;
                    break;
                default:
                    i_validar.IdBanco = null;
                    i_validar.cr_Banco = null;
                    break;
            }

            foreach (var item in i_validar.lst_det)
            {
                if (i_validar.cr_fecha < item.vt_fecha)
                {
                    msg = "Existen comprobantes de venta con fecha mayor a la fecha del cobro aplicado";
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            var param_caja = bus_param_caja.get_info(IdEmpresa);
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_cobro_Info model = new cxc_cobro_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = IdEmpresa,
                IdSucursal = IdSucursal,
                cr_fecha = DateTime.Now.Date,
                IdCobro_tipo = "EFEC",
                lst_det = new List<cxc_cobro_det_Info>(),
            };
            list_det.set_list(new List<cxc_cobro_det_Info>(), model.IdTransaccionSession);
            List_x_Cruzar.set_list(new List<cxc_cobro_det_Info>(), model.IdTransaccionSession);

            cargar_combos(IdEmpresa,model.IdSucursal);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cxc_cobro_Info model)
        {
            model.lst_det = list_det.get_list(model.IdTransaccionSession);
            if (!validar(model,ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario;

            if (!bus_cobro.guardarDB(model))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdCobro= model.IdCobro, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0 , int IdSucursal = 0, decimal IdCobro = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_cobro_Info model = bus_cobro.get_info(IdEmpresa, IdSucursal, IdCobro);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdCobro);
            list_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.IdEntidad = model.IdCliente;
            cargar_combos(IdEmpresa, model.IdSucursal);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cr_fecha, cl_enumeradores.eModulo.CXC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cxc_cobro_Info model)
        {
            model.lst_det = list_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_cobro.modificarDB(model))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdCobro = model.IdCobro, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0 , int IdSucursal = 0, decimal IdCobro = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_cobro_Info model = bus_cobro.get_info(IdEmpresa, IdSucursal, IdCobro);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdCobro);
            list_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.IdEntidad = model.IdCliente;
            cargar_combos(IdEmpresa, model.IdSucursal);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cr_fecha, cl_enumeradores.eModulo.CXC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_cobro_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            if (!bus_cobro.anularDB(model))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Grids
        [ValidateInput(false)]
        public ActionResult GridViewPartial_cobranza(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_cobro.get_list(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin);

            return PartialView("_GridViewPartial_cobranza", model);            
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cobranza_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = list_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cobranza_det", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_cobranza_facturas_x_cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_x_Cruzar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_cobranza_facturas_x_cruzar", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateFactura([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_cobro_det_Info info_det)
        {
            if (ModelState.IsValid)
                list_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = list_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cobranza_det", model);
        }

        public ActionResult EditingDeleteFactura(string secuencia)
        {
            list_det.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = list_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cobranza_det", model);
        }
        #endregion

        #region Json
        public JsonResult GetListFacturas_PorIngresar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, int IdSucursal = 0, decimal IdCliente = 0)
        {
            var lst = bus_det.get_list_cartera(IdEmpresa, IdSucursal, IdCliente);
            List_x_Cruzar.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNewFactura(string IDs = "", double TotalACobrar = 0, decimal IdTransaccionSession = 0)
        {
            double saldo = TotalACobrar;
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = List_x_Cruzar.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');

                foreach (var item in array)
                {
                    var info_det = lst_x_ingresar.Where(q => q.secuencia == item).FirstOrDefault();
                    if (info_det != null)
                        list_det.AddRow(info_det, IdTransaccionSession);
                }
            }
            var lst = list_det.get_list(IdTransaccionSession);
            foreach (var item in lst)
            {
                if (saldo > 0)
                {
                    item.dc_ValorPago = saldo >= Convert.ToDouble(item.Saldo) ? Convert.ToDouble(item.Saldo) : saldo;
                    item.Saldo_final = Convert.ToDouble(item.Saldo) - item.dc_ValorPago;
                    saldo = saldo - item.dc_ValorPago;
                }
                else
                    item.dc_ValorPago = 0;
            }
            list_det.set_list(lst, IdTransaccionSession);            

            var resultado = saldo;
            return Json(Math.Round(resultado,2,MidpointRounding.AwayFromZero), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularSaldo(double TotalACobrar = 0, decimal IdTransaccionSession = 0)
        {
            double saldo = TotalACobrar;

            var lst = list_det.get_list(IdTransaccionSession);
            foreach (var item in lst)
            {
                saldo -= item.dc_ValorPago;
            }
            list_det.set_list(lst, IdTransaccionSession);
            var resultado = saldo;
            return Json(Math.Round(resultado, 2, MidpointRounding.AwayFromZero), JsonRequestBehavior.AllowGet);
        }

        public void VaciarLista(decimal IdTransaccionSession = 0)
        {
            list_det.set_list(new List<cxc_cobro_det_Info>(), IdTransaccionSession);
        }

        public JsonResult GetIdCajaPorSucursal(int IdEmpresa, int IdSucursal)
        {
            var resultado = bus_caja.GetIdCajaPorSucursal(IdEmpresa, IdSucursal);

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }

    public class cxc_cobro_det_List
    {
        string Variable = "cxc_cobro_det_Info";
        public List<cxc_cobro_det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_cobro_det_Info> list = new List<cxc_cobro_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_cobro_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];            
        }

        public void set_list(List<cxc_cobro_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_cobro_det_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_cobro_det_Info> list = get_list(IdTransaccionSession);
            if (list.Where(q => q.secuencia == info_det.secuencia).FirstOrDefault() == null)
            {
                info_det.Saldo_final = Convert.ToDouble(info_det.Saldo) - info_det.dc_ValorPago;
                list.Add(info_det);
            }
        }

        public void UpdateRow(cxc_cobro_det_Info info_det, decimal IdTransaccionSession)
        {
            cxc_cobro_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.Saldo_final = Convert.ToDouble(edited_info.Saldo) - info_det.dc_ValorPago;
            edited_info.dc_ValorPago = info_det.dc_ValorPago;
        }

        public void DeleteRow(string secuencia, decimal IdTransaccionSession)
        {
            List<cxc_cobro_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }
    }

    public class cxc_cobro_det_x_cruzar_List
    {
        string Variable = "cxc_cobro_det_x_cruzar_Info";
        public List<cxc_cobro_det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_cobro_det_Info> list = new List<cxc_cobro_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_cobro_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_cobro_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}