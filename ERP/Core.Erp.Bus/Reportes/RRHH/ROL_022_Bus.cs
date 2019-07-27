using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
   public class ROL_022_Bus
    {

        ROL_022_Data odata = new ROL_022_Data();
     
        public List<ROL_022_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal,int IdDivision, int IdArea)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo, IdSucursal, IdDivision, IdArea);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
