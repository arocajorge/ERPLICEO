using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_orden_giro_det_ing_x_oc_Data
    {
        public List<cp_orden_giro_det_ing_x_oc_Info> get_list_x_ingresar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        {
            try
            {
                List<cp_orden_giro_det_ing_x_oc_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_det_ing_x_oc_x_cruzar
                             where q.inv_IdEmpresa == IdEmpresa
                             && q.inv_IdSucursal == IdSucursal
                             && q.IdProveedor == IdProveedor
                             select new cp_orden_giro_det_ing_x_oc_Info
                             {
                                 IdEmpresa = q.inv_IdEmpresa,
                                 inv_IdSucursal = q.inv_IdSucursal,
                                 inv_IdMovi_inven_tipo = q.inv_IdMovi_inven_tipo,
                                 inv_Secuencia = q.inv_Secuencia,
                                 inv_IdNumMovi = q.inv_IdNumMovi,
                                 oc_IdSucursal = q.oc_IdSucursal,
                                 oc_IdOrdenCompra = q.oc_IdOrdenCompra,
                                 oc_Secuencia = q.oc_Secuencia,
                                 pr_descripcion = q.pr_descripcion,
                                 IdCtaCble = q.IdCtaCtble_Inve,
                                 dm_cantidad = q.dm_cantidad_sinConversion,
                                 do_precioCompra = q.do_precioCompra,
                                 do_porc_des = q.do_porc_des,
                                 do_descuento = q.do_descuento,
                                 do_precioFinal = q.do_precioFinal,
                                 do_subtotal = q.do_subtotal,
                                 do_iva = q.do_iva,
                                 do_total = q.do_total,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 Por_Iva = q.Por_Iva,
                                 IdCod_Impuesto = q.IdCod_Impuesto,
                                 NomUnidadMedida = q.NomUnidadMedida,
                                 IdProveedor = q.IdProveedor,
                                 IdProducto = q.IdProducto,
                                 pc_Cuenta = q.pc_Cuenta

                             }).ToList();

                    Lista.ForEach(q => q.IdGenerado = (q.IdEmpresa.ToString("000") + q.inv_IdSucursal.ToString("000") + q.inv_IdMovi_inven_tipo.ToString("000000") + q.inv_IdNumMovi.ToString("000000") + q.inv_Secuencia.ToString("000000") ));
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_orden_giro_det_ing_x_oc_Info> get_list(int IdEmpresa, decimal IdCbteCble_Ogiro, decimal IdTipoCbte_Ogiro)
        {
            try
            {
                List<cp_orden_giro_det_ing_x_oc_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_det_ing_x_oc
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro
                             && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                             select new cp_orden_giro_det_ing_x_oc_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 inv_IdSucursal = q.inv_IdSucursal,
                                 inv_IdMovi_inven_tipo = q.inv_IdMovi_inven_tipo,
                                 inv_Secuencia = q.inv_Secuencia,
                                 inv_IdNumMovi = q.inv_IdNumMovi,
                                 oc_IdSucursal = q.oc_IdSucursal,
                                 oc_IdOrdenCompra = q.oc_IdOrdenCompra,
                                 oc_Secuencia = q.oc_Secuencia,
                                 Secuencia = q.Secuencia,
                                 pr_descripcion = q.pr_descripcion,
                                 IdCtaCble = q.IdCtaCble,
                                 dm_cantidad = q.dm_cantidad,
                                 do_porc_des = q.do_porc_des,
                                 do_descuento = q.do_descuento,
                                 pc_Cuenta = q.pc_Cuenta,
                                 do_precioFinal = q.do_precioFinal,
                                 do_subtotal = q.do_subtotal,
                                 do_iva = q.do_iva,
                                 do_total = q.do_total,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 Por_Iva = q.Por_Iva,
                                 IdCod_Impuesto = q.IdCod_Impuesto,
                                 NomUnidadMedida = q.NomUnidadMedida,
                                 IdProducto = q.IdProducto,
                                 do_precioCompra = q.do_precioCompra

                             }).ToList();                    
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
