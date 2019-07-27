using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using DevExpress.Web.Mvc;
using Core.Erp.Web.Helps;
using Core.Erp.Info.Helps;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Bus.Facturacion;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    [SessionTimeout]
    public class RetencionController : Controller
    {
        #region variables
        cp_retencion_Bus bus_retencion = new cp_retencion_Bus();
        cp_codigo_SRI_Bus bus_codigo_SRI = new cp_codigo_SRI_Bus();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        ct_cbtecble_det_List_re List_ct_cbtecble_det_List = new ct_cbtecble_det_List_re();
        cp_retencion_det_lst List_cp_retencion_det = new cp_retencion_det_lst();
        List<cp_retencion_det_Info> lst_detalle_ret = new List<cp_retencion_det_Info>();
        List<cp_retencion_Info> lst_retenciones = new List<cp_retencion_Info>();
        cp_parametros_Info info_param_op = new cp_parametros_Info();
        cp_parametros_Bus bus_parametros = new cp_parametros_Bus();
        cp_codigo_SRI_List lst_codigo_retencion = new cp_codigo_SRI_List();
        cp_codigo_SRI_Bus bus_codigo_ret = new cp_codigo_SRI_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        cp_orden_giro_Bus bus_orden_giro = new cp_orden_giro_Bus();
        cp_orden_giro_List List_og = new cp_orden_giro_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        #endregion
        #region vistas
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
                Su_Descripcion = "TODOS"
            });
            ViewBag.lst_sucursal = lst_sucursal;

        }

        public ActionResult GridViewPartial_retenciones(DateTime? fecha_ini, DateTime? fecha_fin ,int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_retencion.get_list(IdEmpresa, IdSucursal, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_retenciones", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_retencion_det()
        {
            try
            {
                cargar_combos_detalle();
                cp_retencion_Info model = new cp_retencion_Info();
                SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
                 model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                return PartialView("_GridViewPartial_retencion_det", model.detalle);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult GridViewPartial_retencio_dc()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }
        #endregion
        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdTipoCbte_Ogiro = 0, decimal IdCbteCble_Ogiro = 0)

        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cp_retencion_Info model = new cp_retencion_Info
            {
                IdEmpresa = IdEmpresa
            };



            Session["info_param_op"] = bus_parametros.get_info(IdEmpresa);
            model = bus_retencion.get_info_factura(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            model.fecha = model.fecha;
            if (model.co_valoriva > 0)
                Session["co_valoriva"] = model.co_valoriva;
            cargar_combos(model);
            cargar_combos_detalle();
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_cp_retencion_det.set_list(new List<cp_retencion_det_Info>(), model.IdTransaccionSession);
            List_ct_cbtecble_det_List.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);
            var lista = bus_codigo_ret.get_list_cod_ret(false, IdEmpresa);
            lst_codigo_retencion.set_list(lista);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(cp_retencion_Info model)
        {
            bus_retencion = new cp_retencion_Bus();
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            info_param_op = Session["info_param_op"] as cp_parametros_Info;
            model.info_comprobante.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;

            string mensaje = bus_retencion.validar(model);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (mensaje != "")
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            else
            {
                var lista_cp_codigo_SRI = lst_codigo_retencion.get_list();

                if (lista_cp_codigo_SRI.Count > 0)
                {
                    model.detalle.ForEach(item =>
                    {
                        cp_codigo_SRI_Info info_ = lista_cp_codigo_SRI.Where(v => v.IdCodigo_SRI == item.IdCodigo_SRI).FirstOrDefault();
                        item.re_Codigo_impuesto = info_.co_codigoBase;

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

                if (bus_retencion.guardarDB(model))
                {
                    return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdRetencion = model.IdRetencion, Exito = true });
                }
                else
                {
                    ViewBag.mensaje = mensaje;
                    cargar_combos(model);
                    cargar_combos_detalle();
                    return View(model);
                }
            }
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdRetencion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Session["info_param_op"] = bus_parametros.get_info(IdEmpresa);
            cp_retencion_Info model = new cp_retencion_Info();
            model = bus_retencion.get_info(IdEmpresa, IdRetencion);
            //model.IdSucursal = model.info_comprobante.IdSucursal;
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            cargar_combos(model);
            cargar_combos_detalle();
            List_ct_cbtecble_det_List.set_list(model.info_comprobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.detalle, model.IdTransaccionSession);

            var lista = bus_codigo_ret.get_list_cod_ret(false, IdEmpresa);
            lst_codigo_retencion.set_list(lista);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.fecha, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(cp_retencion_Info model)
        {
            bus_retencion = new cp_retencion_Bus();
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();
            model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            //info_param_op = Session["info_param_op"] as cp_parametros_Info;
            //model.info_comprobante.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;

            string mensaje = bus_retencion.validar(model);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (mensaje != "")
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            else
            {
                var lista_cp_codigo_SRI = lst_codigo_retencion.get_list();

                if (lista_cp_codigo_SRI.Count > 0)
                {
                    model.detalle.ForEach(item =>
                    {
                        cp_codigo_SRI_Info info_ = lista_cp_codigo_SRI.Where(v => v.IdCodigo_SRI == item.IdCodigo_SRI).FirstOrDefault();
                        item.re_Codigo_impuesto = info_.co_codigoBase;
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

                if (bus_retencion.modificarDB(model))
                {
                    return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdRetencion = model.IdRetencion, Exito = true });
                }
                else
                {
                    ViewBag.mensaje = mensaje;
                    cargar_combos(model);
                    cargar_combos_detalle();
                    return View(model);
                }
            }
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdRetencion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Session["info_param_op"] = bus_parametros.get_info(IdEmpresa);
            cp_retencion_Info model = new cp_retencion_Info();
            model = bus_retencion.get_info(IdEmpresa, IdRetencion);
            //model.IdSucursal = model.info_comprobante.IdSucursal;
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            cargar_combos(model);
            cargar_combos_detalle();
            List_ct_cbtecble_det_List.set_list(model.info_comprobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
            List_cp_retencion_det.set_list(model.detalle, model.IdTransaccionSession);

            var lista = bus_codigo_ret.get_list_cod_ret(false, IdEmpresa);
            lst_codigo_retencion.set_list(lista);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.fecha, cl_enumeradores.eModulo.CXP, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(cp_retencion_Info model)
        {

            bus_retencion = new cp_retencion_Bus();
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            model.detalle = List_cp_retencion_det.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.info_comprobante.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            info_param_op = Session["info_param_op"] as cp_parametros_Info;
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.info_comprobante.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;

            string mensaje = bus_retencion.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            else
            {
                var lista_cp_codigo_SRI = lst_codigo_retencion.get_list();

                if (lista_cp_codigo_SRI.Count > 0)
                {
                    model.detalle.ForEach(item =>
                    {
                        cp_codigo_SRI_Info info_ = lista_cp_codigo_SRI.Where(v => v.IdCodigo_SRI == item.IdCodigo_SRI).FirstOrDefault();
                        item.re_Codigo_impuesto = info_.co_codigoBase;
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

                if (bus_retencion.anularDB(model))
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.mensaje = mensaje;
                    cargar_combos(model);
                    cargar_combos_detalle();
                    return View(model);
                }
            }
        }

        #endregion
        #region Detalle diario
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                List_ct_cbtecble_det_List.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                List_ct_cbtecble_det_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            List_ct_cbtecble_det_List.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = List_ct_cbtecble_det_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencio_dc", model);
        }
        #endregion
        #region Acciones collbak retenciones
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew_ret([ModelBinder(typeof(DevExpressEditorsBinder))]  cp_retencion_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? "0" : SessionFixed.IdEmpresa);
            cp_codigo_SRI_Info info_codifo_sri = new cp_codigo_SRI_Info();
            List<cp_retencion_det_Info> model = new List<cp_retencion_det_Info>();
            info_codifo_sri = bus_codigo_SRI.get_info(IdEmpresa, info_det.IdCodigo_SRI);
            
            info_det.re_Porcen_retencion = info_codifo_sri.co_porRetencion;
            if (info_codifo_sri.IdTipoSRI == "COD_RET_IVA")
            {
                if (info_det.re_baseRetencion != 0)
                {
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;

                    // calculando valores retencion
                    List_cp_retencion_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));




                }
            }
            else
            {
                if (info_codifo_sri.co_porRetencion != 0 & info_det.re_baseRetencion != null & info_det.re_baseRetencion != 0)
                {
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;


                    // calculando valores retencion
                    List_cp_retencion_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));


                }


            }



            cargar_combos_detalle();
            model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_retencion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate_ret([ModelBinder(typeof(DevExpressEditorsBinder))] cp_retencion_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? "0" : SessionFixed.IdEmpresa);
            cp_codigo_SRI_Info info_codifo_sri = new cp_codigo_SRI_Info();
            List<cp_retencion_det_Info> model = new List<cp_retencion_det_Info>();
            info_codifo_sri = bus_codigo_SRI.get_info(IdEmpresa, info_det.IdCodigo_SRI);
            info_det.re_Porcen_retencion = info_codifo_sri.co_porRetencion;
            if (info_codifo_sri.IdTipoSRI == "COD_RET_IVA")
            {
                if (info_det.re_baseRetencion != 0)
                {
                    info_det.re_baseRetencion = info_det.re_baseRetencion;
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;
                    List_cp_retencion_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
            }
            else
            {
                if (info_codifo_sri.co_porRetencion != 0 & info_det.re_baseRetencion != null & info_det.re_baseRetencion != 0)
                {
                    info_det.re_valor_retencion = (info_det.re_baseRetencion * info_codifo_sri.co_porRetencion) / 100;
                    info_det.IdCtacble = info_codifo_sri.info_codigo_ctacble.IdCtaCble;
                    info_det.re_Codigo_impuesto = info_det.re_Codigo_impuesto;
                    List_cp_retencion_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                    model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }
            }

            cargar_combos_detalle();
            model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_retencion_det", model);
        }

        public ActionResult EditingDelete_ret(int Idsecuencia)
        {
            List_cp_retencion_det.DeleteRow(Idsecuencia);
            List<cp_retencion_det_Info> model = new List<cp_retencion_det_Info>();
            model = List_cp_retencion_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_retencion_det", model);
        }

        #endregion
        #region json
        public JsonResult CargarPuntosDeVenta(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            var resultado = bus_punto_venta.get_list_x_tipo_doc(IdEmpresa, IdSucursal, cl_enumeradores.eTipoDocumento.RETEN.ToString());
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUltimoDocumento(int IdSucursal = 0, int IdPuntoVta = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sis_Documento_Tipo_Talonario_Info resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (punto_venta != null)
            {
                var sucursal = bus_sucursal.get_info(IdEmpresa, IdSucursal);
                resultado = bus_talonario.GetUltimoNoUsado(IdEmpresa, cl_enumeradores.eTipoDocumento.RETEN.ToString(), sucursal.Su_CodigoEstablecimiento, punto_venta.cod_PuntoVta, punto_venta.EsElectronico, false);
            }

            if (resultado == null)
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();

            return Json(new { data_puntovta = punto_venta, data_talonario = resultado }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult armar_diario_retencion(int IdEmpresa = 0, decimal IdProveedor = 0, decimal IdTransaccionSession = 0, int IdSucursal_cxp = 0)
        {
            if (IdProveedor != 0)
            {
                var proveedor = bus_proveedor.get_info(IdEmpresa, IdProveedor);
                if (proveedor.info_persona.pe_cedulaRuc == SessionFixed.Ruc)
                {
                    var sucursal = bus_sucursal.get_info(IdEmpresa, IdSucursal_cxp);
                    proveedor.IdCtaCble_CXP = sucursal.IdCtaCble_cxp;
                }
                var detalle_ret = List_cp_retencion_det.get_list(IdTransaccionSession);
                var param_op = bus_parametros.get_info(IdEmpresa);
                List_ct_cbtecble_det_List.delete_detail_New_details(param_op, detalle_ret, IdTransaccionSession, proveedor.IdCtaCble_CXP);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetValorRetencion(int IdCodigoSRI = 0)
        {
            var resultado = bus_codigo_SRI.get_info(IdCodigoSRI);
            if (resultado == null)
                resultado = new cp_codigo_SRI_Info();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Cargar combos
        private void cargar_combos(cp_retencion_Info model)
        {
            var lst_proveedores = bus_proveedor.get_list(model.IdEmpresa, false);
            ViewBag.lst_proveedores = lst_proveedores;

            var lst_punto_venta = bus_punto_venta.get_list_x_tipo_doc(model.IdEmpresa, model.IdSucursal, "RETEN");
            ViewBag.lst_punto_venta = lst_punto_venta;

        }
        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;
            var lista_cp_codigo_SRI = bus_codigo_ret.get_list_cod_ret(false, IdEmpresa);
            ViewBag.lst_codigo_retencion = lista_cp_codigo_SRI;           
        }

        private bool validar(cp_retencion_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.fecha, cl_enumeradores.eModulo.CXP, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.fecha, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Facturas sin retencion
        public ActionResult ComprasSinRetencion()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);

            #region Cargar sucursal
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
            #endregion

            List_og.set_list(bus_orden_giro.get_lst_sin_ret(model.IdEmpresa, model.IdSucursal, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);

            return View(model);
        }

        [HttpPost]
        public ActionResult ComprasSinRetencion(cl_filtros_Info model)
        {
            #region Validar Session
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            #endregion

            #region Cargar sucursal
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
            #endregion

            List_og.set_list(bus_orden_giro.get_lst_sin_ret(model.IdEmpresa, model.IdSucursal, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);

            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_deudas_sin_ret()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_og.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_deudas_sin_ret", model);
        }

        #endregion

    }

    public class cp_codigo_SRI_List
    {
        string variable = "cp_codigo_SRI_Info";
        public List<cp_codigo_SRI_Info> get_list()
        {
            if (HttpContext.Current.Session[variable] == null)
            {
                List<cp_codigo_SRI_Info> list = new List<cp_codigo_SRI_Info>();

                HttpContext.Current.Session[variable] = list;
            }
            return (List<cp_codigo_SRI_Info>)HttpContext.Current.Session[variable];
        }

        public void set_list(List<cp_codigo_SRI_Info> list)
        {
            HttpContext.Current.Session[variable] = list;
        }
    }
    public class ct_cbtecble_det_List_re
    {
        string variable = "ct_cbtecble_det_Info";
        public List<ct_cbtecble_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<ct_cbtecble_det_Info> list = new List<ct_cbtecble_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ct_cbtecble_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ct_cbtecble_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            info_det.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            list.Add(info_det);
        }

        public void UpdateRow(ct_cbtecble_det_Info info_det, decimal IdTransaccionSession)
        {
            ct_cbtecble_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.dc_para_conciliar = info_det.dc_para_conciliar;
            edited_info.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            edited_info.dc_Valor_debe = info_det.dc_Valor_debe;
            edited_info.dc_Valor_haber = info_det.dc_Valor_haber;
        }

        public void DeleteRow(int secuencia, decimal IdTransaccionSession)
        {
            List<ct_cbtecble_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }

        public void delete_detail_New_details(cp_parametros_Info info_param_op, List<cp_retencion_det_Info> detalle_retencion, decimal IdTransaccionSession, string IdCtaCble = "")
        {
            try
            {
                int sec = 2;

                set_list(new List<ct_cbtecble_det_Info>(), IdTransaccionSession);

                ct_cbtecble_det_Info cbtecble_haber_Info = new ct_cbtecble_det_Info();
                cbtecble_haber_Info.secuencia = 1;
                cbtecble_haber_Info.IdEmpresa = info_param_op.IdEmpresa;
                cbtecble_haber_Info.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;
                cbtecble_haber_Info.IdCtaCble = IdCtaCble;
                cbtecble_haber_Info.dc_Valor_debe = Math.Round(detalle_retencion.Sum(v => Convert.ToDouble(v.re_valor_retencion)),2,MidpointRounding.AwayFromZero);
                cbtecble_haber_Info.dc_Valor = Math.Round(detalle_retencion.Sum(v => Convert.ToDouble(v.re_valor_retencion)),2,MidpointRounding.AwayFromZero);
                cbtecble_haber_Info.dc_Observacion = "";
                AddRow(cbtecble_haber_Info, IdTransaccionSession);

                foreach (var item in detalle_retencion)
                {
                    ct_cbtecble_det_Info cbtecble_debe_Info = new ct_cbtecble_det_Info();
                    cbtecble_debe_Info.secuencia = sec;
                    cbtecble_debe_Info.IdEmpresa = info_param_op.IdEmpresa;
                    cbtecble_debe_Info.IdTipoCbte = (int)info_param_op.pa_IdTipoCbte_x_Retencion;
                    cbtecble_debe_Info.IdCtaCble = item.IdCtacble;
                    cbtecble_debe_Info.dc_Valor_haber = Math.Round((double)item.re_valor_retencion,2,MidpointRounding.AwayFromZero);
                    cbtecble_debe_Info.dc_Valor = Math.Round((double)item.re_valor_retencion,2,MidpointRounding.AwayFromZero) *-1;
                    cbtecble_debe_Info.dc_Observacion = "";
                    sec++;
                    AddRow(cbtecble_debe_Info, IdTransaccionSession);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class cp_retencion_det_lst
    {
        string variable = " cp_retencion_det_Info";
        public List<cp_retencion_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable+IdTransaccionSession.ToString()] == null)
            {
                List<cp_retencion_det_Info> list = new List<cp_retencion_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_retencion_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_retencion_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cp_retencion_det_Info info_det, decimal IdTransaccionSession)
        {
            List<cp_retencion_det_Info> list = get_list(IdTransaccionSession);
            info_det.Idsecuencia = list.Count() + 1;
            info_det.re_valor_retencion = Math.Round((double)info_det.re_valor_retencion, 2, MidpointRounding.AwayFromZero);
            list.Add(info_det);
        }

        public void UpdateRow(cp_retencion_det_Info info_det, decimal IdTransaccionSession)
        {
            cp_retencion_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Idsecuencia == info_det.Idsecuencia).First();
            edited_info.re_Codigo_impuesto = info_det.re_Codigo_impuesto;
            edited_info.IdCodigo_SRI = info_det.IdCodigo_SRI;
            edited_info.IdCtacble = info_det.IdCtacble;
            edited_info.re_baseRetencion = info_det.re_baseRetencion;
            edited_info.re_Porcen_retencion = info_det.re_Porcen_retencion;
            edited_info.re_valor_retencion = Math.Round((double)info_det.re_valor_retencion, 2, MidpointRounding.AwayFromZero);
        }

        public void DeleteRow(int secuencia)
        {
            List<cp_retencion_det_Info> list = get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            list.Remove(list.Where(m => m.Idsecuencia == secuencia).First());
        }

   

    }

}