using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_producto_x_tb_bodega_Costo_Historico_Bus
    {
        in_producto_x_tb_bodega_Costo_Historico_Data odata = new in_producto_x_tb_bodega_Costo_Historico_Data();
        public double get_ultimo_costo(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, DateTime Fecha)
        {
            try
            {
                return odata.get_ultimo_costo(IdEmpresa,IdSucursal,IdBodega,IdProducto,Fecha);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<in_producto_x_tb_bodega_Costo_Historico_Info> Recosteo_x_Sucursal(int IdEmpresa, int IdSucursal, int IdBodega, DateTime fecha_ini)
        {
            try
            {
                return odata.Recosteo_x_Sucursal(IdEmpresa, IdSucursal, IdBodega, fecha_ini);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
