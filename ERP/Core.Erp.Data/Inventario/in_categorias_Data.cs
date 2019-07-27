using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_categorias_Data
    {
        public List<in_categorias_Info> get_list (int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                List<in_categorias_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_categorias
                                 where q.IdEmpresa == IdEmpresa
                                 select new in_categorias_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdCategoria = q.IdCategoria,
                                     cod_categoria = q.cod_categoria,
                                     ca_Categoria = q.ca_Categoria,
                                     Estado = q.Estado,
                                     IdCtaCble_venta = q.IdCtaCble_venta,
                                     IdCtaCtble_Costo = q.IdCtaCtble_Costo,
                                     IdCtaCtble_Inve = q.IdCtaCtble_Inve,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    else
                        Lista = (from q in Context.in_categorias
                                  where q.IdEmpresa == IdEmpresa
                                  && q.Estado == "A"
                                  select new in_categorias_Info
                                  {
                                      IdEmpresa = q.IdEmpresa,
                                      IdCategoria = q.IdCategoria,
                                      cod_categoria = q.cod_categoria,
                                      ca_Categoria = q.ca_Categoria,
                                      Estado = q.Estado,
                                      IdCtaCble_venta = q.IdCtaCble_venta,
                                      IdCtaCtble_Costo = q.IdCtaCtble_Costo,
                                      IdCtaCtble_Inve = q.IdCtaCtble_Inve,

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
        public in_categorias_Info get_info(int IdEmpresa, string IdCategoria)
        {
            try
            {
                in_categorias_Info info = new in_categorias_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_categorias Entity = Context.in_categorias.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCategoria == IdCategoria);
                    if (Entity == null) return null;
                    info = new in_categorias_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCategoria = Entity.IdCategoria,
                        cod_categoria = Entity.cod_categoria,
                        ca_Categoria = Entity.ca_Categoria,
                        Estado = Entity.Estado,
                        IdCtaCble_venta = Entity.IdCtaCble_venta,
                        IdCtaCtble_Costo = Entity.IdCtaCtble_Costo,
                        IdCtaCtble_Inve = Entity.IdCtaCtble_Inve
                };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_categorias_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_categorias Entity = new in_categorias
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCategoria = info.IdCategoria,
                        cod_categoria = info.cod_categoria,
                        ca_Categoria = info.ca_Categoria,
                        Estado = info.Estado="A",
                        IdCtaCble_venta = info.IdCtaCble_venta,
                        IdCtaCtble_Costo = info.IdCtaCtble_Costo,
                        IdCtaCtble_Inve = info.IdCtaCtble_Inve,

                    IdUsuario = info.IdUsuario,
                        Fecha_Transac = DateTime.Now
                    };
                    Context.in_categorias.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(in_categorias_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_categorias Entity = Context.in_categorias.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCategoria == info.IdCategoria);
                    if (Entity == null) return false;
                    Entity.ca_Categoria = info.ca_Categoria;
                    Entity.cod_categoria = info.cod_categoria;
                    Entity.IdCtaCble_venta = info.IdCtaCble_venta;
                    Entity.IdCtaCtble_Costo = info.IdCtaCtble_Costo;
                    Entity.IdCtaCtble_Inve = info.IdCtaCtble_Inve;

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
        public bool anularDB(in_categorias_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_categorias Entity = Context.in_categorias.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCategoria == info.IdCategoria);
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
        public bool validar_existe_IdCategoria(int IdEmpresa, string IdCategoria)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_categorias
                              where q.IdEmpresa == IdEmpresa
                              && q.IdCategoria == IdCategoria
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
