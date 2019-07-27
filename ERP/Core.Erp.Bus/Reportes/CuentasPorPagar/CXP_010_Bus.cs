using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_010_Bus
    {
        CXP_010_Data odata = new CXP_010_Data();
        public List<CXP_010_Info> get_list(int IdEmpresa, decimal IdProveedor, DateTime fechaIni, DateTime fechaFin, bool mostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdProveedor, fechaIni, fechaFin, mostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
