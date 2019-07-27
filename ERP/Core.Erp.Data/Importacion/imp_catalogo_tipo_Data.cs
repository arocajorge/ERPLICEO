using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Importacion
{
    public class imp_catalogo_tipo_Data
    {
        public List<imp_catalogo_tipo_Info> get_list()
        {
            try
            {
                List<imp_catalogo_tipo_Info> Lista;
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Lista = (from q in Context.imp_catalogo_tipo
                             select new imp_catalogo_tipo_Info
                             {
                                 IdCatalogo_tipo = q.IdCatalogo_tipo,
                                 ct_descripcion = q.ct_descripcion,
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

        public imp_catalogo_tipo_Info get_info(int IdCatalogo_tipo)
        {
            try
            {
                imp_catalogo_tipo_Info info = new imp_catalogo_tipo_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo_tipo Entity = Context.imp_catalogo_tipo.FirstOrDefault(q => q.IdCatalogo_tipo == IdCatalogo_tipo);
                    if (Entity == null) return null;

                    info = new imp_catalogo_tipo_Info
                    {
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo,
                        ct_descripcion = Entity.ct_descripcion,
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
                    var lst = from q in Context.imp_catalogo_tipo
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

        public bool guardarDB(imp_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo_tipo Entity = new imp_catalogo_tipo
                    {
                        IdCatalogo_tipo = info.IdCatalogo_tipo = get_id(),
                        ct_descripcion = info.ct_descripcion,
                        estado = info.estado = true
                    };
                    Context.imp_catalogo_tipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(imp_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo_tipo Entity = Context.imp_catalogo_tipo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo);
                    if (Entity == null) return false;

                    Entity.ct_descripcion = info.ct_descripcion;
                    Context.SaveChanges();

                }
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(imp_catalogo_tipo_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_catalogo_tipo Entity = Context.imp_catalogo_tipo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo);
                    if (Entity == null) return false;

                    Entity.estado = info.estado;
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
