using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
  public  class cp_retencion_x_ct_cbtecble_Data
    {
        public cp_retencion_x_ct_cbtecble_Info get_info(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                cp_retencion_x_ct_cbtecble_Info info = new cp_retencion_x_ct_cbtecble_Info();
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_retencion_x_ct_cbtecble Entity = Context.cp_retencion_x_ct_cbtecble.FirstOrDefault(q => q.rt_IdEmpresa == IdEmpresa & q.rt_IdRetencion == IdRetencion);
                    if (Entity == null) return null;
                    info = new cp_retencion_x_ct_cbtecble_Info
                    {
                         rt_IdRetencion=Entity.rt_IdRetencion,
                         rt_IdEmpresa=Entity.ct_IdEmpresa,
                         ct_IdEmpresa=Entity.ct_IdEmpresa,
                         ct_IdTipoCbte=Entity.ct_IdTipoCbte,
                         ct_IdCbteCble=Entity.ct_IdCbteCble,
                         Observacion=Entity.Observacion
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean guardarDB(cp_retencion_x_ct_cbtecble_Info info)
        {
            Boolean res = false;
            try
            {
                if (info.Observacion.Length > 50)
                    info.Observacion = info.Observacion.Substring(0, 50);
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    cp_retencion_x_ct_cbtecble Entity = new cp_retencion_x_ct_cbtecble
                    {

                        rt_IdRetencion = info.rt_IdRetencion,
                        rt_IdEmpresa = info.ct_IdEmpresa,
                        ct_IdEmpresa = info.ct_IdEmpresa,
                        ct_IdTipoCbte = info.ct_IdTipoCbte,
                        ct_IdCbteCble = info.ct_IdCbteCble,
                        Observacion = info.Observacion
                    };
                    Context.cp_retencion_x_ct_cbtecble.Add(Entity);
                    Context.SaveChanges();
                }

                return res;
            }
            catch (Exception )
            {
                throw;

            }
        }
    }
}
