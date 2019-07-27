using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_003_Bus
    {
        FAC_003_Data odata = new FAC_003_Data();

        public List<FAC_003_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, bool mostrar_cuotas)
        {
            try
            {
                return odata.get_list( IdEmpresa, IdSucursal, IdBodega, IdCbteVta, mostrar_cuotas);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
