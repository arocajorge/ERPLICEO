using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_001_Bus
    {
        FAC_001_Data odata = new FAC_001_Data();

        public List<FAC_001_Info> get_list(int IdEmpresa, int IdSucursal, int IdVendedor, decimal IdCliente, decimal IdProducto, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdVendedor, IdCliente, IdProducto, fecha_ini, fecha_fin, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
