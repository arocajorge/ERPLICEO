using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
   public class FAC_012_Bus
    {
        FAC_012_Data odata = new FAC_012_Data();
        public List<FAC_012_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCambio)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdCambio);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
