using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_horario_planificacion_Data
    {
        public List<ro_horario_planificacion_Info> get_list(int IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                List<ro_horario_planificacion_Info> Lista;
                DateTime fi = FechaInicio.Date;
                DateTime ff = FechaFin.Date;
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    Lista = (from q in Context.ro_horario_planificacion
                             where q.IdEmpresa == IdEmpresa
                             && q.FechaInicio>=fi&&
                             q.FechaInicio<=ff
                             select new ro_horario_planificacion_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdPlanificacion = q.IdPlanificacion,
                                 Observacion = q.Observacion,
                                 Estado = q.Estado,
                                 FechaInicio=q.FechaInicio,
                                 FechaFin=q.FechaFin,
                                 IdNomina=q.IdNomina,

                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();
                  
                }

                return Lista;
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
                ro_horario_planificacion_Info info = new ro_horario_planificacion_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario_planificacion Entity = Context.ro_horario_planificacion.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdPlanificacion == IdPlanificacion);
                    if (Entity == null) return null;

                    info = new ro_horario_planificacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPlanificacion = Entity.IdPlanificacion,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado,
                        FechaInicio = Entity.FechaInicio,
                        FechaFin = Entity.FechaFin,
                        IdNomina = Entity.IdNomina
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
                    var lst = from q in Context.ro_horario_planificacion
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPlanificacion) + 1;
                }

                return ID;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario_planificacion Entity = new ro_horario_planificacion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNomina=info.IdNomina,
                        IdPlanificacion = info.IdPlanificacion = get_id(info.IdEmpresa),
                        Observacion = info.Observacion,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        FechaInicio=info.FechaInicio,
                        FechaFin=info.FechaFin,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_horario_planificacion.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario_planificacion Entity = Context.ro_horario_planificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPlanificacion == info.IdPlanificacion);
                    if (Entity == null)
                        return false;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = info.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
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
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_horario_planificacion Entity = Context.ro_horario_planificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPlanificacion == info.IdPlanificacion);
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
    }
}
