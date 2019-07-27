using Core.Erp.Data.RRHH;
using Core.Erp.Info.Helps;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public class ro_participacion_utilidad_empleado_Bus
    {
        ro_participacion_utilidad_empleado_Data odata = new ro_participacion_utilidad_empleado_Data();
        List<ro_participacion_utilidad_empleado_Info> lista = new List<ro_participacion_utilidad_empleado_Info>();
        ro_contrato_Bus bus_contrato = new ro_contrato_Bus();
        List<ro_contrato_Info> lista_contratos = new List<ro_contrato_Info>();
        ro_periodo_Bus bus_periodo = new ro_periodo_Bus();

        public List<ro_participacion_utilidad_empleado_Info> get_list(int IdEmpresa, int IdUtilidad)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdUtilidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_participacion_utilidad_empleado_Info> get_list(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNomina,IdNominaTipo,IdPeriodo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_participacion_utilidad_empleado_Info> calcular(int IdEmpresa, int IdNomina, int IdPeriodo, double valorIndividual, double valorCarga)
        {
            try
            {
                ro_periodo_Info info_periodo = bus_periodo.get_info(IdEmpresa, IdPeriodo);
                DateTime FechaInicio = info_periodo.pe_FechaIni;
                DateTime FechaFin = info_periodo.pe_FechaFin;
                double factorB = 0;
                lista= odata.get_list(IdEmpresa, IdNomina, FechaInicio, FechaFin);
                int DiasTrabajados = 0;
                foreach (var item in lista)// calculando dias trabajados
                {
                   if(item.num_contratos==1)// si el empleado tiene un solo contrato dentro del año
                    {
                        item.DiasTrabajados = Dias_trabajados_x_un_contrato(item, FechaInicio, FechaFin);
                    }
                   else// traeigo todos los contratos que tenga el empleado en ese año activos y liquidados
                    {
                        lista_contratos = bus_contrato.get_list(item.IdEmpresa,item.IdEmpleado, FechaInicio,FechaFin);
                        item.DiasTrabajados = Dias_trabajados_x_n_contrato(lista_contratos, FechaInicio, FechaFin);
                    }
                   
                }
                DiasTrabajados = Convert.ToInt32(lista.Sum(v => v.DiasTrabajados));// sumar el total de los dias trabajados
                foreach (var item in lista)// calculando las utilidades individual
                {
                    item.ValorIndividual = ((valorIndividual/DiasTrabajados)*item.DiasTrabajados);
                    if (item.CargasFamiliares > 0)
                    {
                        item.FactorA = item.CargasFamiliares * item.DiasTrabajados;
                        factorB = factorB + item.FactorA; 
                    }
                    item.ValorCargaFamiliar = Math.Round(item.ValorCargaFamiliar,2);
                }
                    foreach (var item in lista)// calculando valor por carga
                    {
                    if(factorB > 0)
                        item.ValorCargaFamiliar = (valorCarga * (item.FactorA / factorB));
                    item.ValorTotal = item.ValorIndividual + item.ValorCargaFamiliar;
                    item.ValorTotal = Math.Round(item.ValorTotal, 2);
                    item.ValorIndividual = Math.Round(item.ValorIndividual, 2);

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<ro_participacion_utilidad_empleado_Info> lista)
        {
            try
            {
                return odata.guardarDB(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }     
        public bool anularDB(int IdEmpresa, int IdUtilidad)
        {
            try
            {
                return odata.anularDB(IdEmpresa,IdUtilidad);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private int Dias_trabajados_x_un_contrato(ro_participacion_utilidad_empleado_Info info, DateTime Fi, DateTime Ff)
        {
            int meses = 0;
            int diaIngresos = 0;
            int DiasSalida = 0;
            int totaldias = 0;
            info.em_fechaIngaRol = Convert.ToDateTime(info.em_fechaIngaRol).Date;
            try
            {
               
                if (info.em_status != cl_enumeradores.eEstadoEmpleadoRRHH.EST_LIQ.ToString() & info.em_status != cl_enumeradores.eEstadoEmpleadoRRHH.EST_PLQ.ToString())
                {
                    if (info.em_fechaIngaRol <= Fi)
                    {
                        totaldias = 360;
                    }
                    else
                    {
                        //info.InfoPersona.pe_nombreCompleto
                        if (info.em_fechaIngaRol > Fi)
                        {
                            diaIngresos = 31 - Convert.ToDateTime(info.em_fechaIngaRol).Day;
                            meses = (Ff.Month - Convert.ToDateTime(info.em_fechaIngaRol).Month);
                            totaldias = diaIngresos + (meses * 30);
                        }
                        else
                        {

                        }

                    }
                }
                else
                {
                    info.em_fechaSalida = Convert.ToDateTime(info.em_fechaSalida).Date;
                    if (info.em_fechaSalida >= Ff)
                    {
                        if (info.em_fechaIngaRol < Fi)
                            totaldias = 360;
                        else
                        {
                            diaIngresos = 31 - Convert.ToDateTime(info.em_fechaIngaRol).Day;
                            meses = (Convert.ToDateTime(Ff).Month - Convert.ToDateTime(info.em_fechaIngaRol).Month);
                            totaldias = (diaIngresos + DiasSalida) + (meses * 30);
                        }
                    }
                    else
                    {
                        //info.InfoPersona.pe_nombreCompleto
                        if (info.em_fechaIngaRol < Fi)
                        {
                            DiasSalida = Convert.ToDateTime(info.em_fechaSalida).Day;
                            meses = (Convert.ToDateTime(info.em_fechaSalida).Month - Fi.Month);
                            totaldias = (diaIngresos + DiasSalida) + (meses * 30);
                        }
                        else
                        {
                            if ((Convert.ToDateTime(info.em_fechaSalida).Month != Convert.ToDateTime(info.em_fechaIngaRol).Month))
                            {
                                diaIngresos = 31 - Convert.ToDateTime(info.em_fechaIngaRol).Day;
                                DiasSalida = Convert.ToDateTime(info.em_fechaSalida).Day;
                                meses = (Convert.ToDateTime(info.em_fechaSalida).Month - Convert.ToDateTime(info.em_fechaIngaRol).Month) - 1;
                                totaldias = (diaIngresos + DiasSalida) + (meses * 30);
                            }
                            else
                            {
                                totaldias = Convert.ToDateTime(info.em_fechaSalida).Day - Convert.ToDateTime(info.em_fechaIngaRol).Day + 1;

                            }
                        }
                    }
                }

                return totaldias;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private int Dias_trabajados_x_n_contrato(List<ro_contrato_Info> lista, DateTime Fi, DateTime Ff)
        {          
            int totaldias = 0;
            try
            {
                foreach (var info in lista)
                {
                    int meses = 0;
                    int diaIngresos = 0;
                    int DiasSalida = 0;
                    info.FechaInicio = Convert.ToDateTime(info.FechaInicio).Date;

                    if (info.EstadoContrato == cl_enumeradores.eEstadoContratoRRHH.ECT_ACT.ToString())
                    {
                        if (info.FechaInicio <= Fi)
                        {
                            totaldias = 360;
                        }
                        else
                        {
                            //info.InfoPersona.pe_nombreCompleto
                            if (info.FechaInicio > Fi)
                            {
                                diaIngresos = 31 - Convert.ToDateTime(info.FechaInicio).Day;
                                meses = (Ff.Month - Convert.ToDateTime(info.FechaInicio).Month);
                                totaldias = diaIngresos + (meses * 30);
                            }
                            else
                            {

                            }

                        }
                    }
                    else
                    {
                        info.FechaFin = Convert.ToDateTime(info.FechaFin).Date;
                        if (info.FechaFin >= Ff)
                        {
                            if (info.FechaInicio < Fi)
                                totaldias = 360;
                            else
                            {
                                diaIngresos = 31 - Convert.ToDateTime(info.FechaInicio).Day;
                                meses = (Convert.ToDateTime(Ff).Month - Convert.ToDateTime(info.FechaInicio).Month);
                                totaldias = (diaIngresos + DiasSalida) + (meses * 30);
                            }
                        }
                        else
                        {
                            //info.InfoPersona.pe_nombreCompleto
                            if (info.FechaInicio < Fi)
                            {
                                DiasSalida = Convert.ToDateTime(info.FechaFin).Day;
                                meses = (Convert.ToDateTime(info.FechaFin).Month - Fi.Month);
                                totaldias = (diaIngresos + DiasSalida) + (meses * 30);
                            }
                            else
                            {
                                if ((Convert.ToDateTime(info.FechaFin).Month != Convert.ToDateTime(info.FechaInicio).Month))
                                {
                                    diaIngresos = 31 - Convert.ToDateTime(info.FechaInicio).Day;
                                    DiasSalida = Convert.ToDateTime(info.FechaFin).Day;
                                    meses = (Convert.ToDateTime(info.FechaFin).Month - Convert.ToDateTime(info.FechaInicio).Month) - 1;
                                    totaldias = (diaIngresos + DiasSalida) + (meses * 30);
                                }
                                else
                                {
                                    totaldias = Convert.ToDateTime(info.FechaFin).Day - Convert.ToDateTime(info.FechaInicio).Day + 1;

                                }
                            }
                        }
                    }
                    totaldias = totaldias + totaldias;
                }

                return totaldias;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
