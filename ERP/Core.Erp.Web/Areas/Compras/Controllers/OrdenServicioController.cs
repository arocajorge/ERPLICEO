using Core.Erp.Bus.Compras;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Compras;
using Core.Erp.Info.CuentasPorPagar;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using Core.Erp.Info.Inventario;
using Core.Erp.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Erp.Web.Areas.Compras.Controllers
{
    public class OrdenServicioController : Controller
    {
        #region Variables
        com_ordencompra_local_Bus bus_ordencompra = new com_ordencompra_local_Bus();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        com_TerminoPago_Bus bus_termino = new com_TerminoPago_Bus();
        com_catalogo_Bus bus_catalogo = new com_catalogo_Bus();
        com_estado_cierre_Bus bus_estado = new com_estado_cierre_Bus();
        com_comprador_Bus bus_comprador = new com_comprador_Bus();
        com_departamento_Bus bus_departamento = new com_departamento_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        com_ordencompra_local_det_List List_det = new com_ordencompra_local_det_List();
        com_ordencompra_local_det_Bus bus_det = new com_ordencompra_local_det_Bus();
        com_parametro_Bus bus_param = new com_parametro_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda proveedor
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor_OS()
        {
            decimal model = new decimal();
            return PartialView("_CmbProveedor_OS", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        #endregion

        #region Metodos ComboBox bajo demanda producto

        public ActionResult CmbProducto_OS()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_OS", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            List<in_Producto_Info> Lista = bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.SOLOSERVICIOS, cl_enumeradores.eModulo.FAC, 0, Convert.ToInt32(SessionFixed.IdSucursal));
            return Lista;
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal)
            };
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_orden_servicio(int IdSucursal, DateTime? fecha_ini, DateTime? fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);
            ViewBag.tipo = "OS";

            var model = bus_ordencompra.get_list(IdEmpresa, IdSucursal, ViewBag.fecha_ini, ViewBag.fecha_fin, true, ViewBag.tipo);
            return PartialView("_GridViewPartial_orden_servicio", model);
        }

        private void CargarCombosConsulta(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        private void cargar_combos(int IdEmpresa)
        {
            var lst_termino = bus_termino.get_list(IdEmpresa, false);
            lst_termino.Add(new com_TerminoPago_Info());
            ViewBag.lst_termino = lst_termino;

            var lst_apro = bus_catalogo.get_list(cl_enumeradores.eTipoCatalogoCOM.EST_APRO.ToString(), false);
            ViewBag.lst_apro = lst_apro;

            var lst_estado = bus_estado.get_list(false);
            ViewBag.lst_estado = lst_estado;

            var lst_comprador = bus_comprador.get_list(IdEmpresa, false);
            ViewBag.lst_comprador = lst_comprador;

            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_dep = bus_departamento.get_list(IdEmpresa, false);
            ViewBag.lst_dep = lst_dep;
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
            com_parametro_Info i_param = bus_param.get_info(IdEmpresa);
            if (i_param == null)
                return RedirectToAction("Index");
            com_ordencompra_local_Info model = new com_ordencompra_local_Info
            {
                IdEmpresa = IdEmpresa,
                oc_fecha = DateTime.Now.Date,
                oc_fechaVencimiento = DateTime.Now.Date,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                IdEstadoAprobacion_cat = i_param.IdEstadoAprobacion_OC,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                Tipo = "OS"

            };
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(com_ordencompra_local_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.lst_det = List_det.get_list(model.IdTransaccionSession);
            if (!ModelState.IsValid)
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            var IdUsuario_Com = SessionFixed.IdUsuario;
            com_comprador_Info info_comprador = bus_comprador.get_info_x_IdUsuario(model.IdEmpresa, IdUsuario_Com);

            if (info_comprador == null)
            {
                model.IdComprador = 0;
            }
            else
            {
                model.IdComprador = info_comprador.IdComprador;
            }

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            if (!bus_ordencompra.guardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            //return RedirectToAction("Index");
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdOrdenCompra = model.IdOrdenCompra, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, decimal IdOrdenCompra = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            com_ordencompra_local_Info model = bus_ordencompra.get_info(IdEmpresa, IdSucursal, IdOrdenCompra);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdOrdenCompra);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(com_ordencompra_local_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            model.lst_det = List_det.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            if (!bus_ordencompra.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            //return RedirectToAction("Index");
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdOrdenCompra = model.IdOrdenCompra, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, decimal IdOrdenCompra = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            com_ordencompra_local_Info model = bus_ordencompra.get_info(IdEmpresa, IdSucursal, IdOrdenCompra);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdOrdenCompra);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(com_ordencompra_local_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.lst_det = List_det.get_list(model.IdTransaccionSession);

            if (!bus_ordencompra.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Funciones del detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_orden_servicio_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_orden_servicio_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] com_ordencompra_local_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
                info_det.pr_descripcion = producto.pr_descripcion_combo;

            if (ModelState.IsValid)
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_orden_servicio_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] com_ordencompra_local_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var producto = bus_producto.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), info_det.IdProducto);
            if (producto != null)
                info_det.pr_descripcion = producto.pr_descripcion_combo;

            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_orden_servicio_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_orden_servicio_det", model);
        }
        #endregion

        #region Json
        public JsonResult GetInfoProducto(int IdEmpresa = 0, int IdProducto = 0)
        {
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_termino_pago_x_proveedor(int IdEmpresa = 0, decimal IdProveedor = 0)
        {
            var info_termino_pago = new com_TerminoPago_Info();
            cp_proveedor_Info info_proveedor = bus_proveedor.get_info(IdEmpresa, IdProveedor);
            com_TerminoPago_Info info_termino_pago_igual = bus_termino.get_info_termino_pago_x_proveedor(IdEmpresa, info_proveedor.pr_plazo, "=");
            com_TerminoPago_Info info_termino_pago_mayor = bus_termino.get_info_termino_pago_x_proveedor(IdEmpresa, info_proveedor.pr_plazo, ">=");

            if (info_termino_pago_igual != null)
            {
                info_termino_pago = info_termino_pago_igual;
            }
            else
            {
                info_termino_pago = info_termino_pago_mayor;
            }

            return Json(info_termino_pago, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_termino_pago(int IdEmpresa = 0, int IdTerminoPago = 0)
        {
            var info_termino_pago = bus_termino.get_info(IdEmpresa, IdTerminoPago);
            return Json(info_termino_pago, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Metodos
        private bool Validar(com_ordencompra_local_Info i_validar, ref string msg)
        {
            i_validar.lst_det = List_det.get_list(i_validar.IdTransaccionSession);

            if (i_validar.IdComprador == 0)
            {
                mensaje = "Debe ingresar su usuario como comprador";
                return false;
            }
            else if (i_validar.lst_det.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto en el detalle de la orden";
                return false;
            }
            else
            {
                foreach (var item1 in i_validar.lst_det)
                {
                    var contador = 0;
                    foreach (var item2 in i_validar.lst_det)
                    {
                        if (item1.IdProducto == item2.IdProducto)
                        {
                            contador++;
                        }

                        if (contador > 1)
                        {
                            mensaje = "Existen productos repetidos en el detalle";
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

    }
    public class com_orden_servicio_det_List
    {
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        string variable = "com_orden_servicio_det_Info";
        public List<com_ordencompra_local_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<com_ordencompra_local_det_Info> list = new List<com_ordencompra_local_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<com_ordencompra_local_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }
        public void set_list(List<com_ordencompra_local_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(com_ordencompra_local_det_Info info_det, decimal IdTransaccionSession)
        {
            List<com_ordencompra_local_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.do_descuento = Math.Round(info_det.do_precioCompra * (info_det.do_porc_des / 100), 2, MidpointRounding.AwayFromZero);
            info_det.do_precioFinal = Math.Round(info_det.do_precioCompra - info_det.do_descuento, 2, MidpointRounding.AwayFromZero);
            info_det.do_subtotal = Math.Round(info_det.do_Cantidad * info_det.do_precioFinal, 2, MidpointRounding.AwayFromZero);
            var impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto);
            if (impuesto != null)
                info_det.Por_Iva = impuesto.porcentaje;
            else
                info_det.Por_Iva = 0;
            info_det.do_iva = Math.Round(info_det.do_subtotal * (info_det.Por_Iva / 100), 2, MidpointRounding.AwayFromZero);
            info_det.do_total = Math.Round(info_det.do_subtotal + info_det.do_iva, 2, MidpointRounding.AwayFromZero);
            list.Add(info_det);
        }

        public void UpdateRow(com_ordencompra_local_det_Info info_det, decimal IdTransaccionSession)
        {
            com_ordencompra_local_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.do_Cantidad = info_det.do_Cantidad;
            edited_info.do_precioCompra = info_det.do_precioCompra;
            edited_info.do_porc_des = info_det.do_porc_des;
            edited_info.IdCod_Impuesto = info_det.IdCod_Impuesto;
            edited_info.do_descuento = Math.Round(info_det.do_precioCompra * (info_det.do_porc_des / 100), 2, MidpointRounding.AwayFromZero);
            edited_info.do_precioFinal = Math.Round(info_det.do_precioCompra - info_det.do_descuento, 2, MidpointRounding.AwayFromZero);
            edited_info.do_subtotal = Math.Round(info_det.do_Cantidad * edited_info.do_precioFinal, 2, MidpointRounding.AwayFromZero);
            var impuesto = bus_impuesto.get_info(edited_info.IdCod_Impuesto);
            if (impuesto != null)
                edited_info.Por_Iva = impuesto.porcentaje;
            else
                edited_info.Por_Iva = 0;
            edited_info.do_iva = Math.Round(edited_info.do_subtotal * (edited_info.Por_Iva / 100), 2, MidpointRounding.AwayFromZero);
            edited_info.do_total = Math.Round(edited_info.do_subtotal + edited_info.do_iva, 2, MidpointRounding.AwayFromZero);

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<com_ordencompra_local_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}