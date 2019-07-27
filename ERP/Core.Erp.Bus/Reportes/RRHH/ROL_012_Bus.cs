using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_012_Bus
    {
        ROL_012_Data odata = new ROL_012_Data();
        public List<ROL_012_Info> get_list(int IdEmpresa, DateTime fecha_inicio, DateTime fecha_fin, string IdRubro)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_inicio, fecha_fin, IdRubro);
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
