using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
   public class FAC_019_Data
    {
        public List<FAC_019_Info> GetList(int IdEmpresa, decimal IdCliente, int IdVendedor, DateTime fechaCorte, bool mostrarSoloVencido, bool mostrarSaldo0, string IdUsuario)
        {
            try
            {
                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 999999999 : IdCliente;

                int IdVendedorIni = IdVendedor;
                int IdVendedorFin = IdVendedor == 0 ? 999999999 : IdVendedor;
                List<FAC_019_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPFAC_019(IdEmpresa, IdClienteIni, IdClienteFin, IdVendedorIni, IdVendedorFin, fechaCorte,  mostrarSoloVencido, mostrarSaldo0, IdUsuario).Select(q => new FAC_019_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        DiasVencido = q.DiasVencido,
                        IdBodega = q.IdBodega,
                        IdCbteVta = q.IdCbteVta,
                        IdCliente = q.IdCliente,
                        IdSucursal = q.IdSucursal,
                        IdVendedor = q.IdVendedor,
                        NumDocumento = q.NumDocumento,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Saldo = q.Saldo,
                        Su_Descripcion = q.Su_Descripcion,
                        Total = q.Total,
                        TotalCobrado = q.TotalCobrado,
                        Ve_Vendedor = q.Ve_Vendedor,
                        vt_fecha = q.vt_fecha,
                        vt_fech_venc = q.vt_fech_venc,
                        ValorRetencion = q.ValorRetencion                       
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
