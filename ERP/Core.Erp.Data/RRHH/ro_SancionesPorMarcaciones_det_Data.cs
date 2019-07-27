using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.RRHH
{
   public class ro_SancionesPorMarcaciones_det_Data
    {
       public List<ro_SancionesPorMarcaciones_det_Info> get_list(int IdEmpresa, decimal IdAjuste)
        {
            try
            {
                List<ro_SancionesPorMarcaciones_det_Info> lista;
                using (Entities_rrhh context=new Entities_rrhh())
                {
                    lista = (from q in context.vwro_SancionesPorMarcaciones_det

                             where q.IdEmpresa == IdEmpresa
                             && q.IdAjuste == IdAjuste
                             select new ro_SancionesPorMarcaciones_det_Info
                             {
                                IdEmpresa=q.IdEmpresa,
                                IdAjuste=q.IdAjuste,
                                Secuencia=q.Secuencia,
                                 IdCalendario = q.IdCalendario,
                                 IdEmpleado = q.IdEmpleado,
                                 IdSucursal = q.IdSucursal,
                                 EsHoraIngreso = q.HoraIngreso,
                                 time_entrada1 = q.HoraIngreso,
                                 time_salida1 = q.HoraSalio,
                                 HoraIni = q.EsHoraIngreso,
                                 HoraFin = q.EsHoraSalida,
                                 es_fechaRegistro = q.FechaRegistro,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 em_codigo = q.em_codigo,
                                 Minutos = q.Minutos,
                                 Observacion = q.Observacion,
                                 
                                 seleccionado=true
                             }).ToList();
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ro_SancionesPorMarcaciones_det_Info> get_list(int IdEmpresa, int IdNomina, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                int Secuencia = 1;
                DateTime fi = FechaInicio.Date;
                DateTime ff = FechaFin.Date;
                List<ro_SancionesPorMarcaciones_det_Info> lista;
                using (Entities_rrhh context = new Entities_rrhh())
                {
                    lista = (from q in context.vwro_marcaciones_x_planificacion_horario

                             where q.IdEmpresa == IdEmpresa
                             && q.IdNomina == IdNomina
                             && q.es_fechaRegistro<=ff
                             && q.es_fechaRegistro>=fi
                             select new ro_SancionesPorMarcaciones_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCalendario = q.IdCalendadrio,
                                 IdEmpleado = q.IdEmpleado,
                                 IdSucursal = q.IdSucursal,
                                 es_fechaRegistro = q.es_fechaRegistro,
                                 pe_cedulaRuc = q.pe_cedulaRuc,
                                 pe_apellido = q.pe_apellido,
                                 pe_nombre = q.pe_nombre,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 em_codigo = q.em_codigo,
                                 time_entrada1=q.time_entrada1,
                                 time_salida1=q.time_salida1,
                                 HoraIni=q.HoraIni,
                                 HoraFin=q.HoraFin,
                                 Sueldo=  q.Sueldo
                             }).ToList();
                }

                lista.ForEach(item =>
               {
                   if(item.time_entrada1>item.HoraIni  && item.time_entrada1.TotalHours > 0)
                   {
                       
                       item.Minutos = (item.time_entrada1.TotalMinutes - item.HoraIni.TotalMinutes);
                   }

                   if (item.time_salida1 < item.HoraFin && item.time_salida1.TotalHours > 0)
                   {
                       item.Minutos = item.Minutos+( item.HoraFin.TotalMinutes - item.time_salida1.TotalMinutes);
                   }
                   item.Secuencia = Secuencia;
                   Secuencia++;
               }
                );
                return lista.Where(v=>v.Minutos>0).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
