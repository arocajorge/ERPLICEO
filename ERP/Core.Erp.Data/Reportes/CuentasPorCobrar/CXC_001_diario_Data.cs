using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
    public class CXC_001_diario_Data
    {
        public List<CXC_001_diario_Info> get_list(int cbr_IdEmpresa, int cbr_IdSucursal, decimal cbr_IdCobro)
        {
            try
            {
                List<CXC_001_diario_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXC_001_diario
                             where q.cbr_IdEmpresa == cbr_IdEmpresa
                             && q.cbr_IdSucursal == cbr_IdSucursal
                             && q.cbr_IdCobro == cbr_IdCobro
                             select new CXC_001_diario_Info
                             {
                                  cbr_IdEmpresa = q.cbr_IdEmpresa,
                                  cbr_IdCobro = q.cbr_IdCobro,
                                  cbr_IdSucursal = q.cbr_IdSucursal,
                                  IdEmpresa = q.IdEmpresa,
                                  IdTipoCbte = q.IdTipoCbte,
                                  IdCbteCble = q.IdCbteCble,
                                  IdCtaCble = q.IdCtaCble,
                                  secuencia = q.secuencia,
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
