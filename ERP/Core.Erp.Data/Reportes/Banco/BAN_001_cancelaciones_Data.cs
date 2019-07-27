using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
   public class BAN_001_cancelaciones_Data
    {
        public List<BAN_001_cancelaciones_Info> get_list(int IdEmpresa_pago, int IdTipoCbte_pago, decimal IdCbteCble_pago)
        {
            try
            {
                List<BAN_001_cancelaciones_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWBAN_001_cancelaciones
                             where q.IdEmpresa == IdEmpresa_pago
                             && q.IdTipoCbte_pago == IdTipoCbte_pago
                             && q.IdCbteCble_pago == IdCbteCble_pago
                             select new BAN_001_cancelaciones_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdTipoCbte_pago = q.IdTipoCbte_pago,
                                 IdCbteCble_pago = q.IdCbteCble_pago,
                                 IdEmpresa_pago = q.IdEmpresa_pago,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_cxp = q.IdEmpresa_cxp,
                                 IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                                 Idcancelacion = q.Idcancelacion,
                                 IdCbteCble_cxp = q.IdCbteCble_cxp,
                                 Referencia = q.Referencia,
                                 Observacion = q.Observacion,
                                 MontoAplicado = q.MontoAplicado
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
