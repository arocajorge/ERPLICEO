using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_008_Bus
    {
        CXP_008_Data odata = new CXP_008_Data();
    
        public List<CXP_008_Info> get_list(int IdEmpresa,DateTime fecha, int IdSucursal, decimal IdProveedor, bool no_mostrar_en_conciliacion, bool no_mostrar_saldo_0)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha, IdSucursal,IdProveedor, no_mostrar_en_conciliacion, no_mostrar_saldo_0);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
