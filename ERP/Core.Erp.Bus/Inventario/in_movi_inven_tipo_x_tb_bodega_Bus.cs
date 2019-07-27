using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_movi_inven_tipo_x_tb_bodega_Bus
    {
        in_movi_inven_tipo_x_tb_bodega_Data odata = new in_movi_inven_tipo_x_tb_bodega_Data();

        public List<in_movi_inven_tipo_x_tb_bodega_Info> get_list(int IdEmpresa, int IdMovi_inven_tipo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdMovi_inven_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<in_movi_inven_tipo_x_tb_bodega_Info> Lista)
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

        public bool eliminarDB(int IdEmpresa, int IdMovi_inven_tipo)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdMovi_inven_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
