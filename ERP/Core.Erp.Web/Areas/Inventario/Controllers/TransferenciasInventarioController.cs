using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;
using Core.Erp.Bus.General;
using Core.Erp.Info.General;
using Core.Erp.Info.Helps;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using Core.Erp.Web.Helps;
using Core.Erp.Bus.Contabilidad;

namespace Core.Erp.Web.Areas.Inventario.Controllers
{
    [SessionTimeout]
    public class TransferenciasInventarioController : Controller
    {
        #region variables
        in_transferencia_Bus bus_trnferencia = new in_transferencia_Bus();
        in_transferencia_det_Bus bus_tras_detalle = new in_transferencia_det_Bus();
        in_transferencia_det_List List_in_transferencia_det = new in_transferencia_det_List();
        in_parametro_Bus bus_in_param = new in_parametro_Bus();
        string mensaje = string.Empty;
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_UnidadMedidaEquivalencia = new in_UnidadMedida_Equiv_conversion_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";

        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProducto_TransferenciaInventario()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_TransferenciaInventario", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),cl_enumeradores.eTipoBusquedaProducto.PORMODULO,cl_enumeradores.eModulo.INV,0,0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region vistas
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal)
            };
            CargarCombosConsulta(Convert.ToInt32( SessionFixed.IdEmpresa));
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombosConsulta(Convert.ToInt32(SessionFixed.IdEmpresa));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_transferencias(int IdSucursal, DateTime? fecha_ini, DateTime? fecha_fin)
        {
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : fecha_ini;
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : fecha_fin;
            ViewBag.IdSucursal = IdSucursal == 0 ? 0 : Convert.ToInt32(IdSucursal);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<in_transferencia_Info> model = bus_trnferencia.get_list(IdEmpresa, IdSucursal, ViewBag. fecha_ini, ViewBag. fecha_fin);
            return PartialView("_GridViewPartial_transferencias", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_transferencias_det()
        {

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_detalle();
            var model = List_in_transferencia_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_transferencias_det",model);
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
            in_transferencia_Info model = new in_transferencia_Info
            {
                IdEmpresa = IdEmpresa,
                tr_fecha = DateTime.Now,
                IdSucursalOrigen = Convert.ToInt32(SessionFixed.IdSucursal),
                IdMovi_inven_tipo_SucuOrig = i_param.IdMovi_inven_tipo_egresoBodegaOrigen,
                IdMovi_inven_tipo_SucuDest = i_param.IdMovi_inven_tipo_ingresoBodegaDestino,
                IdTransaccionSession = Convert.ToInt32(SessionFixed.IdTransaccionSessionActual),
                list_detalle = new List<in_transferencia_det_Info>()

            };
            model.list_detalle = List_in_transferencia_det.get_list(model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_transferencia_Info model)
        {
            model.list_detalle = List_in_transferencia_det.get_list(model.IdTransaccionSession);
            string mensaje = bus_trnferencia.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!bus_trnferencia.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            Session["in_transferencia_det_Info"] = null;
            
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursalOrigen = model.IdSucursalOrigen, IdBodegaOrigen = model.IdBodegaOrigen, IdTransferencia = model.IdTransferencia, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursalOrigen = 0, int IdBodegaOrigen = 0, decimal IdTransferencia = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            in_transferencia_Info model = bus_trnferencia.get_info(IdEmpresa, IdSucursalOrigen, IdBodegaOrigen, IdTransferencia);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.list_detalle = bus_tras_detalle.get_list(IdEmpresa, IdSucursalOrigen, IdBodegaOrigen, IdTransferencia);
            List_in_transferencia_det.set_list(model.list_detalle, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.tr_fecha, cl_enumeradores.eModulo.INV, model.IdSucursalOrigen, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(in_transferencia_Info model)
        {
            model.list_detalle = List_in_transferencia_det.get_list(model.IdTransaccionSession);
            string mensaje = bus_trnferencia.validar(model);
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_trnferencia.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            List_in_transferencia_det.set_list(null, model.IdTransaccionSession);
            
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursalOrigen = model.IdSucursalOrigen, IdBodegaOrigen = model.IdBodegaOrigen, IdTransferencia = model.IdTransferencia, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdSucursalOrigen = 0, int IdBodegaOrigen = 0, decimal IdTransferencia = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            in_transferencia_Info model = bus_trnferencia.get_info(IdEmpresa, IdSucursalOrigen, IdBodegaOrigen, IdTransferencia);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.list_detalle = bus_tras_detalle.get_list(IdEmpresa, IdSucursalOrigen, IdBodegaOrigen, IdTransferencia);
            List_in_transferencia_det.set_list(model.list_detalle, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.tr_fecha, cl_enumeradores.eModulo.INV, model.IdSucursalOrigen, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(in_transferencia_Info model)
        {
            model.list_detalle = List_in_transferencia_det.get_list(model.IdTransaccionSession);
        
            model.tr_userAnulo = SessionFixed.IdUsuario;
            if (!bus_trnferencia.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Funciones del detalle
       
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_transferencia_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        //info_det.IdUnidadMedida = info_producto.IdUnidadMedida;
                        info_det.se_distribuye = info_producto.se_distribuye;
                        info_det.tp_ManejaInven = info_producto.tp_ManejaInven;
                    }
                }


            List_in_transferencia_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_transferencia_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_transferencias_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_transferencia_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        //info_det.IdUnidadMedida = info_producto.IdUnidadMedida;
                        info_det.se_distribuye = info_producto.se_distribuye;
                        info_det.tp_ManejaInven = info_producto.tp_ManejaInven;
                    }
                }

            List_in_transferencia_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_transferencia_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle();
            return PartialView("_GridViewPartial_transferencias_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_in_transferencia_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_in_transferencia_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_transferencias_det", model);
        }
        #endregion

        #region Combo y validaciones

        private void cargar_combos_detalle()
        {
           

            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }
        private void CargarCombosConsulta(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        private void cargar_combos(int IdEmpresa)
        {
            in_Motivo_Inven_Bus bus_motivo = new in_Motivo_Inven_Bus();
            var lst_motivo = bus_motivo.get_list(IdEmpresa, cl_enumeradores.eTipoIngEgr.ING.ToString(), false);
            ViewBag.lst_motivo = lst_motivo;

            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);
            ViewBag.lst_bodega = lst_bodega;
        }
        private bool validar(in_transferencia_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.tr_fecha, cl_enumeradores.eModulo.INV, i_validar.IdSucursal, ref msg))
            {
                return false;
            }

            #region ValidarStock

            var lst_validar = i_validar.list_detalle.GroupBy(q => new { q.IdProducto, q.pr_descripcion, q.tp_ManejaInven, q.se_distribuye }).Select(q => new in_Producto_Stock_Info
            {
                IdEmpresa = i_validar.IdEmpresa,
                IdSucursal = i_validar.IdSucursalOrigen,
                IdBodega = i_validar.IdBodegaOrigen,
                IdProducto = q.Key.IdProducto,
                pr_descripcion = q.Key.pr_descripcion,
                Cantidad = q.Sum(v=> v.dt_cantidad),
                tp_manejaInven = q.Key.tp_ManejaInven,
                CantidadAnterior = q.Sum(v=> v.CantidadAnterior),
                SeDestribuye = q.Key.se_distribuye
            }).ToList();

            if (!bus_producto.validar_stock(lst_validar, ref msg))
            {
                return false;
            }
            #endregion

            return true;
        }
        #endregion

        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            bus_bodega = new tb_bodega_Bus();
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

    public class in_transferencia_det_List
    {
        string Variable = "in_transferencia_det_Info";
        public List<in_transferencia_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_transferencia_det_Info> list = new List<in_transferencia_det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_transferencia_det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_transferencia_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_transferencia_det_Info info_det, decimal IdTransaccionSession)
        {
            List<in_transferencia_det_Info> list = get_list(IdTransaccionSession);
            info_det.dt_secuencia = list.Count == 0 ? 1 : list.Max(q => q.dt_secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            info_det.dt_cantidad = info_det.dt_cantidad;
            info_det.tp_ManejaInven = info_det.tp_ManejaInven;
            info_det.se_distribuye = info_det.se_distribuye;
            list.Add(info_det);
        }

        public void UpdateRow(in_transferencia_det_Info info_det, decimal IdTransaccionSession)
        {
            in_transferencia_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.dt_secuencia == info_det.dt_secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdUnidadMedida = info_det.IdUnidadMedida;
            edited_info.dt_cantidad = info_det.dt_cantidad;
            edited_info.tp_ManejaInven = info_det.tp_ManejaInven;
            edited_info.se_distribuye = info_det.se_distribuye;
        }

        public void DeleteRow(int dt_secuencia, decimal IdTransaccionSession)
        {
            List<in_transferencia_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.dt_secuencia == dt_secuencia).First());
        }

    }
}