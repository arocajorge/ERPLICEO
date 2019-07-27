using Core.Erp.Data.Reportes.CuentasPorPagar;
using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorPagar
{
    public class CXP_011_Bus
    {
        CXP_011_Data odata = new CXP_011_Data();
        public List<CXP_011_Info> GetList(int IdEmpresa, decimal IdSolicitud)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSolicitud);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
