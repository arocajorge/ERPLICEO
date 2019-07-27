using Core.Erp.Info.Importacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Importacion
{
   public class imp_parametro_Data
    {
        public imp_parametro_Info get_info( int IdEmpresa)
        {
            try
            {
                imp_parametro_Info info = new imp_parametro_Info();
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_parametro Entity = Context.imp_parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new imp_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoCbte_liquidacion = Entity.IdTipoCbte_liquidacion,
                        IdTipoCbte_liquidacion_anu = Entity.IdTipoCbte_liquidacion_anu,
                        IdCtaCble=Entity.IdCtaCble,
                        IdSucursal=Entity.IdSucursal,
                        IdBodega=Entity.IdBodega,
                        IdMotivo_Inv_ing=Entity.IdMotivo_Inv_ing,
                        IdMovi_inven_tipo_ing=Entity.IdMovi_inven_tipo_ing,
                        IdCtaCble_invntario=Entity.IdCtaCble_invntario
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(imp_parametro_Info info)
        {
            try
            {
                using (Entities_importacion Context = new Entities_importacion())
                {
                    imp_parametro Entity = Context.imp_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if(Entity == null)
                    {

                        Entity = new imp_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdTipoCbte_liquidacion = info.IdTipoCbte_liquidacion,
                            IdTipoCbte_liquidacion_anu = info.IdTipoCbte_liquidacion_anu,
                            IdCtaCble = info.IdCtaCble,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdMotivo_Inv_ing = info.IdMotivo_Inv_ing,
                            IdMovi_inven_tipo_ing = info.IdMovi_inven_tipo_ing,
                            IdCtaCble_invntario = info.IdCtaCble_invntario
                        };
                        Context.imp_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.IdTipoCbte_liquidacion = info.IdTipoCbte_liquidacion;
                        Entity.IdTipoCbte_liquidacion_anu = info.IdTipoCbte_liquidacion_anu;
                        Entity.IdSucursal = info.IdSucursal;
                        Entity.IdBodega = info.IdBodega;
                        Entity.IdMotivo_Inv_ing = info.IdMotivo_Inv_ing;
                        Entity.IdMovi_inven_tipo_ing = info.IdMovi_inven_tipo_ing;
                        Entity.IdCtaCble = info.IdCtaCble;
                        Entity.IdCtaCble_invntario = info.IdCtaCble_invntario;
                    }
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
