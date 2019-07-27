using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_013_Data
    {
        public List<FAC_013_Info> GetList(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                List<FAC_013_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.VWFAC_013.Where(
                        q => q.IdEmpresa == IdEmpresa
                        && q.IdSucursal == IdSucursal
                        && q.IdBodega == IdBodega
                        && q.IdCbteVta == IdCbteVta
                        ).Select(q => new FAC_013_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        ced_ruc_cliente = q.ced_ruc_cliente,
                        celular_cliente = q.celular_cliente,
                        CodCbteVta = q.CodCbteVta,
                        Codigo = q.Codigo,
                        direccion_cliente = q.direccion_cliente,
                        Estado = q.Estado,
                        IdBodega = q.IdBodega,
                        IdCbteVta = q.IdCbteVta,
                        IdCliente = q.IdCliente,
                        IdProducto = q.IdProducto,
                        IdProforma = q.IdProforma,
                        IdSucursal = q.IdSucursal,
                        nombre_cliente = q.nombre_cliente,
                        nom_TerminoPago = q.nom_TerminoPago,
                        pd_total = q.pd_total,
                        pr_observacion = q.pr_observacion,
                        Secuencia = q.Secuencia,
                        Su_CodigoEstablecimiento = q.Su_CodigoEstablecimiento,
                        Su_Descripcion = q.Su_Descripcion,
                        Su_Direccion = q.Su_Direccion,
                        Su_Telefonos = q.Su_Telefonos,
                        telefono_cliente = q.telefono_cliente,
                        Ve_Vendedor = q.Ve_Vendedor,
                        vt_cantidad = q.vt_cantidad,
                        vt_DescUnitario = q.vt_DescUnitario,
                        vt_fecha = q.vt_fecha,
                        vt_iva = q.vt_iva,
                        vt_Observacion = q.vt_Observacion,
                        vt_plazo = q.vt_plazo,
                        vt_PorDescUnitario = q.vt_PorDescUnitario,
                        vt_por_iva = q.vt_por_iva,
                        vt_Precio = q.vt_Precio,
                        vt_PrecioFinal = q.vt_PrecioFinal,
                        vt_Subtotal = q.vt_Subtotal,
                            pr_descripcion = q.pr_descripcion,
                            vt_NumFactura = q.vt_NumFactura,
                            Cambio = q.Cambio,
                            Descuento = q.Descuento,
                            SubtotalConDscto = q.SubtotalConDscto,
                            SubtotalIVAConDscto = q.SubtotalIVAConDscto,
                            SubtotalIVASinDscto = q.SubtotalIVASinDscto,
                            SubtotalSinDscto = q.SubtotalSinDscto,
                            SubtotalSinIVAConDscto = q.SubtotalSinIVAConDscto,
                            SubtotalSinIVASinDscto = q.SubtotalSinIVASinDscto,
                            Total = q.Total,
                            ValorEfectivo = q.ValorEfectivo,
                            ValorIVA = q.ValorIVA

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
