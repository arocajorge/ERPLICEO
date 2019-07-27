using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_007_Bus
    {
        CXP_007_Data odata = new CXP_007_Data();
    
        public List<CXP_007_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, bool mostrar_agrupado, int IdSucursal)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_ini, fecha_fin, mostrar_agrupado, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
