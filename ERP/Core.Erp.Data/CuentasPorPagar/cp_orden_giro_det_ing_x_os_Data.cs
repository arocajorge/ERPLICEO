using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_orden_giro_det_ing_x_os_Data
    {
        public List<cp_orden_giro_det_ing_x_os_Info> get_list_x_ingresar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        {
            try
            {
                List<cp_orden_giro_det_ing_x_os_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_det_ing_x_os_x_cruzar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdProveedor == IdProveedor
                             select new cp_orden_giro_det_ing_x_os_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 oc_IdSucursal = q.IdSucursal,
                                 oc_IdOrdenCompra = q.IdOrdenCompra,
                                 oc_Secuencia = q.Secuencia,                                 
                                 pr_descripcion = q.pr_descripcion,
                                 dm_cantidad = q.do_Cantidad,
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
                                 IdProveedor = q.IdProveedor,
                                 IdProducto = q.IdProducto,
                                 pe_nombreCompleto = q.pe_nombreCompleto

                             }).ToList();

                    Lista.ForEach(q => q.IdGeneradoOS = (q.IdEmpresa.ToString("000") + q.oc_IdSucursal.ToString("000") + q.oc_IdOrdenCompra.ToString("000000") + q.oc_Secuencia.ToString("000000") ));
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_orden_giro_det_ing_x_os_Info> get_list(int IdEmpresa, decimal IdCbteCble_Ogiro, decimal IdTipoCbte_Ogiro)
        {
            try
            {
                List<cp_orden_giro_det_ing_x_os_Info> Lista;
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_det_ing_x_os
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro
                             && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                             select new cp_orden_giro_det_ing_x_os_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 oc_IdSucursal = q.oc_IdSucursal,
                                 oc_IdOrdenCompra = q.oc_IdOrdenCompra,
                                 oc_Secuencia = q.oc_Secuencia,
                                 Secuencia = q.Secuencia,
                                 pr_descripcion = q.pr_descripcion,
                                 dm_cantidad = q.dm_cantidad,
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
