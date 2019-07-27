using Core.Erp.Data.Reportes.Caja;
using Core.Erp.Info.Reportes.Caja;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Caja
{
    public class CAJ_001_Bus
    {
        CAJ_001_Data odata = new CAJ_001_Data();
    
        public List<CAJ_001_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
