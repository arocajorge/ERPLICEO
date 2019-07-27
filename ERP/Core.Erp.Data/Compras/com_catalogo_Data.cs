using Core.Erp.Data.Compras.Base;
using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Compras
{
    public class com_catalogo_Data
    {
        public List<com_catalogo_Info> get_list(string IdCatalogocompra_tipo, bool mostrar_anulados)
        {

            try
            {
                List<com_catalogo_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if(mostrar_anulados)
                    {
                        Lista = (from q in Context.com_catalogo
                                 where q.IdCatalogocompra_tipo == IdCatalogocompra_tipo
                                 select new com_catalogo_Info
                                 {
                                     IdCatalogocompra_tipo = q.IdCatalogocompra_tipo,
                                     IdCatalogocompra = q.IdCatalogocompra,
                                     Nombre = q.Nombre,
                                     CodCatalogo = q.CodCatalogo,
                                     Estado = q.Estado,
                                     Abrebiatura = q.Abrebiatura,
                                     Orden = q.Orden,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    }
                    else
                    {
                        Lista = (from q in Context.com_catalogo
                                 where q.IdCatalogocompra_tipo == IdCatalogocompra_tipo
                                 && q.Estado == "A"
                                 select new com_catalogo_Info
                                 {

                                     IdCatalogocompra_tipo = q.IdCatalogocompra_tipo,
                                     IdCatalogocompra = q.IdCatalogocompra,
                                     Nombre = q.Nombre,
                                     CodCatalogo = q.CodCatalogo,
                                     Estado = q.Estado,
                                     Abrebiatura = q.Abrebiatura,
                                     Orden = q.Orden,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public com_catalogo_Info get_info(string IdCatalogocompra_tipo, string IdCatalogocompra)
        {
            try
            {
                com_catalogo_Info info = new com_catalogo_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo Entity = Context.com_catalogo.Where(q => q.IdCatalogocompra_tipo == IdCatalogocompra_tipo && q.IdCatalogocompra == IdCatalogocompra).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new com_catalogo_Info
                    {
                        IdCatalogocompra_tipo = Entity.IdCatalogocompra_tipo,
                        IdCatalogocompra = Entity.IdCatalogocompra,
                        Nombre = Entity.Nombre,
                        CodCatalogo = Entity.CodCatalogo,
                        Estado = Entity.Estado,
                        Abrebiatura = Entity.Abrebiatura,
                        Orden = Entity.Orden,
                        NombreIngles  = Entity.NombreIngles
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdCatalogo(string IdCatalogocompra)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    var lst = from q in Context.com_catalogo
                              where q.IdCatalogocompra == IdCatalogocompra
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

        public bool guardarDB(com_catalogo_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo Entity = new com_catalogo
                    {
                        IdCatalogocompra_tipo = info.IdCatalogocompra_tipo,
                        IdCatalogocompra = info.IdCatalogocompra,
                        CodCatalogo = info.CodCatalogo,
                        Nombre = info.Nombre,
                        Abrebiatura = info.Abrebiatura,
                        NombreIngles = info.NombreIngles,
                        Estado = info.Estado = "A",

                        IdUsuario = info.IdUsuario
                    };
                    Context.com_catalogo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(com_catalogo_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo Entity = Context.com_catalogo.Where(q => q.IdCatalogocompra_tipo == info.IdCatalogocompra_tipo && q.IdCatalogocompra == info.IdCatalogocompra).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.CodCatalogo = info.CodCatalogo;
                    Entity.Nombre = info.Nombre;
                    Entity.Abrebiatura = info.Abrebiatura;
                    Entity.Orden  = info.Orden;

                    Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    Entity.FechaUltMod = DateTime.Now;
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(com_catalogo_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo Entity = Context.com_catalogo.Where(q => q.IdCatalogocompra_tipo == info.IdCatalogocompra_tipo && q.IdCatalogocompra == info.IdCatalogocompra).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Estado = info.Estado="I";

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
