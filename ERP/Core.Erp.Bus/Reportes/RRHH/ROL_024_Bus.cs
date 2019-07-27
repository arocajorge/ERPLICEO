using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_024_Bus
    {
        ROL_024_Data odata = new ROL_024_Data();
        public List<ROL_024_Info> GetList(int IdEmpresa, int IdSucursal, int IdNominaTipo, int IdNominaTipoLiqui, decimal IdPeriodo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdNominaTipo, IdNominaTipoLiqui, IdPeriodo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
