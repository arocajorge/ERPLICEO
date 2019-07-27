using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Facturacion
{
   public class fa_catalogo_tipo_Data
    {
        public List<fa_catalogo_tipo_Info> get_list( bool mostrar_anulados)
        {
            try
            {
                List<fa_catalogo_tipo_Info> Lista;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    if(mostrar_anulados)
                    Lista = (from q in Context.fa_catalogo_tipo
                             select new fa_catalogo_tipo_Info
                             {
                                 IdCatalogo_tipo = q.IdCatalogo_tipo,
                                 Descripcion = q.Descripcion,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                    else
                        Lista = (from q in Context.fa_catalogo_tipo
                                 where q.Estado == "A"
                                 select new fa_catalogo_tipo_Info
                                 {
                                     IdCatalogo_tipo = q.IdCatalogo_tipo,
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

        public fa_catalogo_tipo_Info get_info (int IdCatalogo_tipo)
        {
            try
            {
                fa_catalogo_tipo_Info info;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo_tipo Entity = Context.fa_catalogo_tipo.Where(q => q.IdCatalogo_tipo == IdCatalogo_tipo).FirstOrDefault();
                    if (Entity == null) return null;

                    info = new fa_catalogo_tipo_Info
                    {
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
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

        private int get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    var lst = from q in Context.fa_catalogo_tipo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCatalogo_tipo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo_tipo Entity = new fa_catalogo_tipo
                    {
                        IdCatalogo_tipo = info.IdCatalogo_tipo= get_id(),
                        Descripcion = info.Descripcion,
                        Estado = info.Estado = "A"
                    };
                    Context.fa_catalogo_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(fa_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo_tipo Entity = Context.fa_catalogo_tipo.Where(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo).FirstOrDefault();
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

        public bool anularDB(fa_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_facturacion Context = new Entities_facturacion())
                {
                    fa_catalogo_tipo Entity = Context.fa_catalogo_tipo.Where(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo).FirstOrDefault();
                    if (Entity == null) return false;

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
