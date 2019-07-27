using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.General;
using DevExpress.Web.Mvc;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Info.General;
using DevExpress.Web;
using Core.Erp.Web.Areas.Banco.Controllers;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class NotaCreditoController : Controller
    {
        #region variables
        cp_nota_DebCre_Bus bus_orden_giro = new cp_nota_DebCre_Bus();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        cp_codigo_SRI_x_CtaCble_Bus bus_codigo_sri = new cp_codigo_SRI_x_CtaCble_Bus();
        cp_pagos_sri_Bus bus_forma_paogo = new cp_pagos_sri_Bus();
        cp_pais_sri_Bus bus_pais = new cp_pais_sri_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        cp_TipoDocumento_Bus bus_tipo_documento = new cp_TipoDocumento_Bus();
        cp_proveedor_Info info_proveedor = new cp_proveedor_Info();
        cp_proveedor_Bus bus_prov = new cp_proveedor_Bus();
        cp_parametros_Info info_parametro = new cp_parametros_Info();
        cp_parametros_Bus bus_param = new cp_parametros_Bus();
        ct_cbtecble_det_List_nc Lis_ct_cbtecble_det_List_nc = new ct_cbtecble_det_List_nc();
        cp_orden_pago_Bus bus_orden_pago = new cp_orden_pago_Bus();        
        cp_orden_pago_cancelaciones_Bus bus_orden_pago_cancelaciones = new cp_orden_pago_cancelaciones_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        cp_orden_pago_cancelaciones_List List_op = new cp_orden_pago_cancelaciones_List();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        string mensaje = string.Empty;
        cp_orden_pago_cancelaciones_List List_op_det = new cp_orden_pago_cancelaciones_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion
        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor_CXP()
        {
            decimal model = new decimal();
            return PartialView("_CmbProveedor_CXP", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }

        #region CmbCuenta_NC

        public ActionResult CmbCuenta_NC()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_NC", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_ctacble(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_ctacble(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #endregion
        #region vistas partial
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos_sucursal();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            cargar_combos_sucursal();
            return View(model);
        }
        private void cargar_combos_sucursal()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursales = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            lst_sucursales.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "TODAS"
            });
            ViewBag.lst_sucursales = lst_sucursales;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_nota_credito(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            ViewBag.IdSucursal = IdSucursal;
            ViewBag.IdEmpresa = IdEmpresa;
            List<cp_nota_DebCre_Info> model = new List<cp_nota_DebCre_Info>();
            model = bus_orden_giro.get_lst(IdEmpresa, IdSucursal, "C", Convert.ToDateTime(fecha_ini), Convert.ToDateTime(fecha_fin));
            return PartialView("_GridViewPartial_nota_credito", model);
        }

        public ActionResult GridViewPartial_nota_credito_dc()
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det =Lis_ct_cbtecble_det_List_nc.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_credito_dc", model);
        }
        public ActionResult GridViewPartial_nota_credito_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_op.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_nota_credito_det", model);
        }
        public ActionResult GridViewPartial_ordenes_pagos_con_saldo()
        {
            //var model = Session["list_op_por_proveedor"] as List<cp_orden_pago_cancelaciones_Info>;
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_op_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ordenes_pagos_con_saldo", model);
        }

         #endregion
        #region funciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var param = bus_param.get_info(IdEmpresa);
            cp_nota_DebCre_Info model = new cp_nota_DebCre_Info
            {
                IdEmpresa = IdEmpresa,
                Fecha_contable = DateTime.Now,
                cn_fecha = DateTime.Now,
                cn_Fecha_vcto = DateTime.Now,
                PaisPago = "593",
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdTipoCbte_Nota = (int)param.pa_TipoCbte_NC,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            List_op.set_list(model.lst_det_canc_op = new List<cp_orden_pago_cancelaciones_Info>(),model.IdTransaccionSession);
            Lis_ct_cbtecble_det_List_nc.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);
            List_op_det.set_list(new List<cp_orden_pago_cancelaciones_Info>(), model.IdTransaccionSession);
            cargar_combos(IdEmpresa,0,"");
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cp_nota_DebCre_Info model)
        {
            model.info_comrobante = new ct_cbtecble_Info();
            model.DebCre = "C";
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nc.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.lst_det_canc_op = List_op.get_list(model.IdTransaccionSession);
            model.lst_det_canc_op = List_op_det.get_list(model.IdTransaccionSession);
            if (model.info_comrobante.lst_ct_cbtecble_det==null)
            {
                ViewBag.mensaje = "Falta diario contable";
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                cargar_combos_detalle();
                return View(model);

            }
            model.info_comrobante.IdTipoCbte = model.IdTipoCbte_Nota;
            model.IdUsuario = SessionFixed.IdUsuario;            
            string mensaje = bus_orden_giro.validar(model);

            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                cargar_combos_detalle();
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_orden_giro.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipoCbte_Nota = model.IdTipoCbte_Nota, IdCbteCble_Nota= model.IdCbteCble_Nota, Exito = true });

        }
        public ActionResult Modificar(int IdEmpresa = 0 , int IdTipoCbte_Nota = 0, decimal IdCbteCble_Nota = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cp_nota_DebCre_Info model = bus_orden_giro.get_info(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
            if (model == null)
                return RedirectToAction("Index");
            if (model.info_comrobante.lst_ct_cbtecble_det == null)
                model.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lis_ct_cbtecble_det_List_nc.set_list(model.info_comrobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_op.set_list(bus_orden_pago_cancelaciones.get_list_x_pago(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota,SessionFixed.IdUsuario), model.IdTransaccionSession);
            List_op_det.set_list(bus_orden_pago_cancelaciones.get_list_x_pago(model.IdEmpresa, model.IdTipoCbte_Nota, model.IdCbteCble_Nota, SessionFixed.IdUsuario), model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
            cargar_combos_detalle();

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cn_fecha, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(cp_nota_DebCre_Info model)
        {           
            model.info_comrobante = new ct_cbtecble_Info();
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nc.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.lst_det_canc_op = List_op_det.get_list(model.IdTransaccionSession);
                model.lst_det_canc_op = List_op.get_list(model.IdTransaccionSession);
            
            if (model.info_comrobante.lst_ct_cbtecble_det == null)
            {
                ViewBag.mensaje = "Falta detalle de pago";
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                cargar_combos_detalle();
                return View(model);
            }
            
            string mensaje = bus_orden_giro.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                cargar_combos_detalle();
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario;

            if (!bus_orden_giro.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipoCbte_Nota = model.IdTipoCbte_Nota, IdCbteCble_Nota = model.IdCbteCble_Nota, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0 , int IdTipoCbte_Nota = 0, decimal IdCbteCble_Nota = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion           
            cp_nota_DebCre_Info model = bus_orden_giro.get_info(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
            if (model == null)
                return RedirectToAction("Index");
            if (model.info_comrobante.lst_ct_cbtecble_det == null)
                model.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            Lis_ct_cbtecble_det_List_nc.set_list(model.info_comrobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_op.set_list(bus_orden_pago_cancelaciones.get_list_x_pago(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota, SessionFixed.IdUsuario),model.IdTransaccionSession);
            List_op_det.set_list(bus_orden_pago_cancelaciones.get_list_x_pago(model.IdEmpresa, model.IdTipoCbte_Nota, model.IdCbteCble_Nota, SessionFixed.IdUsuario), model.IdTransaccionSession);
            cargar_combos(IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cn_fecha, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(cp_nota_DebCre_Info model)
        {
            bus_orden_giro = new cp_nota_DebCre_Bus();
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            model.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (!bus_orden_giro.anularDB(model))
            {
                cargar_combos(model.IdEmpresa,model.IdProveedor,model.IdIden_credito.ToString());
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region json

        public JsonResult get_list_tipo_doc(int IdEmpresa =0,decimal IdProveedor = 0, string codigoSRI = "")
        {
            var list_tipo_doc = bus_tipo_documento.get_list(IdEmpresa, IdProveedor, codigoSRI);
            return Json(list_tipo_doc, JsonRequestBehavior.AllowGet);
        }
        public JsonResult armar_diario(decimal IdProveedor = 0, double cn_subtotal_iva = 0, double cn_subtotal_siniva = 0,
        double valoriva = 0, double total = 0, string observacion = "", decimal IdTransaccionSession = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
           
                info_proveedor = bus_prov.get_info(IdEmpresa, IdProveedor);

                info_parametro = bus_param.get_info(IdEmpresa);           

            Lis_ct_cbtecble_det_List_nc.delete_detail_New_details(info_proveedor, info_parametro, cn_subtotal_iva, cn_subtotal_siniva, valoriva, total, observacion, IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Buscar_op(int IdEmpresa , decimal IdProveedor, decimal IdTransaccionSession = 0, int IdSucursal = 0)
        {
            try
            {
                string IdTipo_op = cl_enumeradores.eTipoOrdenPago.FACT_PROVEE.ToString();
                string IdUsuario = SessionFixed.IdUsuario;
                List_op_det.set_list(bus_orden_pago_cancelaciones.get_list_con_saldo(IdEmpresa, 0, "PROVEE", IdProveedor, "", IdUsuario, false, IdSucursal), IdTransaccionSession);
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult seleccionar_op(string Ids = "", decimal IdTransaccionSession = 0)
        {
            string[] array = Ids.Split(',');
            var output = array.GroupBy(q => q).ToList();
            var model = List_op.get_list(IdTransaccionSession);
            foreach (var item in output)
            {
                if (item.Key != "")
                {
                    List_op_det.set_list(new List<cp_orden_pago_cancelaciones_Info>(), IdTransaccionSession);
                    var lista_tmp = model.Where(v => v.IdOrdenPago_op == Convert.ToDecimal(item.Key));
                    var info_add = lista_tmp.FirstOrDefault();
                    info_add.MontoAplicado = (double)info_add.MontoAplicado;
                    List_op.AddRow(info_add, IdTransaccionSession);
                }
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region cargar combos

        private void cargar_combos(int IdEmpresa, decimal IdProveedor, string IdTipoSRI)
        {
          

            var lst_codigos_sri = bus_codigo_sri.get_list(IdEmpresa);
            ViewBag.lst_codigos_sri = lst_codigos_sri;

            var lst_forma_pago = bus_forma_paogo.get_list();
            ViewBag.lst_forma_pago = lst_forma_pago;

            var lst_paises = bus_pais.get_list();
            ViewBag.lst_paises = lst_paises;

            var lst_sucursales = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursales = lst_sucursales;
            if (IdProveedor != 0)
            {
                if (IdTipoSRI == "")
                    IdTipoSRI = "01";
                var list_tipo_doc = bus_tipo_documento.get_list(IdEmpresa, IdProveedor, IdTipoSRI);
                ViewBag.lst_tipo_doc = list_tipo_doc;
            }
            else
            {
                ViewBag.lst_tipo_doc = new List<cp_TipoDocumento_Info>();

            }
            Dictionary<string, string> lst_tipo_nota = new Dictionary<string, string>();
            lst_tipo_nota.Add("T_TIP_NOTA_SRI", "SRI");
            lst_tipo_nota.Add("T_TIP_NOTA_INT", "INTERNO");
            ViewBag.lst_tipo_nota = lst_tipo_nota;


            List<string> lst_tipo_servicio = new List<string>();
            lst_tipo_servicio.Add(cl_enumeradores.eTipoServicioCXP.SERVI.ToString());
            lst_tipo_servicio.Add(cl_enumeradores.eTipoServicioCXP.BIEN.ToString());
            lst_tipo_servicio.Add(cl_enumeradores.eTipoServicioCXP.AMBAS.ToString());
            ViewBag.lst_tipo_servicio = lst_tipo_servicio;


            Dictionary<string, string> lst_localizacion = new Dictionary<string, string>();
            lst_localizacion.Add("LOC", "LOCAL");
            lst_localizacion.Add("EXT", "EXTERIOR");
            ViewBag.lst_localizacion = lst_localizacion;

        }

        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;
        }
        

        #endregion
        #region Funcion diario contable
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ct_cbtecble_det_List_nc.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nc.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_credito_dc", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ct_cbtecble_det_List_nc.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nc.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_credito_dc", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            Lis_ct_cbtecble_det_List_nc.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nc.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_credito_dc", model);
        }
        #endregion
        #region Editar y eliminar detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_op([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_pago_cancelaciones_Info info_det)
        {
            //var model = List_op_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            //if (model.Count() > 0)
            //{
            //    cp_orden_pago_cancelaciones_Info edited_info = model.Where(m => m.IdOrdenPago_op == info_det.IdOrdenPago_op).First();

            //    edited_info.MontoAplicado = info_det.MontoAplicado;
            //}
           // List_op_det.set_list(model, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            if (ModelState.IsValid)
                List_op_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            var model = List_op_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return PartialView("_GridViewPartial_nota_credito_det", model);
        }

        public ActionResult EditingDelete_op(/*int IdOrdenPago*/ decimal IdOrdenPago_op)
        {
            //List<cp_orden_pago_det_Info> model = new List<cp_orden_pago_det_Info>();
            //model = Session["list_op_seleccionadas"] as List<cp_orden_pago_det_Info>;
            //if (model.Count() > 0)
            //{
            //    cp_orden_pago_det_Info edited_info = model.Where(m => m.IdOrdenPago == IdOrdenPago).First();
            //    model.Remove(edited_info);
            //    Session["list_op_seleccionadas"] = model;

            //}

            if (ModelState.IsValid)
                List_op_det.DeleteRow(IdOrdenPago_op, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            var model = List_op_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return PartialView("_GridViewPartial_nota_credito_det", model);
        }
        #endregion
    }
    public class ct_cbtecble_det_List_nc
    {
        string Variable = "ct_cbtecble_det_Info";
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public List<ct_cbtecble_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable +IdTransaccionSession.ToString()] == null)
            {
                List<ct_cbtecble_det_Info> list = new List<ct_cbtecble_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ct_cbtecble_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ct_cbtecble_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            info_det.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;

            if (info_det.IdCtaCble != null)
            {
                var cta = bus_plancta.get_info(IdEmpresa, info_det.IdCtaCble);
                if (cta != null)
                    info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
            }

            list.Add(info_det);
        }

        public void UpdateRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            ct_cbtecble_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();

            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.dc_para_conciliar = info_det.dc_para_conciliar;
            edited_info.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            edited_info.dc_Valor_debe = info_det.dc_Valor_debe;
            edited_info.dc_Valor_haber = info_det.dc_Valor_haber;

            var cta = bus_plancta.get_info(IdEmpresa, edited_info.IdCtaCble);
            if (cta != null)
                info_det.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
            edited_info.pc_Cuenta = info_det.pc_Cuenta;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }

        public void delete_detail_New_details(cp_proveedor_Info info_proveedor, cp_parametros_Info info_parametro, double cn_subtotal_iva = 0,
        
        double cn_subtotal_siniva = 0, double cn_valoriva = 0, double cn_total = 0, string observacion = "", decimal IdTransaccionSession = 0)
        {
            try
            {

                set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);

                // cuenta total
                ct_cbtecble_det_Info cbtecble_det_total_Info = new ct_cbtecble_det_Info();
                cbtecble_det_total_Info.secuencia = 3;
                cbtecble_det_total_Info.IdEmpresa = 0;
                cbtecble_det_total_Info.IdTipoCbte = 1;
                cbtecble_det_total_Info.IdCtaCble = info_proveedor.IdCtaCble_CXP;
                cbtecble_det_total_Info.dc_Valor_debe = cn_total;
                cbtecble_det_total_Info.dc_Valor = cn_total * -1;
                cbtecble_det_total_Info.dc_Observacion = observacion;
                AddRow(cbtecble_det_total_Info, IdTransaccionSession);

                if (cn_subtotal_iva > 0)
                {
                    // cuenta iva
                    ct_cbtecble_det_Info cbtecble_det_iva_Info = new ct_cbtecble_det_Info();
                    cbtecble_det_iva_Info.secuencia = 2;
                    cbtecble_det_iva_Info.IdEmpresa = 0;
                    cbtecble_det_iva_Info.IdTipoCbte = 1;
                    cbtecble_det_iva_Info.IdCtaCble = info_parametro.pa_ctacble_iva;
                    cbtecble_det_iva_Info.dc_Valor_haber = cn_valoriva;
                    cbtecble_det_iva_Info.dc_Valor = cn_valoriva;
                    cbtecble_det_iva_Info.dc_Observacion = observacion;
                    AddRow(cbtecble_det_iva_Info, IdTransaccionSession);
                }

                // cuenta sbtotal
                ct_cbtecble_det_Info cbtecble_det_sub_Info = new ct_cbtecble_det_Info();
                cbtecble_det_sub_Info.secuencia = 1;
                cbtecble_det_sub_Info.IdEmpresa = 0;
                cbtecble_det_sub_Info.IdTipoCbte = 1;
                cbtecble_det_sub_Info.IdCtaCble = info_parametro.pa_ctacble_deudora;
                cbtecble_det_sub_Info.dc_Valor_haber = cn_subtotal_iva + cn_subtotal_siniva;
                cbtecble_det_sub_Info.dc_Valor = cn_subtotal_iva + cn_subtotal_siniva;
                cbtecble_det_sub_Info.dc_Observacion = observacion;
                AddRow(cbtecble_det_sub_Info, IdTransaccionSession);

            }
            catch (Exception)
            {
            }
        }

    }
    //public class cp_orden_pago_cancelaciones_NC_List
    //{
    //    public List<cp_orden_pago_cancelaciones_Info> get_list(decimal IdTransaccionSession)
    //    {
    //        if (HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info" + IdTransaccionSession.ToString()] == null)
    //        {
    //            List<cp_orden_pago_cancelaciones_Info> list = new List<cp_orden_pago_cancelaciones_Info>();

    //            HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info" + IdTransaccionSession.ToString()] = list;
    //        }
    //        return (List<cp_orden_pago_cancelaciones_Info>)HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info" + IdTransaccionSession.ToString()];
    //    }

    //    public void set_list(List<cp_orden_pago_cancelaciones_Info> list, decimal IdTransaccionSession)
    //    {
    //        HttpContext.Current.Session["cp_orden_pago_cancelaciones_Info" + IdTransaccionSession.ToString()] = list;
    //    }
        
    //    public void UpdateRow(cp_orden_pago_cancelaciones_Info info_det, decimal IdTransaccionSession)
    //    {
    //        cp_orden_pago_cancelaciones_Info info = get_list(IdTransaccionSession).Where(q => q.IdOrdenPago_op == info_det.IdOrdenPago_op).FirstOrDefault();
    //        if (info != null)
    //        {
    //            info.MontoAplicado = info_det.MontoAplicado;
    //        }
    //    }

    //    public void DeleteRow(decimal IdOrdenPago_op, decimal IdTransaccionSession)
    //    {
    //        List<cp_orden_pago_cancelaciones_Info> list = get_list(IdTransaccionSession);
    //        list.Remove(list.Where(m => m.IdOrdenPago_op == IdOrdenPago_op).First());
    //    }
    //}
}