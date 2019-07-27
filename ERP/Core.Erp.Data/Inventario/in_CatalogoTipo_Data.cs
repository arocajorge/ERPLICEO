using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Inventario
{
    public class in_CatalogoTipo_Data
    {
        public List<in_CatalogoTipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                List<in_CatalogoTipo_Info> Lista;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    if (mostrar_anulados)
                        Lista = (from q in Context.in_CatalogoTipo
                                 select new in_CatalogoTipo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     cod_Catalogo_tipo = q.cod_Catalogo_tipo,
                                     Descripcion = q.Descripcion,
                                     Estado = q.Estado,

                                     EstadoBool = q.Estado == "A" ? true : false

                                 }).ToList();
                    else
                        Lista = (from q in Context.in_CatalogoTipo
                                 where q.Estado == "A"
                                 select new in_CatalogoTipo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
                                     cod_Catalogo_tipo = q.cod_Catalogo_tipo,
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
        public in_CatalogoTipo_Info get_info(int IdCatalogo_tipo)
        {
            try
            {
                in_CatalogoTipo_Info info = new in_CatalogoTipo_Info();
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_CatalogoTipo Entity = Context.in_CatalogoTipo.FirstOrDefault(q => q.IdCatalogo_tipo == IdCatalogo_tipo);
                    if (Entity == null) return null;
                    info = new in_CatalogoTipo_Info
                    {
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
                        cod_Catalogo_tipo = Entity.cod_Catalogo_tipo,
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
        public int get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_inventario Context = new Entities_inventario())
                {
                    var lst = from q in Context.in_CatalogoTipo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q=>q.IdCatalogo_tipo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_CatalogoTipo Entity = new in_CatalogoTipo
                    {
                        IdCatalogo_tipo = info.IdCatalogo_tipo = get_id(),
                        cod_Catalogo_tipo = info.cod_Catalogo_tipo,
                        Descripcion = info.Descripcion,
                        Estado = info.Estado="A"
                    };
                    Context.in_CatalogoTipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(in_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_CatalogoTipo Entity = Context.in_CatalogoTipo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo);
                    if (Entity == null)
                        return false;
                    Entity.cod_Catalogo_tipo = info.cod_Catalogo_tipo;
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
        public bool anularDB(in_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_inventario Context = new Entities_inventario())
                {
                    in_CatalogoTipo Entity = Context.in_CatalogoTipo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = "I";

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
