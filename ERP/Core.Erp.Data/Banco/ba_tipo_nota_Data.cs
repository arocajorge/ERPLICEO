using Core.Erp.Info.Banco;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_tipo_nota_Data
    {
        public List<ba_tipo_nota_Info> get_list(int IdEmpresa,string CodTipo, bool mostrar_anulados)
        {
            try
            {
                List<ba_tipo_nota_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                 if(mostrar_anulados)
                        Lista = (from q in Context.ba_tipo_nota
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Tipo.Contains(CodTipo)
                                 select new ba_tipo_nota_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoNota = q.IdTipoNota,
                                     Descripcion = q.Descripcion,
                                     IdCtaCble = q.IdCtaCble,
                                     Tipo = q.Tipo,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                 else
                        Lista = (from q in Context.ba_tipo_nota
                                 where q.IdEmpresa == IdEmpresa
                                 && q.Estado == "A"
                                 && q.Tipo.Contains(CodTipo)
                                 select new ba_tipo_nota_Info
                                 {                                     
                                     IdEmpresa = q.IdEmpresa,
                                     IdTipoNota = q.IdTipoNota,
                                     Descripcion = q.Descripcion,
                                     IdCtaCble = q.IdCtaCble,
                                     Tipo = q.Tipo,
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

        public ba_tipo_nota_Info get_info(int IdEmpresa, int IdTipoNota)
        {
            try
            {
                ba_tipo_nota_Info info = new ba_tipo_nota_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = Context.ba_tipo_nota.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdTipoNota == IdTipoNota);
                    if (Entity == null) return null;
                    info = new ba_tipo_nota_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoNota = Entity.IdTipoNota,
                        IdCtaCble = Entity.IdCtaCble,
                        Descripcion = Entity.Descripcion,
                        Estado = Entity.Estado,
                        Tipo = Entity.Tipo
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
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_tipo_nota
                              where q.IdEmpresa == IdEmpresa
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoNota) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ba_tipo_nota_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = new ba_tipo_nota
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoNota = info.IdTipoNota=get_id(info.IdEmpresa),
                        IdCtaCble = info.IdCtaCble,
                        Descripcion = info.Descripcion,
                        Estado = info.Estado="A",
                        Tipo = info.Tipo
                    };
                    Context.ba_tipo_nota.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ba_tipo_nota_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = Context.ba_tipo_nota.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota);
                    if (Entity == null) return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.Tipo = info.Tipo;
                    Entity.IdCtaCble = info.IdCtaCble;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(ba_tipo_nota_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_tipo_nota Entity = Context.ba_tipo_nota.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota);
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_tipo_nota_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string Tipo)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<ba_tipo_nota_Info> Lista = new List<ba_tipo_nota_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter, Tipo);

            return Lista;
        }

        public ba_tipo_nota_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, Convert.ToDecimal(args.Value));
        }

        public ba_tipo_nota_Info get_info_demanda(int IdEmpresa, decimal IdTipoNota)
        {
            try
            {
                ba_tipo_nota_Info info = new ba_tipo_nota_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    info = (from q in Context.ba_tipo_nota
                            where q.IdEmpresa == IdEmpresa
                            && q.IdTipoNota == IdTipoNota
                            select new ba_tipo_nota_Info
                            {
                                Descripcion = q.Descripcion,
                                IdTipoNota = q.IdTipoNota

                            }).FirstOrDefault();
                }

                return info;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_tipo_nota_Info> get_list(int IdEmpresa, int skip, int take, string filter , string Tipo)
        {
            try
            {
                List<ba_tipo_nota_Info> Lista = new List<ba_tipo_nota_Info>();
                Entities_banco Context = new Entities_banco();
                var lst = (from
                          p in Context.ba_tipo_nota
                           where
                            p.IdEmpresa == IdEmpresa
                            && p.Tipo == Tipo
                            && (p.IdTipoNota.ToString() + " " + p.Descripcion).Contains(filter)
                           select new
                           {
                               p.IdEmpresa,
                               p.IdTipoNota,
                               p.IdCtaCble,
                               p.Descripcion,
                               p.Tipo,
                               p.Estado
                           })
                             .OrderBy(p => p.IdTipoNota)
                             .Skip(skip)
                             .Take(take)
                             .ToList();
                foreach (var q in lst)
                {
                    Lista.Add(new ba_tipo_nota_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdTipoNota = q.IdTipoNota,
                        Descripcion = q.Descripcion
                    });
                }
                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
