using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_ConciliacionAnticipoDetAnt_Data
    {
        public List<cp_ConciliacionAnticipoDetAnt_Info> getlist(int IdEmpresa, int IdConciliacion)
        {
            try
            {
                List<cp_ConciliacionAnticipoDetAnt_Info> Lista = new List<cp_ConciliacionAnticipoDetAnt_Info>();

                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in db.vwcp_ConciliacionAnticipoDetAnt
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion == IdConciliacion
                             select new cp_ConciliacionAnticipoDetAnt_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion = q.IdConciliacion,
                                 Secuencia = q.Secuencia,
                                 IdOrdenPago = q.IdOrdenPago,
                                 MontoAplicado = q.MontoAplicado,
                                 Fecha = q.Fecha,
                                 Observacion = q.Observacion
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_ConciliacionAnticipoDetAnt_Info> get_list_op_x_cruzar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        {
            try
            {
                List<cp_ConciliacionAnticipoDetAnt_Info> Lista = new List<cp_ConciliacionAnticipoDetAnt_Info>();

                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    Lista = db.vwcp_ConciliacionAnticipoDetAnt_x_cruzar.Where(q=> q.IdEmpresa == IdEmpresa 
                    && q.IdSucursal == IdSucursal && q.IdProveedor == IdProveedor).Select(q=> new cp_ConciliacionAnticipoDetAnt_Info {
                        IdEmpresa = q.IdEmpresa,
                        IdOrdenPago = q.IdOrdenPago,
                        Fecha = q.Fecha,
                        MontoAplicado = q.Saldo,
                        Observacion = q.Observacion
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
