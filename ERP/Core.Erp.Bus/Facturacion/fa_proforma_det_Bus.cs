using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Facturacion
{
   public class fa_proforma_det_Bus
    {
        fa_proforma_det_Data odata = new fa_proforma_det_Data();

        public List<fa_proforma_det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdProforma)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdProforma);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
