using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
   public class CXP_010_Data
    {
        public List<CXP_010_Info> get_list(int IdEmpresa, decimal IdProveedor, DateTime fechaIni, DateTime fechaFin, bool mostrarAnulados)
        {
            try
            {
                fechaFin = Convert.ToDateTime(fechaFin.ToShortDateString());
                fechaIni = Convert.ToDateTime(fechaIni.ToShortDateString());

                decimal IdProveedorIni = IdProveedor;
                decimal IdProveedorFin = IdProveedor == 0 ? 9999 : IdProveedor;
                List<CXP_010_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPCXP_010(IdEmpresa, IdProveedorIni, IdProveedorFin, fechaIni, fechaFin, mostrarAnulados)
                             select new CXP_010_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Estado = q.Estado,
                                 Fecha = q.Fecha,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Referencia = q.Referencia,
                                 Saldo = q.Saldo,
                                 Secuencia = q.Secuencia,
                                 Tipo = q.Tipo,
                                 SaldoModulo = q.SaldoModulo,
                                 co_observacion = q.co_observacion,
                                 Credito = q.Credito,
                                 Debito = q.Debito,
                                 IdProveedor = q.IdProveedor

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
