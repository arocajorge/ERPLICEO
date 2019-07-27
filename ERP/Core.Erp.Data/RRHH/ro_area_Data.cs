using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using DevExpress.Web;

namespace Core.Erp.Data.RRHH
{
   public class ro_area_Data
    {

        public List<ro_area_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_area_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.ro_area
                                 join c in Context.ro_Division
                                 on q.IdDivision equals c.IdDivision
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdEmpresa==c.IdEmpresa
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
                                     IdDivision=q.IdDivision,                                    
                                     Descripcion = q.Descripcion,
                                     Division=c.Descripcion,
                                     estado = q.estado,

                                     EstadoBool = q.estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.ro_area
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado == "A"
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
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

        public List<ro_area_Info> get_list(int IdEmpresa, int IdDivision)
        {
            try
            {
                List<ro_area_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                        
                        Lista = (from q in Context.ro_area
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdDivision==IdDivision
                                 && q.estado == "A"
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado
                                 }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ro_area_Info get_info(int IdEmpresa, int IdArea)
        {
            try
            {
                ro_area_Info info = new ro_area_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_area Entity = Context.ro_area.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdArea == IdArea);
                    if (Entity == null) return null;

                    info = new ro_area_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdArea = Entity.IdArea,
                        Descripcion = Entity.Descripcion,
                        IdDivision = Entity.IdDivision,
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
                    var lst = from q in Context.ro_area
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdArea) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ro_area_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_area Entity = new ro_area
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdDivision = info.IdDivision,
                        IdArea = info.IdArea = get_id(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        estado = info.estado = "A",
                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = info.Fecha_Transac = DateTime.Now
                    };
                    Context.ro_area.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(ro_area_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_area Entity = Context.ro_area.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdArea == info.IdArea);
                    if (Entity == null)
                        return false;
                    Entity.IdDivision = info.IdDivision;
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
        public bool anularDB(ro_area_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_area Entity = Context.ro_area.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdArea == info.IdArea);
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

        public List<ro_area_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool estado, int IdDivision)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ro_area_Info> Lista = new List<ro_area_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, estado, IdDivision);
            return Lista;
        }

        public List<ro_area_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarAnulados, int IdDivision)
        {
            try
            {
                List<ro_area_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (MostrarAnulados)
                        Lista = (from q in Context.ro_area
                                 where q.IdEmpresa == IdEmpresa
                                 &&q.IdDivision == IdDivision
                                 && (q.IdArea.ToString() + " " + q.Descripcion).Contains(filter)
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdArea)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                    else
                        Lista = (from q in Context.ro_area
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdDivision == IdDivision
                                 && q.estado == "A"
                                 && (q.IdArea.ToString() + " " + q.Descripcion).Contains(filter)
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdArea)
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

        public ro_area_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, int IdDivision)
        {
            return get_info(IdEmpresa, args.Value == null ? 0 : (int)args.Value, IdDivision);
        }

        public ro_area_Info get_info(int IdEmpresa, int IdArea, int IdDivision)
        {
            try
            {
                ro_area_Info info = new ro_area_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_area Entity = Context.ro_area.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdArea == IdArea);
                    if (Entity == null) return null;

                    info = new ro_area_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdArea = Entity.IdArea,
                        Descripcion = Entity.Descripcion,
                        IdDivision = Entity.IdDivision,
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

        public List<ro_area_Info> get_list_bajo_demanda_individual(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, bool estado)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ro_area_Info> Lista = new List<ro_area_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, estado);
            return Lista;
        }

        public List<ro_area_Info> get_list(int IdEmpresa, int skip, int take, string filter, bool MostrarAnulados)
        {
            try
            {
                List<ro_area_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    if (MostrarAnulados)
                        Lista = (from q in Context.ro_area
                                 where q.IdEmpresa == IdEmpresa
                                 && (q.IdArea.ToString() + " " + q.Descripcion).Contains(filter)
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdArea)
                                 .Skip(skip)
                                 .Take(take)
                                 .ToList();
                    else
                        Lista = (from q in Context.ro_area
                                 where q.IdEmpresa == IdEmpresa
                                 && q.estado == "A"
                                 && (q.IdArea.ToString() + " " + q.Descripcion).Contains(filter)
                                 select new ro_area_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdArea = q.IdArea,
                                     Descripcion = q.Descripcion,
                                     estado = q.estado,
                                     EstadoBool = q.estado == "A" ? true : false
                                 })
                                 .OrderBy(p => p.IdArea)
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

        public ro_area_Info get_info_bajo_demanda_individual(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            return get_info(IdEmpresa, args.Value == null ? 0 : (int)args.Value);
        }
        
    }
}
