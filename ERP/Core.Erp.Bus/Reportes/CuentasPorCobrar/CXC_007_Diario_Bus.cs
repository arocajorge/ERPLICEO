using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_007_Diario_Bus
    {
        CXC_007_Diario_Data odata = new CXC_007_Diario_Data();
        public List<CXC_007_Diario_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdLiquidacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
