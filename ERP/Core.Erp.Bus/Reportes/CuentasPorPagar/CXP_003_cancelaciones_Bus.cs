using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_003_cancelaciones_Bus
    {
        CXP_003_cancelaciones_Data odata = new CXP_003_cancelaciones_Data();
    
        public List<CXP_003_cancelaciones_Info> get_list(int IdEmpresa_pago, int IdTipoCbte_pago, decimal IdCbteCble_pago)
        {
            try
            {
                return odata.get_list(IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
