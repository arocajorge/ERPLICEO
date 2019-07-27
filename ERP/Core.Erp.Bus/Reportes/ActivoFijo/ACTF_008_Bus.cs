using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
    public class ACTF_008_Bus
    {
        ACTF_008_Data odata = new ACTF_008_Data();
        public List<ACTF_008_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdDepartamento, decimal IdEmpleadoCustodio, decimal IdEmpleadoEncargado)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdDepartamento, IdEmpleadoCustodio, IdEmpleadoEncargado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
