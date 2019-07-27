using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_006_Bus
    {
        CXP_006_Data odata = new CXP_006_Data();

        public List<CXP_006_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdRetencion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
