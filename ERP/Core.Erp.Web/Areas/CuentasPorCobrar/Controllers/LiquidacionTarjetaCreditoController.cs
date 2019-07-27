using Core.Erp.Bus.Banco;
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
    public class LiquidacionTarjetaCreditoController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        cxc_LiquidacionTarjeta_Bus bus_LiquidacionTarjeta = new cxc_LiquidacionTarjeta_Bus();
        cxc_LiquidacionTarjeta_x_cxc_cobro_Bus bus_LiquidacionTarjeta_cxc_cobro = new cxc_LiquidacionTarjeta_x_cxc_cobro_Bus();
        cxc_LiquidacionTarjetaDet_Bus bus_LiquidacionTarjetaDet = new cxc_LiquidacionTarjetaDet_Bus();
        cxc_LiquidacionTarjetaDet_List Lista_LiquidacionTarjetaDet = new cxc_LiquidacionTarjetaDet_List();
        cxc_LiquidacionTarjeta_x_cxc_cobro_List Lista_LiquidacionTarjeta_x_cxc_cobro = new cxc_LiquidacionTarjeta_x_cxc_cobro_List();
        cxc_LiquidacionTarjeta_x_cxc_cobro_pendientes_List Lista_Liquidacion_x_cobro_pendiente = new cxc_LiquidacionTarjeta_x_cxc_cobro_pendientes_List();
        string mensaje = string.Empty;
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;
        }
        private bool Validar(cxc_LiquidacionTarjeta_Info i_validar, ref string msg)
        {
            i_validar.ListaDet = Lista_LiquidacionTarjetaDet.get_list(i_validar.IdTransaccionSession);
            i_validar.ListaCobros = Lista_LiquidacionTarjeta_x_cxc_cobro.get_list(i_validar.IdTransaccionSession);
            i_validar.IdUsuarioCreacion = SessionFixed.IdUsuario;
            i_validar.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (i_validar.ListaCobros.Count == 0)
            {
                msg = "Seleccione al menos un cobro por tarjeta de crédito para liquidar";
                return false;
            }

            if (i_validar.ListaDet.Where(q=> q.Valor == 0).Count() > 0)
            {
                msg = "No pueden existir motivos con valor 0";
                return false;
            }
            double Diferencia = i_validar.ListaCobros.Sum(q => q.Valor) - i_validar.ListaDet.Sum(q => q.Valor);
            if (Math.Round(Diferencia,2,MidpointRounding.AwayFromZero) <= 0)
            {
                msg = "Los motivos de liquidación no deben superar el valor de los cobros a liquidar";
                return false;
            }

            return true;
        }
        #endregion

        #region Metodos ComboBox bajo demanda tarjeta
        //tb_TarjetaCredito_Bus bus_tarjeta = new tb_TarjetaCredito_Bus();
        //public ActionResult CmbTarjetaCredito()
        //{
        //    decimal model = new decimal();
        //    return PartialView("_CmbTarjetaCredito", model);
        //}
        //public List<tb_TarjetaCredito_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        //{
        //    return bus_tarjeta.get_list_bajo_demanda(args);
        //}
        //public tb_TarjetaCredito_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        //{
        //    return bus_tarjeta.get_info_bajo_demanda(args);
        //}
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
        public ActionResult GridViewPartial_LiquidacionTarjetaCredito(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_LiquidacionTarjeta.GetList(IdEmpresa, IdSucursal,true);
            return PartialView("_GridViewPartial_LiquidacionTarjetaCredito", model);
        }
        #endregion

        #region Grillas
        public ActionResult GridViewPartial_CobrosPendientesLiquidacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            
            var model = Lista_Liquidacion_x_cobro_pendiente.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            return PartialView("_GridViewPartial_CobrosPendientesLiquidacion", model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {
            string GiradoA = string.Empty;

            if (IDs != "")
            {
                var lst_x_cruzar = Lista_Liquidacion_x_cobro_pendiente.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst_x_cruzar.Where(q => q.IdCobro == Convert.ToInt32(item)).FirstOrDefault();
                    if (info_det != null)
                    {                        
                        Lista_LiquidacionTarjeta_x_cxc_cobro.AddRow(info_det, IdTransaccionSession);
                    }
                }
            }
            var model = Lista_LiquidacionTarjeta_x_cxc_cobro.get_list(IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditingDelete(int secuencia)
        {
            Lista_LiquidacionTarjeta_x_cxc_cobro.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_LiquidacionTarjeta_x_cxc_cobro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_cobranza_det", model);
        }

        public ActionResult GridViewPartial_CobrosLiquidacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_LiquidacionTarjeta_x_cxc_cobro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CobrosLiquidacion", model);
        }

        public ActionResult GridViewPartial_MotivosLiquidacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_LiquidacionTarjetaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MotivosLiquidacion", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewMotivo([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_LiquidacionTarjetaDet_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_LiquidacionTarjetaDet.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_LiquidacionTarjetaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MotivosLiquidacion", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateMotivo([ModelBinder(typeof(DevExpressEditorsBinder))] cxc_LiquidacionTarjetaDet_Info info_det)
        {
            if (ModelState.IsValid)
                Lista_LiquidacionTarjetaDet.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_LiquidacionTarjetaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MotivosLiquidacion", model);
        }

        public ActionResult EditingDeleteMotivo(int Secuencia)
        {
            Lista_LiquidacionTarjetaDet.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_LiquidacionTarjetaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MotivosLiquidacion", model);
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

            cxc_LiquidacionTarjeta_Info model = new cxc_LiquidacionTarjeta_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                Fecha = DateTime.Now.Date,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)           
            };

            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cxc_LiquidacionTarjeta_Info model)
        {
            if (!Validar(model,ref mensaje))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_LiquidacionTarjeta.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdLiquidacion = model.IdLiquidacion, Exito = true });
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, decimal IdLiquidacion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_LiquidacionTarjeta_Info model = bus_LiquidacionTarjeta.GetInfo(IdEmpresa, IdSucursal, IdLiquidacion);
            if(model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            Lista_LiquidacionTarjetaDet.set_list(bus_LiquidacionTarjetaDet.GetList(IdEmpresa, IdSucursal, IdLiquidacion), model.IdTransaccionSession);
            Lista_LiquidacionTarjeta_x_cxc_cobro.set_list(bus_LiquidacionTarjeta_cxc_cobro.GetList(IdEmpresa, IdSucursal, IdLiquidacion), model.IdTransaccionSession);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo BAN
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            #region Validacion Periodo CXC
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.CXC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cxc_LiquidacionTarjeta_Info model)
        {
            if (!Validar(model, ref mensaje))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }
            if (!bus_LiquidacionTarjeta.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdLiquidacion = model.IdLiquidacion, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, decimal IdLiquidacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_LiquidacionTarjeta_Info model = bus_LiquidacionTarjeta.GetInfo(IdEmpresa, IdSucursal, IdLiquidacion);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            Lista_LiquidacionTarjetaDet.set_list(bus_LiquidacionTarjetaDet.GetList(IdEmpresa, IdSucursal, IdLiquidacion), model.IdTransaccionSession);
            Lista_LiquidacionTarjeta_x_cxc_cobro.set_list(bus_LiquidacionTarjeta_cxc_cobro.GetList(IdEmpresa, IdSucursal, IdLiquidacion), model.IdTransaccionSession);

            #region Validacion Periodo BAN
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            #region Validacion Periodo CXC
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.CXC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            cargar_combos(IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_LiquidacionTarjeta_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_LiquidacionTarjeta.anularDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Json
        public JsonResult GetCobrosPendientes(int IdEmpresa = 0, int IdSucursal = 0, decimal IdTransaccionSession = 0)
        {
            Lista_Liquidacion_x_cobro_pendiente.set_list(bus_LiquidacionTarjeta_cxc_cobro.GetList(IdEmpresa, IdSucursal,null),IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class cxc_LiquidacionTarjeta_x_cxc_cobro_List
    {
        string Variable = "cxc_LiquidacionTarjeta_x_cxc_cobro_Info";
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list = new List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_LiquidacionTarjeta_x_cxc_cobro_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            list.Add(info_det);
        }

        public void UpdateRow(cxc_LiquidacionTarjeta_x_cxc_cobro_Info info_det, decimal IdTransaccionSession)
        {
            cxc_LiquidacionTarjeta_x_cxc_cobro_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdCobro = info_det.IdCobro;
            edited_info.IdLiquidacion = info_det.IdLiquidacion;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

    public class cxc_LiquidacionTarjeta_x_cxc_cobro_pendientes_List
    {
        string Variable = "cxc_LiquidacionTarjeta_x_cxc_cobro_pendientes_Info";
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list = new List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_LiquidacionTarjeta_x_cxc_cobro_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;


            list.Add(info_det);
        }

        public void UpdateRow(cxc_LiquidacionTarjeta_x_cxc_cobro_Info info_det, decimal IdTransaccionSession)
        {
            cxc_LiquidacionTarjeta_x_cxc_cobro_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdCobro = info_det.IdCobro;
            edited_info.IdLiquidacion = info_det.IdLiquidacion;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

    public class cxc_LiquidacionTarjetaDet_List
    {
        string Variable = "cxc_LiquidacionTarjetaDet_Info";
        public List<cxc_LiquidacionTarjetaDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_LiquidacionTarjetaDet_Info> list = new List<cxc_LiquidacionTarjetaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_LiquidacionTarjetaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_LiquidacionTarjetaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_LiquidacionTarjetaDet_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_LiquidacionTarjetaDet_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;


            list.Add(info_det);
        }

        public void UpdateRow(cxc_LiquidacionTarjetaDet_Info info_det, decimal IdTransaccionSession)
        {
            cxc_LiquidacionTarjetaDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdMotivo = info_det.IdMotivo;
            edited_info.Valor = info_det.Valor;
            edited_info.Secuencia = info_det.Secuencia;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cxc_LiquidacionTarjetaDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}