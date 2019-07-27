using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public  class ROL_001_Bus
    {
        ROL_001_Data odata = new ROL_001_Data();
        public List<ROL_001_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal)
        {
            try
            {
                return odata.get_list(IdEmpresa,IdNomina,IdNominaTipo,IdPeriodo, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }
   }
}
