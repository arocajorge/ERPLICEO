using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
  public  class ROL_016_Bus
    {
        ROL_016_Data odata = new ROL_016_Data();
        public List<ROL_016_Info> get_list(int IdEmpresa, decimal IdEmpleado, DateTime fecha_ini, DateTime fecha_fin)

        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
