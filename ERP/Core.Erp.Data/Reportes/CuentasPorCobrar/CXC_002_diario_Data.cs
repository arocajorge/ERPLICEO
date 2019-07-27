using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorCobrar
{
  public  class CXC_002_diario_Data
    {
        public List<CXC_002_diario_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega_Cbte, decimal IdCbte_vta_nota, string dc_TipoDocumento)
        {
            try
            {
                List<CXC_002_diario_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXC_002_diario
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega_Cbte == IdBodega_Cbte
                             && q.IdCbte_vta_nota == IdCbte_vta_nota
                             && q.dc_TipoDocumento == dc_TipoDocumento
                             select new CXC_002_diario_Info
                             {
                              IdEmpresa  = q.IdEmpresa,
                              IdSucursal = q.IdSucursal,
                              IdCobro = q.IdCobro,
                              secuencial = q.secuencial,
                              dc_TipoDocumento = q.dc_TipoDocumento,
                              IdBodega_Cbte = q.IdBodega_Cbte,
                              IdCbteCble_ct = q.IdCbteCble_ct,
                              IdCbte_vta_nota = q.IdCbte_vta_nota,
                              IdEmpresa_ct = q.IdEmpresa_ct,
                              dc_Valor = q.dc_Valor,
                              dc_Valor_Debe = q.dc_Valor_Debe,
                              dc_Valor_Haber = q.dc_Valor_Haber,
                              IdCtaCble = q.IdCtaCble,
                              IdTipoCbte_ct = q.IdTipoCbte_ct,
                              pc_Cuenta = q.pc_Cuenta


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
