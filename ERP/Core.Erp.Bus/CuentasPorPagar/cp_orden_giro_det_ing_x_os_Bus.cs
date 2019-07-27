using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_orden_giro_det_ing_x_os_Bus
    {
        cp_orden_giro_det_ing_x_os_Data odata = new cp_orden_giro_det_ing_x_os_Data();
        public List<cp_orden_giro_det_ing_x_os_Info> get_list_x_ingresar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        {
            try
            {
                return odata.get_list_x_ingresar(IdEmpresa, IdSucursal, IdProveedor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_orden_giro_det_ing_x_os_Info> get_list(int IdEmpresa, decimal IdCbteCble_Ogiro, decimal IdTipoCbte_Ogiro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCbteCble_Ogiro, IdTipoCbte_Ogiro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
