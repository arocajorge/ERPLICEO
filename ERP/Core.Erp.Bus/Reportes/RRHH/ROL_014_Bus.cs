using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_014_Bus
    {
        ROL_014_Data odata = new ROL_014_Data();

        public List<ROL_014_Info> get_list(int IdEmpresa, int IdSucursal, int IdTipoNomina, int IdArea, int IdDivision)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdTipoNomina, IdArea, IdDivision);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
