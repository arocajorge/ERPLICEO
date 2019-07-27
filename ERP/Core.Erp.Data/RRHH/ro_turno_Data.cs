using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
namespace Core.Erp.Data.RRHH
{
   public class ro_turno_Data
    {
        public List<ro_turno_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_turno_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_turno
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_turno_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTurno = q.IdTurno,
                                     tu_descripcion = q.tu_descripcion,
                                     Lunes=q.Lunes,
                                     Martes=q.Martes,
                                     Miercoles=q.Miercoles,
                                     Jueves=q.Jueves,
                                     Viernes=q.Viernes,
                                     Sabado=q.Sabado,
                                     Domingo=q.Domingo,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_turno
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 select new ro_turno_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTurno = q.IdTurno,
                                     tu_descripcion = q.tu_descripcion,
                                     Lunes = q.Lunes,
                                     Martes = q.Martes,
                                     Miercoles = q.Miercoles,
                                     Jueves = q.Jueves,
                                     Viernes = q.Viernes,
                                     Sabado = q.Sabado,
                                     Domingo = q.Domingo,
                                     Estado = q.Estado,

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
        public ro_turno_Info get_info(int IdEmpresa, int IdTurno)
        {
            try
            {
                ro_turno_Info info = new ro_turno_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_turno Entity = Context.ro_turno.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTurno == IdTurno);
                    if (Entity == null) return null;

                    info = new ro_turno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTurno = Entity.IdTurno,
                        tu_descripcion = Entity.tu_descripcion,
                        Lunes=Entity.Lunes,
                        Martes = Entity.Martes,
                        Miercoles = Entity.Miercoles,
                        Jueves = Entity.Jueves,
                        Viernes=Entity.Viernes,
                        Sabado = Entity.Sabado,
                        Domingo = Entity.Domingo,
                        Estado = Entity.Estado,
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
                    var lst = from q in Context.ro_turno
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTurno) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_turno_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_turno Entity = new ro_turno
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTurno = info.IdTurno=get_id(info.IdEmpresa),
                        tu_descripcion = info.tu_descripcion,
                        Lunes = info.Lunes,
                        Martes = info.Martes,
                        Miercoles = info.Miercoles,
                        Jueves = info.Jueves,
                        Viernes=info.Viernes,
                        Sabado = info.Sabado,
                        Domingo = info.Domingo,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_turno.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_turno_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_turno Entity = Context.ro_turno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTurno == info.IdTurno);
                    if (Entity == null)
                        return false;
                    Entity.tu_descripcion = info.tu_descripcion;
                    Entity.Lunes = info.Lunes;
                    Entity.Martes = info.Martes;
                    Entity.Miercoles = info.Miercoles;
                    Entity.Jueves = info.Jueves;
                    Entity.Viernes = info.Viernes;
                    Entity.Sabado = info.Sabado;
                    Entity.Domingo = info.Domingo;
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
        public bool anularDB(ro_turno_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_turno Entity = Context.ro_turno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTurno == info.IdTurno);
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

