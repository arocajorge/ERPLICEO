using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using DevExpress.Web;

namespace Core.Erp.Data.RRHH
{
   public class ro_division_Data
    {
        public List<ro_division_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_division_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.ro_Division
                             where q.IdEmpresa == IdEmpresa
                             select new ro_division_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdDivision = q.IdDivision,
                                 Descripcion = q.Descripcion,
                                 estado = q.estado,

                                 EstadoBool = q.estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.ro_Division
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado=="A"
                                 select new ro_division_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdDivision = q.IdDivision,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_division_Info get_info(int IdEmpresa, int IdDivision)
        {
            try
            {
                ro_division_Info info = new ro_division_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Division Entity = Context.ro_Division.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdDivision == IdDivision);
                    if (Entity == null) return null;

                    info = new ro_division_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDivision = Entity.IdDivision,
                        Descripcion = Entity.Descripcion,
                        estado = Entity.estado,
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
                    var lst = from q in Context.ro_Division
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdDivision) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_division_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Division Entity = new ro_Division
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdDivision = info.IdDivision = get_id(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        estado = info.estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_Division.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_division_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Division Entity = Context.ro_Division.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDivision == info.IdDivision);
                    if (Entity == null)
                        return false;
                    Entity.Descripcion = info.Descripcion;
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

        public bool anularDB(ro_division_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Division Entity = Context.ro_Division.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDivision == info.IdDivision);
                    if (Entity == null)
                        return false;
                    Entity.estado = info.estado = "I";

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

        public List<ro_division_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool estado)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ro_division_Info> Lista = new List<ro_division_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, estado);
            return Lista;
        }

        public List<ro_division_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarAnulados)
        {
            try
            {
                List<ro_division_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (MostrarAnulados)
                        Lista = (from q in Context.ro_Division
                                 where q.IdEmpresa == IdEmpresa
                                 && (q.IdDivision.ToString() + " " + q.Descripcion).Contains(filter)
                                 select new ro_division_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdDivision = q.IdDivision,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdDivision)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                    else
                        Lista = (from q in Context.ro_Division
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado == "A"
                                 && (q.IdDivision.ToString() + " " + q.Descripcion).Contains(filter)
                                 select new ro_division_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdDivision = q.IdDivision,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdDivision)
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

        public ro_division_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            return get_info(IdEmpresa, args.Value == null ? 0 : (int)args.Value);
        }

    }
}
