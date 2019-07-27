using Core.Erp.Data.Reportes.Facturacion;
using Core.Erp.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Facturacion
{
    public class FAC_009_Bus
    {
        FAC_009_Data odata = new FAC_009_Data();
        public List<FAC_009_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdGuiaRemision)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdGuiaRemision);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
