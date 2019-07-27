using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Inventario
{
    public class in_Producto_x_fa_NivelDescuento_Bus
    {
        in_Producto_x_fa_NivelDescuento_Data odata = new in_Producto_x_fa_NivelDescuento_Data();
        public List<in_Producto_x_fa_NivelDescuento_Info> get_list(int IdEmpresa)
        {
            try
            {
                return odata.get_list_nuevo(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public in_Producto_x_fa_NivelDescuento_Info GetInfo(int IdEmpresa, decimal IdProducto, int IdNivel)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdProducto, IdNivel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<in_Producto_x_fa_NivelDescuento_Info> get_list(int IdEmpresa, decimal IdProducto)
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

        public bool guardarDB(List<in_Producto_x_fa_NivelDescuento_Info> Lista)
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
