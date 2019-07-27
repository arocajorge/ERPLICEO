using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
   public class FAC_004_Bus
    {
        FAC_004_Data odata = new FAC_004_Data();
    
        public List<FAC_004_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
