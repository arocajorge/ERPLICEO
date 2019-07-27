using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;

namespace Core.Erp.Data.RRHH
{
   public class ro_nomina_x_horas_extras_det_Data
    {
        public List<ro_nomina_x_horas_extras_det_Info> get_list(int IdEmpresa, int IdHorasExtras)
        {
            try
            {
                List<ro_nomina_x_horas_extras_det_Info> Lista;
                int secuen = 0;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    
                        Lista = (from q in Context.vwro_nomina_x_horas_extras_det
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdHorasExtras == IdHorasExtras

                                 select new ro_nomina_x_horas_extras_det_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdHorasExtras = q.IdHorasExtras,
                                     IdEmpleado = q.IdEmpleado,
                                     IdCalendario=q.IdCalendario,
                                     IdTurno = q.IdTurno,
                                     IdHorario = q.IdHorario,
                                     FechaRegistro = q.FechaRegistro,
                                     time_entrada1 = q.time_entrada1,
                                     time_entrada2 = q.time_entrada2,
                                     time_salida1 = q.time_salida1,
                                     time_salida2 = q.time_salida2,
                                     hora_extra25 = q.hora_extra25,
                                     hora_extra50 = q.hora_extra50,
                                     hora_extra100 = q.hora_extra100,
                                     Valor25=q.Valor25,
                                     Valor50=q.Valor50,
                                     Valor100=q.Valor100,
                                     Sueldo_base=q.Sueldo_base,
                                     hora_atraso = q.hora_atraso,
                                     hora_temprano = q.hora_temprano,
                                     hora_trabajada = q.hora_trabajada,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_nombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                     ca_descripcion = q.ca_descripcion,

                                 }).ToList();
                   
                }
                Lista.ForEach(v => v.Secuencia = secuen++);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_nomina_x_horas_extras_det_Info> get_list_x_extado_aprobacion(int IdEmpresa, decimal IdHorasExtras, bool estado_aprobacion)
        {
            try
            {
                List<ro_nomina_x_horas_extras_det_Info> Lista;
                int secuen = 0;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    
                        Lista = (from q in Context.vwro_nomina_x_horas_extras_det
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdHorasExtras == IdHorasExtras
                                 && q.es_HorasExtrasAutorizadas == estado_aprobacion
                                 && q.hora_trabajada>0
                                 select new ro_nomina_x_horas_extras_det_Info
                                 {

                                     IdEmpresa = q.IdEmpresa,
                                     IdHorasExtras = q.IdHorasExtras,
                                     IdTurno = q.IdTurno,
                                     IdHorario = q.IdHorario,
                                     FechaRegistro = q.FechaRegistro,
                                     time_entrada1 = q.time_entrada1,
                                     time_entrada2 = q.time_entrada2,
                                     time_salida1 = q.time_salida1,
                                     time_salida2 = q.time_salida2,
                                     hora_extra25 = q.hora_extra25,
                                     hora_extra50 = q.hora_extra50,
                                     hora_extra100 = q.hora_extra100,
                                     hora_atraso = q.hora_atraso,
                                     hora_temprano = q.hora_temprano,
                                     hora_trabajada = q.hora_trabajada,
                                     IdEmpleado = q.IdEmpleado,
                                     pe_cedulaRuc = q.pe_cedulaRuc,
                                     pe_nombreCompleto = q.pe_apellido + " " + q.pe_nombre,
                                     ca_descripcion = q.ca_descripcion,

                                 }).ToList();
                    }
                
                Lista.ForEach(v => v.Secuencia = secuen++);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_nomina_x_horas_extras_det_Info get_info(int IdEmpresa, decimal IdHorasExtras)
        {
            try
            {
                ro_nomina_x_horas_extras_det_Info info = new ro_nomina_x_horas_extras_det_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras_det Entity = Context.ro_nomina_x_horas_extras_det.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdHorasExtras == IdHorasExtras);
                    if (Entity == null) return null;

                    info = new ro_nomina_x_horas_extras_det_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdHorasExtras = Entity.IdHorasExtras,
                        IdTurno = Entity.IdTurno,
                        IdHorario = Entity.IdHorario,
                        FechaRegistro = Entity.FechaRegistro,
                        time_entrada1 = Entity.time_entrada1,
                        time_entrada2 = Entity.time_entrada2,
                        time_salida1 = Entity.time_salida1,
                        time_salida2 = Entity.time_salida2,
                        hora_extra25 = Entity.hora_extra25,
                        hora_extra50 = Entity.hora_extra50,
                        hora_extra100 = Entity.hora_extra100,
                        hora_atraso = Entity.hora_atraso,
                        hora_temprano = Entity.hora_temprano,
                        hora_trabajada = Entity.hora_trabajada,
                        IdEmpleado = Entity.IdEmpleado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(int IdEmpresa, int IdHorasExtras, List<ro_nomina_x_horas_extras_det> lista)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Context.Database.ExecuteSqlCommand("delete ro_nomina_x_horas_extras_det where IdEmpresa='" + IdEmpresa + "' and IdHorasExtras='" + IdHorasExtras + "'");
                    Context.ro_nomina_x_horas_extras_det.AddRange(lista);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_nomina_x_horas_extras_det_Info item)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras_det Entity = Context.ro_nomina_x_horas_extras_det.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdEmpleado == item.IdEmpleado && q.IdCalendario == item.IdCalendario && q.IdHorasExtras == item.IdHorasExtras);
                    if (Entity == null)
                        return false;
                    Entity.IdCalendario = item.IdCalendario;
                    Entity.IdTurno = item.IdTurno;
                    Entity.FechaRegistro = item.FechaRegistro;
                    Entity.time_entrada1 = item.time_entrada1;
                    Entity.time_entrada2 = item.time_entrada2;
                    Entity.time_salida1 = item.time_salida1;
                    Entity.time_salida2 = item.time_salida2;
                    Entity.hora_extra25 = item.hora_extra25;
                    Entity.hora_extra50 = item.hora_extra50;
                    Entity.hora_extra100 = item.hora_extra100;
                    Entity.hora_atraso = item.hora_atraso;
                    Entity.hora_temprano = item.hora_temprano;
                    Entity.hora_trabajada = item.hora_trabajada;
                    Context.SaveChanges();
                }

                return true;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_nomina_x_horas_extras_det Entity = Context.ro_nomina_x_horas_extras_det.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEmpleado == info.IdEmpleado && q.IdCalendario == info.IdCalendario && q.IdHorasExtras == info.IdHorasExtras);
                    if (Entity == null)
                        return false;
                    Context.ro_nomina_x_horas_extras_det.Remove(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool si_existe(ro_nomina_x_horas_extras_det_Info info)
        {
            try
            {

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_nomina_x_horas_extras_det
                              where q.IdEmpresa == info.IdEmpresa
                              && q.IdEmpleado == info.IdEmpleado
                              && q.IdCalendario == info.IdCalendario
                              && q.IdHorasExtras==info.IdHorasExtras
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else return false;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_nomina_x_horas_extras_det>get_lis_calcular_horas_extras(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo)
        {
            try
            {
                IEnumerable<ro_nomina_x_horas_extras_det> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {

                   
                    Lista = (from p in Context.ro_nomina_x_horas_extras
                             join q in Context.ro_nomina_x_horas_extras_det
                             on new { p.IdEmpresa, p.IdHorasExtras } equals new { q.IdEmpresa, q.IdHorasExtras }
                             where
                              p.IdEmpresa == IdEmpresa
                             && p.IdNominaTipo == IdNomina
                             && p.IdNominaTipoLiqui == IdNominaTipo
                             && p.IdPeriodo == IdPeriodo

                             select q).ToList();
                }
                return new List<ro_nomina_x_horas_extras_det>(Lista);
            }
            catch (Exception )
            {

                throw;
            }
        }
        public List<ro_nomina_x_horas_extras_det_Info> get_lst_horas_extras_aprobar(int IdEmpresa, int IdNomina, int IdNominaTipo, int IdPeriodo)
        {
            try
            {
                IEnumerable<ro_nomina_x_horas_extras_det_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from p in Context.vwro_nomina_x_horas_extras_aprobadas
                             where
                              p.IdEmpresa == IdEmpresa
                             && p.IdNominaTipo == IdNomina
                             && p.IdNominaTipoLiqui == IdNominaTipo
                             && p.IdPeriodo == IdPeriodo

                             select new ro_nomina_x_horas_extras_det_Info
                             {
                                IdEmpresa=p.IdEmpresa,                               
                                IdEmpleado=p.IdEmpleado,
                                Valor25=p.Valor25,
                                Valor50=p.Valor50,
                                Valor100=p.Valor100,
                                IdSucursal=p.IdSucursal
                             }).ToList();
                }
                return new List<ro_nomina_x_horas_extras_det_Info>(Lista);
            }
            catch (Exception )
            {

                throw;
            }
        }

        public bool Modificar_estado_aprobacion( int IdHorasExtras, int Estado)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Context.Database.ExecuteSqlCommand("Update ro_nomina_x_horas_extras_det set es_HorasExtrasAutorizadas='"+Estado+"' where IdHorasExtras='" + IdHorasExtras+"'");
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
