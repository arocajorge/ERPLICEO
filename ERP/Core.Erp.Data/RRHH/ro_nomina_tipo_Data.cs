using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.RRHH;
using DevExpress.Web;

namespace Core.Erp.Data.RRHH
{
   public class ro_nomina_tipo_Data
    {
        public List<ro_nomina_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<ro_nomina_tipo_Info> Lista;

                using (Entities_rrhh Context = new Entities_rrhh())
                {if(mostrar_anulados)
                    Lista = (from q in Context.ro_Nomina_Tipo
                             where q.IdEmpresa == IdEmpresa
                             select new ro_nomina_tipo_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdNomina_Tipo = q.IdNomina_Tipo,
                                 Descripcion = q.Descripcion,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                else
                        Lista = (from q in Context.ro_Nomina_Tipo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado=="A"
                                 select new ro_nomina_tipo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdNomina_Tipo = q.IdNomina_Tipo,
                                     Descripcion = q.Descripcion,
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
        public ro_nomina_tipo_Info get_info(int IdEmpresa, int IdNomina_Tipo)
        {
            try
            {
                ro_nomina_tipo_Info info = new ro_nomina_tipo_Info();

                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipo Entity = Context.ro_Nomina_Tipo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdNomina_Tipo == IdNomina_Tipo);
                    if (Entity == null) return null;

                    info = new ro_nomina_tipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNomina_Tipo = Entity.IdNomina_Tipo,
                        Descripcion = Entity.Descripcion,
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
                    var lst = from q in Context.ro_Nomina_Tipo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNomina_Tipo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_nomina_tipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipo Entity = new ro_Nomina_Tipo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNomina_Tipo = info.IdNomina_Tipo = get_id(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Estado = info.Estado = "A",
                        IdUsuario = info.IdUsuario,
                        FechaTransac = DateTime.Now
                    };
                    Context.ro_Nomina_Tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ro_nomina_tipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipo Entity = Context.ro_Nomina_Tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNomina_Tipo == info.IdNomina_Tipo);
                    if (Entity == null)
                        return false;
                    Entity.Descripcion = info.Descripcion;
                    Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                    Entity.FechaTransac = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ro_nomina_tipo_Info info)
        {
            try
            {
                using (Entities_rrhh Context = new Entities_rrhh())
                {
                    ro_Nomina_Tipo Entity = Context.ro_Nomina_Tipo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNomina_Tipo == info.IdNomina_Tipo);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioAnu = info.IdUsuarioAnu;
                    Entity.FechaAnu = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_nomina_tipo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ro_nomina_tipo_Info> Lista = new List<ro_nomina_tipo_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter);
            return Lista;
        }

        public ro_nomina_tipo_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, (int)args.Value);
        }

        public List<ro_nomina_tipo_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<ro_nomina_tipo_Info> Lista = new List<ro_nomina_tipo_Info>();

                Entities_rrhh context= new Entities_rrhh();

                var lstg = context.ro_Nomina_Tipo.Where(q => q.Estado == "A" && q.IdEmpresa == IdEmpresa && (q.IdNomina_Tipo.ToString() + " " + q.Descripcion).Contains(filter)).OrderBy(q => q.IdNomina_Tipo).Skip(skip).Take(take);
                foreach (var q in lstg)
                {
                    Lista.Add(new ro_nomina_tipo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdNomina_Tipo = q.IdNomina_Tipo,
                        Descripcion = q.Descripcion
                    });
                }

                context.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_nomina_tipo_Info get_info_demanda(int IdEmpresa, int value)
        {
            ro_nomina_tipo_Info info = new ro_nomina_tipo_Info();
            using (Entities_rrhh Contex = new Entities_rrhh())
            {
                info = (from q in Contex.ro_Nomina_Tipo
                        where q.IdEmpresa == IdEmpresa
                        select new ro_nomina_tipo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdNomina_Tipo = q.IdNomina_Tipo,
                            Descripcion = q.Descripcion
                        }).FirstOrDefault();
            }
            return info;
        }
    }
}
