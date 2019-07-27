using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_002_diario_Bus
    {
        CXP_002_diario_Data odata = new CXP_002_diario_Data();
    
        public List<CXP_002_diario_Info> get_list(int IdEmpresa_Ogiro, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                return odata.get_list(IdEmpresa_Ogiro, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
