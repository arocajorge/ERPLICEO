using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
    public class ACTF_008_Bus
    {
        ACTF_008_Data odata = new ACTF_008_Data();
        public List<ACTF_008_Info> GetList(int IdEmpresa, int IdSucursal, int IdArea, decimal IdDepartamento, decimal IdEmpleadoCustodio, decimal IdEmpleadoEncargado, int IdTipo, int IdCategoria, double MontoMin, double MontoMax, string IdTipoCatalogo_Ubicacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdArea, IdDepartamento, IdEmpleadoCustodio, IdEmpleadoEncargado, IdTipo, IdCategoria, MontoMin, MontoMax, IdTipoCatalogo_Ubicacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
