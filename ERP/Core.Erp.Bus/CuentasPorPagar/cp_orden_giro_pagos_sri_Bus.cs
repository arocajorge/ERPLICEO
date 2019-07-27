using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public  class cp_orden_giro_pagos_sri_Bus
    {
        cp_orden_giro_pagos_sri_Data data = new cp_orden_giro_pagos_sri_Data();
        public cp_orden_giro_pagos_sri_Info get_info(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                return data.get_info(IdEmpresa,IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
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
                return data.EliminarDB(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
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
                return data.GuardarDB(Info);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
    }
