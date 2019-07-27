using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_008_Bus
    {
        ROL_008_Data odata = new ROL_008_Data();
    
        public List<ROL_008_Info> get_list(int IdEmpresa, decimal IdPrestamo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdPrestamo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
