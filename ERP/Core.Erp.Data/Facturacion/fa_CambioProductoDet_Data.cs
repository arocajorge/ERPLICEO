using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
    public class fa_CambioProductoDet_Data
    {
        public List<fa_CambioProductoDet_Info> GetList(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCambio)
        {
            try
            {
                List<fa_CambioProductoDet_Info> Lista;

                using (Entities_facturacion db = new Entities_facturacion())
                {
                    Lista = db.vwfa_CambioProductoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCambio == IdCambio).Select(q => new fa_CambioProductoDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdBodega = q.IdBodega,
                        IdCambio = q.IdCambio,
                        Secuencia = q.Secuencia,
                        IdCbteVta = q.IdCbteVta,
                        SecuenciaFact = q.SecuenciaFact,
                        IdProductoFact = q.IdProductoFact,
                        IdProductoCambio = q.IdProductoCambio,
                        CantidadFact = q.CantidadFact,
                        CantidadCambio = q.CantidadCambio,
                        pr_descripcionCambio = q.pr_descripcionCambio,
                        pr_descripcionFact = q.pr_descripcionFact,
                        NombreCliente = q.NombreCliente,
                        vt_NumFactura = q.vt_NumFactura                                              
                    }).ToList();
                }
                Lista.ForEach(q => q.IdSecuencial = q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00") + q.IdBodega.ToString("00") + q.IdCbteVta.ToString("000000000") + q.SecuenciaFact.ToString("0000"));
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_CambioProductoDet_Info> GetListFacturas(int IdEmpresa, int IdSucursal, int IdBodega, decimal NumeroFactura, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<fa_CambioProductoDet_Info> Lista;
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                using (Entities_facturacion db = new Entities_facturacion())
                {
                    Lista = db.vwfa_CambioProductoDet_facturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && FechaIni <= q.vt_fecha && q.vt_fecha <= FechaFin).Select(q => new fa_CambioProductoDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdBodega = q.IdBodega,    
                        IdCbteVta = q.IdCbteVta,                    
                        SecuenciaFact = q.Secuencia,
                        IdProductoFact = q.IdProducto,
                        IdProductoCambio = q.IdProducto,
                        CantidadFact = q.vt_cantidad,
                        CantidadCambio = q.vt_cantidad,                        
                        pr_descripcionCambio = q.pr_descripcion,
                        pr_descripcionFact = q.pr_descripcion,
                        vt_NumFactura = q.vt_NumFactura,
                        NombreCliente = q.NombreCliente,                        
                        vt_fecha = q.vt_fecha
                    }).ToList();

                    if (NumeroFactura > 0)
                        Lista = Lista.Where(q => NumeroFactura == (string.IsNullOrEmpty(q.vt_NumFactura) ? 0 : Convert.ToDecimal(q.vt_NumFactura))).ToList();
                }
                Lista.ForEach(q => q.IdSecuencial = q.IdEmpresa.ToString("00") + q.IdSucursal.ToString("00")+ q.IdBodega.ToString("00") + q.IdCbteVta.ToString("000000000") + q.SecuenciaFact.ToString("0000"));
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
