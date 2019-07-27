using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.CuentasPorPagar;
namespace Core.Erp.Data.CuentasPorPagar
{
   public class cp_orden_giro_pagos_sri_Data
    {
        public cp_orden_giro_pagos_sri_Info get_info(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                cp_orden_giro_pagos_sri_Info info;

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {

                    cp_orden_giro_pagos_sri Entity = Context.cp_orden_giro_pagos_sri.FirstOrDefault(q => q.IdEmpresa == IdEmpresa
                   && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                   && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro);
                    if (Entity == null) return null;
                    else

                        info = new cp_orden_giro_pagos_sri_Info
                        {

                            IdEmpresa = Entity.IdEmpresa,
                            IdTipoCbte_Ogiro = Entity.IdTipoCbte_Ogiro,
                            IdCbteCble_Ogiro = Entity.IdCbteCble_Ogiro,
                            codigo_pago_sri = Entity.codigo_pago_sri,
                            formas_pago_sri = Entity.formas_pago_sri
                        };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarDB(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    string comando = "delete cp_orden_giro_pagos_sri where IdEmpresa = " + IdEmpresa + " and IdTipoCbte_Ogiro = '"+ IdTipoCbte_Ogiro + "' and IdCbteCble_Ogiro='"+ IdCbteCble_Ogiro + "'";
                    Context.Database.ExecuteSqlCommand(comando);
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(cp_orden_giro_pagos_sri_Info Info)
        {
            try
            {
                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                   
                        cp_orden_giro_pagos_sri Entity = new cp_orden_giro_pagos_sri
                        {
                         IdEmpresa = Info.IdEmpresa,
                        IdTipoCbte_Ogiro = Info.IdTipoCbte_Ogiro,
                        IdCbteCble_Ogiro = Info.IdCbteCble_Ogiro,
                        codigo_pago_sri = Info.codigo_pago_sri,
                        formas_pago_sri = Info.formas_pago_sri,
                        };
                        Context.cp_orden_giro_pagos_sri.Add(Entity);
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
