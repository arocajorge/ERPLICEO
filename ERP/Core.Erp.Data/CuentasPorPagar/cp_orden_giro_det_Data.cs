using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_orden_giro_det_Data
    {
        public List<cp_orden_giro_det_Info> get_list(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                List<cp_orden_giro_det_Info> Lista;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in Context.vwcp_orden_giro_det
                             where q.IdEmpresa == IdEmpresa
                              && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                              && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro
                             select new cp_orden_giro_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 Secuencia = q.Secuencia,
                                 IdProducto = q.IdProducto,
                                 Cantidad = q.Cantidad,
                                 CostoUni = q.CostoUni,
                                 PorDescuento = q.PorDescuento,
                                 DescuentoUni = q.DescuentoUni,
                                 CostoUniFinal = q.CostoUniFinal,
                                 Subtotal = q.Subtotal,
                                 IdCod_Impuesto_Iva = q.IdCod_Impuesto_Iva,
                                 PorIva = q.PorIva,
                                 ValorIva = q.ValorIva,
                                 Total = q.Total,
                                 IdCtaCbleInv = q.IdCtaCbleInv,
                                 IdUnidadMedida = q.IdUnidadMedida,
                                 pr_descripcion = q.pr_descripcion,
                                 IdEmpresa_oc = q.IdEmpresa_oc,
                                 IdSucursal_oc =  q.IdSucursal_oc,
                                 IdOrdenCompra = q.IdOrdenCompra,
                                 Secuencia_oc = q.Secuencia_oc
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public List<cp_orden_giro_det_Info> GetListPorIngresar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        //{
        //    try
        //    {
        //        List<cp_orden_giro_det_Info> Lista;

        //        using (Entities_compras db = new Entities_compras())
        //        {
        //            Lista = db.vwcom_ordencompra_local_x_ingresar.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdProveedor == IdProveedor).Select(q => new cp_orden_giro_det_Info
        //            {
        //                IdEmpresa = q.IdEmpresa,
        //                IdSucursal_oc = q.IdSucursal,
        //                IdOrdenCompra = q.IdOrdenCompra,
        //                Secuencia_oc = q.Secuencia,
        //                IdCod_Impuesto_Iva = q.IdCod_Impuesto,
        //                PorIva = q.Por_Iva,
        //                Cantidad = q.Saldo,
        //                CostoUni = q.do_precioCompra,
        //                PorDescuento = q.do_porc_des,
        //                CostoUniFinal = q.do_precioFinal,
        //                Subtotal = q.do_subtotal,
        //                ValorIva = q.do_iva,
        //                Total = q.do_total,
        //                DescuentoUni = q.do_descuento,
        //                IdUnidadMedida = q.IdUnidadMedida,
        //                pr_descripcion = q.pr_descripcion,
        //                IdEmpresa_oc = q.IdEmpresa,
        //                IdProducto = q.IdProducto,
        //                oc_fecha = q.oc_fecha,
        //                do_Cantidad = q.do_Cantidad,
        //                Saldo = q.Saldo,
        //                oc_observacion = q.oc_observacion,
        //                NomUnidadMedida = q.NomUnidadMedida,
        //                CantidadIngresada = q.CantidadIngresada,
        //                IdCtaCbleInv = q.IdCtaCtble_Inve
        //            }).ToList();
        //        }

        //        Lista.ForEach(q => q.SecuencialID = Convert.ToDecimal(q.IdOrdenCompra).ToString("0000000000") + Convert.ToInt32(q.Secuencia_oc).ToString("0000"));

        //        return Lista;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
