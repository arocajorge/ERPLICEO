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
    public class LiquidacionOrdenCompraController : Controller
    {
        #region variables
        imp_liquidacion_Bus bus_liquidacion = new imp_liquidacion_Bus();
        cp_proveedor_Bus bus_proveedor = new cp_proveedor_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus(); 
        imp_ordencompra_ext_det_Info_lst info_detalle = new imp_ordencompra_ext_det_Info_lst();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        in_UnidadMedida_Bus bus_unidad_medida = new in_UnidadMedida_Bus();
        imp_ordencompra_ext_det_Bus bus_detalle = new imp_ordencompra_ext_det_Bus();
        imp_catalogo_Bus bus_catalogo = new imp_catalogo_Bus();
        tb_moneda_Bus bus_moneda = new tb_moneda_Bus();
        imp_liquidacion_Info_diario_contable Lis_imp_liquidacion_Info_diario_contable = new imp_liquidacion_Info_diario_contable();
        imp_orden_compra_ext_ct_cbteble_det_gastos_Bus bus_gastos = new imp_orden_compra_ext_ct_cbteble_det_gastos_Bus();
        imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst info_gastos_lst = new imp_orden_compra_ext_ct_cbteble_det_gastos_Info_lst();
        imp_parametro_Bus bus_param = new imp_parametro_Bus();
        imp_parametro_Info param = new imp_parametro_Info();
        imp_gasto_Bus bus_gastos_tipo = new imp_gasto_Bus();

        ct_cbtecble_det_Bus bus_comprobante_det = new ct_cbtecble_det_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult CmbProveedor_exterior()
        {
            cp_proveedor_Info model = new cp_proveedor_Info();
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


        public ActionResult CmbCuenta_contable_liquidacion()
        {
            imp_ordencompra_ext_Info model = new imp_ordencompra_ext_Info();

            return PartialView("_CmbCuenta_contable_liquidacion", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda_cta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), true);
        }
        public ct_plancta_Info get_info_bajo_demanda_cta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
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

        public ActionResult GridViewPartial_liquidacion_oc(DateTime? Fecha_ini, DateTime? Fecha_fin)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            var model = bus_liquidacion.get_list(IdEmpresa, ViewBag.Fecha_ini, ViewBag.Fecha_fin);
            return PartialView("_GridViewPartial_liquidacion_oc", model);
        }
        public ActionResult GridViewPartial_liquidacion_oc_det()
        {
            List<imp_ordencompra_ext_det_Info> model = new List<imp_ordencompra_ext_det_Info>();
            model = info_detalle.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            if (model == null)
                model = new List<imp_ordencompra_ext_det_Info>();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_oc_det", model);
        }
        public ActionResult GridViewPartial_liqidacion_dc()
        {
            List<ct_cbtecble_det_Info> model = new List<ct_cbtecble_det_Info>();
            model = Lis_imp_liquidacion_Info_diario_contable.get_list();
            if (model == null)
                model = new List<ct_cbtecble_det_Info>();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liqidacion_dc", model);
        }
        public ActionResult GridViewPartial_liquidacion_gastos( decimal IdTransaccionSession=0)
        {
            List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> model = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
            model = info_gastos_lst.get_list(IdTransaccionSession);
            if (model == null)
                model = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liquidacion_gastos", model);
        }

        

        #endregion

        #region acciones
        public ActionResult Nuevo( int IdEmpresa = 0 , decimal IdOrdenCompra_ext=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            imp_liquidacion_Info model = bus_liquidacion.get_liquidar_oc(IdEmpresa, IdOrdenCompra_ext);
            if (model != null)
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            var lst_detalle = bus_detalle.get_list(IdEmpresa, IdOrdenCompra_ext);
            var lst_gastos = bus_gastos.get_list_gastos_asignados(IdEmpresa, IdOrdenCompra_ext);
            model.IdEmpresa = IdEmpresa;
            info_gastos_lst.set_list(lst_gastos, model.IdTransaccionSession);
           info_detalle.set_list(lst_detalle, model.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(imp_liquidacion_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.lst_detalle = info_detalle.get_list(model.IdTransaccionSession);
            model.lst_comprobante = Lis_imp_liquidacion_Info_diario_contable.get_list();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            string mensaje = "";

            mensaje = bus_liquidacion.validar_liquidacion(model);
            model.IdUsuario_creacion = SessionFixed.IdUsuario.ToString();
            if (mensaje != "")
            {
                cargar_combos(model.IdEmpresa);
                ViewBag.mensaje = mensaje;
                return View(model);
            }
            if (!bus_liquidacion.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Anular(int IdEmpresa = 0 , decimal IdOrdenCompra_ext = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            imp_liquidacion_Info model = bus_liquidacion.get_info(IdEmpresa, IdOrdenCompra_ext);
            if (model != null)
                model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            var lst_detalle = bus_detalle.get_list(IdEmpresa, IdOrdenCompra_ext);
            var lst_gastos = bus_gastos.get_list_gastos_asignados(IdEmpresa, IdOrdenCompra_ext);

            info_gastos_lst.set_list(lst_gastos, model.IdTransaccionSession);
            info_detalle.set_list(lst_detalle, model.IdTransaccionSession);

            var lst_diario = bus_comprobante_det.get_list(model.IdEmpresa, model.IdTipoCbte_ct == null ? 0: Convert.ToInt32( model.IdTipoCbte_ct),model.IdCbteCble_ct==null?0: Convert.ToDecimal( model.IdCbteCble_ct));
            Lis_imp_liquidacion_Info_diario_contable.set_list(lst_diario);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos(IdEmpresa);
            cargar_combos_detalle();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(imp_liquidacion_Info model)
        {
            model.lst_detalle = info_detalle.get_list(model.IdTransaccionSession);
            model.lst_comprobante = Lis_imp_liquidacion_Info_diario_contable.get_list();
            model.IdUsuario_anulacion = SessionFixed.IdUsuario.ToString();
            if (!bus_liquidacion.Anular(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }


        #endregion

        #region json
        public JsonResult calcular_costo(decimal IdTransaccionSession=0, decimal IdOrdenCompra_ext = 0, string IdCtaCble_importacion="")
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            param = bus_param.get_info(IdEmpresa);
           info_detalle.set_list( bus_liquidacion.calcular_costos(IdEmpresa, IdOrdenCompra_ext), IdTransaccionSession);
           Lis_imp_liquidacion_Info_diario_contable.delete_detail_New_details(info_detalle.get_list(IdTransaccionSession), info_gastos_lst.get_list(IdTransaccionSession), param, IdCtaCble_importacion);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    
        #endregion

        private void cargar_combos(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            var lst_bodega = bus_bodega.get_list(IdEmpresa, false);
            ViewBag.lst_bodega = lst_bodega;

            var lst_catalogos = bus_catalogo.get_list(1);
            ViewBag.lst_catalogos = lst_catalogos;

         

        }
        private void cargar_combos_detalle()
        {
            var lst_undades = bus_unidad_medida.get_list(false);
            ViewBag.lst_undades = lst_undades;

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            var lst_cuentas = bus_cuenta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_cuentas;

            var lst_gastos = bus_gastos_tipo.get_list();
            ViewBag.lst_gastos = lst_gastos;
        }

        #region Diario contable

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_imp_liquidacion_Info_diario_contable.AddRow(info_det);
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_imp_liquidacion_Info_diario_contable.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liqidacion_dc", model.lst_ct_cbtecble_det);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ct_cbtecble_det_Info info_det)
        {
            if (ModelState.IsValid)
                Lis_imp_liquidacion_Info_diario_contable.UpdateRow(info_det);
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_imp_liquidacion_Info_diario_contable.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liqidacion_dc", model.lst_ct_cbtecble_det);
        }

        public ActionResult EditingDelete(int secuencia)
        {
            Lis_imp_liquidacion_Info_diario_contable.DeleteRow(secuencia);
            ct_cbtecble_Info model = new ct_cbtecble_Info();
            model.lst_ct_cbtecble_det = Lis_imp_liquidacion_Info_diario_contable.get_list();
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_liqidacion_dc", model.lst_ct_cbtecble_det);
        }

        #endregion

    }
    
    public class imp_liquidacion_Info_diario_contable
    {
        public List<ct_cbtecble_det_Info> get_list()
        {
            if (HttpContext.Current.Session["ct_cbtecble_det_Info"] == null)
            {
                List<ct_cbtecble_det_Info> list = new List<ct_cbtecble_det_Info>();

                HttpContext.Current.Session["ct_cbtecble_det_Info"] = list;
            }
            return (List<ct_cbtecble_det_Info>)HttpContext.Current.Session["ct_cbtecble_det_Info"];
        }

        public void set_list(List<ct_cbtecble_det_Info> list)
        {
            HttpContext.Current.Session["ct_cbtecble_det_Info"] = list;
        }
        
        public void AddRow(ct_cbtecble_det_Info info_det)
        {
            List<ct_cbtecble_det_Info> list = get_list();
            info_det.secuencia = list.Count == 0 ? 1 : list.Max(q => q.secuencia) + 1;
            info_det.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            list.Add(info_det);
        }

        public void UpdateRow(ct_cbtecble_det_Info info_det)
        {
            ct_cbtecble_det_Info edited_info = get_list().Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.IdCtaCble = info_det.IdCtaCble;
            edited_info.dc_para_conciliar = info_det.dc_para_conciliar;
            edited_info.dc_Valor = info_det.dc_Valor_debe > 0 ? info_det.dc_Valor_debe : info_det.dc_Valor_haber * -1;
            edited_info.dc_Valor_debe = info_det.dc_Valor_debe;
            edited_info.dc_Valor_haber = info_det.dc_Valor_haber;
        }

        public void DeleteRow(int secuencia)
        {
            List<ct_cbtecble_det_Info> list = get_list();
            list.Remove(list.Where(m => m.secuencia == secuencia).First());
        }

        public void delete_detail_New_details(List<imp_ordencompra_ext_det_Info> detalle, List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> detalle_costo, imp_parametro_Info param, string CuentacontableImp)
        {
            try
            {

                int secuencia = 1;
                set_list(new List<ct_cbtecble_det_Info>());
                double costo_total = Convert.ToDouble(detalle.Sum(v => v.od_costo_total));
                ct_cbtecble_det_Info info_total = new ct_cbtecble_det_Info();
                info_total.IdEmpresa = param.IdEmpresa;
                info_total.IdTipoCbte = param.IdTipoCbte_liquidacion;
                info_total.IdCtaCble = CuentacontableImp;
                info_total.dc_Valor = costo_total *-1;
                info_total.dc_Valor_debe = costo_total ;
                info_total.dc_Observacion = "Ingreso a inventario por importación";
                info_total.secuencia = secuencia;
                AddRow(info_total);

                
                ct_cbtecble_det_Info info_merca = new ct_cbtecble_det_Info();
                info_merca.IdEmpresa = param.IdEmpresa;
                info_merca.IdTipoCbte = param.IdTipoCbte_liquidacion;
                info_merca.IdCtaCble = param.IdCtaCble_invntario;
                info_merca.dc_Valor = costo_total;
                info_merca.dc_Valor_haber = costo_total;
                info_merca.dc_Observacion = "Ingreso a inventario por importación";
                info_merca.secuencia = secuencia + 1;
                AddRow(info_merca);

              

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}