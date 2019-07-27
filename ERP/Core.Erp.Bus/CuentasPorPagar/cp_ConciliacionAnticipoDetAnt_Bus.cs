using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_ConciliacionAnticipoDetAnt_Bus
    {
        cp_ConciliacionAnticipoDetAnt_Data odata = new cp_ConciliacionAnticipoDetAnt_Data();
        public List<cp_ConciliacionAnticipoDetAnt_Info> get_list_op_x_cruzar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        {
            try
            {
                return odata.get_list_op_x_cruzar(IdEmpresa, IdSucursal, IdProveedor);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
