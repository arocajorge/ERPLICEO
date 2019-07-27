using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
    public class ro_empleado_x_rubro_acumulado_detalle_Bus
    {
        ro_empleado_x_rubro_acumulado_detalle_Data odata = new ro_empleado_x_rubro_acumulado_detalle_Data();

        public List<ro_empleado_x_rubro_acumulado_detalle_Info> get_list(int IdEmpresa, decimal IdEmpleado, string IdRubro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
