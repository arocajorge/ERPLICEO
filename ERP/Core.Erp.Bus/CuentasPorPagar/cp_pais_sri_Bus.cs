using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_pais_sri_Bus
    {
        cp_pais_sri_Data odata = new cp_pais_sri_Data();
    
        public List<cp_pais_sri_Info> get_list()
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
