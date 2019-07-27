using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_020_Bus
    {
        ROL_020_Data odata = new ROL_020_Data();
        public List<ROL_020_Info> GetList(int IdEmpresa, int IdSucursal, int IdNominaTipo, int IdNomina, int IdPeriodo,  int IdDivision, int Idarea, int IdProceso)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdNominaTipo, IdNomina, IdPeriodo,IdDivision,Idarea, IdProceso);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
