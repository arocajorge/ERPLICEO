using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_026_Bus
    {
        ROL_026_Data odata = new ROL_026_Data();
        public List<ROL_026_Info> GetList(int IdEmpresa, int IdSucursal, int IdNomina_Tipo, int IdAnio, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdNomina_Tipo, IdAnio, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
