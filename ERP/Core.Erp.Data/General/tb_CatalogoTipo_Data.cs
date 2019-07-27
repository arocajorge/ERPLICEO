using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
    public class tb_CatalogoTipo_Data
    {
        public List<tb_CatalogoTipo_Info> get_list()
        {
            try
            {
                List<tb_CatalogoTipo_Info> Lista;
                using (Entities_general Context = new Entities_general())
                {
                    Lista = (from q in Context.tb_CatalogoTipo
                             select new tb_CatalogoTipo_Info
                             {
                                 IdTipoCatalogo = q.IdTipoCatalogo,
                                 Codigo = q.Codigo,
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

        public tb_CatalogoTipo_Info get_info(int IdTipoCatalogo)
        {
            try
            {
                tb_CatalogoTipo_Info info = new tb_CatalogoTipo_Info();

               using (Entities_general Context = new Entities_general())
                {
                    tb_CatalogoTipo Entity = Context.tb_CatalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == IdTipoCatalogo);
                    if (Entity == null) return null;
                    info = new tb_CatalogoTipo_Info
                    {
                        IdTipoCatalogo = Entity.IdTipoCatalogo,
                        Codigo = Entity.Codigo,
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
    
        public int get_id()
        {
            try
            {
                int ID = 1;
                using (Entities_general Context = new Entities_general())
                {
                    var lst = from q in Context.tb_CatalogoTipo
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdTipoCatalogo) + 1;
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_CatalogoTipo_Info info)
        {
            try
            {

                using (Entities_general Context = new Entities_general())
                {
                    tb_CatalogoTipo Entity = new tb_CatalogoTipo
                    {
                        IdTipoCatalogo = info.IdTipoCatalogo = get_id(),
                        Codigo = info.Codigo,
                        tc_Descripcion = info.tc_Descripcion
                    };
                    Context.tb_CatalogoTipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(tb_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_CatalogoTipo Entity = Context.tb_CatalogoTipo.FirstOrDefault(q => q.IdTipoCatalogo == info.IdTipoCatalogo);
                    if (Entity == null)
                        return false;
                    Entity.IdTipoCatalogo = info.IdTipoCatalogo;
                    Entity.Codigo = info.Codigo;
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
