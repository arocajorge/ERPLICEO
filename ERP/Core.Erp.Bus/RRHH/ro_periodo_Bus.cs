using Core.Erp.Data.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Globalization;
namespace Core.Erp.Bus.RRHH
{
    public class ro_periodo_Bus
    {

        List<ro_periodo_Info> lista_periodos = null;
        ro_periodo_Data odata = new ro_periodo_Data();
        public List<ro_periodo_Info> get_list(int IdEmpresa, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_periodo_Info get_info(int IdEmpresa, int IdPeriodo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdPeriodo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_periodo_Info info)
        {
            try
            {
                if(info.pe_FechaIni.Month==info.pe_FechaFin.Month)
                {
                    info.pe_mes = info.pe_FechaIni.Month;
                    info.pe_anio = info.pe_FechaIni.Year;
                }
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_periodo_Info info)
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
        public bool anularDB(ro_periodo_Info info)
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

        public bool Generar_Periodos(ro_periodo_Info info)
        {
            try
            {
                bool si_grabo = false;
                lista_periodos = new List<ro_periodo_Info>();
                if (info.CodCatalogo == cl_enumeradores.eTipoPeriodoRRHH.MEN.ToString())
                {
                 if(  generarMensual(Convert.ToInt32( info.pe_anio)))
                    {
                        foreach (var item in lista_periodos)
                        {
                            item.IdEmpresa = info.IdEmpresa;
                            item.IdUsuario = info.IdUsuario;
                            si_grabo= odata.guardar_periodos_generadosDB(item);
                        }
                    }
                }
                if (info.CodCatalogo == cl_enumeradores.eTipoPeriodoRRHH.QUINCE.ToString())
                {
                    if (generarQuincenal(Convert.ToInt32(info.pe_anio)))
                    {
                        foreach (var item in lista_periodos)
                        {
                            item.IdEmpresa = info.IdEmpresa;
                            item.IdUsuario = info.IdUsuario;
                            si_grabo = odata.guardar_periodos_generadosDB(item);
                        }
                    }
                }
                if (info.CodCatalogo == cl_enumeradores.eTipoPeriodoRRHH.SEM.ToString())
                {
                    if (generarSemanal(Convert.ToInt32(info.pe_anio)))
                    {
                        foreach (var item in lista_periodos)
                        {
                            item.IdEmpresa = info.IdEmpresa;
                            item.IdUsuario = info.IdUsuario;
                            si_grabo = odata.guardar_periodos_generadosDB(item);
                        }
                    }

                }
                return si_grabo;
            }
            catch (Exception)
            {
                throw;
            }


        }


       private bool generarMensual(int anio)
        {

            try
            {

                int periodo = 12;

                for (int i = 1; i <= periodo; i++)
                {
                    DateTime k = new DateTime(anio, i, 1);
                    int mes = k.Month;

                    
                    int  dayMes = System.DateTime.DaysInMonth(anio, mes);
                    DateTime p = new DateTime(anio, i, 1);
                    DateTime q = new DateTime(anio, i, dayMes);
                    string Id = "";
                    int IdPeriodo = 0;
                    if (i >= 1 && i <= 9)
                    {
                        Id = Convert.ToString(anio) + 0 + mes;
                        IdPeriodo = Convert.ToInt32(Id);
                    }

                    else
                    {
                        Id = Convert.ToString(anio) + mes;
                        IdPeriodo = Convert.ToInt32(Id);
                    }
                    ro_periodo_Info item = new ro_periodo_Info
                    {
                        IdPeriodo = IdPeriodo,
                        pe_anio = anio,
                        pe_mes = mes,
                        pe_FechaIni = p,
                        pe_FechaFin = q,
                        pe_estado = "A",
                    };
                    
                    lista_periodos.Add(item);

                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool generarQuincenal(int anio)
        {

            try
            {
                int periodo = 12;
                for (int i = 1; i <= periodo; i++)
                {
                    DateTime k = new DateTime(anio, i, 1);
                    int mes = k.Month;
                    int dayMes = System.DateTime.DaysInMonth(anio, mes);

                    for (int j = 1; j <= 2; j++)
                    {
                        ro_periodo_Info item = new ro_periodo_Info();
                        if (j == 1)
                        {
                            DateTime p = new DateTime(anio, i, 1);
                            DateTime q = new DateTime(anio, i, 15);
                            string Id = "";
                            int IdPeriodo = 0;
                            if (i >= 1 && i <= 9)
                            {
                                Id = Convert.ToString(anio) + 0 + mes + 0 + j;
                                IdPeriodo = Convert.ToInt32(Id);
                            }
                            else
                            {
                                Id = Convert.ToString(anio) + mes + 0 + j;
                                IdPeriodo = Convert.ToInt32(Id);
                            }

                            item.IdPeriodo = IdPeriodo;
                            item.pe_anio = anio;
                            item.pe_mes = mes;
                            item.pe_FechaIni = p;
                            item.pe_FechaFin = q;
                            item.pe_estado = "A";
                            lista_periodos.Add(item);

                        }

                        else
                        {
                            if (j == 2)
                            {
                                DateTime p = new DateTime(anio, i, 16);
                                DateTime q = new DateTime(anio, i, dayMes);
                                string Id = "";
                                int IdPeriodo = 0;
                                if (i >= 1 && i <= 9)
                                {
                                    Id = Convert.ToString(anio) + 0 + mes + 0 + j;
                                    IdPeriodo = Convert.ToInt32(Id);
                                }

                                else
                                {
                                    Id = Convert.ToString(anio) + mes + 0 + j;
                                    IdPeriodo = Convert.ToInt32(Id);
                                }

                                item.IdPeriodo = IdPeriodo;
                                item.pe_anio = anio;
                                item.pe_mes = mes;
                                item.pe_FechaIni = p;
                                item.pe_FechaFin = q;
                                item.pe_estado = "A";
                                lista_periodos.Add(item);
                            }

                        }

                    }

                 

                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private bool generarSemanal(int anio)
        {
            try
            {
                lista_periodos = new List<ro_periodo_Info>();
                DateTime FIniSemana = new DateTime(1900, 1, 1);
                DateTime FFinSemana = new DateTime(1900, 1, 1);

                int IdSemana = 0;
                int IdSemanaAux = 0;

                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;


                DateTime fecha = new DateTime(anio, 1, 1);
                if (Convert.ToString(fecha.DayOfWeek) == "Sunday")
                {
                    fecha = fecha.AddDays(1);
                }
                if (Convert.ToString(fecha.DayOfWeek) == "Saturday")
                {
                    fecha = fecha.AddDays(2);
                }

                //contador dias del año
                int dias;
                // DateTime fechar = new DateTime(aniox, 1, 1);
                int contaDiasAnio = 0;
                for (int i = 1; i <= 12; i++)
                {
                    DateTime fechAnio = new DateTime(anio, i, 1);
                    dias = DateTime.DaysInMonth(fechAnio.Year, fechAnio.Month);
                    contaDiasAnio = contaDiasAnio + dias;
                    //MessageBox.Show("Dias" + dias);
                }
                //contador dias del año  

                FIniSemana = fecha;
                
                int j = 1;
                for (int i = 1; i <= contaDiasAnio; i++)
                {
                    IdSemana = cal.GetWeekOfYear(fecha, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                    

                    if (IdSemanaAux < IdSemana || IdSemanaAux == 0)
                    {
                        int cont = (anio * 1000) + j;
                        ro_periodo_Info item = new ro_periodo_Info
                        {
                            IdPeriodo = cont,
                            pe_anio = anio,
                            pe_mes = fecha.Month,
                            pe_FechaIni = FIniSemana,
                            pe_FechaFin = FIniSemana.AddDays(7 - (int)FIniSemana.DayOfWeek),
                            pe_estado = "A",
                        };
                        lista_periodos.Add(item);
                        // lista.Add("Semana : " + IdSemana + "fecha desde:" + FIniSemana.ToString() + "   Hasta:" + FFinSemana.ToString());
                        FIniSemana = item.pe_FechaFin.AddDays(1);
                        IdSemanaAux = IdSemana;
                        j = j + 1;
                    }
                    fecha = fecha.AddDays(1);
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
