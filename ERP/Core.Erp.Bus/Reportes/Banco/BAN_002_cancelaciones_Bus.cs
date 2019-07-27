using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_002_cancelaciones_Bus
    {
        BAN_002_cancelaciones_Data odata = new BAN_002_cancelaciones_Data();

        public List<BAN_002_cancelaciones_Info> get_list(int mba_IdEmpresa, int mba_IdTipocbte, decimal mba_IdCbteCble)
        {
            try
            {
                return odata.get_list( mba_IdEmpresa,  mba_IdTipocbte,  mba_IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
