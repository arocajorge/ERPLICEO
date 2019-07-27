using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_015_Bus
    {
        CXP_015_Data odata = new CXP_015_Data();
        public List<CXP_015_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdProveedor, DateTime fecha_corte, bool mostrarSaldo0)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdProveedor, fecha_corte, mostrarSaldo0);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
