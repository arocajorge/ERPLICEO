using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_014_Bus
    {
        CXP_014_Data odata = new CXP_014_Data();
        public List<CXP_014_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdProveedor, DateTime fecha_ini, DateTime fecha_fin, string IdTipoServicio, bool mostrar_anulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdProveedor, fecha_ini, fecha_fin, IdTipoServicio, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
