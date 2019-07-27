using Core.Erp.Data.Reportes.Inventario;
using Core.Erp.Info.Reportes.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Inventario
{
    public class INV_005_Bus
    {
        INV_005_Data odata = new INV_005_Data();

        public List<INV_005_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdProducto, DateTime fecha_ini, DateTime fecha_fin, string IdUsuario, bool no_mostrar_valores_en_0, bool mostrar_detallado, decimal IdProductoPadre)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdProducto, fecha_ini, fecha_fin, IdUsuario, no_mostrar_valores_en_0, mostrar_detallado, IdProductoPadre);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
