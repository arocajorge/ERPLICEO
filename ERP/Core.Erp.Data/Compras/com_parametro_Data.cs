using Core.Erp.Info.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Data.Compras.Base;

namespace Core.Erp.Data.Compras
{
    public class com_parametro_Data
    {
        public com_parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                com_parametro_Info info = new com_parametro_Info();
                using (Entities_compras Context = new Entities_compras())
                {
                    com_parametro Entity = Context.com_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new com_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdEstadoAnulacion_OC = Entity.IdEstadoAnulacion_OC,
                        IdEstadoAprobacion_OC = Entity.IdEstadoAprobacion_OC,
                        IdEstado_cierre = Entity.IdEstado_cierre,
                        IdMovi_inven_tipo_OC = Entity.IdMovi_inven_tipo_OC,
                        DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(com_parametro_Info info)
        {
            try
            {
                using (Entities_compras Context = new Entities_compras())
                {
                    com_parametro Entity = Context.com_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    if (Entity == null)
                    {
                        Entity = new com_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdEstadoAnulacion_OC = info.IdEstadoAnulacion_OC,
                            IdEstadoAprobacion_OC = info.IdEstadoAprobacion_OC,
                            IdEstado_cierre = info.IdEstado_cierre,
                            IdMovi_inven_tipo_OC = info.IdMovi_inven_tipo_OC,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo
                        };
                        Context.com_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.IdEstadoAnulacion_OC = info.IdEstadoAnulacion_OC;
                        Entity.IdEstadoAprobacion_OC = info.IdEstadoAprobacion_OC;
                        Entity.IdEstado_cierre = info.IdEstado_cierre;
                        Entity.IdMovi_inven_tipo_OC = info.IdMovi_inven_tipo_OC;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
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
