using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.Inventario
{
    public class in_producto_x_tb_bodega_Bus
    {
        in_producto_x_tb_bodega_Data odata = new in_producto_x_tb_bodega_Data();


        public List<in_producto_x_tb_bodega_Info> get_list(int IdEmpresa, int IdSucursal)
        {
            try
            {
                return odata.get_lis(IdEmpresa, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_producto_x_tb_bodega_Info> get_list(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                return odata.get_lis(IdEmpresa, IdProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
