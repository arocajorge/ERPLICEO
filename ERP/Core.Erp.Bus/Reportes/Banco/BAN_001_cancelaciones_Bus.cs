using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
   public class BAN_001_cancelaciones_Bus
    {
        BAN_001_cancelaciones_Data odata = new BAN_001_cancelaciones_Data();

        public List<BAN_001_cancelaciones_Info> get_list(int IdEmpresa_pago, int IdTipoCbte_pago, decimal IdCbteCble_pago)
        {
            try
            {
                return odata.get_list(IdEmpresa_pago, IdTipoCbte_pago, IdCbteCble_pago);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
