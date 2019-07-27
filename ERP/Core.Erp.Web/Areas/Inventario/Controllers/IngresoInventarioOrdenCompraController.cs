using Core.Erp.Bus.Compras;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
using Core.Erp.Info.Compras;
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

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    public class IngresoInventarioOrdenCompraController : Controller
    {
        #region variables
        in_Ing_Egr_Inven_Bus bus_ing_inv = new in_Ing_Egr_Inven_Bus();
        in_Ing_Egr_Inven_det_Bus bus_det_ing_inv = new in_Ing_Egr_Inven_det_Bus();
        in_Ing_Egr_Inven_det_OCList List_in_Ing_Egr_Inven_det = new in_Ing_Egr_Inven_det_OCList();
        com_ordencompra_local_det_List ListaPorIngresar = new com_ordencompra_local_det_List();
        in_parametro_Bus bus_in_param = new in_parametro_Bus();
        string mensaje = string.Empty;
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_UnidadMedidaEquivalencia = new in_UnidadMedida_Equiv_conversion_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        com_parametro_Bus bus_com_param = new com_parametro_Bus();
        com_ordencompra_local_Bus bus_orden_compra = new com_ordencompra_local_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProducto_IngresoInventario()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_IngresoInventario", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.INV, 0, 0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Metodos ComboBox bajo demanda proveedor
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public ActionResult CmbProveedor_IngresoOrdenCompra()
        {
            decimal model = new decimal();
            return PartialView("_CmbProveedor_IngresoOrdenCompra", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_proveedor(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_proveedor(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.PROVEE.ToString());
        }
        #endregion

        #region Vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdBodega = 0
            };
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        private void CargarCombosConsulta(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(IdEmpresa, true);
            ViewBag.lst_bodega = lst_bodega;
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombosConsulta(model.IdEmpresa);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_IngresoOrdenCompra(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0, int IdBodega=0)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSucursal = IdSucursal;
            ViewBag.IdBodega = IdBodega;

            com_parametro_Info info_parametro_compra = bus_com_param.get_info(IdEmpresa);
            List<in_Ing_Egr_Inven_Info> model = bus_ing_inv.get_list_orden_compra(IdEmpresa, IdSucursal, true, IdBodega, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_IngresoOrdenCompra", model);
        }
        #endregion

        #region Cargar Unidad de medida
        public ActionResult CargarUnidadMedida()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            decimal IdProducto = Request.Params["in_IdProducto"] != null ? Convert.ToDecimal(Request.Params["in_IdProducto"]) : 0;

            in_Producto_Info info_produto = bus_producto.get_info(IdEmpresa, IdProducto);
            return GridViewExtension.GetComboBoxCallbackResult(p =>
            {
                p.TextField = "Descripcion";
                p.ValueField = "IdUnidadMedida_equiva";
                p.ValueType = typeof(string);
                p.BindList(bus_UnidadMedidaEquivalencia.get_list_combo(info_produto.IdUnidadMedida_Consumo));
            });

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
            in_parametro_Info i_param = bus_in_param.get_info(IdEmpresa);
            if (i_param == null)
                return RedirectToAction("Index");
            in_Ing_Egr_Inven_Info model = new in_Ing_Egr_Inven_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdEmpresa = IdEmpresa,
                cm_fecha = DateTime.Now,
                signo = "+",
                IdMovi_inven_tipo = i_param.P_IdMovi_inven_tipo_default_ing
            };
            List_in_Ing_Egr_Inven_det.set_list(new List<in_Ing_Egr_Inven_det_Info>(), model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(in_Ing_Egr_Inven_Info model)
        {
            com_parametro_Info info_param_oc = bus_com_param.get_info(model.IdEmpresa);
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            //model.IdResponsable = model.IdProveedor;
            model.IdUsuario = SessionFixed.IdUsuario;
            model.signo = "+";
            model.IdMovi_inven_tipo = info_param_oc.IdMovi_inven_tipo_OC;
            model.lst_in_Ing_Egr_Inven_det.ForEach(q => q.IdBodega_inv = model.IdBodega);
            model.lst_in_Ing_Egr_Inven_det.ForEach(q => { q.IdEmpresa_oc = model.IdEmpresa; });
            if (!bus_ing_inv.guardarDB(model, "+"))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdMovi_inven_tipo = model.IdMovi_inven_tipo, IdNumMovi = model.IdNumMovi, Exito = true });
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            in_Ing_Egr_Inven_Info model = bus_ing_inv.get_info(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            if (model == null)
                return RedirectToAction("Index");
            //model.IdProveedor = Convert.ToInt32(model.IdResponsable);
            model.lst_in_Ing_Egr_Inven_det = bus_det_ing_inv.get_list(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_in_Ing_Egr_Inven_det.set_list(model.lst_in_Ing_Egr_Inven_det, model.IdTransaccionSession);
            cargar_combos(model);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cm_fecha, cl_enumeradores.eModulo.INV, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_Ing_Egr_Inven_Info model)
        {
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuarioUltModi = SessionFixed.IdUsuario;
            //model.IdResponsable = model.IdProveedor;
            
            if (!bus_ing_inv.modificarDB(model))
            {
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdMovi_inven_tipo = model.IdMovi_inven_tipo, IdNumMovi = model.IdNumMovi, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, int IdMovi_inven_tipo = 0, decimal IdNumMovi = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            in_Ing_Egr_Inven_Info model = bus_ing_inv.get_info(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            if (model == null)
                return RedirectToAction("Index");
            model.lst_in_Ing_Egr_Inven_det = bus_det_ing_inv.get_list(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            List_in_Ing_Egr_Inven_det.set_list(model.lst_in_Ing_Egr_Inven_det, model.IdTransaccionSession);
            cargar_combos(model);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.cm_fecha, cl_enumeradores.eModulo.INV, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_Ing_Egr_Inven_Info model)
        {
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(model.IdTransaccionSession);
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdusuarioUltAnu = SessionFixed.IdUsuario;

            if (!bus_ing_inv.anularDB(model))
            {
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Funciones del detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_inv_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_inv_det", model);
        }
        private void cargar_combos_detalle()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = ListaPorIngresar.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    int IdEmpresaOC = Convert.ToInt32(item.Substring(0,3));
                    int IdSucursalOC = Convert.ToInt32(item.Substring(3, 3));
                    int IdOrdenCompra = Convert.ToInt32(item.Substring(6, 6));
                    int Secuencia = Convert.ToInt32(item.Substring(12, 6));

                    var info_det = lst_x_ingresar.Where(q => q.IdEmpresa == IdEmpresaOC && q.IdSucursal == IdSucursalOC && q.IdOrdenCompra == IdOrdenCompra && q.Secuencia == Secuencia).FirstOrDefault();

                    in_Ing_Egr_Inven_det_Info info_det_inv = new in_Ing_Egr_Inven_det_Info();
                    com_parametro_Info info_param_oc = bus_com_param.get_info(IdEmpresaSesion);
                    if (info_det != null)
                    {
                        info_det_inv.IdEmpresa = info_det.IdEmpresa;
                        info_det_inv.IdSucursal = info_det.IdSucursal;
                        info_det_inv.IdOrdenCompra = info_det.IdOrdenCompra;
                        info_det_inv.IdProducto = info_det.IdProducto;
                        info_det_inv.pr_descripcion = info_det.pr_descripcion;

                        info_det_inv.mv_costo_sinConversion = info_det.do_precioFinal;
                        info_det_inv.dm_cantidad_sinConversion = info_det.do_Cantidad_vw;
                        info_det_inv.IdUnidadMedida_sinConversion = info_det.IdUnidadMedida;
                        info_det_inv.Saldo = info_det.Saldo;

                        info_det_inv.IdEmpresa_oc = IdEmpresaOC;
                        info_det_inv.IdSucursal_oc = IdSucursalOC;

                        List_in_Ing_Egr_Inven_det.AddRow(info_det_inv, IdTransaccionSession);
                    }
                }
            }
            var model = List_in_Ing_Egr_Inven_det.get_list(IdTransaccionSession);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_Ing_Egr_Inven_det_Info info_det)
        {
            com_ordencompra_local_det_List Lista_OC = new com_ordencompra_local_det_List();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            List<in_Ing_Egr_Inven_det_Info> lista_detalle = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            List<com_ordencompra_local_Info> lista_detalle_oc = Lista_OC.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            in_Ing_Egr_Inven_det_Info info_det_editar = lista_detalle.Where(q=> q.Secuencia == info_det.Secuencia).FirstOrDefault();
            com_ordencompra_local_Info info_oc = lista_detalle_oc.Where(q => q.IdEmpresa == info_det_editar.IdEmpresa && q.IdSucursal == info_det_editar.IdSucursal && q.IdOrdenCompra == info_det_editar.IdOrdenCompra && q.IdProducto == info_det_editar.IdProducto).FirstOrDefault();
            info_det_editar.dm_cantidad_sinConversion = info_det.dm_cantidad_sinConversion;

            if (info_det_editar != null)
                if (info_det_editar.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det_editar.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.Saldo = info_det_editar.Saldo;
                        info_det.IdProducto = info_det_editar.IdProducto;
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        info_det.IdUnidadMedida_sinConversion = info_producto.IdUnidadMedida;
                    }
                }


            if (info_det_editar.dm_cantidad_sinConversion > 0 && info_det_editar.dm_cantidad_sinConversion <= info_det_editar.Saldo)
                List_in_Ing_Egr_Inven_det.UpdateRow(info_det_editar, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_inv_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_in_Ing_Egr_Inven_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            in_Ing_Egr_Inven_Info model = new in_Ing_Egr_Inven_Info();
            model.lst_in_Ing_Egr_Inven_det = List_in_Ing_Egr_Inven_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_inv_det", model.lst_in_Ing_Egr_Inven_det);
        }
        #endregion

        #region OC por ingresar
        public ActionResult GridViewPartial_oc_x_ingresar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaPorIngresar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_oc_x_ingresar", model);
        }
        #endregion

        #region validaciones cargar cobo
        private bool validar(in_Ing_Egr_Inven_Info i_validar, ref string msg)
        {
            if (i_validar.lst_in_Ing_Egr_Inven_det.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto";
                return false;
            }
            else
            {
                com_ordencompra_local_det_List Lista_OC = new com_ordencompra_local_det_List();
                List<com_ordencompra_local_Info> lista_detalle_oc = Lista_OC.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

                if (i_validar.IdNumMovi == 0)
                {
                    foreach (var item in i_validar.lst_in_Ing_Egr_Inven_det)
                    {
                        com_ordencompra_local_Info info_oc = lista_detalle_oc.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdSucursal == item.IdSucursal && q.IdOrdenCompra == item.IdOrdenCompra && q.IdProducto == item.IdProducto).FirstOrDefault();
                        if (item.dm_cantidad_sinConversion > info_oc.Saldo)
                        {
                            mensaje = "La cantidad ingresada supera al saldo pendiente del producto: "+item.pr_descripcion;
                            return false;
                        }
                    }                                      
                }
            }
            

            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.cm_fecha, cl_enumeradores.eModulo.INV, i_validar.IdSucursal, ref msg))
            {
                return false;
            }
            return true;
        }
        private void cargar_combos(in_Ing_Egr_Inven_Info model)
        {
            in_Motivo_Inven_Bus bus_motivo = new in_Motivo_Inven_Bus();
            var lst_motivo = bus_motivo.get_list(model.IdEmpresa, cl_enumeradores.eTipoIngEgr.ING.ToString(), false);
            ViewBag.lst_motivo = lst_motivo;

            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;
        }
        #endregion

        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            bus_bodega = new tb_bodega_Bus();
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListPorIngresar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, int IdSucursal = 0, decimal IdResponsable=0)
        {
            com_parametro_Info info_parametro_compra = bus_com_param.get_info(IdEmpresa);
            var lst = bus_orden_compra.get_list_x_ingresar(IdEmpresa, IdSucursal, IdResponsable);
            ListaPorIngresar.set_list(lst, IdTransaccionSession);

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class in_Ing_Egr_Inven_det_OCList
    {
        string Variable = "in_Ing_Egr_Inven_det_Info";
        public List<in_Ing_Egr_Inven_det_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_Ing_Egr_Inven_det_Info> list = new List<in_Ing_Egr_Inven_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_Ing_Egr_Inven_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_Ing_Egr_Inven_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_Ing_Egr_Inven_det_Info info_det_inv, decimal IdTransaccionSession)
        {
            List<in_Ing_Egr_Inven_det_Info> list = get_list(IdTransaccionSession);
            info_det_inv.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det_inv.secuencia_inv = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det_inv.Secuencia_oc = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;

            list.Add(info_det_inv);
        }

        public void UpdateRow(in_Ing_Egr_Inven_det_Info info_det, decimal IdTransaccionSession)
        {
            in_Ing_Egr_Inven_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();

            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdUnidadMedida_sinConversion = info_det.IdUnidadMedida_sinConversion;
            edited_info.mv_costo_sinConversion = info_det.mv_costo_sinConversion;
            edited_info.dm_cantidad_sinConversion = info_det.dm_cantidad_sinConversion;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.Saldo = info_det.Saldo;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<in_Ing_Egr_Inven_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }

    public class com_ordencompra_local_det_List
    {
        string Variable = "com_ordencompra_local_Info";
        public List<com_ordencompra_local_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<com_ordencompra_local_Info> list = new List<com_ordencompra_local_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<com_ordencompra_local_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<com_ordencompra_local_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(com_ordencompra_local_Info info_det, decimal IdTransaccionSession)
        {
            List<com_ordencompra_local_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            list.Add(info_det);
        }

        public void UpdateRow(com_ordencompra_local_Info info_det, decimal IdTransaccionSession)
        {
            com_ordencompra_local_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdUnidadMedida = info_det.IdUnidadMedida;
            edited_info.pr_descripcion = info_det.pr_descripcion;
            edited_info.IdProducto = info_det.IdProducto;
        }
    }
}