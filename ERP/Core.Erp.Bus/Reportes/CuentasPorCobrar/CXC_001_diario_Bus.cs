using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_001_diario_Bus
    {
        CXC_001_diario_Data odata = new CXC_001_diario_Data();
    
        public List<CXC_001_diario_Info> get_list(int cbr_IdEmpresa, int cbr_IdSucursal, decimal cbr_IdCobro)
        {
            try
            {
                return odata.get_list(cbr_IdEmpresa, cbr_IdSucursal, cbr_IdCobro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
