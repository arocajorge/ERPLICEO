using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Banco
{
    public class BAN_002_cancelaciones_Data
    {
        public List<BAN_002_cancelaciones_Info> get_list(int mba_IdEmpresa, int mba_IdTipocbte, decimal mba_IdCbteCble)
        {
            try
            {
                List<BAN_002_cancelaciones_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWBAN_002_cancelaciones
                             where q.mba_IdEmpresa == mba_IdEmpresa
                             && q.mba_IdTipocbte == mba_IdTipocbte
                             && q.mba_IdCbteCble == mba_IdCbteCble
                             select new BAN_002_cancelaciones_Info
                             {
                                 mba_IdTipocbte = q.mba_IdTipocbte,
                                 mba_IdCbteCble = q.mba_IdCbteCble,
                                 mba_IdEmpresa = q.mba_IdEmpresa,
                                 Observacion = q.Observacion,
                                 Referencia = q.Referencia,
                                 dc_TipoDocumento =q.dc_TipoDocumento,
                                 IdBodega_Cbte = q.IdBodega_Cbte,
                                 IdCbte_vta_nota = q.IdCbte_vta_nota,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 monto = q.monto
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
