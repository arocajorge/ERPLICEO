using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Erp.Data.Reportes.Facturacion
{
    public class FAC_003_Data
    {
        public List<FAC_003_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, bool mostrar_cuotas)
        {
            try
            {
                List<FAC_003_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                        Lista = (from q in Context.VWFAC_003
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdBodega == IdBodega
                                 && q.IdCbteVta == IdCbteVta
                                 select new FAC_003_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdBodega = q.IdBodega,
                                     IdCbteVta = q.IdCbteVta,
                                     IdProducto = q.IdProducto,
                                     IdSucursal = q.IdSucursal,
                                     vt_cantidad = q.vt_cantidad,
                                     vt_fecha = q.vt_fecha,
                                     vt_iva = q.vt_iva,
                                     vt_NumFactura = q.vt_NumFactura,
                                     vt_Precio = q.vt_Precio,
                                     
                                     cli_cedulaRuc = q.cli_cedulaRuc,
                                     cli_correo = q.cli_correo,
                                     cli_direccion = q.cli_direccion,
                                     cli_Nombre = q.cli_Nombre,
                                     cli_Telefonos = q.cli_Telefonos,
                                     
                                     FormaDePago = q.FormaDePago,
                                     pr_descripcion = q.pr_descripcion,
                                     Secuencia = q.Secuencia,
                                     SubtotalConDscto = q.SubtotalConDscto,
                                     SubtotalIVAConDscto =  q.SubtotalIVAConDscto,
                                     SubtotalSinDscto = q.SubtotalSinDscto,
                                     SubtotalSinIVAConDscto = q.SubtotalSinIVAConDscto,
                                     Su_Descripcion = q.Su_Descripcion,
                                     Su_Direccion = q.Su_Direccion,
                                     Su_Telefonos = q.Su_Telefonos,
                                     vt_Total = q.vt_Total,
                                     Fecha_Autorizacion = q.Fecha_Autorizacion,
                                     IdCatalogo_FormaPago = q.IdCatalogo_FormaPago,
                                     vt_autorizacion = q.vt_autorizacion,
                                     Cambio = q.Cambio,
                                     ValorEfectivo = q.ValorEfectivo,
                                      vt_Observacion = q.vt_Observacion,
                                      Descuento = q.Descuento,
                                      SubtotalIVASinDscto = q.SubtotalIVASinDscto,
                                      SubtotalSinIVASinDscto = q.SubtotalSinIVASinDscto,
                                      Total = q.Total,
                                      T_SubtotalConDscto = q.T_SubtotalConDscto,
                                      T_SubtotalSinDscto = q.T_SubtotalSinDscto,
                                      ValorIVA = q.ValorIVA,
                                      vt_detallexItems = q.vt_detallexItems
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
