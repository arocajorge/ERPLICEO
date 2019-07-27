using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_023_Bus
    {
        ROL_023_Data odata = new ROL_023_Data();
        public List<ROL_023_Info> GetList(int IdEmpresa, int IdSucursal, int IdNomina, int IdNominaTipoLiqui, int IdPeriodo, int IdDivision, int IdArea, int IdDepartamento)
        {
            try
            {
                return odata.GetList(IdEmpresa,  IdSucursal,  IdNomina,  IdNominaTipoLiqui,  IdPeriodo,  IdDivision,  IdArea,  IdDepartamento);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
