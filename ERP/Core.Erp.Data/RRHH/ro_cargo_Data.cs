using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;

namespace Core.Erp.Data.RRHH
{
  public  class ro_cargo_Data
    {
        public List<ro_cargo_Info> get_list(int IdEmpresa, bool  mostrar_anulados)
        {
            try
            {
                List<ro_cargo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.ro_cargo
                             where q.IdEmpresa == IdEmpresa
                             select new ro_cargo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCargo = q.IdCargo,
                                 ca_descripcion = q.ca_descripcion,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.ro_cargo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado=="A"
                                 select new ro_cargo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCargo = q.IdCargo,
                                     ca_descripcion = q.ca_descripcion,
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
        public ro_cargo_Info get_info(int IdEmpresa, int IdCargo)
        {
            try
            {
                ro_cargo_Info info = new ro_cargo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargo Entity = Context.ro_cargo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCargo == IdCargo);
                    if (Entity == null) return null;

                    info = new ro_cargo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCargo = Entity.IdCargo,
                        ca_descripcion = Entity.ca_descripcion,
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
        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    var lst = from q in Context.ro_cargo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCargo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_cargo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargo Entity = new ro_cargo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCargo = info.IdCargo = get_id(info.IdEmpresa),
                        ca_descripcion = info.ca_descripcion,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_cargo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_cargo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargo Entity = Context.ro_cargo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCargo == info.IdCargo);
                    if (Entity == null)
                        return false;
                    Entity.ca_descripcion = info.ca_descripcion;
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
        public bool anularDB(ro_cargo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_cargo Entity = Context.ro_cargo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCargo == info.IdCargo);
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
