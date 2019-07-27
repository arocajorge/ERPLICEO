using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using DevExpress.Web;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_historico_vacaciones_x_empleado_Bus
    {
        ro_empleado_Bus bus_empleado = new ro_empleado_Bus();
        ro_empleado_Info info_empleado = new ro_empleado_Info();
        ro_historico_vacaciones_x_empleado_Data odata = new ro_historico_vacaciones_x_empleado_Data();
        public Boolean GrabarBD(ro_historico_vacaciones_x_empleado_Info info)
        {
            try
            {
                return odata.GrabarBD(info);
            }
            catch (Exception )
            {
                throw;
            }
        }
        public Boolean ModificarBD(ro_historico_vacaciones_x_empleado_Info info)
        {
            try
            {
                return odata.ModificarBD(info);
            }
            catch (Exception )
            {
                throw;
            }
        }
        public List<ro_historico_vacaciones_x_empleado_Info> get_list(int IdEmpresa,decimal IdEmpleado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ro_historico_vacaciones_x_empleado_Info> get_list_periodo_con_saldo(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_list_periodo_con_saldo(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ro_historico_vacaciones_x_empleado_Info> get_list(int IdEmpresa, decimal IdEmpleado, decimal IdSolicitud)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdEmpleado, IdSolicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ro_historico_vacaciones_x_empleado_Info> calcular_vacaciones(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                odata = new ro_historico_vacaciones_x_empleado_Data();               
                string msg = "";
                int IdVacacion = 1;
                info_empleado = bus_empleado.get_info(IdEmpresa, IdEmpleado);
                List<ro_historico_vacaciones_x_empleado_Info> lst_vacaciones = new List<ro_historico_vacaciones_x_empleado_Info>();
                List<ro_historico_vacaciones_x_empleado_Info> listadoTmp = new List<ro_historico_vacaciones_x_empleado_Info>();
                #region empleados gozan quince dias

                if (!info_empleado.GozaMasDeQuinceDiasVaciones)
                {
                    DateTime fechaActual = DateTime.Now;
                    DateTime fechaIngreso = Convert.ToDateTime(info_empleado.em_fechaIngaRol);
                    DateTime fechaNueva = new DateTime();
                    double dias = 0;
                    int meses = 0;
                    int anio = 0;
                    if (info_empleado.em_status == "EST_PLQ")
                    {
                        dias = CalcularDiasDeDiferencia(fechaIngreso, Convert.ToDateTime(info_empleado.em_fechaSalida));
                        meses = CalcularMesesDeDiferencia(fechaIngreso, Convert.ToDateTime(info_empleado.em_fechaSalida));
                        anio = CalcularAniosDeDiferencia(fechaIngreso, Convert.ToDateTime(info_empleado.em_fechaSalida));
                    }
                    else
                    {
                        dias = CalcularDiasDeDiferencia(fechaIngreso, fechaActual);
                        meses = CalcularMesesDeDiferencia(fechaIngreso, fechaActual);
                        anio = CalcularAniosDeDiferencia(fechaIngreso, fechaActual);
                    }
                    int minAnio = 5;
                    int maxDiasGanados = 15;
                    int minDiasGanados = 15;
                    int diasGanados = 0;
                    int contDiasGanados = 0;
                    //VALIDA SI TIENE MAS DE 1 AÑO
                    if (dias > 360)
                    {
                        fechaNueva = fechaIngreso;
                        //RECORRE LA CANTIDAD DE AÑOS QUE TIENE DE SERVICIO
                        for (int i = 0; i < anio; i++)
                        {
                            IdVacacion++;
                            if (i < minAnio)//VALIDA LOS 5 AÑOS BASE
                            {
                                diasGanados = minDiasGanados;
                                contDiasGanados = 0;
                            }
                            else
                            {
                                if (i >= minAnio && contDiasGanados < maxDiasGanados)//VALIDA QUE SOLO ACUMULE 30 DIAS DE VACACIONES A PARTIR DEL 5 AÑO
                                {
                                    contDiasGanados++;
                                    diasGanados = minDiasGanados + contDiasGanados;
                                }
                                else
                                {
                                    diasGanados = 30;//DE AQUI EN ADELANTE TENDREA SOLO 30 DIAS 
                                }
                            }
                            ro_historico_vacaciones_x_empleado_Info info = new ro_historico_vacaciones_x_empleado_Info();
                            info.IdEmpresa = info_empleado.IdEmpresa;
                            info.IdEmpleado = info_empleado.IdEmpleado;
                            info.FechaIni = fechaNueva.AddYears(i);
                            info.FechaFin = info.FechaIni.AddYears(1).AddDays(-1);
                            info.DiasGanado = diasGanados;
                            info.DiasPendientes = diasGanados;
                            info.DiasTomados = 0;
                            info.Descripcion = info.FechaIni.Date.ToString().Substring(0, 10) + " " + info.FechaFin.Date.ToString().Substring(0, 10) + " " + info.DiasGanado.ToString();
                            info.IdVacacion = IdVacacion;
                            info.IdPeriodo_Inicio = Convert.ToInt32(info.FechaIni.ToString("ddMMyyyy"));
                            info.IdPeriodo_Fin = Convert.ToInt32(info.FechaFin.ToString("ddMMyyyy"));

                            lst_vacaciones.Add(info);
                        }
                    }
                    else
                    {
                        ro_historico_vacaciones_x_empleado_Info info = new ro_historico_vacaciones_x_empleado_Info();
                        info.IdEmpresa = info_empleado.IdEmpresa;
                        info.IdEmpleado = info_empleado.IdEmpleado;
                        info.FechaIni = Convert.ToDateTime(info_empleado.em_fechaIngaRol);
                        info.FechaFin = Convert.ToDateTime(Convert.ToDateTime(info_empleado.em_fechaIngaRol).AddYears(1).AddDays(-1));
                        info.DiasGanado = Convert.ToInt32(dias * 15) / 360;
                        info.DiasPendientes = Convert.ToInt32(dias * 15) / 360;
                        info.Descripcion = info.FechaIni.Date.ToString() + " " + info.FechaFin.Date.ToString() + " " + info.DiasGanado.ToString();
                        info.IdVacacion = IdVacacion + 1;
                        info.IdPeriodo_Inicio = Convert.ToInt32(info.FechaIni.ToString("ddMMyyyy"));
                        info.IdPeriodo_Fin = Convert.ToInt32(info.FechaFin.ToString("ddMMyyyy"));
                        lst_vacaciones.Add(info);

                    }

                    foreach (var item in lst_vacaciones)
                    {
                        if (!odata.GetExiste(item, ref msg))
                            odata.GrabarBD(item);
                        else
                        {
                            odata.ModificarBD(item);
                        }
                        item.FechaFin = item.FechaFin.Date;
                        item.FechaIni = item.FechaIni.Date;

                    }
                }
                else
                {

                    DateTime fechaActual = DateTime.Now;
                    DateTime fechaIngreso = Convert.ToDateTime(info_empleado.em_fechaIngaRol);
                    DateTime fechaNueva = new DateTime();
                    double dias = 0;
                    int meses = 0;
                    int anio = 0;
                    if (info_empleado.em_status == "EST_PLQ")
                    {
                        dias = CalcularDiasDeDiferencia(fechaIngreso, Convert.ToDateTime(info_empleado.em_fechaSalida));
                        meses = CalcularMesesDeDiferencia(fechaIngreso, Convert.ToDateTime(info_empleado.em_fechaSalida));
                        anio = CalcularAniosDeDiferencia(fechaIngreso, Convert.ToDateTime(info_empleado.em_fechaSalida));
                    }
                    else
                    {
                        dias = CalcularDiasDeDiferencia(fechaIngreso, fechaActual);
                        meses = CalcularMesesDeDiferencia(fechaIngreso, fechaActual);
                        anio = CalcularAniosDeDiferencia(fechaIngreso, fechaActual);
                    }
                    int minAnio = 5;
                    int maxDiasGanados =Convert.ToInt32( info_empleado.DiasVacaciones);
                    int minDiasGanados = Convert.ToInt32(info_empleado.DiasVacaciones);
                    int diasGanados = 0;
                    int contDiasGanados = 0;
                    //VALIDA SI TIENE MAS DE 1 AÑO
                    if (dias > 180)
                    {
                        fechaNueva = fechaIngreso;
                        //RECORRE LA CANTIDAD DE AÑOS QUE TIENE DE SERVICIO
                        for (int i = 0; i < anio; i++)
                        {
                            IdVacacion++;
                            if (i < minAnio)//VALIDA LOS 5 AÑOS BASE
                            {
                                diasGanados = minDiasGanados;
                                contDiasGanados = 0;
                            }
                            else
                            {
                                if (i >= minAnio && contDiasGanados < maxDiasGanados)//VALIDA QUE SOLO ACUMULE 30 DIAS DE VACACIONES A PARTIR DEL 5 AÑO
                                {
                                    contDiasGanados++;
                                    diasGanados = minDiasGanados + contDiasGanados;
                                }
                                else
                                {
                                    diasGanados = 30;//DE AQUI EN ADELANTE TENDREA SOLO 30 DIAS 
                                }
                            }
                            ro_historico_vacaciones_x_empleado_Info info = new ro_historico_vacaciones_x_empleado_Info();
                            info.IdEmpresa = info_empleado.IdEmpresa;
                            info.IdEmpleado = info_empleado.IdEmpleado;
                            info.FechaIni = fechaNueva.AddYears(i);
                            info.FechaFin = info.FechaIni.AddYears(1).AddDays(-1);
                            info.DiasGanado = diasGanados;
                            info.DiasPendientes = diasGanados;
                            info.DiasTomados = 0;
                            info.Descripcion = info.FechaIni.Date.ToString().Substring(0, 10) + " " + info.FechaFin.Date.ToString().Substring(0, 10) + " " + info.DiasGanado.ToString();
                            info.IdVacacion = IdVacacion;
                            info.IdPeriodo_Inicio = Convert.ToInt32(info.FechaIni.ToString("ddMMyyyy"));
                            info.IdPeriodo_Fin = Convert.ToInt32(info.FechaFin.ToString("ddMMyyyy"));

                            lst_vacaciones.Add(info);
                        }
                    }
                    else
                    {
                        ro_historico_vacaciones_x_empleado_Info info = new ro_historico_vacaciones_x_empleado_Info();
                        info.IdEmpresa = info_empleado.IdEmpresa;
                        info.IdEmpleado = info_empleado.IdEmpleado;
                        info.FechaIni = Convert.ToDateTime(info_empleado.em_fechaIngaRol);
                        info.FechaFin = Convert.ToDateTime(Convert.ToDateTime(info_empleado.em_fechaIngaRol).AddYears(1).AddDays(-1));
                        info.DiasGanado = Convert.ToInt32(dias* info_empleado.DiasVacaciones) / 360;
                        info.DiasPendientes = Convert.ToInt32(dias * info_empleado.DiasVacaciones) / 360;
                        info.Descripcion = info.FechaIni.Date.ToString() + " " + info.FechaFin.Date.ToString() + " " + info.DiasGanado.ToString();
                        info.IdVacacion = IdVacacion + 1;
                        info.IdPeriodo_Inicio = Convert.ToInt32(info.FechaIni.ToString("ddMMyyyy"));
                        info.IdPeriodo_Fin = Convert.ToInt32(info.FechaFin.ToString("ddMMyyyy"));
                        lst_vacaciones.Add(info);

                    }

                    foreach (var item in lst_vacaciones)
                    {
                        if (!odata.GetExiste(item, ref msg))
                            odata.GrabarBD(item);
                        else
                        {
                            odata.ModificarBD(item);
                        }
                        item.FechaFin = item.FechaFin.Date;
                        item.FechaIni = item.FechaIni.Date;

                    }
                }
                #endregion

                return lst_vacaciones;
            }
            catch (Exception )
            {
                throw;
            }

        }
        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
            }
            catch (Exception )
            {
                throw;
            }

        }
        public double CalcularDiasDeDiferencia(DateTime primerFecha, DateTime segundaFecha)
        {
            try
            {
                TimeSpan diferencia;
                diferencia = primerFecha - segundaFecha;

                return Math.Abs(diferencia.Days);
            }
            catch (Exception )
            {
                throw;
            }

        }
        public int CalcularAniosDeDiferencia(DateTime primerFecha, DateTime segundaFecha)
        {
            try
            {
                return Math.Abs(primerFecha.Year - segundaFecha.Year);
            }
            catch (Exception )
            {
                throw;
            }

        }

    }
}
