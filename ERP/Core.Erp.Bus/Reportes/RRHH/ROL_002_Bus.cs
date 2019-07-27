using Core.Erp.Data.Reportes.RRHH;
using Core.Erp.Info.Reportes.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.RRHH
{
    public class ROL_002_Bus
    {
        ROL_002_Data odata = new ROL_002_Data();
        public List<ROL_002_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo, IdSucursal, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ROL_002_Info> get_list_empleados(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo, int IdSucursal)
        {
            try
            {
                return odata.get_list_empleados(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ROL_002_Info> get_list_detalle_prestamos(int IdEmpresa, int IdNominaTipo, int IdPeriodo, int IdEmpleado)
        {
            try
            {
                return odata.get_list_detalle_prestamos(IdEmpresa, IdNominaTipo, IdPeriodo, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
