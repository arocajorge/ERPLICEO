using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_cliente_x_fa_Vendedor_x_sucursal_Bus
    {
        fa_cliente_x_fa_Vendedor_x_sucursal_Data odata = new fa_cliente_x_fa_Vendedor_x_sucursal_Data();
    
        public List<fa_cliente_x_fa_Vendedor_x_sucursal_Info> get_list(int IdEmpresa, decimal IdCliente)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_cliente_x_fa_Vendedor_x_sucursal_Info get_info(int IdEmpresa, decimal IdCliente, int IdSucursal)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCliente, IdSucursal);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
