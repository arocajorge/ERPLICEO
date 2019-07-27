using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core.Erp.Bus.RRHH
{
    public class ro_horario_planificacion_Bus
    {
        ro_horario_planificacion_Data odata = new ro_horario_planificacion_Data();
        ro_horario_planificacion_det_Data odata_det = new ro_horario_planificacion_det_Data();

        List<ro_horario_planificacion_det_Info> lst_planificada = new List<ro_horario_planificacion_det_Info>();
        List<ro_horario_planificacion_det_Info> lst_empleados = new List<ro_horario_planificacion_det_Info>();
        ro_horario_planificacion_Info info = new ro_horario_planificacion_Info();

        public List<ro_horario_planificacion_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
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
        public ro_horario_planificacion_Info get_list(int IdEmpresa, int IdNomina, int IdSucursal, int IdDivision, int IdArea, int IdDepartamento, int IdCargo, decimal IdEmpleado ,DateTime fi, DateTime ff, int IdHorario)
        {
            try
            {
              lst_empleados=  odata_det.get_list(IdEmpresa, IdNomina,IdSucursal,IdDivision,IdArea,IdDepartamento,IdCargo, IdEmpleado);
              return  get_planificacion(fi, ff,IdHorario);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_horario_planificacion_Info get_info(int IdEmpresa, int IdPlanificacion)
        {
            try
            {

                info = odata.get_info(IdEmpresa, IdPlanificacion);
                info.lst_planificacion_det = odata_det.get_list(IdEmpresa, IdPlanificacion);
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_horario_planificacion_Info info)
        {
            try
            {
                if (odata.guardarDB(info))
                    info.lst_planificacion_det.ForEach(var => { var.IdPlanificacion = info.IdPlanificacion;var.IdEmpresa = info.IdEmpresa; });
                  return  odata_det.guardarDB(info.lst_planificacion_det);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_horario_planificacion_Info info)
        {
            try
            {

                if (odata.modificarDB(info))
                {
                    odata_det.anularDB(info);
                }
                    info.lst_planificacion_det.ForEach(var => { var.IdPlanificacion = info.IdPlanificacion; var.IdEmpresa = info.IdEmpresa; var.IdCalendario =Convert.ToInt32(Convert.ToDateTime( var.fecha).ToString("ddMMyyyy")); });
                return odata_det.guardarDB(info.lst_planificacion_det);
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

        private ro_horario_planificacion_Info get_planificacion(DateTime FechaInicio, DateTime FechaFin, int IdHorario)
        {
            try
            {
                ro_horario_planificacion_det_Info info_det;
                int cont = 0;
                int Secuencia = 0;
                int IdCalendadio = 0;
                int num_dias =Convert.ToInt32( (FechaFin - FechaInicio).TotalDays);
                for (int i = 0; i <= num_dias; i++)
                {
                    List<ro_horario_planificacion_det_Info> listmp = (from q in lst_empleados
                                                                      select new ro_horario_planificacion_det_Info
                                                                      {
                                                                          IdCalendario = q.IdCalendario,
                                                                          IdHorario = q.IdHorario,
                                                                          fecha = q.fecha,
                                                                          IdEmpresa = q.IdEmpresa,
                                                                          IdEmpleado = q.IdEmpleado,
                                                                          pe_cedulaRuc = q.pe_cedulaRuc,
                                                                          pe_nombreCompleto = q.pe_nombreCompleto,
                                                                          ca_descripcion = q.ca_descripcion,
                                                                          de_descripcion = q.de_descripcion,
                                                                          di_descripcion = q.di_descripcion,
                                                                          ar_descripcion = q.ar_descripcion,
                                                                          Su_Descripcion = q.Su_Descripcion
                                                                      }).ToList();                    
                    cont++;                  
                    IdCalendadio = Convert.ToInt32(FechaInicio.ToString("ddMMyyyy"));
                    foreach (var item in listmp)
                    {
                        info_det = new ro_horario_planificacion_det_Info();
                        info_det = item;
                        info_det.IdCalendario = IdCalendadio;
                        info_det.IdHorario = IdHorario;
                        info_det.fecha = FechaInicio;
                        info_det.Secuencia = Secuencia++;
                        info.lst_planificacion_det.Add(info_det);
                    }
                    
                    FechaInicio = FechaInicio.AddDays(1);

                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
