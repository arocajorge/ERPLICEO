using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjetaDet_Bus
    {
        cxc_LiquidacionTarjetaDet_Data odata = new cxc_LiquidacionTarjetaDet_Data();

        public List<cxc_LiquidacionTarjetaDet_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
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
