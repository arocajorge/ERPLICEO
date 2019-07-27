using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_001_Bus
    {
        CXP_001_Data odata = new CXP_001_Data();
    
        public List<CXP_001_Info> get_list(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
