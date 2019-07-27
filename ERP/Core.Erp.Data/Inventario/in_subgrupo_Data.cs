using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
   public class in_subgrupo_Data
    {
        public List<in_subgrupo_Info> get_list(int IdEmpresa, string IdCategoria, int IdLinea, int IdGrupo, bool mostrar_anulados)
        {
            try
            {
                List<in_subgrupo_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_subgrupo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdCategoria == IdCategoria
                                 && q.IdLinea == IdLinea
                                 && q.IdGrupo == IdGrupo
                                 select new in_subgrupo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCategoria = q.IdCategoria,
                                     IdLinea = q.IdLinea,
                                     IdGrupo = q.IdGrupo,
                                     IdSubgrupo = q.IdSubgrupo,
                                     cod_subgrupo = q.cod_subgrupo,
                                     nom_subgrupo = q.nom_subgrupo,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_subgrupo
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdCategoria == IdCategoria
                                 && q.IdLinea == IdLinea
                                 && q.IdGrupo == IdGrupo
                                 && q.Estado == "A"
                                 select new in_subgrupo_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCategoria = q.IdCategoria,
                                     IdLinea = q.IdLinea,
                                     IdGrupo = q.IdGrupo,
                                     IdSubgrupo = q.IdSubgrupo,
                                     cod_subgrupo = q.cod_subgrupo,
                                     nom_subgrupo = q.nom_subgrupo,
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

        public in_subgrupo_Info get_info(int IdEmpresa, string IdCategoria, int IdLinea, int IdGrupo, int IdSubgrupo)
        {
            try
            {
                in_subgrupo_Info info = new in_subgrupo_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_subgrupo Entity = Context.in_subgrupo.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCategoria == IdCategoria && q.IdLinea == IdLinea && q.IdGrupo == IdGrupo && q.IdSubgrupo == IdSubgrupo);
                    if (Entity == null) return null;
                    info = new in_subgrupo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCategoria = Entity.IdCategoria,
                        IdLinea = Entity.IdLinea,
                        IdGrupo = Entity.IdGrupo,
                        IdSubgrupo = Entity.IdSubgrupo,
                        cod_subgrupo = Entity.cod_subgrupo,
                        nom_subgrupo = Entity.nom_subgrupo,
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

        private int get_id(int IdEmpresa, string IdCategoria, int IdLinea, int IdGrupo)
        {

            try
            {
                int ID = 1;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_subgrupo
                              where q.IdEmpresa == IdEmpresa
                              && q.IdCategoria == IdCategoria
                              && q.IdLinea == IdLinea
                              && q.IdGrupo == IdGrupo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdSubgrupo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_subgrupo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_subgrupo Entity = new in_subgrupo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCategoria = info.IdCategoria,
                        IdLinea = info.IdLinea,
                        IdGrupo = info.IdGrupo,
                        IdSubgrupo = info.IdSubgrupo = get_id(info.IdEmpresa, info.IdCategoria, info.IdLinea, info.IdGrupo),
                        cod_subgrupo = info.cod_subgrupo,
                        nom_subgrupo = info.nom_subgrupo,
                        Estado = info.Estado="A",

                        IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.in_subgrupo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(in_subgrupo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_subgrupo Entity = Context.in_subgrupo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCategoria == info.IdCategoria && q.IdLinea == info.IdLinea && q.IdGrupo == info.IdGrupo && q.IdSubgrupo == info.IdSubgrupo);
                    if (Entity == null) return false;
                    Entity.cod_subgrupo = info.cod_subgrupo;
                    Entity.nom_subgrupo = info.nom_subgrupo;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.Fecha_UltMod = DateTime.Now;
                    Context.SaveChanges();
                }
                    return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(in_subgrupo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_subgrupo Entity = Context.in_subgrupo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCategoria == info.IdCategoria && q.IdLinea == info.IdLinea && q.IdGrupo == info.IdGrupo && q.IdSubgrupo == info.IdSubgrupo);
                    if (Entity == null) return false;
                    Entity.Estado = info.Estado = "I";

                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Entity.Fecha_UltAnu = DateTime.Now;
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
