using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_001_Bus
    {
        CXC_001_Data odata = new CXC_001_Data();
    
         public List<CXC_001_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCobro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
