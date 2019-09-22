using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_PrestamoMasivo_Det_Bus
    {
        ro_PrestamoMasivo_Det_Data odata = new ro_PrestamoMasivo_Det_Data();

        public List<ro_PrestamoMasivo_Det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCarga)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCarga);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
