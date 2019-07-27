using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_cobro_tipo_Param_conta_x_sucursal_Bus
    {
        cxc_cobro_tipo_Param_conta_x_sucursal_Data odata = new cxc_cobro_tipo_Param_conta_x_sucursal_Data();
        public List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> get_list(int IdEmpresa, string IdCobro_tipo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCobro_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
