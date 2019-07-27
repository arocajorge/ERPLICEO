using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Facturacion
{
   public class FAC_011_Data
    {
        public List<FAC_011_Info> get_list(int IdEmpresa, decimal IdCliente, DateTime fechaIni, DateTime fechaFin, bool mostrarAnulados)
        {
            try
            {
                decimal IdClienteIni = IdCliente;
                decimal IdClienteFin = IdCliente == 0 ? 9999 : IdCliente;
                List<FAC_011_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.SPFAC_011(IdEmpresa, IdClienteIni, IdClienteFin, fechaIni, fechaFin, mostrarAnulados)
                             select new FAC_011_Info
                             {
                                 IdEmpresa =q.IdEmpresa,
                                 IdCliente = q.IdCliente,
                                 Creditos = q.Creditos,
                                 Debitos = q.Debitos,
                                 Estado = q.Estado,
                                 Fecha = q.Fecha,
                                 Observacion = q.Observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Referencia = q.Referencia,
                                 Saldo =q.Saldo,
                                 Secuencia =q.Secuencia,
                                 Tipo =q.Tipo,
                                 SaldoModulo =q.SaldoModulo

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
