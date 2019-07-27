using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Bus.Inventario;
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
    public class ConsignacionController : Controller
    {
        // GET: Inventario/Consignacion
        #region Variables
        in_Consignacion_Bus bus_in_Consignacion = new in_Consignacion_Bus();
        in_ConsignacionDet_List in_ConsignacionDet_List = new in_ConsignacionDet_List();
        in_parametro_Bus bus_in_param = new in_parametro_Bus();
        string mensaje = string.Empty;
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        in_UnidadMedida_Equiv_conversion_Bus bus_UnidadMedidaEquivalencia = new in_UnidadMedida_Equiv_conversion_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        public ConsignacionController()
        {

        }

        #region Metodos ComboBox bajo demanda
        in_ConsignacionDet_Bus bus_consignacion_det = new in_ConsignacionDet_Bus();

        #region Proveedor
        public ActionResult CmbProveedor_Consignacion()
        {
            in_Consignacion_Info model = new in_Consignacion_Info();
            return PartialView("_CmbProveedor_Consignacion", model);
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

        #region  Producto
        public ActionResult CmbProducto_Consignacion()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_Consignacion", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.INV, 0,0);
        }
        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal)
            };
            CargarCombos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            model.IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            CargarCombos(model.IdEmpresa);
            return View(model);
        }
        #endregion

        #region Metodos
        private void CargarCombosAccion(int IdEmpresa, int IdSucursal)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_bodega = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_bodega = lst_bodega;
        }

        private void CargarCombos(int IdEmpresa)
        {
            try
            {
                var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
                ViewBag.lst_sucursal = lst_sucursal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Json
        public JsonResult CargarBodega(int IdEmpresa = 0, int IdSucursal = 0)
        {
            var resultado = bus_bodega.get_list(IdEmpresa, IdSucursal, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult GridViewPartial_Consignacion(DateTime? fecha_ini, DateTime? fecha_fin, int IdSucursal = 0)
        {
            ViewBag.IdSucursal = IdSucursal;
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            List<in_Consignacion_Info> model = bus_in_Consignacion.GetList(IdEmpresa, IdSucursal, true, ViewBag.fecha_ini, ViewBag.fecha_fin);
            return PartialView("_GridViewPartial_Consignacion", model);
        }

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
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Consignacion_Info model = new in_Consignacion_Info {
                IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                IdSucursal = string.IsNullOrEmpty(SessionFixed.IdSucursal) ? 0 : Convert.ToInt32(SessionFixed.IdSucursal),
                Fecha = DateTime.Now.Date
            };

            in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);
            CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(in_Consignacion_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.lst_producto_consignacion = in_ConsignacionDet_List.get_list(model.IdTransaccionSession);
            
            if (!Validar(model, ref mensaje))
            {
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_in_Consignacion.GuardarBD(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdConsignacion = model.IdConsignacion, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdConsignacion=0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Consignacion_Info model = bus_in_Consignacion.GetInfo(IdEmpresa, IdConsignacion);

            if (model == null)
                return RedirectToAction("Index");
    
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_producto_consignacion = bus_consignacion_det.GetList(model.IdEmpresa, Convert.ToInt32(model.IdConsignacion));
            in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.INV, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(in_Consignacion_Info model)
        {
            model.lst_producto_consignacion = in_ConsignacionDet_List.get_list(model.IdTransaccionSession);
            model.IdUsuarioUltMod = Session["IdUsuario"].ToString();

            if (!Validar(model, ref mensaje))
            {
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
           
            if (!bus_in_Consignacion.ModificarBD(model))
            {
                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdConsignacion = model.IdConsignacion, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdConsignacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            in_Consignacion_Info model = bus_in_Consignacion.GetInfo(IdEmpresa, Convert.ToInt32(IdConsignacion));
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_producto_consignacion = bus_consignacion_det.GetList(model.IdEmpresa, Convert.ToInt32(model.IdConsignacion));
            in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);

            CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.INV, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(in_Consignacion_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_in_Consignacion.AnularBD(model))
            {                
                ViewBag.mensaje = "No se ha podido anular el registro";

                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
                model.lst_producto_consignacion = bus_consignacion_det.GetList(model.IdEmpresa, Convert.ToInt32(model.IdConsignacion));
                in_ConsignacionDet_List.set_list(model.lst_producto_consignacion, model.IdTransaccionSession);

                CargarCombosAccion(model.IdEmpresa, model.IdSucursal);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion

        private bool Validar(in_Consignacion_Info i_validar, ref string msg)
        {
            i_validar.lst_producto_consignacion = in_ConsignacionDet_List.get_list(i_validar.IdTransaccionSession);

            if (i_validar.lst_producto_consignacion.Count == 0)
            {
                mensaje = "Debe ingresar un detalle en la consignación";
                return false;
            }

            if (i_validar.IdSucursal == 0)
            {
                mensaje = "Debe ingresar una sucursal";
                return false;
            }

            if (i_validar.IdBodega == 0)
            {
                mensaje = "Debe ingresar una bodega";
                return false;
            }

            if (i_validar.IdProveedor == 0)
            {
                mensaje = "Debe ingresar un proveedor";
                return false;
            }
            return true;
        }
        #region Funciones del detalle
        private bool validar_detalle(in_ConsignacionDet_Info item_validar, ref string msg)
        {
            if (item_validar.IdProducto == 0)
            {
                mensaje = "Debe ingresar producto";
                return false;
            }

            if (item_validar.Cantidad == 0)
            {
                mensaje = "Debe ingresar cantidad mayor a 0";
                return false;
            }

            if (item_validar.Costo == 0)
            {
                mensaje = "Debe ingresar costo mayor a 0";
                return false;
            }

            if (item_validar.IdUnidadMedida == "")
            {
                mensaje = "Debe ingresar unidad de medida";
                return false;
            }

            return true;
        }

        public ActionResult GridViewPartial_ConsignacionDet()
        {
            //siempre copiar
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            
            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        private void cargar_combos_detalle()
        {
            in_UnidadMedida_Bus bus_unidad = new in_UnidadMedida_Bus();
            var lst_unidad = bus_unidad.get_list(false);
            ViewBag.lst_unidad = lst_unidad;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] in_ConsignacionDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (info_det != null)
            {
                if (!validar_detalle(info_det, ref mensaje))
                {
                    cargar_combos_detalle();
                    ViewBag.mensaje = mensaje;
                }
                else
                {
                    if (info_det.IdProducto != 0)
                    {
                        in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                        if (info_producto != null)
                        {
                            info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                in_ConsignacionDet_List.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }

            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] in_ConsignacionDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

            if (info_det != null)
            {
                if (!validar_detalle(info_det, ref mensaje))
                {
                    cargar_combos_detalle();
                    ViewBag.mensaje = mensaje;
                }
                else
                {
                    if (info_det.IdProducto != 0)
                    {
                        in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                        if (info_producto != null)
                        {
                            info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                        }
                    }
                }
            }                    

            if (ModelState.IsValid)
            {
                in_ConsignacionDet_List.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            
            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            in_ConsignacionDet_List.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = in_ConsignacionDet_List.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_ConsignacionDet", model);
        }

        #endregion
    }

    //siempre incluir clase para detalle
    public class in_ConsignacionDet_List
    {
        string Variable = "in_ConsignacionDet_Info";
        public List<in_ConsignacionDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<in_ConsignacionDet_Info> list = new List<in_ConsignacionDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<in_ConsignacionDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<in_ConsignacionDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(in_ConsignacionDet_Info info_det, decimal IdTransaccionSession)
        {
            List<in_ConsignacionDet_Info> list = get_list(IdTransaccionSession);
            info_det.IdConsignacion = info_det.IdConsignacion;
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            info_det.Cantidad = info_det.Cantidad;
            info_det.Costo = info_det.Costo;
            info_det.Observacion = info_det.Observacion;

            list.Add(info_det);
        }

        public void UpdateRow(in_ConsignacionDet_Info info_det, decimal IdTransaccionSession)
        {
            in_ConsignacionDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdUnidadMedida = info_det.IdUnidadMedida;
            edited_info.Cantidad = info_det.Cantidad;
            edited_info.Costo = info_det.Costo;
            edited_info.Observacion = info_det.Observacion;
            edited_info.pr_descripcion = info_det.pr_descripcion;
        }

        public void DeleteRow(int Secuencial, decimal IdTransaccionSession)
        {
            List<in_ConsignacionDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencial).First());
        }
    }
}