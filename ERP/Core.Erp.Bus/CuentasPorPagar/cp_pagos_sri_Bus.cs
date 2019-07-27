using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_pagos_sri_Bus
    {
        cp_pagos_sri_Data data = new cp_pagos_sri_Data();
        public List<cp_pagos_sri_Info> get_list()
        {
            try
            {
                return data.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
