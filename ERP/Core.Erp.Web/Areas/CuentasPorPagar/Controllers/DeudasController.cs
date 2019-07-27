using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using Core.Erp.Info.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Inventario;
using Core.Erp.Info.Banco;
using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Presupuesto;
using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.Compras;
using Core.Erp.Info.Compras;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    [SessionTimeout]
    public class DeudasController : Controller
    {
        #region variables
        cp_orden_giro_Bus bus_orden_giro = new cp_orden_giro_Bus();
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
        ct_cbtecble_det_List_fp Lis_ct_cbtecble_det_List = new ct_cbtecble_det_List_fp();
        cp_orden_giro_det_Info_List List_det = new cp_orden_giro_det_Info_List();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        cp_orden_giro_det_Bus bus_det = new cp_orden_giro_det_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_documento = new tb_sis_Documento_Tipo_Talonario_Bus();
        cp_orden_giro_det_ing_x_oc_Bus bus_orden_giro_det_ing_x_oc = new cp_orden_giro_det_ing_x_oc_Bus();
        cp_orden_giro_det_ing_x_os_Bus bus_orden_giro_det_ing_x_os = new cp_orden_giro_det_ing_x_os_Bus();
        cp_orden_giro_det_ing_x_oc_List ListaPorIngresar = new cp_orden_giro_det_ing_x_oc_List();
        cp_orden_giro_det_ing_x_oc_ListaDetalle ListaDetalleOC = new cp_orden_giro_det_ing_x_oc_ListaDetalle();
        cp_orden_giro_det_ing_x_os_List ListaOSPorIngresar = new cp_orden_giro_det_ing_x_os_List();
        cp_orden_giro_det_ing_x_os_ListaDetalle ListaDetalleOS = new cp_orden_giro_det_ing_x_os_ListaDetalle();
        com_ordencompra_local_Bus bus_ordencompra = new com_ordencompra_local_Bus();

        ct_cbtecble_det_List_re List_ct_cbtecble_det_List_retencion = new ct_cbtecble_det_List_re();
        cp_retencion_det_lst List_cp_retencion_det = new cp_retencion_det_lst();
        cp_retencion_Bus bus_retencion = new cp_retencion_Bus();
        cp_orden_giro_det_PorIngresar_List List_det_PorIngresar = new cp_orden_giro_det_PorIngresar_List();
        cp_codigo_SRI_Bus bus_sri = new cp_codigo_SRI_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor_CXP()
        {
            cp_orden_giro_Info model = new cp_orden_giro_Info();
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

        public ActionResult CmbCuenta_Deuda()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_Deuda", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demandaPlancta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demandaPlancta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos ComboBox bajo demanda flujo
        ba_TipoFlujo_Bus bus_tipo = new ba_TipoFlujo_Bus();
        public ActionResult CmbFlujo_Deudas()
        {
            decimal model = new decimal();
            return PartialView("_CmbFlujo_Deudas", model);
        }
        public List<ba_TipoFlujo_Info> get_list_bajo_demandaFlujo(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_tipo.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoIngEgr.EGR.ToString());
        }
        public ba_TipoFlujo_Info get_info_bajo_demandaFlujo(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_tipo.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos ComboBox bajo demanda de producto
        public ActionResult CmbProducto_deudas()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_deudas", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda_producto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORSUCURSAL, cl_enumeradores.eModulo.FAC, 0, Convert.ToInt32(SessionFixed.IdSucursal));
        }
        public in_Producto_Info get_info_bajo_demanda_producto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region MyRegion
        public ActionResult CmbCuenta_OC()
        {
            cp_orden_giro_det_ing_x_oc_Info model = new cp_orden_giro_det_ing_x_oc_Info();
            return PartialView("_CmbCuenta_OC_det", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_cmbCuenta_OC_det(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_cmbCuenta_OC_det(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Facturas por proveedor
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

        [ValidateInput(false)]
        public ActionResult GridViewPartial_deudas(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            if (IdSucursal == 0)
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_orden_giro.get_lst(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_deudas", model);
        }

        #endregion

        #region Aprobacion de facturas por proveedor
        public ActionResult Index3()
        {
            cp_orden_giro_Info model = new cp_orden_giro_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            Session["list_facturas_seleccionadas"] = null;
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_aprobacion_facturas()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<cp_orden_giro_aprobacion_Info> model = new List<cp_orden_giro_aprobacion_Info>();
            model = Session["list_facturas_seleccionadas"] as List<cp_orden_giro_aprobacion_Info>;
            return PartialView("_GridViewPartial_aprobacion_facturas", model);
        }
        public ActionResult GridViewPartial_facturas_con_saldos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<cp_orden_giro_Info> model = (List<cp_orden_giro_Info>)Session["list_ordenes_giro"];            
            return PartialView("_GridViewPartial_facturas_con_saldos", model);
        }
        #endregion

        #region vista de Detalle de cuotas y diario contable
        [ValidateInput(false)]
        public ActionResult GridViewPartial_deudas_dc()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_deudas_dc", model);
        }

        #endregion

        #region Funciones cargar combos
        private void cargar_combos(cp_orden_giro_Info model)
        {
            var lst_codigos_sri = bus_codigo_sri.get_list(model.IdEmpresa);
            ViewBag.lst_codigos_sri = lst_codigos_sri;

            var lst_forma_pago = bus_forma_paogo.get_list();
            ViewBag.lst_forma_pago = lst_forma_pago;

            var lst_paises = bus_pais.get_list();
            ViewBag.lst_paises = lst_paises;

            var lst_doc_tipo = bus_tipo_documento.get_list(false);
            ViewBag.lst_doc_tipo = lst_doc_tipo;

            var lst_sucursales = bus_sucursal.GetList(model.IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursales = lst_sucursales;

            var lst_sucursales_cxp = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursales_cxp = lst_sucursales_cxp;

            var lst_bodega = bus_bodega.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;

            if (model.IdProveedor != 0)
            {
                var list_tipo_doc = bus_tipo_documento.get_list(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                ViewBag.lst_tipo_doc = list_tipo_doc;
            }
            else
            {
                ViewBag.lst_tipo_doc = new List<cp_TipoDocumento_Info>();

            }


            Dictionary<string, string> lst_pagos = new Dictionary<string, string>();
            lst_pagos.Add("LOC", "LOCAL");
            lst_pagos.Add("EXT", "EXTERIOR");
            ViewBag.lst_pagos = lst_pagos;

            var lst_punto_venta = bus_punto_venta.get_list_x_tipo_doc(model.IdEmpresa, model.IdSucursal, "RETEN");
            ViewBag.lst_punto_venta = lst_punto_venta;

        }

        private void cargar_combos_consulta()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            lst_sucursal.Add(new tb_sucursal_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = 0,
                Su_Descripcion = "TODAS"
            });
            ViewBag.lst_sucursal = lst_sucursal;
        }

        private bool validar(cp_orden_giro_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.co_FechaFactura, cl_enumeradores.eModulo.CXP, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.co_FechaFactura, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            return true;
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

            tb_sis_Documento_Tipo_Talonario_Info info_documento = new tb_sis_Documento_Tipo_Talonario_Info();
            var sucursal = bus_sucursal.get_info(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal));

            cp_orden_giro_Info model = new cp_orden_giro_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                co_FechaFactura = DateTime.Now.Date,
                co_FechaContabilizacion = DateTime.Now.Date,
                co_FechaFactura_vct = DateTime.Now.Date,
                PaisPago = "593",
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTipoServicio = cl_enumeradores.eTipoServicioCXP.SERVI.ToString(),
                info_retencion = new cp_retencion_Info()
            };

            model.info_retencion = new cp_retencion_Info();
            if (model.info_retencion.IdRetencion == 0)
            {
                model.TieneRetencion = 0;
            }
            else
            {
                model.TieneRetencion = 1;
            }

            List_cp_retencion_det.set_list(new List<cp_retencion_det_Info>(), model.IdTransaccionSession);
            List_ct_cbtecble_det_List_retencion.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);

            Lis_ct_cbtecble_det_List.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);
            List_det.set_list(new List<cp_orden_giro_det_Info>(), model.IdTransaccionSession);

            ListaDetalleOC.set_list(new List<cp_orden_giro_det_ing_x_oc_Info>(), model.IdTransaccionSession);
            ListaDetalleOS.set_list(new List<cp_orden_giro_det_ing_x_os_Info>(), model.IdTransaccionSession);

            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cp_orden_giro_Info model)
        {
            info_proveedor = bus_prov.get_info(model.IdEmpresa, model.IdProveedor);
            info_parametro = bus_param.get_info(model.IdEmpresa);

            if (bus_orden_giro.si_existe(model))
            {
                ViewBag.mensaje = "El documento " + model.co_serie + " " + model.co_factura + ", ya se encuentra registrado";
                cargar_combos(model);

                return View(model);
            }

            if (info_parametro == null)
            {
                ViewBag.mensaje = "Falta parametrizar el módulo de cuentas por pagar";
                cargar_combos(model);

                return View(model);
            }
            model.info_comrobante = new ct_cbtecble_Info();
            model.info_comrobante.IdTipoCbte = (int)info_parametro.pa_TipoCbte_OG;

            var ct_ret = List_ct_cbtecble_det_List_retencion.get_list(model.IdTransaccionSession);
            var ct_factura = Lis_ct_cbtecble_det_List.get_list(model.IdTransaccionSession);

            model.info_retencion.detalle = List_cp_retencion_det.get_list(model.IdTransaccionSession);
            model.info_retencion.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List_retencion.get_list(model.IdTransaccionSession);
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list(model.IdTransaccionSession);

            if (model.info_retencion.detalle.Count() > 0)
            {
                model.info_retencion.detalle.ForEach(item =>
                {
                    cp_codigo_SRI_Info info_ = bus_sri.get_info(model.IdEmpresa, item.IdCodigo_SRI);
                    item.re_Codigo_impuesto = info_.codigoSRI;
                    if (info_.IdTipoSRI == "COD_RET_IVA")
                    {
                        item.re_tipoRet = "IVA";
                    }
                    if (info_.IdTipoSRI == "COD_RET_FUE")
                    {
                        item.re_tipoRet = "RTF";
                    }
                });
            }

            if (Lis_ct_cbtecble_det_List.get_list(model.IdTransaccionSession).Count() == 0)
            {
                ViewBag.mensaje = "Falta diario contable";
                cargar_combos(model);

                return View(model);

            }
            if (info_proveedor.info_persona.pe_cedulaRuc != SessionFixed.Ruc)
                model.IdSucursal_cxp = null;
            string mensaje = bus_orden_giro.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = true;
                return View(model);
            }

            model.IdUsuario = SessionFixed.IdUsuario;
            if (!validar(model, ref mensaje))
            {
                ViewBag.MostrarBoton = true;
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            model.lst_det = List_det.get_list(model.IdTransaccionSession);
            model.lst_det_oc = ListaDetalleOC.get_list(model.IdTransaccionSession);
            model.lst_det_os = ListaDetalleOS.get_list(model.IdTransaccionSession);

            if (!bus_orden_giro.guardarDB(model))
            {
                ViewBag.MostrarBoton = true;
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipoCbte_Ogiro = model.IdTipoCbte_Ogiro, IdCbteCble_Ogiro = model.IdCbteCble_Ogiro, Exito = true });

        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipoCbte_Ogiro = 0, decimal IdCbteCble_Ogiro = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cp_orden_giro_Info model = bus_orden_giro.get_info(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");

            if (model.info_comrobante.lst_ct_cbtecble_det == null)
                model.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();

            Lis_ct_cbtecble_det_List.set_list(model.info_comrobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.info_retencion.detalle, model.IdTransaccionSession);
            List_det.set_list(bus_det.get_list(model.IdEmpresa, model.IdTipoCbte_Ogiro, model.IdCbteCble_Ogiro), model.IdTransaccionSession);

            model.info_retencion = bus_retencion.get_info(model.IdEmpresa, model.IdCbteCble_Ogiro, model.IdTipoCbte_Ogiro);
            model.info_retencion = bus_retencion.get_info(model.info_retencion.IdEmpresa, model.info_retencion.IdRetencion);

            model.lst_det_oc = bus_orden_giro_det_ing_x_oc.get_list(model.IdEmpresa, model.IdCbteCble_Ogiro, model.IdTipoCbte_Ogiro);
            model.lst_det_os = bus_orden_giro_det_ing_x_os.get_list(model.IdEmpresa, model.IdCbteCble_Ogiro, model.IdTipoCbte_Ogiro);

            if (model.info_retencion.IdRetencion == 0)
            {
                model.TieneRetencion = 0;
            }
            else
            {
                model.TieneRetencion = 1;
            }

            List_ct_cbtecble_det_List_retencion.set_list(model.info_retencion.info_comprobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.info_retencion.detalle, model.IdTransaccionSession);

            ListaDetalleOC.set_list(model.lst_det_oc, model.IdTransaccionSession);
            ListaDetalleOS.set_list(model.lst_det_os, model.IdTransaccionSession);

            cargar_combos(model);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.co_FechaFactura, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(cp_orden_giro_Info model)
        {
            info_proveedor = bus_prov.get_info(model.IdEmpresa, model.IdProveedor);
            info_parametro = bus_param.get_info(model.IdEmpresa);

            if (info_parametro == null)
            {
                ViewBag.mensaje = "Falta parametros del modulo cuenta por pagar";
                cargar_combos(model);
                ViewBag.MostrarBoton = true;
                return View(model);
            }

            model.info_comrobante.IdTipoCbte = (int)info_parametro.pa_TipoCbte_OG;
            model.info_retencion.detalle = List_cp_retencion_det.get_list(model.IdTransaccionSession);
            model.info_retencion.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List_retencion.get_list(model.IdTransaccionSession);
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list(model.IdTransaccionSession);

            if (model.info_retencion.detalle.Count() > 0)
            {
                model.info_retencion.detalle.ForEach(item =>
                {
                    cp_codigo_SRI_Info info_ = bus_sri.get_info(model.IdEmpresa, item.IdCodigo_SRI);
                    item.re_Codigo_impuesto = info_.codigoSRI;
                    if (info_.IdTipoSRI == "COD_RET_IVA")
                    {
                        item.re_tipoRet = "IVA";
                    }
                    if (info_.IdTipoSRI == "COD_RET_FUE")
                    {
                        item.re_tipoRet = "RTF";
                    }
                });

            }

            if (model.info_comrobante.lst_ct_cbtecble_det.Count() == 0)
            {
                ViewBag.mensaje = "Falta diario contable";
                cargar_combos(model);
                ViewBag.MostrarBoton = true;
                return View(model);

            }
            if (info_proveedor.info_persona.pe_cedulaRuc != SessionFixed.Ruc)
                model.IdSucursal_cxp = null;

            string mensaje = bus_orden_giro.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = true;
                return View(model);
            }

            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            model.IdUsuario = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = true;
                return View(model);
            }

            model.lst_det = List_det.get_list(model.IdTransaccionSession);
            model.lst_det_oc = ListaDetalleOC.get_list(model.IdTransaccionSession);
            model.lst_det_os = ListaDetalleOS.get_list(model.IdTransaccionSession);

            if (bus_orden_giro.ValidarExisteOrdenPAgo(model.IdEmpresa, model.IdTipoCbte_Ogiro, model.IdCbteCble_Ogiro) == true)
            {
               if(!bus_orden_giro.ModificarDBCabecera(model))
                {
                    cargar_combos(model);
                    ViewBag.MostrarBoton = true;
                    return View(model);
                }               
            }
            else
            if (!bus_orden_giro.modificarDB(model))
            {
                ViewBag.MostrarBoton = true;
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipoCbte_Ogiro = model.IdTipoCbte_Ogiro, IdCbteCble_Ogiro = model.IdCbteCble_Ogiro, Exito = true });

        }
        public ActionResult Anular(int IdEmpresa = 0, int IdTipoCbte_Ogiro = 0, decimal IdCbteCble_Ogiro = 0)
        {
            cp_orden_giro_Info model = bus_orden_giro.get_info(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            #endregion
            if (model == null)
                return RedirectToAction("Index");
            if (model.info_comrobante.lst_ct_cbtecble_det == null)
                model.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();

            Lis_ct_cbtecble_det_List.set_list(model.info_comrobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_det.set_list(bus_det.get_list(model.IdEmpresa, model.IdTipoCbte_Ogiro, model.IdCbteCble_Ogiro), model.IdTransaccionSession);
            model.info_retencion = bus_retencion.get_info(model.IdEmpresa, model.IdCbteCble_Ogiro, model.IdTipoCbte_Ogiro);
            model.info_retencion = bus_retencion.get_info(model.info_retencion.IdEmpresa, model.info_retencion.IdRetencion);

            if (model.info_retencion.IdRetencion == 0)
            {
                model.TieneRetencion = 0;
            }
            else
            {
                model.TieneRetencion = 1;
            }

            model.lst_det_oc = bus_orden_giro_det_ing_x_oc.get_list(model.IdEmpresa, model.IdCbteCble_Ogiro, model.IdTipoCbte_Ogiro);
            ListaDetalleOC.set_list(model.lst_det_oc, model.IdTransaccionSession);

            model.lst_det_os = bus_orden_giro_det_ing_x_os.get_list(model.IdEmpresa, model.IdCbteCble_Ogiro, model.IdTipoCbte_Ogiro);
            ListaDetalleOS.set_list(model.lst_det_os, model.IdTransaccionSession);

            List_ct_cbtecble_det_List_retencion.set_list(model.info_retencion.info_comprobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.info_retencion.detalle, model.IdTransaccionSession);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.co_FechaFactura, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cp_orden_giro_Info model)
        {
            info_proveedor = bus_prov.get_info(model.IdEmpresa, model.IdProveedor);
            info_parametro = bus_param.get_info(model.IdEmpresa);

            if (info_parametro == null)
            {
                ViewBag.mensaje = "Falta parametros del modulo cuenta por pagar";
                ViewBag.MostrarBoton = true;
                cargar_combos(model);
                return View(model);
            }

            model.info_comrobante = new ct_cbtecble_Info();

            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list(model.IdTransaccionSession);
            model.info_retencion.detalle = List_cp_retencion_det.get_list(model.IdTransaccionSession);

            string mensaje = bus_orden_giro.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = true;
                return View(model);
            }
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = true;
                return View(model);
            }
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.lst_det = List_det.get_list(model.IdTransaccionSession);
            model.lst_det_oc = ListaDetalleOC.get_list(model.IdTransaccionSession);
            model.lst_det_os = ListaDetalleOS.get_list(model.IdTransaccionSession);

            if (!bus_orden_giro.anularDB(model))
            {
                ViewBag.MostrarBoton = true;
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region json
        public JsonResult AutorizarSRI(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            string retorno = string.Empty;

            if (bus_retencion.ModificarEstadoAutorizacion(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro))
                retorno = "Autorización exitosa";
            
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidarCompraSucursales(int IdEmpresa = 0, decimal IdProveedor = 0)
        {
            string retorno = string.Empty;

            if (IdProveedor != 0)
            {
                var prov = bus_proveedor.get_info(IdEmpresa, IdProveedor);
                if (prov != null && prov.info_persona.pe_cedulaRuc == SessionFixed.Ruc)
                    retorno = "S";
            }
            
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListOrdenesPorPagar(int IdEmpresa = 0)
        {

            string retorno = string.Empty;
            var lst  = bus_orden_giro.get_lst_orden_giro_x_pagar(IdEmpresa);

                Session["list_ordenes_giro"] = lst;
                retorno = "S";
            
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_list_tipo_doc(int IdEmpresa = 0, decimal IdProveedor = 0, string codigoSRI = "")
        {
            var list_tipo_doc = bus_tipo_documento.get_list(IdEmpresa, IdProveedor, codigoSRI);
            return Json(list_tipo_doc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult armar_diario(decimal IdProveedor = 0, double co_subtotal_iva = 0, double co_subtotal_siniva = 0, double co_valoriva = 0, double co_total = 0, string observacion = "", decimal IdTransaccionSession = 0, int IdSucursal_cxp = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            info_proveedor = bus_prov.get_info(IdEmpresa, IdProveedor);
            if (info_proveedor.info_persona.pe_cedulaRuc == SessionFixed.Ruc)
            {
                var sucursal = bus_sucursal.get_info(IdEmpresa, IdSucursal_cxp);
                info_proveedor.IdCtaCble_CXP = sucursal.IdCtaCble_cxp;
            }
            info_parametro = bus_param.get_info(IdEmpresa);

            double Subtotal0 = 0;
            double SubtotalIVA = 0;
            double IVA = 0;
            double Total = 0;
            double Diferencia = 0;
            
            Lis_ct_cbtecble_det_List.set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);
            var lst_det = List_det.get_list(IdTransaccionSession);
            var lst_det_oc = ListaDetalleOC.get_list(IdTransaccionSession);
            string TipoDiario = lst_det_oc.Count > 0 ? "OC" : (lst_det.Count> 0 ? "INV" : "NA");

            if (TipoDiario == "NA")
            {
                Subtotal0 = Math.Round(co_subtotal_siniva, 2, MidpointRounding.AwayFromZero);
                SubtotalIVA = Math.Round(co_subtotal_iva, 2, MidpointRounding.AwayFromZero);
                IVA = Math.Round(co_valoriva, 2, MidpointRounding.AwayFromZero);
                Total = Math.Round(co_total, 2, MidpointRounding.AwayFromZero);
            }
            else if (TipoDiario == "OC")
            {
                Subtotal0 = Math.Round((lst_det_oc.Where(q => q.Por_Iva == 0).ToList().Sum(q => q.do_subtotal)), 2, MidpointRounding.AwayFromZero);
                SubtotalIVA = Math.Round((lst_det_oc.Where(q => q.Por_Iva > 0).ToList().Sum(q => q.do_subtotal)), 2, MidpointRounding.AwayFromZero);
                IVA = Math.Round(lst_det_oc.Sum(q => q.do_iva), 2, MidpointRounding.AwayFromZero);
                Total = Math.Round(lst_det_oc.Sum(q => q.do_total), 2, MidpointRounding.AwayFromZero);                
            }
            else
            {
                Subtotal0 = Math.Round(lst_det.Where(q=>q.PorIva == 0).Sum(q=>q.Subtotal), 2, MidpointRounding.AwayFromZero);
                SubtotalIVA = Math.Round(lst_det.Where(q => q.PorIva > 0).Sum(q => q.Subtotal), 2, MidpointRounding.AwayFromZero);
                IVA = Math.Round(lst_det.Sum(q => q.ValorIva), 2, MidpointRounding.AwayFromZero);
                Total = Math.Round(lst_det.Sum(q => q.Total), 2, MidpointRounding.AwayFromZero);
            }
            Diferencia = Math.Round(Subtotal0 + SubtotalIVA + IVA - Total, 2);
            if (Math.Round(Diferencia, 2) != 0)
                Total += Diferencia;
            Total = Math.Round(Total, 2, MidpointRounding.AwayFromZero);
            #region Armar diario sin inventario

            #region Proveedor
            Lis_ct_cbtecble_det_List.AddRow(new ct_cbtecble_det_Info
            {
                IdCtaCble = info_proveedor.IdCtaCble_CXP,
                dc_Valor_haber = Math.Round(Total, 2, MidpointRounding.AwayFromZero),
                dc_Valor = Math.Round(Total * -1, 2, MidpointRounding.AwayFromZero),
                dc_Observacion = observacion,
            }, IdTransaccionSession);
            #endregion

            #region I.V.A.
            if (IVA > 0)
            {                
                Lis_ct_cbtecble_det_List.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = info_parametro.pa_ctacble_iva,
                    dc_Valor_debe = Math.Round(IVA, 2),
                    dc_Valor = Math.Round(IVA, 2),
                    dc_Observacion = observacion,
                }, IdTransaccionSession);                
            }
            #endregion

            #region Subtotal
            if (TipoDiario == "NA")
            {
                Lis_ct_cbtecble_det_List.AddRow(new ct_cbtecble_det_Info
                {
                    IdCtaCble = info_proveedor.IdCtaCble_Gasto,
                    dc_Valor_debe = Math.Round(Subtotal0 + SubtotalIVA, 2),
                    dc_Valor = Math.Round(Subtotal0 + SubtotalIVA, 2),
                    dc_Observacion = observacion
                }, IdTransaccionSession);
            }
            else if(TipoDiario =="OC")
            {
                var lst_g = (from q in lst_det_oc
                             group q by new { q.IdCtaCble } into g
                             select new
                             {
                                 IdCtaCble = g.Key.IdCtaCble,
                                 Valor = g.Sum(q => q.do_subtotal)
                             }).ToList();

                int cont = 1;
                foreach (var item in lst_g)
                {
                    ct_cbtecble_det_Info det = new ct_cbtecble_det_Info
                    {
                        IdCtaCble = item.IdCtaCble,
                        dc_Valor_debe = Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero),
                        dc_Valor = Math.Round(item.Valor, 2, MidpointRounding.AwayFromZero),
                        dc_Observacion = observacion
                    };
                    Lis_ct_cbtecble_det_List.AddRow(det, IdTransaccionSession);
                    cont++;
                }
            }
            else
            {
                var lst_g = (from q in lst_det
                             group q by new { q.IdCtaCbleInv } into g
                             select new
                             {
                                 IdCtaCble = g.Key.IdCtaCbleInv,
                                 Valor = g.Sum(q => q.Subtotal)
                             }).ToList();

                int cont = 1;
                foreach (var item in lst_g)
                {
                    ct_cbtecble_det_Info det = new ct_cbtecble_det_Info
                    {
                        IdCtaCble = item.IdCtaCble,
                        dc_Valor_debe = Math.Round(item.Valor, 2,MidpointRounding.AwayFromZero),
                        dc_Valor = Math.Round(item.Valor, 2,MidpointRounding.AwayFromZero),
                        dc_Observacion = observacion
                    };
                    Lis_ct_cbtecble_det_List.AddRow(det, IdTransaccionSession);
                    cont++;
                }
            }
            #endregion

            #endregion

            return Json(new { Subtotal0 = Subtotal0, SubtotalIVA = SubtotalIVA, IVA = IVA, Total = Total}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult guardar_aprobacion(string Ids)
        {
           
            List<cp_orden_giro_aprobacion_Info> model = new List<cp_orden_giro_aprobacion_Info>();
            model = Session["list_facturas_seleccionadas"] as List<cp_orden_giro_aprobacion_Info>;
            foreach (var item in model)
            {
                bus_orden_giro.Generar_OP_x_orden_giro(new cp_orden_giro_Info
                {
                    IdEmpresa = item.IdEmpresa,
                    IdTipoCbte_Ogiro = item.IdTipoCbte_Ogiro,
                    IdCbteCble_Ogiro = item.IdCbteCble_Ogiro,
                    IdProveedor = item.IdProveedor,
                    co_factura = item.co_factura,
                    co_valorpagar = item.co_valorpagar,
                    nom_tipo_Documento = item.nom_tipo_Documento,
                    IdSucursal = (int)item.IdSucursal,
                    IdUsuario = SessionFixed.IdUsuario
                });
            }
            Session["list_facturas_seleccionadas"] = null;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult seleccionar_aprobacion(string Ids)
        {
            if (Ids != null)
            {
                string[] array = Ids.Split(',');
                var output = array.GroupBy(q => q).ToList();
                List<cp_orden_giro_Info> model = new List<cp_orden_giro_Info>();
                List<cp_orden_giro_aprobacion_Info> list_facturas_seleccionadas = new List<cp_orden_giro_aprobacion_Info>();
                model = Session["list_ordenes_giro"] as List<cp_orden_giro_Info>;
                list_facturas_seleccionadas = Session["list_facturas_seleccionadas"] as List<cp_orden_giro_aprobacion_Info>;
                if (list_facturas_seleccionadas == null)
                    list_facturas_seleccionadas = new List<cp_orden_giro_aprobacion_Info>();
                foreach (var item in output)
                {
                    if (item.Key != "")
                    {
                        var lista_tmp = model.Where(v => v.SecuencialID == item.Key);
                        if (lista_tmp.Count() == 1 & list_facturas_seleccionadas.Where(v => v.SecuencialID == item.Key).Count() == 0)// agrego si existe y no esta repetida
                        {
                            var info_add = lista_tmp.FirstOrDefault();
                            info_add.co_valorpagar = (double)info_add.Saldo_OG;

                            list_facturas_seleccionadas.Add(new cp_orden_giro_aprobacion_Info{
                                IdEmpresa = info_add.IdEmpresa,
                                IdTipoCbte_Ogiro = info_add.IdTipoCbte_Ogiro,
                                IdCbteCble_Ogiro = info_add.IdCbteCble_Ogiro,
                                co_factura = info_add.co_factura,
                                co_fechaOg = info_add.co_fechaOg,
                                co_FechaFactura_vct = info_add.co_FechaFactura_vct,
                                Tipo_Vcto = info_add.Tipo_Vcto,
                                Saldo_OG = info_add.Saldo_OG,
                                co_valorpagar = info_add.co_valorpagar,
                                IdProveedor = info_add.IdProveedor,
                                SecuencialID = info_add.SecuencialID,
                                nom_tipo_Documento = info_add.nom_tipo_Documento,
                                info_proveedor = new cp_proveedor_Info{
                                    info_persona = new tb_persona_Info{
                                        pe_nombreCompleto = info_add.info_proveedor.info_persona.pe_nombreCompleto
                                    }
                                },
                                IdSucursal = info_add.IdSucursal
                                ,IdUsuario = SessionFixed.IdUsuario
                            });
                        }
                    }
                }
                Session["list_facturas_seleccionadas"] = list_facturas_seleccionadas;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInfo_Producto(int IdEmpresa = 0, decimal IdProducto = 0)
        {
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListIng_Inv_OC_PorIngresar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, int IdSucursal = 0, decimal IdProveedor= 0)
        {
            var lst = bus_orden_giro_det_ing_x_oc.get_list_x_ingresar(IdEmpresa, IdSucursal, IdProveedor);
            ListaPorIngresar.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList_OrderServicio_PorIngresar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, int IdSucursal = 0, decimal IdProveedor = 0)
        {
            var lst = bus_orden_giro_det_ing_x_os.get_list_x_ingresar(IdEmpresa, IdSucursal, IdProveedor);
            ListaOSPorIngresar.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUltimoDocumento(int IdEmpresa=0, int IdSucursal = 0, int IdPuntoVta = 0, int TieneRetencion= 0)
        {
            tb_sis_Documento_Tipo_Talonario_Info resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            //cp_retencion_Info info_retencion = new cp_retencion_Info();

            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            //info_retencion = bus_retencion.get_info(IdEmpresa, IdCbteCble_Ogiro, IdTipoCbte_Ogiro);

            if (punto_venta != null)
            {
                var sucursal = bus_sucursal.get_info(IdEmpresa, IdSucursal);
                if(TieneRetencion == 0)
                {
                    resultado = bus_talonario.GetUltimoNoUsado(IdEmpresa, cl_enumeradores.eTipoDocumento.RETEN.ToString(), sucursal.Su_CodigoEstablecimiento, punto_venta.cod_PuntoVta, punto_venta.EsElectronico, false);
                }                
                //if (info_retencion.IdRetencion == 0)
                //{
                //    resultado = bus_talonario.GetUltimoNoUsado(IdEmpresa, cl_enumeradores.eTipoDocumento.RETEN.ToString(), sucursal.Su_CodigoEstablecimiento, punto_venta.cod_PuntoVta, punto_venta.EsElectronico, false);
                //}                
            }

            if (resultado == null)
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();

            return Json(new { data_puntovta = punto_venta, data_talonario = resultado }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalle de inventario
        public ActionResult GridViewPartial_deudas_det()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            cp_orden_giro_Info model = new cp_orden_giro_Info();
            model.lst_det = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));         
            return PartialView("_GridViewPartial_deudas_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewDetalle([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_giro_det_Info info_det)
        {
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
            {
                info_det.pr_descripcion = producto.pr_descripcion_combo;
                info_det.IdCtaCbleInv = producto.IdCtaCtble_Inve;
            }

            if (ModelState.IsValid)
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cp_orden_giro_Info model = new cp_orden_giro_Info();
            model.lst_det = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_deudas_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdateDetalle([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_giro_det_Info info_det)
        {
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
            {
                info_det.pr_descripcion = producto.pr_descripcion_combo;
                info_det.IdCtaCbleInv = producto.IdCtaCtble_Inve;
            }

            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cp_orden_giro_Info model = new cp_orden_giro_Info();
            model.lst_det = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_deudas_det", model);
        }

        public ActionResult EditingDeleteDetalle(int secuencia)
        {
            List_det.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cp_orden_giro_Info model = new cp_orden_giro_Info();
            model.lst_det = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_deudas_det", model);
        }
        #endregion

        #region Diario contable

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ct_cbtecble_det_List.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list( Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_deudas_dc", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ct_cbtecble_det_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            
            return PartialView("_GridViewPartial_deudas_dc", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            Lis_ct_cbtecble_det_List.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List.get_list( Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_deudas_dc", model);
        }

        #endregion

        #region editar y eliminar detalle lista de aprobacion
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_og([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_giro_aprobacion_Info info_det)
        {
            List<cp_orden_giro_aprobacion_Info> model = new List<cp_orden_giro_aprobacion_Info>();
            model = Session["list_facturas_seleccionadas"] as List<cp_orden_giro_aprobacion_Info>;
            if (model.Count() > 0)
            {
                cp_orden_giro_aprobacion_Info edited_info = model.Where(m => m.SecuencialID == info_det.SecuencialID).FirstOrDefault();
                info_det.co_serie = "0";
                info_det.IdProveedor = 1;
                edited_info.co_valorpagar = info_det.co_valorpagar;
            }
            
            return PartialView("_GridViewPartial_aprobacion_facturas", model);
        }
        public ActionResult EditingDelete_og(string SecuencialID)
        {
            List<cp_orden_giro_aprobacion_Info> model = new List<cp_orden_giro_aprobacion_Info>();
            model = Session["list_facturas_seleccionadas"] as List<cp_orden_giro_aprobacion_Info>;
            if (model.Count() > 0)
            {
                cp_orden_giro_aprobacion_Info edited_info = model.Where(m => m.SecuencialID == SecuencialID).FirstOrDefault();
                model.Remove(edited_info);
                Session["list_facturas_seleccionadas"] = model;
            }
            return PartialView("_GridViewPartial_aprobacion_facturas", model);
        }

        #endregion

        #region Funciones del detalle IOC
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ing_inv_oc_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDetalleOC.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle_oc();
            return PartialView("_GridViewPartial_ing_inv_oc_det", model);
        }
        private void cargar_combos_detalle_oc()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;

            tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
            var lst_impuestos = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuestos = lst_impuestos;
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew_IOC(string IDs = "", decimal IdTransaccionSession = 0)
        {
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = ListaPorIngresar.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst_x_ingresar.Where(q => q.IdGenerado == item).FirstOrDefault();

                    cp_orden_giro_det_ing_x_oc_Info info_det_inv = new cp_orden_giro_det_ing_x_oc_Info();
                    
                    if (info_det != null)
                    {
                        info_det_inv.IdEmpresa = info_det.IdEmpresa;
                        info_det_inv.inv_IdSucursal = info_det.inv_IdSucursal;
                        info_det_inv.inv_IdMovi_inven_tipo = info_det.inv_IdMovi_inven_tipo;
                        info_det_inv.inv_Secuencia = info_det.inv_Secuencia;
                        info_det_inv.inv_IdNumMovi = info_det.inv_IdNumMovi;
                        info_det_inv.oc_IdSucursal = info_det.oc_IdSucursal;
                        info_det_inv.oc_IdOrdenCompra = info_det.oc_IdOrdenCompra;
                        info_det_inv.oc_Secuencia = info_det.oc_Secuencia;
                        info_det_inv.pr_descripcion = info_det.pr_descripcion;
                        info_det_inv.IdCtaCble = info_det.IdCtaCble;
                        info_det_inv.IdCtaCble_oc = info_det.IdCtaCble_oc;
                        info_det_inv.dm_cantidad = info_det.dm_cantidad;
                        info_det_inv.do_precioCompra = info_det.do_precioCompra;
                        info_det_inv.do_precioFinal = info_det.do_precioFinal;
                        info_det_inv.IdUnidadMedida = info_det.IdUnidadMedida;
                        info_det_inv.IdCod_Impuesto = info_det.IdCod_Impuesto;
                        info_det_inv.NomUnidadMedida = info_det.NomUnidadMedida;
                        info_det_inv.IdProveedor = info_det.IdProveedor;
                        info_det_inv.IdProducto = info_det.IdProducto;
                        info_det_inv.pc_Cuenta = info_det.pc_Cuenta;
                        info_det_inv.do_precioCompra = info_det.do_precioCompra;

                        ListaDetalleOC.AddRow(info_det_inv, IdTransaccionSession);
                    }
                }
            }
            List<cp_orden_giro_det_ing_x_oc_Info> lista = ListaDetalleOC.get_list(IdTransaccionSession);
            var model = ListaDetalleOC.get_list(IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_IOC([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_giro_det_ing_x_oc_Info info_det)
        {            
            ListaDetalleOC.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListaDetalleOC.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ing_inv_oc_det", model);
        }

        public ActionResult EditingDelete_IOC(int Secuencia)
        {
            ListaDetalleOC.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<cp_orden_giro_det_ing_x_oc_Info> model = new List<cp_orden_giro_det_ing_x_oc_Info>();
            model = ListaDetalleOC.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
           
            return PartialView("_GridViewPartial_ing_inv_oc_det", model);
        }
        #endregion

        #region Funciones del detalle IOS
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ing_inv_os_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDetalleOS.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle_os();
            return PartialView("_GridViewPartial_ing_inv_os_det", model);
        }
        private void cargar_combos_detalle_os()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;

            tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
            var lst_impuestos = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuestos = lst_impuestos;
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew_IOS(string IDs = "", decimal IdTransaccionSession = 0)
        {
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = ListaOSPorIngresar.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    var info_det = lst_x_ingresar.Where(q => q.IdGeneradoOS == item).FirstOrDefault();

                    cp_orden_giro_det_ing_x_os_Info info_det_inv = new cp_orden_giro_det_ing_x_os_Info();

                    if (info_det != null)
                    {
                        info_det_inv.IdEmpresa = info_det.IdEmpresa;
                        info_det_inv.oc_IdSucursal = info_det.oc_IdSucursal;
                        info_det_inv.oc_IdOrdenCompra = info_det.oc_IdOrdenCompra;
                        info_det_inv.oc_Secuencia = info_det.oc_Secuencia;
                        info_det_inv.pr_descripcion = info_det.pr_descripcion;
                        info_det_inv.dm_cantidad = info_det.dm_cantidad;
                        info_det_inv.do_precioCompra = info_det.do_precioCompra;
                        info_det_inv.do_precioFinal = info_det.do_precioFinal;
                        info_det_inv.IdUnidadMedida = info_det.IdUnidadMedida;
                        info_det_inv.IdCod_Impuesto = info_det.IdCod_Impuesto;
                        info_det_inv.NomUnidadMedida = info_det.NomUnidadMedida;
                        info_det_inv.IdProveedor = info_det.IdProveedor;
                        info_det_inv.IdProducto = info_det.IdProducto;
                        info_det_inv.do_precioCompra = info_det.do_precioCompra;

                        ListaDetalleOS.AddRow(info_det_inv, IdTransaccionSession);
                    }
                }
            }
            List<cp_orden_giro_det_ing_x_os_Info> lista = ListaDetalleOS.get_list(IdTransaccionSession);
            var model = ListaDetalleOS.get_list(IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_IOS([ModelBinder(typeof(DevExpressEditorsBinder))] cp_orden_giro_det_ing_x_os_Info info_det)
        {
            ListaDetalleOS.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListaDetalleOS.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ing_inv_os_det", model);
        }

        public ActionResult EditingDelete_IOS(int Secuencia)
        {
            ListaDetalleOS.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<cp_orden_giro_det_ing_x_os_Info> model = new List<cp_orden_giro_det_ing_x_os_Info>();
            model = ListaDetalleOS.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ing_inv_os_det", model);
        }
        #endregion

        #region OC por ingresar
        public ActionResult GridViewPartial_ing_inv_oc_x_ingresar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaPorIngresar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ing_inv_oc_x_ingresar", model);
        }
        #endregion

        #region OS por ingresar
        public ActionResult GridViewPartial_ing_inv_os_x_ingresar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaOSPorIngresar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ing_inv_os_x_ingresar", model);
        }
        #endregion
    }


    public class ct_cbtecble_det_List_fp
    {
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();

        string Variable = "ct_cbtecble_det_List_fp";
        public List<ct_cbtecble_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
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
            if (!string.IsNullOrEmpty(info_det.IdCtaCble))
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

            if (!string.IsNullOrEmpty(info_det.IdCtaCble))
            {
                var cta = bus_plancta.get_info(IdEmpresa, info_det.IdCtaCble);
                if (cta != null)
                    edited_info.pc_Cuenta = cta.IdCtaCble + " - " + cta.pc_Cuenta;
            }
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }        
    }

    public class cp_orden_giro_det_PorIngresar_List
    {
        string Variable = "cp_orden_giro_det_PorIngresar";
        public List<cp_orden_giro_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_det_Info> list = new List<cp_orden_giro_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cp_orden_giro_det_Info_List
    {
        in_categorias_Bus bus_categoria = new in_categorias_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        string Variable = "cp_orden_giro_det_Info";
        public List<cp_orden_giro_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_det_Info> list = new List<cp_orden_giro_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_orden_giro_det_Info info_det, decimal IdTransaccionSession)
        {
            List<cp_orden_giro_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : (list.Max(q => q.Secuencia) + 1);
            info_det.DescuentoUni = info_det.CostoUni * (info_det.PorDescuento / 100);
            info_det.CostoUniFinal = info_det.CostoUni - info_det.DescuentoUni;
            info_det.Subtotal = info_det.Cantidad * info_det.CostoUniFinal;
            var impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto_Iva);
            if (impuesto != null)
                info_det.PorIva = impuesto.porcentaje;                
            else
                info_det.PorIva = 0;
            info_det.ValorIva = info_det.Subtotal * (info_det.PorIva / 100);
            info_det.Total = info_det.Subtotal + info_det.ValorIva;
            
            list.Add(info_det);
        }

        public void UpdateRow(cp_orden_giro_det_Info info_det, decimal IdTransaccionSession)
        {
            cp_orden_giro_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();

            edited_info.IdProducto = info_det.IdProducto;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.Cantidad = info_det.Cantidad;
            edited_info.CostoUni = info_det.CostoUni;
            edited_info.PorDescuento = info_det.PorDescuento;
            edited_info.IdCod_Impuesto_Iva = info_det.IdCod_Impuesto_Iva;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.DescuentoUni = info_det.CostoUni * (info_det.PorDescuento / 100);
            edited_info.CostoUniFinal = info_det.CostoUni - edited_info.DescuentoUni;
            edited_info.Subtotal = info_det.Cantidad * edited_info.CostoUniFinal;
            var impuesto = bus_impuesto.get_info(edited_info.IdCod_Impuesto_Iva);
            if (impuesto != null)
                edited_info.PorIva = impuesto.porcentaje;
            else
                edited_info.PorIva = 0;
            edited_info.ValorIva = edited_info.Subtotal * (edited_info.PorIva / 100);
            edited_info.Total = edited_info.Subtotal + edited_info.ValorIva;
            edited_info.IdCtaCbleInv = info_det.IdCtaCbleInv;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<cp_orden_giro_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == secuencia).FirstOrDefault());
        }
    }

    public class cp_orden_giro_det_ing_x_oc_ListaDetalle
    {
        string Variable = "cp_orden_giro_det_ing_x_oc_Info";
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();

        public List<cp_orden_giro_det_ing_x_oc_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_det_ing_x_oc_Info> list = new List<cp_orden_giro_det_ing_x_oc_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_det_ing_x_oc_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_det_ing_x_oc_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_orden_giro_det_ing_x_oc_Info info_det_inv, decimal IdTransaccionSession)
        {
            List<cp_orden_giro_det_ing_x_oc_Info> list = get_list(IdTransaccionSession);

            tb_sis_Impuesto_Info info_impuesto = bus_impuesto.get_info(info_det_inv.IdCod_Impuesto);
           
            info_det_inv.Por_Iva = info_impuesto.porcentaje;        
            info_det_inv.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det_inv.do_descuento = info_det_inv.do_precioCompra * (info_det_inv.do_porc_des/ 100);
            info_det_inv.do_precioFinal = info_det_inv.do_precioCompra - info_det_inv.do_descuento;
            info_det_inv.do_subtotal = info_det_inv.dm_cantidad * info_det_inv.do_precioFinal;
            info_det_inv.do_iva = info_det_inv.do_subtotal * (info_det_inv.Por_Iva/100);
            info_det_inv.do_total = info_det_inv.do_subtotal + info_det_inv.do_iva;

            list.Add(info_det_inv);
        }

        public void UpdateRow(cp_orden_giro_det_ing_x_oc_Info info_det, decimal IdTransaccionSession)
        {
            tb_sis_Impuesto_Info info_impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto);
            cp_orden_giro_det_ing_x_oc_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.IdCtaCble_oc = info_det.IdCtaCble_oc;

            edited_info.dm_cantidad = info_det.dm_cantidad;
            edited_info.do_porc_des = info_det.do_porc_des;
            edited_info.IdCod_Impuesto = info_det.IdCod_Impuesto;

            edited_info.Por_Iva = info_impuesto.porcentaje;
            edited_info.do_descuento = edited_info.do_precioCompra * (edited_info.do_porc_des / 100);
            edited_info.do_precioFinal = edited_info.do_precioCompra - edited_info.do_descuento;
            edited_info.do_subtotal = edited_info.dm_cantidad * edited_info.do_precioFinal;
            edited_info.do_iva = edited_info.do_subtotal * (edited_info.Por_Iva / 100);
            edited_info.do_total = edited_info.do_subtotal + edited_info.do_iva;

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cp_orden_giro_det_ing_x_oc_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }

    public class cp_orden_giro_det_ing_x_oc_List
    {
        string Variable = "cp_orden_giro_det_ing_x_oc_x_cruzar_Info";
        public List<cp_orden_giro_det_ing_x_oc_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_det_ing_x_oc_Info> list = new List<cp_orden_giro_det_ing_x_oc_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_det_ing_x_oc_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_det_ing_x_oc_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cp_orden_giro_det_ing_x_os_ListaDetalle
    {
        string Variable = "cp_orden_giro_det_ing_x_os_Info";
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();

        public List<cp_orden_giro_det_ing_x_os_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_det_ing_x_os_Info> list = new List<cp_orden_giro_det_ing_x_os_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_det_ing_x_os_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_det_ing_x_os_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_orden_giro_det_ing_x_os_Info info_det_inv, decimal IdTransaccionSession)
        {
            List<cp_orden_giro_det_ing_x_os_Info> list = get_list(IdTransaccionSession);

            tb_sis_Impuesto_Info info_impuesto = bus_impuesto.get_info(info_det_inv.IdCod_Impuesto);

            info_det_inv.Por_Iva = info_impuesto.porcentaje;
            info_det_inv.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det_inv.do_descuento = info_det_inv.do_precioCompra * (info_det_inv.do_porc_des / 100);
            info_det_inv.do_precioFinal = info_det_inv.do_precioCompra - info_det_inv.do_descuento;
            info_det_inv.do_subtotal = info_det_inv.dm_cantidad * info_det_inv.do_precioFinal;
            info_det_inv.do_iva = info_det_inv.do_subtotal * (info_det_inv.Por_Iva / 100);
            info_det_inv.do_total = info_det_inv.do_subtotal + info_det_inv.do_iva;

            list.Add(info_det_inv);
        }

        public void UpdateRow(cp_orden_giro_det_ing_x_os_Info info_det, decimal IdTransaccionSession)
        {
            tb_sis_Impuesto_Info info_impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto);
            cp_orden_giro_det_ing_x_os_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            edited_info.dm_cantidad = info_det.dm_cantidad;
            edited_info.do_porc_des = info_det.do_porc_des;
            edited_info.IdCod_Impuesto = info_det.IdCod_Impuesto;

            edited_info.Por_Iva = info_impuesto.porcentaje;
            edited_info.do_descuento = edited_info.do_precioCompra * (edited_info.do_porc_des / 100);
            edited_info.do_precioFinal = edited_info.do_precioCompra - edited_info.do_descuento;
            edited_info.do_subtotal = edited_info.dm_cantidad * edited_info.do_precioFinal;
            edited_info.do_iva = edited_info.do_subtotal * (edited_info.Por_Iva / 100);
            edited_info.do_total = edited_info.do_subtotal + edited_info.do_iva;

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cp_orden_giro_det_ing_x_os_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }

    public class cp_orden_giro_det_ing_x_os_List
    {
        string Variable = "cp_orden_giro_det_ing_x_os_x_cruzar_Info";
        public List<cp_orden_giro_det_ing_x_os_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_det_ing_x_os_Info> list = new List<cp_orden_giro_det_ing_x_os_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_det_ing_x_os_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_det_ing_x_os_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cp_orden_giro_List
    {
        string Variable = "cp_orden_giro_Info";
        public List<cp_orden_giro_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_orden_giro_Info> list = new List<cp_orden_giro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_orden_giro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_orden_giro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}