using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_005_Bus
    {
        CXP_005_Data odata = new CXP_005_Data();
    
        public List<CXP_005_Info> get_list(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdConciliacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
