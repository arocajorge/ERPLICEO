using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Erp.Info.Importacion;
using Core.Erp.Bus.Importacion;
using Core.Erp.Info.Helps;
using Core.Erp.Web.Helps;
using Core.Erp.Info.CuentasPorPagar;
using DevExpress.Web;
using Core.Erp.Bus.CuentasPorPagar;
using Core.Erp.Info.Contabilidad;
using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.General;
using Core.Erp.Info.Inventario;
using Core.Erp.Bus.Inventario;
using DevExpress.Web.Mvc;

namespace Core.Erp.Web.Areas.Importacion.Controllers
{
    [SessionTimeout]
    public class OrdenCompraExteriorController : Controller
    {
        #region variables
        imp_ordencompra_ext_Bus bus_orden = new imp_ordencompra_ext_Bus();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        tb_pais_Bus bus_paises = new tb_pais_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        imp_ordencompra_ext_det_Info_lst Lis_imp_ordencompra_ext_det_Info_lst = new imp_ordencompra_ext_det_Info_lst();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        in_UnidadMedida_Bus bus_unidad_medida = new in_UnidadMedida_Bus();
        imp_ordencompra_ext_det_Bus bus_detalle = new imp_ordencompra_ext_det_Bus();
        imp_catalogo_Bus bus_catalogo = new imp_catalogo_Bus();
        tb_moneda_Bus bus_moneda = new tb_moneda_Bus();
        imp_parametro_Bus param_bus = new imp_parametro_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProveedor_exterior()
        {
            decimal model = new decimal();
            return PartialView("_CmbProveedor_exterior", model);
        }
        public List<cp_proveedor_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_proveedor.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        public cp_proveedor_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_proveedor.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }


        public ActionResult CmbCuenta_cta_contable()
        {
            imp_ordencompra_ext_Info model = new imp_ordencompra_ext_Info();
           
            return PartialView("_CmbCuenta_contable", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_cta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            string cta_padre = "";
            var param = param_bus.get_info(Convert.ToInt32(SessionFixed.IdEmpresa));
            if (param != null)
             cta_padre = param.IdCtaCble;

            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false, cta_padre);
        }
        public ct_plancta_Info get_info_bajo_demanda_cta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }


        public ActionResult CmbProducto_importacion()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_importacion", model);
        }
        public List<in_Producto_Info> get_list_bajo_demanda_productos(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.COM, 0,0);
        }
        public in_Producto_Info get_info_bajo_demanda_productos(ListEditItemRequestedByValueEventArgs args)
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
        public ActionResult GridViewPartial_orden_compra_ext(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            if (IdSucursal == 0)
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            ViewBag.IdSucursal = IdSucursal;

            var model = bus_orden.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_orden_compra_ext", model);
        }
        public ActionResult GridViewPartial_orden_compra_ext_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lis_imp_ordencompra_ext_det_Info_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_orden_compra_ext_det", model);
        }
        
        public ActionResult GridViewPartial_orden_compra_con_saldo()
        {
            List<imp_ordencompra_ext_Info> model = new List<imp_ordencompra_ext_Info>();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model = bus_orden.get_list(IdEmpresa);
            return PartialView("_GridViewPartial_orden_compra_con_saldo", model);
        }
        public ActionResult GridViewPartial_orden_compra_por_liquidar()
        {
            List<imp_ordencompra_ext_Info> model = new List<imp_ordencompra_ext_Info>();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model = bus_orden.get_list_oc_por_liquidar(IdEmpresa);
            return PartialView("_GridViewPartial_orden_compra_por_liquidar", model);
        }
        public ActionResult Ordencompra_por_liquidar()
        {
           
            return View("Ordencompra_por_liquidar");
        }
        
         public ActionResult OrdencompraConsaldos()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            return View(model);
        }

        #endregion

        #region acciones
        public ActionResult Nuevo(int IdEmpresa = 0 )
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            imp_parametro_Bus bus_para = new imp_parametro_Bus();
            var param = bus_para.get_info(IdEmpresa);
            if (param == null)
                param = new imp_parametro_Info();
            imp_ordencompra_ext_Info model = new imp_ordencompra_ext_Info
            {
                IdEmpresa = IdEmpresa,
                fecha_creacion = DateTime.Now,
                IdCtaCble_importacion = param.IdCtaCble,
                oe_fecha = DateTime.Now,
                oe_fecha_llegada = DateTime.Now,
                oe_fecha_embarque = DateTime.Now,
                oe_fecha_desaduanizacion = DateTime.Now,
                IdPais_origen = "1",
                IdCiudad_destino = "09",
                IdPais_embarque = "1",
                IdCatalogo_forma_pago=1,
                IdTransaccionSession= Convert.ToDecimal(SessionFixed.IdTransaccionSession)



            };
            cargar_combos_detalle();
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(imp_ordencompra_ext_Info model)
        {
            model.lst_detalle = Lis_imp_ordencompra_ext_det_Info_lst.get_list(model.IdTransaccionSession);
            string mensaje = bus_orden.validar(model);
            if(mensaje!="")
            {
                cargar_combos();
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (!bus_orden.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            Lis_imp_ordencompra_ext_det_Info_lst.set_list(new List<imp_ordencompra_ext_det_Info>(),model.IdTransaccionSession);
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0 , decimal IdOrdenCompra_ext = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            imp_ordencompra_ext_Info model = bus_orden.get_info(IdEmpresa, IdOrdenCompra_ext);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            var lst_detalle = bus_detalle.get_list(IdEmpresa, IdOrdenCompra_ext);
            Lis_imp_ordencompra_ext_det_Info_lst.set_list(lst_detalle, model.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(imp_ordencompra_ext_Info model)
        {
            model.lst_detalle = Lis_imp_ordencompra_ext_det_Info_lst.get_list(model.IdTransaccionSession);
            string mensaje = bus_orden.validar(model);
            if (mensaje != "")
            {
                cargar_combos();
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (!bus_orden.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            Lis_imp_ordencompra_ext_det_Info_lst.set_list(new List<imp_ordencompra_ext_det_Info>(), model.IdTransaccionSession);
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0 , decimal IdOrdenCompra_ext = 0 )
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            imp_ordencompra_ext_Info model = bus_orden.get_info(IdEmpresa, IdOrdenCompra_ext);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            var lst_detalle = bus_detalle.get_list(IdEmpresa, IdOrdenCompra_ext);
            Lis_imp_ordencompra_ext_det_Info_lst.set_list(lst_detalle, model.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            cargar_combos_detalle();
            return View(model); ;
        }

        [HttpPost]
        public ActionResult Anular(imp_ordencompra_ext_Info model)
        {
            if (!bus_orden.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            Lis_imp_ordencompra_ext_det_Info_lst.set_list(new List<imp_ordencompra_ext_det_Info>(), model.IdTransaccionSession);
            return RedirectToAction("Index");
        }
        #endregion

        private void cargar_combos()
        {
            var lst_paises = bus_paises.get_list(false);
            ViewBag.lst_paises = lst_paises;

            var lst_ciudades = bus_ciudad.get_list("09", false);
            ViewBag.lst_ciudades = lst_ciudades;

            var lst_forma_pago = bus_catalogo.get_list(2);
            ViewBag.lst_forma_pago = lst_forma_pago;

            var lst_catalogos = bus_catalogo.get_list(1);
            ViewBag.lst_catalogos = lst_catalogos;

            var lst_monedas = bus_moneda.get_list();
            ViewBag.lst_monedas = lst_monedas;

        }
        private void cargar_combos_detalle()
        {
            var lst_undades = bus_unidad_medida.get_list(false);
            ViewBag.lst_undades = lst_undades;
        }

        #region funciones del detalle
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] imp_ordencompra_ext_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        if (info_det.IdProducto != 0 & info_det.od_cantidad > 0 && info_det.od_costo > 0)
                        {
                            info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                            info_det.IdUnidadMedida = info_producto.IdUnidadMedida;
                            info_det.od_total_fob = info_det.od_cantidad * info_det.od_costo;
                            Lis_imp_ordencompra_ext_det_Info_lst.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                        }

                    }
                }

            var model = Lis_imp_ordencompra_ext_det_Info_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_orden_compra_ext_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] imp_ordencompra_ext_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (info_det != null)
                if (info_det.IdProducto != 0)
                {
                    in_Producto_Info info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        if (info_det.IdProducto != 0 & info_det.od_cantidad > 0 && info_det.od_costo > 0)
                        {
                            info_det.pr_descripcion = info_producto.pr_descripcion_combo;
                            info_det.IdUnidadMedida = info_producto.IdUnidadMedida;
                            info_det.od_total_fob = info_det.od_cantidad * info_det.od_costo;
                            Lis_imp_ordencompra_ext_det_Info_lst.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
                        }
                    }
                }

            var model = Lis_imp_ordencompra_ext_det_Info_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_orden_compra_ext_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            Lis_imp_ordencompra_ext_det_Info_lst.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lis_imp_ordencompra_ext_det_Info_lst.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_orden_compra_ext_det", model);
        }
        #endregion
    }
        

    public class imp_ordencompra_ext_det_Info_lst
    {
        string variable = "imp_ordencompra_ext_det_Info";
        public List<imp_ordencompra_ext_det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] == null)
            {
                List<imp_ordencompra_ext_det_Info> list = new List<imp_ordencompra_ext_det_Info>();

                HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<imp_ordencompra_ext_det_Info>)HttpContext.Current.Session[variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<imp_ordencompra_ext_det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(imp_ordencompra_ext_det_Info info_det, decimal IdTransaccionSession)
        {
            List<imp_ordencompra_ext_det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdProducto = info_det.IdProducto;
            info_det.IdUnidadMedida = info_det.IdUnidadMedida;
            info_det.od_costo = info_det.od_costo;
            info_det.od_cantidad = info_det.od_cantidad;

            list.Add(info_det);
        }

        public void UpdateRow(imp_ordencompra_ext_det_Info info_det, decimal IdTransaccionSession)
        {
            imp_ordencompra_ext_det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdProducto = info_det.IdProducto;
            edited_info.IdUnidadMedida = info_det.IdUnidadMedida;
            edited_info.od_costo = info_det.od_costo;
            edited_info.od_cantidad = info_det.od_cantidad;
            edited_info.pr_descripcion = info_det.pr_descripcion;

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<imp_ordencompra_ext_det_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}