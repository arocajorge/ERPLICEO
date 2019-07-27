using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_ConciliacionAnticipoDetCXP_Bus
    {
        cp_ConciliacionAnticipoDetCXP_Data odata = new cp_ConciliacionAnticipoDetCXP_Data();
        public List<cp_ConciliacionAnticipoDetCXP_Info> get_list_facturas_x_cruzar(int IdEmpresa, int IdSucursal, decimal IdProveedor, string Usuario)
        {
            try
            {
                return odata.get_list_facturas_x_cruzar(IdEmpresa, IdSucursal, IdProveedor, Usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
