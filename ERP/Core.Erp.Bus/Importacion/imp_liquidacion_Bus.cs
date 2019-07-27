using Core.Erp.Bus.Contabilidad;
using Core.Erp.Bus.Inventario;
using Core.Erp.Data.Importacion;
using Core.Erp.Info.Importacion;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Importacion
{
   public class imp_liquidacion_Bus
    {
        #region variables
        imp_liquidacion_Data odata = new imp_liquidacion_Data();
        imp_ordencompra_ext_Data odata_oc = new imp_ordencompra_ext_Data();
        imp_ordencompra_ext_det_Data odta_det_oc = new imp_ordencompra_ext_det_Data();
        in_Ing_Egr_Inven_Bus bus_ingreso = new in_Ing_Egr_Inven_Bus();
        ct_cbtecble_Bus bus_contabilidad = new ct_cbtecble_Bus();
        imp_parametro_Bus param_bus = new imp_parametro_Bus();
        imp_parametro_Info param = new imp_parametro_Info();
        imp_ordencompra_ext_Bus bus_orden_compra = new imp_ordencompra_ext_Bus();
        List<imp_ordencompra_ext_det_Info> lst_detalle = new List<imp_ordencompra_ext_det_Info>();
        imp_ordencompra_ext_det_Data odata_det = new imp_ordencompra_ext_det_Data();

        List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info> lst_gastos_asignados = new List<imp_orden_compra_ext_ct_cbteble_det_gastos_Info>();
        imp_orden_compra_ext_ct_cbteble_det_gastos_Data data_gastos = new imp_orden_compra_ext_ct_cbteble_det_gastos_Data();
        
        #endregion
        public List<imp_liquidacion_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_inicio, Fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_liquidacion_Info get_info(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdOrdenCompra_ext);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_liquidacion_Info get_liquidar_oc(int IdEmpresa, decimal IdOrdenCompra_ext)
        {
            try
            {
                imp_liquidacion_Info info = new imp_liquidacion_Info();
                var orden_compra= bus_orden_compra.get_info_recepcion_merca(IdEmpresa, IdOrdenCompra_ext);
                info.IdEmpresa = orden_compra.IdEmpresa;
                info.IdOrdenCompra_ext = orden_compra.IdOrdenCompra_ext;
                info.li_observacion = orden_compra.oe_observacion;
                info.li_fecha = DateTime.Now;
                info.pe_cedulaRuc = orden_compra.pe_cedulaRuc;
                info.pe_nombreCompleto = orden_compra.pe_nombreCompleto;
                info.li_fecha = DateTime.Now;
                info.oe_fecha = DateTime.Now;
                info.li_observacion = orden_compra.oe_observacion;
                info.IdCtaCble_importacion = orden_compra.IdCtaCble_importacion;
                


                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
   
        public bool modificarDB(imp_liquidacion_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string validar_liquidacion(imp_liquidacion_Info model)
        {
            try
            {
                string mensaje = "";
                if (model.lst_comprobante == null)

                    mensaje = "No existe diario contable";
                else
                {
                    if (model.lst_comprobante.Count() == 0)
                    {
                        mensaje = "No existe diario contable";

                    }
                    else
                    {
                        foreach (var item in model.lst_comprobante)
                        {
                            if (item.IdCtaCble == "" | item.IdCtaCble == null)
                                mensaje = "Faltan cuentas contables";
                        }
                    }

                    double sum = model.lst_comprobante.Sum(v => v.dc_Valor);
                    if (sum > 1 | sum < 0)
                        mensaje = "El diario esta descuadrado";

                }

                if (model.lst_detalle == null)
                    mensaje = "No existe detalle";
                else
                {
                    if (model.lst_comprobante.Count() == 0)
                    {
                        mensaje = "No existe detalle";
                    }
                    else
                    {
                        foreach (var item in model.lst_detalle)
                        {
                            if (item.od_costo_total == 0)
                                mensaje = "Faltan costo en uno de los registros";
                        }
                    }



                }
                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region funciones de liquidacion de oc
        public bool guardarDB(imp_liquidacion_Info info)
        {
            try
            {
                odata = new imp_liquidacion_Data();
                info.info_comrobante = new Info.Contabilidad.ct_cbtecble_Info();
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Fecha = (DateTime)info.oe_fecha;
                info.info_comrobante.IdSucursal = Convert.ToInt32(info.IdSucursal_inv);
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Observacion = info.li_observacion;
                info.info_comrobante.lst_ct_cbtecble_det = info.lst_comprobante;

                var info_inventario = get_ingreso(info);
                info_inventario.cm_fecha = Convert.ToDateTime(info.li_fecha);
                info.info_comrobante.IdTipoCbte = param.IdTipoCbte_liquidacion;
                bus_ingreso.guardarDB(info_inventario, "+");

                bus_contabilidad.guardarDB(info.info_comrobante);
                info.IdEmpresa_ct = Convert.ToInt32(info.info_comrobante.IdEmpresa);
                info.IdTipoCbte_ct = Convert.ToInt32(info.info_comrobante.IdTipoCbte);
                info.IdCbteCble_ct = Convert.ToInt32(info.info_comrobante.IdCbteCble);

                info.IdEmpresa_inv = Convert.ToInt32(info_inventario.IdEmpresa);
                info.IdSucursal_inv = Convert.ToInt32(info_inventario.IdSucursal);
                info.IdMovi_inven_tipo_inv = Convert.ToInt32(info_inventario.IdMovi_inven_tipo);
                info.IdNumMovi_inv = Convert.ToInt32(info_inventario.IdNumMovi);


                info.IdEmpresa = info.IdEmpresa;
                info.IdOrdenCompra_ext = info.IdOrdenCompra_ext;
                odata. guardarDB(info);
                imp_ordencompra_ext_Info info_oc = new imp_ordencompra_ext_Info();
                info_oc.IdEmpresa = info.IdEmpresa;
                info_oc.IdOrdenCompra_ext = info.IdOrdenCompra_ext;
                info_oc.lst_detalle = info.lst_detalle;
                bus_orden_compra.guardarLiquidacionDB(info_oc);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Anular(imp_liquidacion_Info info)
        {
            try
            {
                odata = new imp_liquidacion_Data();
                info.info_comrobante = new Info.Contabilidad.ct_cbtecble_Info();
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Fecha = (DateTime)info.oe_fecha;
                info.info_comrobante.IdSucursal = (info.IdSucursal_inv) == null ? 0 : Convert.ToInt32(info.IdSucursal_inv);
                info.info_comrobante.cb_Estado = "A";
                info.info_comrobante.IdPeriodo = Convert.ToInt32(info.info_comrobante.cb_Fecha.Year.ToString() + info.info_comrobante.cb_Fecha.Month.ToString().PadLeft(2, '0'));
                info.info_comrobante.IdEmpresa = info.IdEmpresa;
                info.info_comrobante.cb_Observacion = info.li_observacion;
                info.info_comrobante.lst_ct_cbtecble_det = info.lst_comprobante;

                var info_inventario = get_ingreso(info);
                info_inventario.cm_fecha = Convert.ToDateTime(info.li_fecha);
                info_inventario.IdEmpresa = info.IdEmpresa;
                info_inventario.IdSucursal = (info.IdSucursal_inv)==null?0:Convert.ToInt32(info.IdSucursal_inv);
                info_inventario.IdBodega = info.IdBodega_inv;
                info_inventario.IdNumMovi = (info.IdNumMovi_inv) == null ? 0 : Convert.ToInt32(info.IdNumMovi_inv);
                bus_ingreso.anularDB(info_inventario);

                info.info_comrobante.IdEmpresa = (info.IdEmpresa_ct) == null ? 0 : Convert.ToInt32(info.IdEmpresa_ct);
                info.info_comrobante.IdTipoCbte = (info.IdTipoCbte_ct) == null ? 0 : Convert.ToInt32(info.IdTipoCbte_ct);
                info.info_comrobante.IdCbteCble = (info.IdCbteCble_ct) == null ? 0 : Convert.ToInt32(info.IdCbteCble_ct);

                bus_contabilidad.anularDB(info.info_comrobante);
                odata.AnularDB(info);
              
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<imp_ordencompra_ext_det_Info> calcular_costos(int IdEmpresa, decimal IdOrdenCompraExter)
        {
            try
            {
                double costo_incurridos = 0;
                double valor_compra = 0;
                lst_detalle = odata_det.get_list(IdEmpresa, IdOrdenCompraExter);
                lst_gastos_asignados = data_gastos.get_list_gastos_asignados(IdEmpresa, IdOrdenCompraExter);
                if (lst_gastos_asignados != null)
                    costo_incurridos = lst_gastos_asignados.Sum(v => v.dc_Valor);
                if (lst_gastos_asignados != null)
                    valor_compra = Convert.ToDouble(lst_detalle.Sum(v => v.od_total_fob));
                foreach (var item in lst_detalle)
                {
                    item.od_factor_costo = (costo_incurridos + valor_compra) / valor_compra;
                    item.od_costo_bodega = item.od_costo * item.od_factor_costo;
                    item.od_costo_total = item.od_costo_bodega * item.od_cantidad_recepcion;
                }
                return lst_detalle;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private in_Ing_Egr_Inven_Info get_ingreso(imp_liquidacion_Info info)
        {
            try
            {
                param = param_bus.get_info(info.IdEmpresa);
                in_Ing_Egr_Inven_Info ingreso = new in_Ing_Egr_Inven_Info();
                ingreso.IdEmpresa = info.IdEmpresa;
                ingreso.IdNumMovi = 0;
                ingreso.CodMoviInven = "0";
                ingreso.cm_fecha = info.oe_fecha;
                ingreso.IdUsuario = info.IdUsuario_creacion;
                ingreso.nom_pc = "";
                ingreso.ip = "";
                ingreso.Fecha_Transac = DateTime.Now;
                ingreso.signo = "+";
                ingreso.IdSucursal = param.IdSucursal;
                ingreso.IdBodega = param.IdBodega;
                ingreso.cm_observacion = "Ingreso por importacion. " + info.li_observacion;
                ingreso.IdMovi_inven_tipo = param.IdMovi_inven_tipo_ing;
                ingreso.IdMotivo_Inv = param.IdMotivo_Inv_ing;
                foreach (var item in info.lst_detalle)
                {
                    in_Ing_Egr_Inven_det_Info info_det = new in_Ing_Egr_Inven_det_Info();
                    info_det.IdEmpresa = item.IdEmpresa;
                    info_det.IdSucursal = param.IdSucursal;
                    info_det.IdNumMovi = 0;
                    info_det.Secuencia = item.Secuencia;
                    info_det.IdBodega = param.IdBodega;
                    info_det.IdProducto = item.IdProducto;
                    info_det.dm_cantidad = item.od_cantidad_recepcion;
                    info_det.dm_observacion = "Ingreso por orden de compra del exterior";
                    info_det.mv_costo = Math.Round(item.od_costo_total / item.od_cantidad_recepcion, 2);
                    info_det.mv_costo_sinConversion = Math.Round(item.od_costo_total / item.od_cantidad_recepcion, 2);
                    info_det.dm_cantidad_sinConversion = item.od_cantidad_recepcion;
                    info_det.dm_cantidad = item.od_cantidad_recepcion;
                    info_det.IdUnidadMedida = item.IdUnidadMedida;
                    info_det.IdUnidadMedida_sinConversion = item.IdUnidadMedida;
                    ingreso.lst_in_Ing_Egr_Inven_det.Add(info_det);
                }
                return ingreso;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
