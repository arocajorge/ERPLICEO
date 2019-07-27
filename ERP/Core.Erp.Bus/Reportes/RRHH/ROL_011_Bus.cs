using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_011_Bus
    {
        ROL_011_Data odata = new ROL_011_Data();
    
        public List<ROL_011_Info> get_list(int IdEmpresa, int IdHorasExtras)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdHorasExtras);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
