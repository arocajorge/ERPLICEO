using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_012_Bus
    {
        CXP_012_Data odata = new CXP_012_Data();

        public List<CXP_012_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdRetencion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
