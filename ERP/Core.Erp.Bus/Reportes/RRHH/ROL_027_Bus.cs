using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_027_Bus
    {
        ROL_027_Data odata = new ROL_027_Data();
        public List<ROL_027_Info> GetList(int IdEmpresa, int IdSucursal, int IdNomina_Tipo, int IdDivision, int IdArea, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdNomina_Tipo,  IdDivision, IdArea, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
