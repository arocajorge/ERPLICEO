using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
    public class com_catalogo_tipo_Data
    {
        public List<com_catalogo_tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<com_catalogo_tipo_Info> Lista;
                using (Entities_compras Context = new Entities_compras())
                {
                    if(mostrar_anulados)
                    {
                        Lista = (from q in Context.com_catalogo_tipo
                                 select new com_catalogo_tipo_Info
                                 {
                                     IdCatalogocompra_tipo = q.IdCatalogocompra_tipo,
                                     Descripcion = q.Descripcion,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false
                                 }).ToList();
                    }
                    else
                    {
                        Lista = (from q in Context.com_catalogo_tipo
                                 where q.Estado == "A"
                                 select new com_catalogo_tipo_Info
                                 {
                                     IdCatalogocompra_tipo = q.IdCatalogocompra_tipo,
                                     Descripcion = q.Descripcion,
                                     Estado = q.Estado,

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

        public com_catalogo_tipo_Info get_info (string IdCatalogocompra_tipo)
        {
            try
            {
                com_catalogo_tipo_Info info = new com_catalogo_tipo_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo_tipo Entity = Context.com_catalogo_tipo.Where(q => q.IdCatalogocompra_tipo == IdCatalogocompra_tipo).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new com_catalogo_tipo_Info
                    {
                        IdCatalogocompra_tipo = Entity.IdCatalogocompra_tipo,
                        Descripcion = Entity.Descripcion,
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

        public bool validar_existe_IdCatalogotipo(string IdCatalogocompra_tipo)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    var lst = from q in Context.com_catalogo_tipo
                              where q.IdCatalogocompra_tipo == IdCatalogocompra_tipo
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

        public bool guardarDB(com_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo_tipo Entity = new com_catalogo_tipo
                    {
                        IdCatalogocompra_tipo = info.IdCatalogocompra_tipo,
                        Descripcion = info.Descripcion,
                        Estado = info.Estado="A"
                    };
                    Context.com_catalogo_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(com_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo_tipo Entity = Context.com_catalogo_tipo.Where(q => q.IdCatalogocompra_tipo == info.IdCatalogocompra_tipo).FirstOrDefault();
                    if (Entity == null) return false;

                    Entity.Descripcion = info.Descripcion;
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(com_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_catalogo_tipo Entity = Context.com_catalogo_tipo.Where(q => q.IdCatalogocompra_tipo == info.IdCatalogocompra_tipo).FirstOrDefault();
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


    }
}
