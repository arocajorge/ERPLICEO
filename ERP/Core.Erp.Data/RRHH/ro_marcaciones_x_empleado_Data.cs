using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;

namespace Core.Erp.Data.RRHH
{
   public class ro_marcaciones_x_empleado_Data
    {
        public List<ro_marcaciones_x_empleado_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ro_marcaciones_x_empleado_Info> Lista;
              DateTime fi = FechaInicio.Date;
                DateTime ff = FechaFin.Date;
                using (Entities_rrhh Context = new Entities_rrhh())
                {

                    Lista = (from q in Context.vwro_marcaciones_x_empleado
                            where q.IdEmpresa==IdEmpresa
                            && q.es_fechaRegistro>=fi
                            && q.es_fechaRegistro<=ff
                             select new ro_marcaciones_x_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 IdRegistro=q.IdRegistro,
                                 IdTipoMarcaciones = q.IdTipoMarcaciones,
                                 es_Hora = q.es_Hora,
                                 es_fechaRegistro = q.es_fechaRegistro,
                                
                                 Observacion = q.Observacion,
                                 IdUsuario = q.IdUsuario,
                                
                                 Estado = q.Estado,
                                 ca_descripcion=q.ma_descripcion,
                                 cargo=q.cargo,
                                 pe_NombreCompleato=q.pe_apellido+" "+q.pe_nombre,
                                 pe_cedula=q.pe_cedulaRuc,
                                 em_codigo=q.em_codigo,

                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();


                    return Lista;
                }
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
                ro_marcaciones_x_empleado_Info info = new ro_marcaciones_x_empleado_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_marcaciones_x_empleado Entity = Context.ro_marcaciones_x_empleado.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado==IdEmpleado&& q.IdRegistro == IdRegistro);
                    if (Entity == null) return null;

                    info = new ro_marcaciones_x_empleado_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEmpleado = Entity.IdEmpleado,
                        IdCalendadrio=Entity.IdCalendadrio,
                        IdTipoMarcaciones = Entity.IdTipoMarcaciones,
                        IdNomina = Entity.IdNomina,
                        es_Hora = Entity.es_Hora,
                        es_fechaRegistro = Entity.es_fechaRegistro,
                        Observacion = Entity.Observacion,
                        IdUsuario = Entity.IdUsuario,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_marcaciones_x_empleado
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdRegistro) + 1;
                }

                return ID;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_marcaciones_x_empleado Entity = new ro_marcaciones_x_empleado
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRegistro=info.IdRegistro=get_id(info.IdEmpresa),
                        IdEmpleado = info.IdEmpleado,
                        IdCalendadrio = info.IdCalendadrio,
                        IdTipoMarcaciones = info.IdTipoMarcaciones,
                        IdNomina = info.IdNomina,
                        es_Hora = info.es_Hora,
                        es_fechaRegistro = info.es_fechaRegistro.Date,
                        
                        Observacion = info.Observacion,
                        IdUsuario = info.IdUsuario,
                        Estado = info.Estado = "A",
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_marcaciones_x_empleado.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
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
                decimal IdRegistro = get_id(IdEmpresa);
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    foreach (var item in lista)
                    {
                        ro_marcaciones_x_empleado Entity = new ro_marcaciones_x_empleado
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdRegistro = item.IdRegistro = IdRegistro,
                            
                            IdEmpleado = item.IdEmpleado,
                            IdCalendadrio = item.IdCalendadrio,
                            IdTipoMarcaciones = item.IdTipoMarcaciones,
                            IdNomina = item.IdNomina,
                            es_Hora = item.es_Hora,
                            es_fechaRegistro = item.es_fechaRegistro.Date,
                            
                            Observacion = item.Observacion,
                            IdUsuario = item.IdUsuario,
                            Estado = item.Estado = "A",
                            Fecha_Transac = item.Fecha_Transac = DateTime.Now
                        };
                        IdRegistro++;
                       if( !si_existe(item))
                        Context.ro_marcaciones_x_empleado.Add(Entity);
                    }
                    Context.SaveChanges();
                }
                return true;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_marcaciones_x_empleado Entity = Context.ro_marcaciones_x_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRegistro == info.IdRegistro);
                    if (Entity == null)
                        return false;
                        Entity.IdEmpleado = info.IdEmpleado;
                        Entity.IdCalendadrio = info.IdCalendadrio;
                        Entity.IdTipoMarcaciones = info.IdTipoMarcaciones;
                        Entity.IdNomina = info.IdNomina;
                        Entity.es_Hora = info.es_Hora;
                        Entity.es_fechaRegistro = info.es_fechaRegistro.Date;
                        Entity.Observacion = info.Observacion;
                        Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                        Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                        Context.SaveChanges();
                }

                return true;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_marcaciones_x_empleado Entity = Context.ro_marcaciones_x_empleado.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRegistro == info.IdRegistro);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool si_existe(ro_marcaciones_x_empleado_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_marcaciones_x_empleado
                              where q.IdEmpresa == info.IdEmpresa
                              && q.IdEmpleado==info.IdEmpleado
                              && q.IdTipoMarcaciones==info.IdTipoMarcaciones
                              && q.IdCalendadrio==info.IdCalendadrio
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
