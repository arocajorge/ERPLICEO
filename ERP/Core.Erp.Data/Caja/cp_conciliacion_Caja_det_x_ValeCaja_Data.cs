using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Caja
{
    public class cp_conciliacion_Caja_det_x_ValeCaja_Data
    {
        public List<cp_conciliacion_Caja_det_x_ValeCaja_Info> get_list(int IdEmpresa, decimal IdConciliacion_caja)
        {
            try
            {
                List<cp_conciliacion_Caja_det_x_ValeCaja_Info> Lista;

                using (Entities_caja Context = new Entities_caja())
                {
                    Lista = (from q in Context.vwcp_conciliacion_Caja_det_x_ValeCaja
                             where q.IdEmpresa == IdEmpresa 
                             && q.IdConciliacion_Caja == IdConciliacion_caja
                             select new cp_conciliacion_Caja_det_x_ValeCaja_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion_Caja = q.IdConciliacion_Caja,
                                 Secuencia = q.Secuencia,
                                 IdEmpresa_movcaja = q.IdEmpresa_movcaja,
                                 IdCbteCble_movcaja = q.IdCbteCble_movcaja,
                                 IdTipocbte_movcaja = q.IdTipocbte_movcaja,
                                 IdCtaCble = q.IdCtaCble,
                                 fecha = q.cm_fecha,
                                 valor = q.cm_valor,
                                 idTipoMovi = q.IdTipoMovi,
                                 IdPersona = q.IdPersona,
                                 Observacion = q.cm_observacion,
                                 pe_nombreCompleto = q.pe_nombreCompleto
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
