using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_006_diario_Data
    {
        public List<CXP_006_diario_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                List<CXP_006_diario_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_006_diario
                             where q.IdEmpresa == IdEmpresa
                             && q.IdRetencion == IdRetencion
                             select new CXP_006_diario_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRetencion = q.IdRetencion,
                                 IdEmpresa_Ogiro = q.IdEmpresa_Ogiro,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 secuencia = q.secuencia,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber

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
