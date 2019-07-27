using Core.Erp.Data.Reportes.Presupuesto;
using Core.Erp.Info.Reportes.Presupuesto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Presupuesto
{
    public class PRE_001_Bus
    {
        PRE_001_Data odata = new PRE_001_Data();
        public List<PRE_001_Info> get_list(int IdEmpresa, decimal IdPresupuesto)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdPresupuesto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
