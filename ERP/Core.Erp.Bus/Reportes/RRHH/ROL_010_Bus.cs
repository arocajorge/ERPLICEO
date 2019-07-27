using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_010_Bus
    {
        ROL_010_Data odata = new ROL_010_Data();

        public List<ROL_010_Info> get_list(int IdEmpresa, int IdSucursal, int IdDivision, int IdArea, int IdTipoNomina, string em_status, string Ubicacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdDivision, IdArea, IdTipoNomina, em_status, Ubicacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
