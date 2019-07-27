using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_x_cxc_cobro_Bus
    {
        cxc_LiquidacionTarjeta_x_cxc_cobro_Data odata = new cxc_LiquidacionTarjeta_x_cxc_cobro_Data();
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> GetList(int IdEmpresa, int IdSucursal, decimal? IdLiquidacion)
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
