using Core.Erp.Bus.Facturacion;
using Core.Erp.Bus.General;
using Core.Erp.Info.Facturacion;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;

namespace Core.Erp.Web.Areas.Facturacion.Controllers
{
    [SessionTimeout]

    public class GuiaRemisionController : Controller
    {
        #region variables
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_guia_remision_Bus bus_guia = new fa_guia_remision_Bus();
        fa_guia_remision_det_Bus bus_detalle = new fa_guia_remision_det_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        tb_transportista_Bus bus_transportista = new tb_transportista_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        fa_factura_Bus bus_factura = new fa_factura_Bus();
        fa_guia_remision_det_Info_lst detalle_info = new fa_guia_remision_det_Info_lst();
        fa_cliente_contactos_Bus bus_contacto = new fa_cliente_contactos_Bus();
        fa_factura_x_fa_guia_remision_Bus bus_detalle_x_factura = new fa_factura_x_fa_guia_remision_Bus();
        fa_catalogo_Bus bus_catalogo = new fa_catalogo_Bus();
        fa_factura_x_fa_guia_remision_Info_List List_rel = new fa_factura_x_fa_guia_remision_Info_List();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        fa_MotivoTraslado_Bus bus_traslado = new fa_MotivoTraslado_Bus();

        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda cliente
        public ActionResult CmbCliente_Guia()
        {
            decimal model = new decimal();
            return PartialView("_CmbCliente_Guia", model);
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

        #region Metodos ComboBox bajo demanda producto

        public ActionResult CmbProducto_Guia()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_Guia", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            List<in_Producto_Info> Lista = bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORSUCURSAL, cl_enumeradores.eModulo.FAC, 0, Convert.ToInt32(SessionFixed.IdSucursal));
            return Lista;
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region vistas

        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }


        public ActionResult GridViewPartial_guias_remision(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            if (IdSucursal == 0)
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            ViewBag.IdSucursal = IdSucursal;

            List<fa_guia_remision_Info> model = new List<fa_guia_remision_Info>();
            model = bus_guia.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_guias_remision", model);
        }
        public ActionResult GridViewPartial_guias_remision_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = detalle_info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_guias_remision_det", model);
        }

        public ActionResult GridViewPartial_FacturasSinGuia()
        {
            List<fa_factura_Info> model = new List<fa_factura_Info>();
            model = Session["fa_factura_Info"] as List<fa_factura_Info>;
            return PartialView("_GridViewPartial_FacturasSinGuia", model);
        }

        public ActionResult GridViewPartial_Facturas_x_guia()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_rel.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Facturas_x_guia", model);
        }
        private bool validar(fa_guia_remision_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.gi_fecha, cl_enumeradores.eModulo.FAC, i_validar.IdSucursal, ref msg))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Metodos
        private void cargar_combos(fa_guia_remision_Info model)
        {
            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_punto_venta = bus_punto_venta.get_list_x_tipo_doc(model.IdEmpresa, model.IdSucursal, "GUIA");
            ViewBag.lst_punto_venta = lst_punto_venta;

            var lst_transportista = bus_transportista.get_list(model.IdEmpresa, false);
            ViewBag.lst_transportista = lst_transportista;

            var lst_contacto = bus_contacto.get_list(model.IdEmpresa, model.IdCliente);
            ViewBag.lst_contacto = lst_contacto;

            var lst_tipo_traslado = bus_traslado.get_list(model.IdEmpresa, false);
            ViewBag.lst_tipo_traslado = lst_tipo_traslado;

        }

        private void cargar_combos_detalle()
        {
            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        #endregion

        #region acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_guia_remision_Info model = new fa_guia_remision_Info
            {
                
                gi_fecha = DateTime.Now,
                gi_FechaFinTraslado = DateTime.Now,
                gi_FechaInicioTraslado = DateTime.Now,
                IdEmpresa = IdEmpresa,
                IdSucursal = IdSucursal,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                lst_detalle = new List<fa_guia_remision_det_Info>(),
                lst_detalle_x_factura = new List<fa_factura_x_fa_guia_remision_Info>()
            };
            detalle_info.set_list(model.lst_detalle, model.IdTransaccionSession);
            List_rel.set_list(model.lst_detalle_x_factura, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(fa_guia_remision_Info model)
        {
            try
            {
                fa_PuntoVta_Info info_puntovta = new fa_PuntoVta_Info();
                info_puntovta = bus_punto_venta.get_info(model.IdEmpresa, model.IdSucursal, model.IdPuntoVta);

                model.IdBodega = info_puntovta.IdBodega;
                model.IdUsuarioCreacion = SessionFixed.IdUsuario;
                model.CodGuiaRemision = (model.CodGuiaRemision == null) ? "" : model.CodGuiaRemision;
                model.lst_detalle_x_factura = List_rel.get_list(model.IdTransaccionSession);
                model.lst_detalle = detalle_info.get_list(model.IdTransaccionSession);
                model.CodDocumentoTipo = cl_enumeradores.eTipoDocumento.GUIA.ToString();

                string mensaje = bus_guia.validar(model);
                if (mensaje != "")
                {
                    cargar_combos(model);
                    ViewBag.mensaje = mensaje;
                    return View(model);
                }
                if (!validar(model, ref mensaje))
                {
                    cargar_combos(model);
                    ViewBag.mensaje = mensaje;
                    return View(model);
                }
                if (!bus_guia.guardarDB(model))
                {
                    cargar_combos(model);
                    return View(model);
                }

                return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdGuiaRemision = model.IdGuiaRemision, Exito = true });
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                cargar_combos(model);
                return View(model);
            }
        }
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdGuiaRemision=0, bool Exito = false)
        {
            bus_guia = new fa_guia_remision_Bus();
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_guia_remision_Info model = bus_guia.get_info(IdEmpresa, IdGuiaRemision);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_detalle = bus_detalle.get_list(IdEmpresa, IdGuiaRemision);
            detalle_info.set_list(model.lst_detalle, model.IdTransaccionSession);
            List_rel.set_list(bus_detalle_x_factura.get_list(IdEmpresa, IdGuiaRemision), model.IdTransaccionSession);
            cargar_combos(model);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.gi_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(fa_guia_remision_Info model)
        {
            try
            {
                model.IdUsuarioModificacion = SessionFixed.IdUsuario.ToString();
                model.CodGuiaRemision = (model.CodGuiaRemision == null) ? "" : model.CodGuiaRemision;
                model.CodDocumentoTipo = "GUIA";
                model.lst_detalle_x_factura = List_rel.get_list(model.IdTransaccionSession);
                model.lst_detalle = detalle_info.get_list(model.IdTransaccionSession);
                string mensaje = bus_guia.validar(model);
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
                    return View(model);
                }
                if (!bus_guia.modificarDB(model))
                {
                    cargar_combos(model);
                    return View(model);
                }

                MensajeSuccess = "Registro actualizado exitósamente";
                return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdGuiaRemision = model.IdGuiaRemision, Exito = true });
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());
                ViewBag.error = ex.Message.ToString();
                cargar_combos(model);
                return View(model);
            }
           
        }
        public ActionResult Anular(int IdEmpresa = 0, decimal IdGuiaRemision=0)
        {
            bus_guia = new fa_guia_remision_Bus();
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_guia_remision_Info model = bus_guia.get_info(IdEmpresa, IdGuiaRemision);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            detalle_info.set_list(bus_detalle.get_list(IdEmpresa, IdGuiaRemision), model.IdTransaccionSession);
            List_rel.set_list(bus_detalle_x_factura.get_list(IdEmpresa, IdGuiaRemision), model.IdTransaccionSession);
            cargar_combos(model);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.gi_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_guia_remision_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            try
            {
                if (!bus_guia.anularDB(model))
                {
                    cargar_combos(model);
                    return View(model);
                }
                Session["fa_guia_remision_det_Info"] = null;
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());
                ViewBag.error = ex.Message.ToString();
                cargar_combos(model);
                return View(model);
            }
        }
        #endregion

        #region Json
        public JsonResult GetInfoProducto(int IdEmpresa = 0, int IdProducto = 0)
        {
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarPuntosDeVenta(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            var resultado = bus_punto_venta.get_list_x_tipo_doc(IdEmpresa, IdSucursal, cl_enumeradores.eTipoDocumento.GUIA.ToString());
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
                resultado = bus_talonario.GetUltimoNoUsado(IdEmpresa, cl_enumeradores.eTipoDocumento.GUIA.ToString(), sucursal.Su_CodigoEstablecimiento, punto_venta.cod_PuntoVta, punto_venta.EsElectronico,false);
            }

            if (resultado == null)
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();

            return Json(new { data_puntovta = punto_venta, data_talonario = resultado }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Cargar_facturas(decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_facturas_sin_guias = bus_factura.get_list_fac_sin_guia(IdEmpresa, IdCliente);
            Session["fa_factura_Info"] = lst_facturas_sin_guias;
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult seleccionar_aprobacion(string Ids, int IdSucursal=0, int IdPuntoVta=0, decimal IdTransaccionSession = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            fa_PuntoVta_Info punto_venta = new fa_PuntoVta_Info();
            punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);

            if (Ids != null)
            {
                var facturas_x_seleccionar = Session["fa_factura_Info"] as List<fa_factura_Info>;                
                
                string[] array = Ids.Split(',');
                var output = array.GroupBy(q => q).ToList();
                var lst_det = detalle_info.get_list(IdTransaccionSession);
                var lst_rel = List_rel.get_list(IdTransaccionSession);

                foreach (var item in output)
                {
                    if (item.Key != "")
                    {
                        if (lst_det.Where(q => q.IdCbteVta == Convert.ToDecimal(item.Key)).Count() == 0)
                        {
                            var lst_tmp = bus_detalle.get_list_x_factura(IdEmpresa, IdSucursal, punto_venta.IdBodega, Convert.ToDecimal(item.Key));
                            lst_det.AddRange(lst_tmp);
                        }

                        if (facturas_x_seleccionar.Where(q => q.IdCbteVta == Convert.ToDecimal(item.Key)).Count() >0)
                        {

                            var factura = facturas_x_seleccionar.Where(q => q.IdCbteVta == Convert.ToDecimal(item.Key)).FirstOrDefault();
                            if (factura != null)
                            {
                                if(lst_rel.Where(q => q.IdCbteVta == Convert.ToDecimal(item.Key)).Count()==0)
                                    lst_rel.Add(new fa_factura_x_fa_guia_remision_Info
                                {
                                    IdEmpresa=factura.IdEmpresa,
                                    IdSucursal=factura.IdSucursal,
                                    IdBodega=factura.IdBodega,
                                    IdCbteVta=factura.IdCbteVta,
                                    vt_serie1=factura.vt_serie1,
                                    vt_serie2=factura.vt_serie2,
                                    vt_NumFactura=factura.vt_NumFactura,
                                    vt_tipoDoc="FAC"
                                });
                                
                            }

                        }
                    }
                }
                List_rel.set_list(lst_rel, IdTransaccionSession);
                detalle_info.set_list(lst_det, IdTransaccionSession);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_direcciones(decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            fa_cliente_contactos_Info resultado = new fa_cliente_contactos_Info();

            fa_cliente_Info info_cliente = bus_cliente.get_info(IdEmpresa, IdCliente);

            if (info_cliente != null)
            {
                resultado = bus_contacto.get_info(IdEmpresa, IdCliente, info_cliente.IdContacto);
            }            

            if (resultado != null)
                resultado.Direccion_emp = SessionFixed.em_direccion.ToString();
            else
                resultado = new fa_cliente_contactos_Info();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_placa(int Idtransportista = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            var resultado = bus_transportista.get_info(IdEmpresa, Idtransportista);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult cargar_contactos(decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            var resultado = bus_contacto.get_list(IdEmpresa, IdCliente);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_direccion_origen(int  IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_sucursal.get_info(IdEmpresa, IdSucursal);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region funciones del detalle

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_guia_remision_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
            {
                info_det.pr_descripcion = producto.pr_descripcion_combo;
                info_det.IdCod_Impuesto = producto.IdCod_Impuesto_Iva;
            }                

            if (ModelState.IsValid)
                detalle_info.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var model = detalle_info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_guias_remision_det", model);
        }
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_guia_remision_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
                if (info_det.Secuencia != 0 && info_det.gi_cantidad!=0)
                {
                    var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
                    if (producto != null)
                    {
                        info_det.pr_descripcion = producto.pr_descripcion_combo;
                        info_det.IdCod_Impuesto = producto.IdCod_Impuesto_Iva;
                    }                        

                    if (ModelState.IsValid)
                        detalle_info.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                }

            var model = detalle_info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_guias_remision_det", model);
        }
        public ActionResult EditingDelete(int Secuencia)
        {
            decimal IdComprobante = Convert.ToDecimal( detalle_info.get_list( Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(v => v.Secuencia == Secuencia).FirstOrDefault().IdCbteVta);
            if(detalle_info.get_list( Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(v => v.IdCbteVta == IdComprobante).Count()==1)
            {
                var list_facturas_seleccionadas = List_rel.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                var Info = list_facturas_seleccionadas.Where(v => v.IdCbteVta == IdComprobante).FirstOrDefault();
                list_facturas_seleccionadas.Remove(Info);
                List_rel.set_list(list_facturas_seleccionadas,Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            detalle_info.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = detalle_info.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_guias_remision_det", model);
        }
        #endregion             
    }
    public class fa_guia_remision_det_Info_lst
    {
        string Variable = "fa_guia_remision_det_Info";
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();

        public List<fa_guia_remision_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_guia_remision_det_Info> list = new List<fa_guia_remision_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_guia_remision_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_guia_remision_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
        public void AddRow(fa_guia_remision_det_Info info_det, decimal IdTransaccionSession)
        {
            List<fa_guia_remision_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            info_det.gi_descuentoUni = Math.Round(info_det.gi_precio * (info_det.gi_por_desc / 100), 2, MidpointRounding.AwayFromZero);
            info_det.gi_PrecioFinal = Math.Round(info_det.gi_precio - info_det.gi_descuentoUni, 2, MidpointRounding.AwayFromZero);
            info_det.gi_Subtotal = Math.Round(info_det.gi_cantidad * info_det.gi_PrecioFinal, 2, MidpointRounding.AwayFromZero);
            var impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto);
            if (impuesto != null)
                info_det.gi_por_iva = impuesto.porcentaje;
            else
                info_det.gi_por_iva = 0;
            info_det.gi_Iva = Math.Round(info_det.gi_Subtotal * (info_det.gi_por_iva / 100), 2, MidpointRounding.AwayFromZero);
            info_det.gi_Total = Math.Round(info_det.gi_Subtotal + info_det.gi_Iva, 2, MidpointRounding.AwayFromZero);

            list.Add(info_det);
        }

        public void UpdateRow(fa_guia_remision_det_Info info_det, decimal IdTransaccionSession)
        {
            fa_guia_remision_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();

            edited_info.IdProducto = info_det.IdProducto;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.gi_cantidad = info_det.gi_cantidad;
            edited_info.gi_precio = info_det.gi_precio;
            edited_info.gi_por_desc = info_det.gi_por_desc;
            edited_info.IdCod_Impuesto = info_det.IdCod_Impuesto;
            edited_info.gi_descuentoUni = Math.Round(info_det.gi_precio * (info_det.gi_por_desc / 100), 2, MidpointRounding.AwayFromZero);
            edited_info.gi_PrecioFinal = Math.Round(info_det.gi_precio - info_det.gi_descuentoUni, 2, MidpointRounding.AwayFromZero);
            edited_info.gi_Subtotal = Math.Round(info_det.gi_cantidad * edited_info.gi_PrecioFinal, 2, MidpointRounding.AwayFromZero);
            var impuesto = bus_impuesto.get_info(edited_info.IdCod_Impuesto);
            if (impuesto != null)
                edited_info.gi_por_iva = impuesto.porcentaje;
            else
                edited_info.gi_por_iva = 0;
            
            edited_info.gi_Iva = Math.Round(edited_info.gi_Subtotal * (edited_info.gi_por_iva / 100), 2, MidpointRounding.AwayFromZero);
            edited_info.gi_Total = Math.Round(edited_info.gi_Subtotal + edited_info.gi_Iva, 2, MidpointRounding.AwayFromZero);

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<fa_guia_remision_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
    public class fa_factura_x_fa_guia_remision_Info_List
    {
        string Variable = "fa_factura_x_fa_guia_remision_Info";

        public List<fa_factura_x_fa_guia_remision_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_factura_x_fa_guia_remision_Info> list = new List<fa_factura_x_fa_guia_remision_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_factura_x_fa_guia_remision_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_factura_x_fa_guia_remision_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}