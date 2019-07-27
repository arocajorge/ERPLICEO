using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_003_Bus
    {
        CXC_003_Data odata = new CXC_003_Data();
        public List<CXC_003_Info> get_list(int IdEmpresa, decimal IdCliente, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCliente, Fecha_ini, Fecha_fin);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
