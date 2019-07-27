using Core.Erp.Data;
using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public class ro_nomina_x_horas_extras_det_Bus
    {
        #region variables
        //JORNADA NOCTURNA
        TimeSpan inicioHora25 = TimeSpan.FromHours(19); //19:00 PM
        TimeSpan finalHora25 = TimeSpan.FromHours(6); //06:00 AM
        //HORAS EXTRAS 50
        TimeSpan inicioHora50 = TimeSpan.FromHours(6); //06:00 AM
        TimeSpan finalHora50 = TimeSpan.FromHours(24); //24:00 PM
        //HORAS EXTRAS 100
        TimeSpan inicioHora100 = TimeSpan.FromHours(0); //00:00 PM
        TimeSpan finalHora100 = TimeSpan.FromHours(6); //06:00 AM
        TimeSpan unDia = TimeSpan.FromHours(24); //24:00 PM
        double horaExtra25 = 0;
        double horaExtra50 = 0;
        double horaExtra100 = 0;
        double horaTrabajada = 0;
        List<ro_nomina_x_horas_extras_det_Info> lst_horas_extras_aprobar = new List<ro_nomina_x_horas_extras_det_Info>();
        ro_nomina_x_horas_extras_det_Data odata = new ro_nomina_x_horas_extras_det_Data();
        ro_horario_Data odata_horario = new ro_horario_Data();
        ro_turno_Data odata_turno = new ro_turno_Data();
        List<ro_horario_Info> lst_horaio = new List<ro_horario_Info>();
        List<ro_turno_Info> lsr_turnos = new List<ro_turno_Info>();
        ro_empleado_novedad_Bus bus_novedad = new ro_empleado_novedad_Bus();
        List<ro_nomina_x_horas_extras_det> lst_nomina_horas_extras = new List<ro_nomina_x_horas_extras_det>();
        #endregion
        public List<ro_nomina_x_horas_extras_det_Info> get_list(int IdEmpresa, int IdHorasExtra)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdHorasExtra);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_nomina_x_horas_extras_det_Info> get_list_x_extado_aprobacion(int IdEmpresa, decimal IdPlanificacion, bool estado_aprobacion)
        {
            try
            {
                return odata.get_list_x_extado_aprobacion(IdEmpresa, IdPlanificacion, estado_aprobacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_nomina_x_horas_extras_det_Info get_info(int IdEmpresa, decimal IdPlanificacion)
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

        public bool guardarDB(List<ro_nomina_x_horas_extras_det_Info> info)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_nomina_x_horas_extras_det_Info info)
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

        public bool anularDB(ro_nomina_x_horas_extras_det_Info info)
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

        public bool calcular_horas_extras(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo)
        {
            try
            {
                int IdHorasExtras = 0;
                ro_horario_Info horarioActual = null;
                ro_turno_Info info_turno = null;
                lst_horaio = odata_horario.get_list(IdEmpresa, false);
                lsr_turnos = odata_turno.get_list(IdEmpresa, false);
                lst_nomina_horas_extras = odata.get_lis_calcular_horas_extras(IdEmpresa, IdNomina, IdNominaTipo, IdPeriodo);
                bool banderaHorario=false;
                foreach (var item in lst_nomina_horas_extras)
                {
                    horaExtra25=0;
                     horaExtra50=0;
                     horaExtra100=0;
                    double minutos_lunch = 0;
                    IdHorasExtras = item.IdHorasExtras;
                    // obteniendo el horario por empleado y por idcalendario
                    horarioActual = lst_horaio.Where(v => v.IdHorario == item.IdHorario).FirstOrDefault();
                    // obteniendo el turno por empleado y por idcalendario
                    info_turno = lsr_turnos.Where(v => v.IdTurno == item.IdTurno).FirstOrDefault();
                    if (horarioActual != null && info_turno != null)
                    {
                        #region calculo horas tabajadas
                        //CALCULA LAS HORAS TRABAJADAS                    
                        if (item.time_salida1 >= item.time_entrada1)
                        {

                            if (item.time_salida1 > item.time_entrada1 && item.time_salida1 < item.time_entrada1)
                            {
                                horaTrabajada =((TimeSpan) (item.time_salida1 - item.time_entrada1)).TotalHours +((TimeSpan) (item.time_salida2 - item.time_entrada2)).TotalHours;
                            }
                            else
                            {
                                horaTrabajada = ((TimeSpan)(item.time_salida1 - item.time_entrada1)).TotalHours;
                            }
                        }
                        else
                        {
                            if (item.time_salida1 > item.time_entrada1 && item.time_salida1 < item.time_entrada1)
                            {
                                horaTrabajada = ((TimeSpan)(item.time_salida1 - item.time_entrada1)).TotalHours +((TimeSpan) (unDia - item.time_entrada1)).TotalHours +((TimeSpan) item.time_salida2).TotalHours;
                            }
                            else
                            {
                                horaTrabajada = ((TimeSpan)(unDia - item.time_entrada1)).TotalHours +((TimeSpan) item.time_salida1).TotalHours;
                            }
                        }
                        #endregion
                        #region variables de calculos
                        item.hora_trabajada = horaTrabajada;
                        minutos_lunch = ((TimeSpan)((TimeSpan)horarioActual.RegLunch) - horarioActual.SalLunch).TotalHours; // obtengo los minutos de descanso                                    
                        //VERIFICAR HORARIO DEL MISMO DIA VS. HORARIO DEL DIA SIGUIENTE
                        if (horarioActual.HoraIni <= horarioActual.HoraFin)//CORRESPONDE AL MISMO DIA
                        {
                            banderaHorario = false;
                        }
                        else
                        {
                            //CORRESPONDE A PARTE DEL MISMO DIA, Y PARTE DEL DIA SIGUIENTE 
                            banderaHorario = true;
                        }
                        int dia_semana = Convert.ToInt32(item.FechaRegistro.DayOfWeek);
                        #endregion
                        #region turno diurno
                        if (banderaHorario == false)//TURNO DIURNO
                        {
                            if ((dia_semana == 7 | dia_semana == 6) && (info_turno.Domingo == false | info_turno.Sabado == false))
                            {
                                item.hora_trabajada = ((TimeSpan)(item.time_salida1 - item.time_entrada1)).TotalHours - ((TimeSpan)(horarioActual.RegLunch - horarioActual.SalLunch)).TotalHours;
                                item.hora_extra100 = item.hora_trabajada;
                                horaExtra100 = item.hora_extra100;
                            }
                            else
                            {
                                if (item.hora_trabajada > 8)
                                {
                                    //VERIFICA QUE LA SALIDA CORRESPONDA A HORAS EXTRAS DEL MISMO DIA
                                    if (item.time_salida1 > (TimeSpan)horarioActual.HoraFin && item.time_salida1 > item.time_entrada1)
                                    {
                                        //VERIFICA SI TIENE HORAS EXTRAS 100
                                        if (item.time_salida1 > inicioHora100 && item.time_salida1 <= finalHora100)
                                        {
                                            horaExtra100 = ((TimeSpan)(item.time_salida1 - finalHora100)).TotalHours; //TOTAL DE HORAS EXTRAS 100
                                        }
                                        else
                                        {
                                            //VERIFICA SI TIENE HORAS EXTRAS 50%
                                            if (item.time_salida1 > inicioHora50 && item.time_salida1 < finalHora50)
                                            {
                                                horaExtra50 = ((TimeSpan)(item.time_salida1 - (TimeSpan)horarioActual.HoraFin)).TotalHours; //TOTAL DE HORAS EXTRAS 50% 
                                                horaExtra50 = horaExtra50 - minutos_lunch;
                                                item.hora_extra50 = horaExtra50;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (item.time_salida1 < (TimeSpan)horarioActual.HoraIni)
                                        {
                                            horaExtra50 = (finalHora50 - (TimeSpan)horarioActual.HoraFin).TotalHours; //TOTAL DE HORAS EXTRAS 50%                                          
                                            horaExtra100 = ((TimeSpan)(item.time_salida1 - inicioHora100)).TotalHours; //TOTAL DE HORAS EXTRAS 100
                                        }
                                    }
                                }
                            }

                        }
                        #endregion
                        #region turno nocturno
                        else
                        {
                            //VERIFICA SI ES JORNADA NOCTURNA
                            if (item.time_entrada1 >= inicioHora25 && item.time_entrada1 <= (unDia + finalHora25) && banderaHorario)
                            {
                                horaExtra25 = ((TimeSpan)((unDia + item.time_salida2) - item.time_entrada1)).TotalHours; //TOTAL DE HORAS DE JORNADA NOCTURNA
                            }
                            //CALCULO DE HORAS EXTRAS
                            if (item.time_salida2 > item.time_entrada1 && item.time_salida2 < finalHora50)
                            {
                                horaTrabajada = (((TimeSpan) item.time_salida2).TotalHours - ((TimeSpan)item.time_entrada1).TotalHours);
                            }
                            else
                            {
                                if (item.time_salida1 > inicioHora100)
                                {
                                    horaTrabajada = ((TimeSpan)(unDia + item.time_salida1)).TotalHours - ((TimeSpan)item.time_entrada1).TotalHours;

                                    if (item.time_salida1 > (TimeSpan)horarioActual.HoraFin && item.time_salida1 <= finalHora100)
                                    {
                                        horaExtra100 = ((TimeSpan)(item.time_salida1 - (TimeSpan)horarioActual.HoraFin)).TotalHours;
                                    }
                                    else
                                    {
                                        if (item.time_salida1 > (TimeSpan)horarioActual.HoraFin && item.time_salida1 > inicioHora50 && item.time_salida1 <= finalHora50)
                                        {
                                            horaExtra100 = (inicioHora50 - (TimeSpan)horarioActual.HoraFin).TotalHours;
                                            horaExtra50 = ((TimeSpan)(item.time_salida2 - inicioHora50)).TotalHours;
                                        }
                                    }
                                }
                            }







                        }
                        #endregion

                    }

                    item.hora_trabajada = horaTrabajada-minutos_lunch;
                    item.hora_extra25 = horaExtra25;
                    item.hora_extra50 = horaExtra50;
                    item.hora_extra100 = horaExtra100;

                    item.Valor25 = ((item.Sueldo_base / 240) * 1.25)* horaExtra25;
                    item.Valor50 = ((item.Sueldo_base / 240) * 1.5)* horaExtra50;
                    item.Valor100 = ((item.Sueldo_base / 240) *2) * horaExtra100;


                }
                return odata.guardarDB(IdEmpresa, IdHorasExtras, lst_nomina_horas_extras);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
