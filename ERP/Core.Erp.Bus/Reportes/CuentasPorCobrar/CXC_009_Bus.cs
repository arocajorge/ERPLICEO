using Core.Erp.Data.Reportes.CuentasPorCobrar;
using Core.Erp.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_009_Bus
    {
        CXC_009_Data odata = new CXC_009_Data();
        public List<CXC_009_Info> GetList(int IdEmpresa, decimal IdCliente, DateTime fechaCorte)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdCliente, fechaCorte);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
