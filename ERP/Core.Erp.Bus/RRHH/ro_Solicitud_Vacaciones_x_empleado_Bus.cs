using Core.Erp.Bus.General;
using Core.Erp.Data.RRHH;
using Core.Erp.Info.General;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_Solicitud_Vacaciones_x_empleado_Bus
    {

        ro_Solicitud_Vacaciones_x_empleado_Data odata = new ro_Solicitud_Vacaciones_x_empleado_Data();
        public List<ro_Solicitud_Vacaciones_x_empleado_Info> get_list(int IdEmpresa,  DateTime FechaInicio, DateTime FechaFin)
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
        public ro_Solicitud_Vacaciones_x_empleado_Info get_info(int IdEmpresa,decimal IdEmpleado, decimal IdSolicitud)
        {
            try
            {
                
                return odata.get_info(IdEmpresa,IdEmpleado, IdSolicitud);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_Solicitud_Vacaciones_x_empleado_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_Solicitud_Vacaciones_x_empleado_Bus", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(ro_Solicitud_Vacaciones_x_empleado_Info info)
        {
            try
            {

                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "ro_Solicitud_Vacaciones_x_empleado_Bus", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(ro_Solicitud_Vacaciones_x_empleado_Info info)
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

        public string validar(ro_Solicitud_Vacaciones_x_empleado_Info info)
        {
            try
            {
                string mensaje = "";

                if (info.Fecha_Retorno <= info.Fecha_Hasta)
                    mensaje = "La fecha de retorno no puede ser menor a fecha fin de vacaciones";
                if (info.Fecha_Hasta <= info.Fecha_Desde)
                    mensaje = "La fecha inicio no puede ser mayor que fecha fin";
                if (info.Dias_a_disfrutar > info.Dias_q_Corresponde)
                    mensaje = "No puede tomar mas dias de los ganados";


                return mensaje;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
