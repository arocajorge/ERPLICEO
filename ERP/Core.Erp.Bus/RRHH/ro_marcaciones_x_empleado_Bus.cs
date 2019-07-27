using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_marcaciones_x_empleado_Bus
    {
        ro_marcaciones_x_empleado_Data odata = new ro_marcaciones_x_empleado_Data();
        public List<ro_marcaciones_x_empleado_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, FechaInicio, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_marcaciones_x_empleado_Info get_info(int IdEmpresa, decimal IdEmpleado, decimal IdRegistro)
        {
            try
            {
                return odata.get_info(IdEmpresa,IdEmpleado, IdRegistro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_marcaciones_x_empleado_Info info)
        {
            try
            {
                info.IdCalendadrio = Convert.ToInt32(info.es_fechaRegistro.ToString("ddMMyyyy"));
                if(!odata.si_existe(info))
                return odata.guardarDB(info);
                else
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(List<ro_marcaciones_x_empleado_Info> lista, int IdEmpresa)
        {
            try
            {
                    return odata.guardarDB(lista, IdEmpresa);
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_marcaciones_x_empleado_Info info)
        {
            try
            {
                info.IdCalendadrio =Convert.ToInt32( info.es_fechaRegistro.ToString("ddMMyyyy"));
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_marcaciones_x_empleado_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
