using Core.Erp.Bus.Banco;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Info.Banco;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.CuentasPorPagar.Controllers
{
    public class NotaDebitoController : Controller
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
        ct_cbtecble_det_List_nd Lis_ct_cbtecble_det_List_nd = new ct_cbtecble_det_List_nd();
        cp_orden_pago_Bus bus_orden_pago = new cp_orden_pago_Bus();
        cp_nota_DebCre_Bus bus_notaDebCre = new cp_nota_DebCre_Bus();
        List<cp_orden_pago_det_Info> lst_detalle_op = new List<cp_orden_pago_det_Info>();
        cp_orden_pago_cancelaciones_Bus bus_orden_pago_cancelaciones = new cp_orden_pago_cancelaciones_Bus();
        List<cp_orden_pago_det_Info> list_op_seleccionadas = new List<cp_orden_pago_det_Info>();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        List<cp_nota_DebCre_Info> Lista_NotaDebito = new List<cp_nota_DebCre_Info>();
        cp_nota_DebCre_List ListaNotaDebito = new cp_nota_DebCre_List();

        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion
        #region Metodos ComboBox bajo demanda flujo
        ba_TipoFlujo_Bus bus_tipo = new ba_TipoFlujo_Bus();
        public ActionResult CmbFlujo_ND()
        {
            decimal model = new decimal();
            return PartialView("_CmbFlujo_ND", model);
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

        #region CmbCuenta_ND

        public ActionResult CmbCuenta_ND()
        {
            ct_cbtecble_det_Info model = new ct_cbtecble_det_Info();
            return PartialView("_CmbCuenta_ND", model);
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
        public ActionResult GridViewPartial_nota_debito(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_orden_giro.get_lst(IdEmpresa, IdSucursal, "D", Convert.ToDateTime(fecha_ini), Convert.ToDateTime(fecha_fin));
            return PartialView("_GridViewPartial_nota_debito", model);
        }

        public ActionResult GridViewPartial_nota_debito_dc()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_debito_dc", model);
        }

        #endregion
        #region cargar combos
        private void cargar_combos(int IdEmpresa, decimal IdProveedor , string IdTipoSRI )
        {
            var lst_codigos_sri = bus_codigo_sri.get_list(IdEmpresa);
            ViewBag.lst_codigos_sri = lst_codigos_sri;

            var lst_forma_pago = bus_forma_paogo.get_list();
            ViewBag.lst_forma_pago = lst_forma_pago;

            var lst_paises = bus_pais.get_list();
            ViewBag.lst_paises = lst_paises;

            var lst_sucursales = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
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
            lst_tipo_nota.Add("T_TIP_NOTA_INT", "INTERNO");
            lst_tipo_nota.Add("T_TIP_NOTA_SRI", "SRI");
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
        private bool validar(cp_nota_DebCre_Info i_validar, ref string msg)
        {
            i_validar.lst_detalle_ct = Lis_ct_cbtecble_det_List_nd.get_list(i_validar.IdTransaccionSession);



            foreach (var item in i_validar.lst_detalle_ct)
            {
                if (string.IsNullOrEmpty(item.IdCtaCble))
                {
                    mensaje = "Faltan cuentas contables, por favor verifique";
                    return false;
                }
            }

            if (i_validar.lst_detalle_ct.Where(q => q.dc_Valor == 0).Count() > 0)
            {
                mensaje = "Existen detalles con valor 0 en el debe o haber";
                return false;
            }
            if (i_validar.lst_detalle_ct.Count == 0)
            {
                mensaje = "Debe ingresar registros en el detalle";
                return false;
            }

            if (i_validar.lst_detalle_ct.Sum(q => q.dc_Valor) != 0)
            {
                mensaje = "La suma de los detalles debe ser 0";
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cn_fecha, cl_enumeradores.eModulo.CXP, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cn_fecha, cl_enumeradores.eModulo.CONTA, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            return true;
        }


        #endregion
        #region json

        public JsonResult get_list_tipo_doc(int IdEmpresa = 0, decimal IdProveedor = 0, string codigoSRI = "")
        {
            var list_tipo_doc = bus_tipo_documento.get_list(IdEmpresa, IdProveedor, codigoSRI);
            return Json(list_tipo_doc, JsonRequestBehavior.AllowGet);
        }

        public JsonResult armar_diario(int IdEmpresa = 0, decimal IdProveedor = 0, double cn_subtotal_iva = 0, double cn_subtotal_siniva = 0,
            double valoriva = 0, double total = 0, string observacion = "", decimal IdTransaccionSession = 0)
        {
            info_proveedor = bus_prov.get_info(IdEmpresa, IdProveedor);
            info_parametro = bus_param.get_info(IdEmpresa);

            Lis_ct_cbtecble_det_List_nd.delete_detail_New_details(info_proveedor, info_parametro, cn_subtotal_iva, cn_subtotal_siniva, valoriva, total, observacion,IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }
       
        #endregion
        #region funciones
        public ActionResult Nuevo(int IdEmpresa = 0)
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
                IdTipoCbte_Nota = (int)param.pa_TipoCbte_NC,
                PaisPago = "593",
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
            Lis_ct_cbtecble_det_List_nd.set_list(new List<ct_cbtecble_det_Info>(), model.IdTransaccionSession);
            return View(model);

        }

        [HttpPost]
        public ActionResult Nuevo(cp_nota_DebCre_Info model)
        {
            model.DebCre = "D";
            model.info_comrobante = new ct_cbtecble_Info();
            model.info_comrobante.IdTipoCbte = model.IdTipoCbte_Nota;
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(model.IdTransaccionSession);

            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!validar(model, ref mensaje))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            if (!bus_orden_giro.guardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                ViewBag.mensaje = "Ha ocurrido un error, comuníquese con sistemas";
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                cargar_combos_detalle();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipoCbte_Nota = model.IdTipoCbte_Nota, IdCbteCble_Nota = model.IdCbteCble_Nota, Exito = true });

        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipoCbte_Nota = 0, decimal IdCbteCble_Nota = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cp_nota_DebCre_Info model = bus_orden_giro.get_info(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            if (model.info_comrobante.lst_ct_cbtecble_det == null)
                model.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();
            Lis_ct_cbtecble_det_List_nd.set_list(model.info_comrobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
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
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!validar(model, ref mensaje))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                ViewBag.mensaje = mensaje;
                cargar_combos_detalle();
                return View(model);
            }
            if (!bus_orden_giro.modificarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdTipoCbte_Nota = model.IdTipoCbte_Nota, IdCbteCble_Nota = model.IdCbteCble_Nota, Exito = true });

        }
        public ActionResult Anular(int IdEmpresa = 0, int IdTipoCbte_Nota = 0, decimal IdCbteCble_Nota = 0)
        {
            cp_nota_DebCre_Info model = bus_orden_giro.get_info(IdEmpresa, IdTipoCbte_Nota, IdCbteCble_Nota);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            if (model.info_comrobante.lst_ct_cbtecble_det == null)
                model.info_comrobante.lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>();
            Lis_ct_cbtecble_det_List_nd.set_list(model.info_comrobante.lst_ct_cbtecble_det, model.IdTransaccionSession);
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

            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            model.info_comrobante.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (!bus_orden_giro.anularDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa, model.IdProveedor, model.IdIden_credito.ToString());
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Funcion diario contable
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ct_cbtecble_det_List_nd.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_debito_dc", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_ct_cbtecble_det_List_nd.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_debito_dc", model);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            Lis_ct_cbtecble_det_List_nd.DeleteRow(secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_ct_cbtecble_det_List_nd.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_nota_debito_dc", model);
        }
        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadValidationSettings_imp.UploadValidationSettings, UploadValidationSettings_imp.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cp_nota_DebCre_Info model = new cp_nota_DebCre_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(cp_nota_DebCre_Info model)
        {
            try
            {
                var Lista_NotaDebito = ListaNotaDebito.get_list(model.IdTransaccionSession);

                foreach (var item in Lista_NotaDebito)
                {
                    if (item.IdProveedor != 0)
                    {
                        if (!bus_notaDebCre.guardar_importacionDB(item))
                        {
                            ViewBag.mensaje = "Error al importar el archivo";
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GridViewPartial_notadebito_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaNotaDebito.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_notadebito_importacion", model);
        }
        #endregion
    }

    #region Importacion Excel
    public class UploadValidationSettings_imp
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            string ruc_proveedor = "";
            List<cp_nota_DebCre_Info> Lista_NotaDebito = new List<cp_nota_DebCre_Info>();
            cp_nota_DebCre_List ListaNotaDebito = new cp_nota_DebCre_List();
            int cont = 0;
            int IdCbteCble_Nota = 1;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            cp_parametros_Bus bus_cp_parametros = new cp_parametros_Bus();
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region NotaDebito     
                var info_cp_parametro = bus_cp_parametros.get_info(IdEmpresa);

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                         ruc_proveedor = Convert.ToString(reader.GetValue(1)).Trim();
                        var info_proveedor = bus_proveedor.get_info_x_num_cedula(IdEmpresa, ruc_proveedor);
                        var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
                        var Su_CodigoEstablecimiento = Convert.ToString(reader.GetValue(0)).Trim();
                        var IdSucursal = Convert.ToInt32(reader.GetValue(0));

                        if (info_proveedor != null && info_proveedor.IdProveedor != 0)
                        {
                            cp_nota_DebCre_Info info = new cp_nota_DebCre_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdCbteCble_Nota = IdCbteCble_Nota++,
                                IdTipoCbte_Nota = Convert.ToInt32(info_cp_parametro.pa_TipoCbte_ND),
                                DebCre = "D",
                                IdTipoNota = "T_TIP_NOTA_INT",
                                IdProveedor = info_proveedor.IdProveedor,
                                //IdSucursal = lst_sucursal.Where(q => q.Su_CodigoEstablecimiento == Su_CodigoEstablecimiento).FirstOrDefault().IdSucursal,
                                IdSucursal = IdSucursal,
                                cn_fecha = Convert.ToDateTime(reader.GetValue(5)),
                                Fecha_contable = Convert.ToDateTime(reader.GetValue(5)),
                                cn_Fecha_vcto = Convert.ToDateTime(reader.GetValue(6)),
                                cn_observacion = Convert.ToString(reader.GetValue(7)),
                                cn_subtotal_iva = 0,
                                cn_subtotal_siniva = Convert.ToDouble(reader.GetValue(4)),
                                cn_baseImponible = 0,
                                cn_Por_iva = 12,
                                cn_valoriva = 0,
                                cn_Ice_base = 0,
                                cn_Ice_por = 0,
                                cn_Ice_valor = 0,
                                cn_Serv_por = 0,
                                cn_Serv_valor = 0,
                                cn_BaseSeguro = 0,
                                cn_total = Convert.ToDecimal(reader.GetValue(4)),
                                cn_vaCoa = "N",
                                cod_nota = Convert.ToString(reader.GetValue(2)),
                                IdUsuario = SessionFixed.IdUsuario,
                                Fecha_Transac = DateTime.Now,
                                Nombre_proveedor= info_proveedor.info_persona.pe_razonSocial
                            };

                            Lista_NotaDebito.Add(info);
                        }
                        else
                        {
                            cp_nota_DebCre_Info info = new cp_nota_DebCre_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdCbteCble_Nota = IdCbteCble_Nota++,
                                IdTipoCbte_Nota = Convert.ToInt32(info_cp_parametro.pa_TipoCbte_ND),
                                DebCre = "D",
                                IdTipoNota = "T_TIP_NOTA_INT",
                                cn_fecha = Convert.ToDateTime(reader.GetValue(5)),
                                Fecha_contable = Convert.ToDateTime(reader.GetValue(5)),
                                cn_Fecha_vcto = Convert.ToDateTime(reader.GetValue(6)),
                                cn_observacion = Convert.ToString(reader.GetValue(7)),
                                cn_subtotal_iva = 0,
                                cn_subtotal_siniva = Convert.ToDouble(reader.GetValue(4)),
                                cn_baseImponible = 0,
                                cn_Por_iva = 12,
                                cn_valoriva = 0,
                                cn_Ice_base = 0,
                                cn_Ice_por = 0,
                                cn_Ice_valor = 0,
                                cn_Serv_por = 0,
                                cn_Serv_valor = 0,
                                cn_BaseSeguro = 0,
                                cn_total = Convert.ToDecimal(reader.GetValue(4)),
                                cn_vaCoa = "N",
                                cod_nota = Convert.ToString(reader.GetValue(2)),
                                IdUsuario = SessionFixed.IdUsuario,
                                Fecha_Transac = DateTime.Now,
                                Nombre_proveedor = ruc_proveedor

                            };
                            Lista_NotaDebito.Add(info);
                        }
                    }
                    else
                    {

                        
                        cont++;
                    }
                }
                ListaNotaDebito.set_list(Lista_NotaDebito, IdTransaccionSession);
                #endregion
            }
        }
    }
    #endregion

    public class ct_cbtecble_det_List_nd
    {
        string Variable = "ct_cbtecble_det_Info";
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
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
                cbtecble_det_total_Info.dc_Valor_haber = cn_total;
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
                    cbtecble_det_iva_Info.dc_Valor_debe = cn_valoriva;
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
                cbtecble_det_sub_Info.dc_Valor_debe = cn_subtotal_iva + cn_subtotal_siniva;
                cbtecble_det_sub_Info.dc_Valor = cn_subtotal_iva + cn_subtotal_siniva;
                cbtecble_det_sub_Info.dc_Observacion = observacion;
                AddRow(cbtecble_det_sub_Info, IdTransaccionSession);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class cp_nota_DebCre_List
    {
        string Variable = "cp_nota_DebCre_Info";
        public List<cp_nota_DebCre_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cp_nota_DebCre_Info> list = new List<cp_nota_DebCre_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cp_nota_DebCre_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cp_nota_DebCre_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}