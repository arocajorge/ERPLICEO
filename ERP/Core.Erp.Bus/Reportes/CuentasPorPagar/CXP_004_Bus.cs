using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_004_Bus
    {
        CXP_004_Data odata = new CXP_004_Data();

        public List<CXP_004_Info> get_list(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdOrdenPago);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
