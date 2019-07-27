using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_002_Bus
    {
        INV_002_Data odata = new INV_002_Data();

        public List<INV_002_Info> get_list(int IdEmpresa, int idSucursal, int IdMovi_inven_tipo, decimal IdNumMovi)

        {
            try
            {
                return odata.get_list(IdEmpresa, idSucursal, IdMovi_inven_tipo, IdNumMovi);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
