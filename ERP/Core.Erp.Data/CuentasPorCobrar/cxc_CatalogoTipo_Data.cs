using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorCobrar
{
    public class cxc_CatalogoTipo_Data
    {
        public List<cxc_CatalogoTipo_Info> get_list()
        {
            try
            {
                List<cxc_CatalogoTipo_Info> Lista;
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    Lista = (from q in Context.cxc_CatalogoTipo
                             select new cxc_CatalogoTipo_Info
                             {
                                 Descripcion = q.Descripcion,
                                 IdCatalogo_tipo = q.IdCatalogo_tipo
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cxc_CatalogoTipo_Info get_info (string IdCatalogo_tipo)
        {
            try
            {
                cxc_CatalogoTipo_Info info = new cxc_CatalogoTipo_Info();
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_CatalogoTipo Entity = Context.cxc_CatalogoTipo.FirstOrDefault(q => q.IdCatalogo_tipo == IdCatalogo_tipo);
                    if (Entity == null) return null;
                    info = new cxc_CatalogoTipo_Info
                    {
                        Descripcion = Entity.Descripcion,
                        IdCatalogo_tipo = Entity.IdCatalogo_tipo
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cxc_CatalogoTipo_Info info)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_CatalogoTipo Entity = new cxc_CatalogoTipo
                    {
                        Descripcion = info.Descripcion,
                        IdCatalogo_tipo = info.IdCatalogo_tipo
                    };
                    Context.cxc_CatalogoTipo.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(cxc_CatalogoTipo_Info info)
         {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    cxc_CatalogoTipo Entity = Context.cxc_CatalogoTipo.FirstOrDefault(q => q.IdCatalogo_tipo == info.IdCatalogo_tipo);
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

        public bool validar_existe_IdCatalogotipo(string IdCatalogo_tipo)
        {
            try
            {
                using (Entities_cuentas_por_cobrar Context = new Entities_cuentas_por_cobrar())
                {
                    var lst = from q in Context.cxc_CatalogoTipo
                              where q.IdCatalogo_tipo == IdCatalogo_tipo
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
