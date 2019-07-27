using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_Producto_Composicion_Bus
    {
        in_Producto_Composicion_Data odata = new in_Producto_Composicion_Data();

        public List<in_Producto_Composicion_Info> get_list(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<in_Producto_Composicion_Info> Lista)
        {
            try
            {
               
                return odata.guardarDB(Lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
