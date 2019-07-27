using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_019_Bus
    {
        ROL_019_Data odata = new ROL_019_Data();
        public List<ROL_019_Info> get_list(int IdEmpresa, int IdSucursal, int IdNominaTipoLiqui, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdNominaTipoLiqui, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
