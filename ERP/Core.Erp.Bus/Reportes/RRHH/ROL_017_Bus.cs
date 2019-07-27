using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
   public class ROL_017_Bus
    {
        ROL_017_Data odata = new ROL_017_Data();
        public List<ROL_017_Info> get_list(int IdEmpresa, decimal IdEmpleado, DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado, fechaIni, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
