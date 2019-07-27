using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
   public class ROL_015_Bus
    {
        ROL_015_Data odata = new ROL_015_Data();
        public List<ROL_015_Info> get_list( int IdEmpresa , decimal IdEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado, fechaInicio, fechaFin );
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
