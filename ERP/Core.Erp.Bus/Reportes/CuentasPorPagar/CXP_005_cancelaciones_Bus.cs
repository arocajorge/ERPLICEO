using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_005_cancelaciones_Bus
    {
        CXP_005_cancelaciones_Data odata = new CXP_005_cancelaciones_Data();
    
        public List<CXP_005_cancelaciones_Info> get_list(int IdEmpresa_conciliacion, decimal IdConciliacion)
        {
            try
            {
                return odata.get_list(IdEmpresa_conciliacion, IdConciliacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
