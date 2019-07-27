using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_003_Bus
    {
        ROL_003_Data odata = new ROL_003_Data();
    
        public List<ROL_003_Info> get_list(int IdEmpresa, decimal IdEmpleado, decimal IdNovedad)
        {
            try
            {
                return odata.get_list( IdEmpresa, IdEmpleado, IdNovedad);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
