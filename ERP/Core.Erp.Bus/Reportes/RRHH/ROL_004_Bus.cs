using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_004_Bus
    {
        ROL_004_Data odata = new ROL_004_Data();
        public List<ROL_004_Info> get_list(int IdEmpresa, int IdUtilidad)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdUtilidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
