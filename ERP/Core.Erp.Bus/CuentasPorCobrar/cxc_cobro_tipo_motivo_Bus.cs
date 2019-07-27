using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_cobro_tipo_motivo_Bus
    {
        cxc_cobro_tipo_motivo_Data odata = new cxc_cobro_tipo_motivo_Data();
    
        public List<cxc_cobro_tipo_motivo_Info> get_list()
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
