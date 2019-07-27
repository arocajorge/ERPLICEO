using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
   public class tb_parametro_Data
    {
        public tb_parametro_Info GetInfo(int IdEmpresa)
        {
            try
            {
                tb_parametro_Info info = new tb_parametro_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_parametro Entity = Context.tb_parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new tb_parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        EsMultiSucursal = Entity.EsMultiSucursal,
                        IdCod_Impuesto = Entity.IdCod_Impuesto,
                        Porcentaje = Entity.Porcentaje
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(tb_parametro_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_parametro Entity = Context.tb_parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new tb_parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            EsMultiSucursal = info.EsMultiSucursal,
                            IdCod_Impuesto = info.IdCod_Impuesto,
                            Porcentaje = info.Porcentaje
                        };
                        Context.tb_parametro.Add(Entity);
                    }
                    else
                    {
                        Entity.EsMultiSucursal = info.EsMultiSucursal;
                        Entity.IdCod_Impuesto = info.IdCod_Impuesto;
                        Entity.Porcentaje = info.Porcentaje;
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
