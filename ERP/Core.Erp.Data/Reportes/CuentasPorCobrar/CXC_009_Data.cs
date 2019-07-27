using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
   public class CXC_009_Data
    {
        public List<CXC_009_Info> GetList(int IdEmpresa, decimal IdCliente, DateTime fechaCorte)
        {
            try
            {
                List<CXC_009_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = Context.SPCXC_009(IdEmpresa, IdCliente, fechaCorte).Select(q => new CXC_009_Info
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
                        VENCIDO30 = q.VENCIDO30,
                        VENCIDO60 = q.VENCIDO60,
                        VENCIDO90 = q.VENCIDO90,
                        VENCIDO90MAS = q.VENCIDO90MAS,
                        Ve_Vendedor = q.Ve_Vendedor,
                        vt_fecha = q.vt_fecha,
                        vt_fech_venc = q.vt_fech_venc,
                        vt_Observacion = q.vt_Observacion,
                        vt_tipoDoc = q.vt_tipoDoc,
                        X_VENCER = q.X_VENCER
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
