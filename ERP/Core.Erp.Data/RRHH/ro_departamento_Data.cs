using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using DevExpress.Web;

namespace Core.Erp.Data.RRHH
{
  public  class ro_departamento_Data
    {
        public List<ro_departamento_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_departamento_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.ro_Departamento
                             where q.IdEmpresa == IdEmpresa
                             select new ro_departamento_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdDepartamento = q.IdDepartamento,
                                 de_descripcion = q.de_descripcion,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.ro_Departamento
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado=="A"
                                 select new ro_departamento_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdDepartamento = q.IdDepartamento,
                                     de_descripcion = q.de_descripcion,
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
        public ro_departamento_Info get_info(int IdEmpresa, int IdDepartamento)
        {
            try
            {
                ro_departamento_Info info = new ro_departamento_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Departamento Entity = Context.ro_Departamento.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdDepartamento == IdDepartamento);
                    if (Entity == null) return null;

                    info = new ro_departamento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDepartamento = Entity.IdDepartamento,
                        de_descripcion = Entity.de_descripcion,
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
                    var lst = from q in Context.ro_Departamento
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdDepartamento) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_departamento_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Departamento Entity = new ro_Departamento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdDepartamento = info.IdDepartamento = get_id(info.IdEmpresa),
                        de_descripcion = info.de_descripcion,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac =  DateTime.Now
                    };
                    Context.ro_Departamento.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_departamento_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Departamento Entity = Context.ro_Departamento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDepartamento == info.IdDepartamento);
                    if (Entity == null)
                        return false;
                    Entity.de_descripcion = info.de_descripcion;
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

        public bool anularDB(ro_departamento_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Departamento Entity = Context.ro_Departamento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDepartamento == info.IdDepartamento);
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

        public List<ro_departamento_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool estado)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ro_departamento_Info> Lista = new List<ro_departamento_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, estado);
            return Lista;
        }

        public List<ro_departamento_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarAnulados)
        {
            try
            {
                List<ro_departamento_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (MostrarAnulados)
                        Lista = (from q in Context.ro_Departamento
                                 where q.IdEmpresa == IdEmpresa
                                 && (q.IdDepartamento.ToString() + " " + q.de_descripcion).Contains(filter)
                                 select new ro_departamento_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdDepartamento = q.IdDepartamento,
                                     de_descripcion = q.de_descripcion,
                                     Estado = q.Estado
                                 })
                                 .OrderBy(p => p.IdDepartamento)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                    else
                        Lista = (from q in Context.ro_Departamento
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 && (q.IdDepartamento.ToString() + " " + q.de_descripcion).Contains(filter)
                                 select new ro_departamento_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdDepartamento = q.IdDepartamento,
                                     de_descripcion = q.de_descripcion,
                                     Estado = q.Estado
                                 })
                                 .OrderBy(p => p.IdDepartamento)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ro_departamento_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            return get_info(IdEmpresa, args.Value == null ? 0 : (int)args.Value);
        }
    }
}
