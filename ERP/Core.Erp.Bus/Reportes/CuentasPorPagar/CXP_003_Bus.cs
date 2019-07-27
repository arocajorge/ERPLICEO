using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_003_Bus
    {
        CXP_003_Data odata = new CXP_003_Data();
    
        public List<CXP_003_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
