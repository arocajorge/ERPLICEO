using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_empleado_x_division_x_area_Bus
    {
        ro_empleado_x_division_x_area_Data odata = new ro_empleado_x_division_x_area_Data();

        public List<ro_empleado_x_division_x_area_Info> GetList(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_lis(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
