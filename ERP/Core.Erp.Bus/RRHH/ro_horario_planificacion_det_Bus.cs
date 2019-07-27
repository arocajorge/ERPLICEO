using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_horario_planificacion_det_Bus
    {
        ro_horario_planificacion_det_Data odata = new ro_horario_planificacion_det_Data();
        public List<ro_horario_planificacion_det_Info> get_list(int IdEmpresa, decimal IdPlanificacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdPlanificacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_horario_planificacion_det_Info get_info(int IdEmpresa, decimal IdPlanificacion)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdPlanificacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<ro_horario_planificacion_det_Info> info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_horario_planificacion_det_Info info)
        {
            try
            {

                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_horario_planificacion_Info info)
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
