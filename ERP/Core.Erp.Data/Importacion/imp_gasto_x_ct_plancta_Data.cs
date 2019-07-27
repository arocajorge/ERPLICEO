using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Importacion
{
    public class imp_gasto_x_ct_plancta_Data
    {
        public imp_gasto_x_ct_plancta_Info get_info(int IdEmpresa, int IdGasto_tipo)
        {
            try
            {
                imp_gasto_x_ct_plancta_Info info = new imp_gasto_x_ct_plancta_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_gasto_x_ct_plancta Entity = Context.imp_gasto_x_ct_plancta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdGasto_tipo == IdGasto_tipo);
                    if (Entity == null) return null;
                             info = new imp_gasto_x_ct_plancta_Info
                             {
                                 IdEmpresa = Entity.IdEmpresa,
                                 IdGasto_tipo = Entity.IdGasto_tipo,
                                 IdCtaCble = Entity.IdCtaCble
                             };
                }
                 return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_gasto_x_ct_plancta_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_gasto_x_ct_plancta Entity = new imp_gasto_x_ct_plancta
                    {

                        IdEmpresa = info.IdEmpresa,
                        IdGasto_tipo = info.IdGasto_tipo,
                        IdCtaCble = info.IdCtaCble
                    };
                    Context.imp_gasto_x_ct_plancta.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, int IdGasto_tipo)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    Context.Database.ExecuteSqlCommand("delete imp_gasto_x_ct_plancta where IdGasto_tipo = '" + IdGasto_tipo + "' and IdEmpresa = " + IdEmpresa);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public imp_gasto_x_ct_plancta_Info get_info(int IdEmpresa, string IdCtaCble)
        {
            try
            {
                imp_gasto_x_ct_plancta_Info info = new imp_gasto_x_ct_plancta_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_gasto_x_ct_plancta Entity = Context.imp_gasto_x_ct_plancta.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdCtaCble == IdCtaCble);
                    if (Entity == null) return null;
                    info = new imp_gasto_x_ct_plancta_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdGasto_tipo = Entity.IdGasto_tipo,
                        IdCtaCble = Entity.IdCtaCble
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
