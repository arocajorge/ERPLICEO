using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Banco
{
    public class ba_CatalogoTipo_Data
    {
        public List<ba_CatalogoTipo_Info> get_list()
        {
            try
            {
                List<ba_CatalogoTipo_Info> Lista;
                using (Entities_banco Context = new Entities_banco())
                {
                    Lista = (from q in Context.ba_CatalogoTipo
                             select new ba_CatalogoTipo_Info
                             {
                                 IdTipoCatalogo = q.IdTipoCatalogo,
                                 tc_Descripcion = q.tc_Descripcion
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ba_CatalogoTipo_Info get_info(string IdTipoCatalogo)
        {
            try
            {
                ba_CatalogoTipo_Info info = new ba_CatalogoTipo_Info();
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_CatalogoTipo Entity = Context.ba_CatalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo);
                    if (Entity == null) return null;
                    info = new ba_CatalogoTipo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        tc_Descripcion = Entity.tc_Descripcion
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
                using (Entities_banco Context = new Entities_banco())
                {
                    var lst = from q in Context.ba_CatalogoTipo
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

        public bool guardarDB(ba_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_CatalogoTipo Entity = new ba_CatalogoTipo
                    {
                        IdTipoCatalogo = info.IdTipoCatalogo,
                        tc_Descripcion = info.tc_Descripcion
                    };
                    Context.ba_CatalogoTipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(ba_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_banco Context = new Entities_banco())
                {
                    ba_CatalogoTipo Entity = Context.ba_CatalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo);
                    if (Entity == null) return false;

                    Entity.tc_Descripcion = info.tc_Descripcion;
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
