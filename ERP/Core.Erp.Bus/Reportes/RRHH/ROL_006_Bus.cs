using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_006_Bus
    {
        ROL_006_Data odata = new ROL_006_Data();
        public List<ROL_006_Info> get_list(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
