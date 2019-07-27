using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.General
{
   public class tb_sis_Impuesto_x_ctacble_Data
    {
        public tb_sis_Impuesto_x_ctacble_Info get_info(string IdCod_Impuesto, int IdEmpresa)
        {
            try
            {
                tb_sis_Impuesto_x_ctacble_Info info = new tb_sis_Impuesto_x_ctacble_Info();
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Impuesto_x_ctacble Entity = Context.tb_sis_Impuesto_x_ctacble.FirstOrDefault(q => q.IdCod_Impuesto == IdCod_Impuesto && q.IdEmpresa_cta == IdEmpresa);
                    if (Entity == null) return null;
                    info = new tb_sis_Impuesto_x_ctacble_Info
                    {
                        IdCod_Impuesto = Entity.IdCod_Impuesto,
                        IdCtaCble = Entity.IdCtaCble,
                        IdCtaCble_vta = Entity.IdCtaCble_vta,
                        IdEmpresa_cta =  Entity.IdEmpresa_cta,
                        observacion = Entity.observacion
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(tb_sis_Impuesto_x_ctacble_Info info)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    tb_sis_Impuesto_x_ctacble Entity = new tb_sis_Impuesto_x_ctacble
                    {
                        IdCod_Impuesto = info.IdCod_Impuesto,
                        IdCtaCble = info.IdCtaCble,
                        IdCtaCble_vta = info.IdCtaCble_vta,
                        IdEmpresa_cta = info.IdEmpresa_cta,
                        observacion = info.observacion
                    };
                    Context.tb_sis_Impuesto_x_ctacble.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(string IdCod_Impuesto, int IdEmpresa)
        {
            try
            {
                using (Entities_general Context = new Entities_general())
                {
                    Context.Database.ExecuteSqlCommand("delete tb_sis_Impuesto_x_ctacble where IdCod_Impuesto = '"+ IdCod_Impuesto + "' and IdEmpresa_cta = "+IdEmpresa);
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
