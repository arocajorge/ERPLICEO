using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_008_Bus
    {
        CXC_008_Data odata = new CXC_008_Data();
        public List<CXC_008_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdCliente, String IdCobro_tipo, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdCliente,IdCobro_tipo, fecha_ini, fecha_fin, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
