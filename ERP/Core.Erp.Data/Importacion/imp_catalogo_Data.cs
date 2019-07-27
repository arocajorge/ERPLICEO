using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Importacion;

namespace Core.Erp.Data.Importacion
{
    public class imp_catalogo_Data
    {
        public List<imp_catalogo_Info> get_list(int IdCatalogo_tipo)
        {
            try
            {
                List<imp_catalogo_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.imp_catalogo
                             where q.IdCatalogo_tipo == IdCatalogo_tipo
                             select new imp_catalogo_Info
                             {
                                 IdCatalogo = q.IdCatalogo,
                                 IdCatalogo_tipo = q.IdCatalogo_tipo,
                                 ca_descripcion = q.ca_descripcion,
                                 estado = q.estado == true
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_catalogo_Info get_info(int IdCatalogo_tipo, int IdCatalogo)
        {
            try
            {
                imp_catalogo_Info info = new imp_catalogo_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo Entity = Context.imp_catalogo.FirstOrDefault(q => q.IdCatalogo_tipo == IdCatalogo_tipo && q.IdCatalogo == IdCatalogo);
                    if (Entity == null) return null;
                    info = new imp_catalogo_Info
                    {
                        IdCatalogo = Entity.IdCatalogo,
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
                        ca_descripcion = Entity.ca_descripcion,
                        estado = Entity.estado
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
                using (Entities_importacion Context = new Entities_importacion())
                {
                    var lst = from q in Context.imp_catalogo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCatalogo) +1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_catalogo_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo Entity = new imp_catalogo
                    {
                        IdCatalogo = info.IdCatalogo = get_id(),
                        IdCatalogo_tipo = info.IdCatalogo_tipo,
                        ca_descripcion = info.ca_descripcion,
                        estado = info.estado = true
                    };
                    Context.imp_catalogo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(imp_catalogo_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo Entity = Context.imp_catalogo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;

                    Entity.ca_descripcion = info.ca_descripcion;
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(imp_catalogo_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo Entity = Context.imp_catalogo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo && q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null) return false;

                    Entity.estado = info.estado= false;
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
