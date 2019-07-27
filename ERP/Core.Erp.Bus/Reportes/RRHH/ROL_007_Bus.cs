using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_007_Bus
    {
        ROL_007_Data odata = new ROL_007_Data();
    
        public List<ROL_007_Info> get_list(int IdEmpresa, decimal IdEmpleado, int IdSolicitud)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado, IdSolicitud);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
