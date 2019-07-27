using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.ActivoFijo
{
    public class Af_CatalogoTipo_Data
    {
        public List<Af_CatalogoTipo_Info> get_list()
        {
            try
            {
                List<Af_CatalogoTipo_Info> Lista;
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Lista = (from q in Context.Af_CatalogoTipo
                             select new Af_CatalogoTipo_Info
                             {
                                 IdTipoCatalogo = q.IdTipoCatalogo,
                                 Descripcion = q.Descripcion

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Af_CatalogoTipo_Info get_info(string IdTipoCatalogo)
        {
            try
            {
                Af_CatalogoTipo_Info info = new Af_CatalogoTipo_Info();
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_CatalogoTipo Entity = Context.Af_CatalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo);
                    if (Entity == null) return null;
                    info = new Af_CatalogoTipo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        Descripcion = Entity.Descripcion
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdTipoCatalogo(string IdTipoCatalogo)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    var lst = from q in Context.Af_CatalogoTipo
                              where q.IdTipoCatalogo == IdTipoCatalogo
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

        public bool guardarDB(Af_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_CatalogoTipo Entity = new Af_CatalogoTipo
                    {
                        IdTipoCatalogo = info.IdTipoCatalogo,
                        Descripcion = info.Descripcion
                    };
                    Context.Af_CatalogoTipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(Af_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_activo_fijo Context = new Entities_activo_fijo())
                {
                    Af_CatalogoTipo Entity = Context.Af_CatalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo);
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

    }
}
