using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_005_Bus
    {
        CXC_005_Data odata = new CXC_005_Data();
        public List<CXC_005_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCLiente, DateTime? fecha_corte, bool mostrarSaldo0)

        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCLiente,  fecha_corte, mostrarSaldo0);
            }
            catch (Exception)
            {

                throw;
            }
        }
            }
}
