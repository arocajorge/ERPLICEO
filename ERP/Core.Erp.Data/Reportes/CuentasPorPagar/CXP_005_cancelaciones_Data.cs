using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_005_cancelaciones_Data
    {
        public List<CXP_005_cancelaciones_Info> get_list(int IdEmpresa_conciliacion, decimal IdConciliacion)
        {
            try
            {
                List<CXP_005_cancelaciones_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_005_cancelaciones
                             where q.IdEmpresa_conciliacion == IdEmpresa_conciliacion
                             && q.IdConciliacion == IdConciliacion
                             select new CXP_005_cancelaciones_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 Idcancelacion = q.Idcancelacion,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_cxp = q.IdEmpresa_cxp,
                                 IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                                 IdCbteCble_cxp = q.IdCbteCble_cxp,
                                 Referencia = q.Referencia,
                                 Observacion = q.Observacion,
                                 MontoAplicado = q.MontoAplicado,
                                 IdEmpresa_conciliacion = q.IdEmpresa_conciliacion,
                                 IdConciliacion = q.IdConciliacion
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
