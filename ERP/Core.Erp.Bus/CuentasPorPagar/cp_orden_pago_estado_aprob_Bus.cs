using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_orden_pago_estado_aprob_Bus
    {
        cp_orden_pago_estado_aprob_Data odata = new cp_orden_pago_estado_aprob_Data();
        public List<cp_orden_pago_estado_aprob_Info> get_list()
        {
            try
            {
                return odata.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }
        }
}
