using Core.Erp.Info.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.Caja
{
    public class CAJ_002_ingresos_Data
    {
        public List<CAJ_002_ingresos_Info> get_list(int IdEmpresa, decimal IdConciliacionCaja)
        {
            try
            {
                List<CAJ_002_ingresos_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCAJ_002_ingresos
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion_Caja == IdConciliacionCaja
                             select new CAJ_002_ingresos_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion_Caja = q.IdConciliacion_Caja,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdTipocbte = q.IdTipocbte,
                                 IdCbteCble = q.IdCbteCble,
                                 valor_disponible = q.valor_disponible,
                                 valor_aplicado = q.valor_aplicado,
                                 cr_Valor = q.cr_Valor,
                                 cm_fecha = q.cm_fecha,
                                 cm_observacion = q.cm_observacion,
                                 SecuenciaCaja = q.SecuenciaCaja
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
