using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_003_cancelaciones_Data
    {
        public List<CXP_003_cancelaciones_Info> get_list(int IdEmpresa_pago, int IdTipoCbte_pago, decimal IdCbteCble_pago)
        {
            try
            {
                List<CXP_003_cancelaciones_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_003_cancelaciones
                             where q.IdEmpresa_pago == IdEmpresa_pago
                             && q.IdTipoCbte_pago == IdTipoCbte_pago
                             && q.IdCbteCble_pago == IdCbteCble_pago
                             select new CXP_003_cancelaciones_Info
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
                                 IdEmpresa_pago = q.IdEmpresa_pago,
                                 IdTipoCbte_pago = q.IdTipoCbte_pago,
                                 IdCbteCble_pago = q.IdCbteCble_pago
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
