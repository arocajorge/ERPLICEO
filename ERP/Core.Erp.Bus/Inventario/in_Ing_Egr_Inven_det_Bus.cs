using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_Ing_Egr_Inven_det_Bus
    {
        in_Ing_Egr_Inven_det_Data odata = new in_Ing_Egr_Inven_det_Data();

        public List<in_Ing_Egr_Inven_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public in_Ing_Egr_Inven_det_Info get_info(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_Ing_Egr_Inven_det_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool eliminarDB(int IdEmpresa, int IdSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)
        {

            try
            {
                return odata.eliminarDB(IdEmpresa, IdSucursal, IdMovi_inven_tipo, IdNumMovi);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
