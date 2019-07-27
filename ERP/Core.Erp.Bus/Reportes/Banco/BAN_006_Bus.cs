using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_006_Bus
    {
        BAN_006_Data odata = new BAN_006_Data();
        public List<BAN_006_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble, int NumDesde, int NumHasta, int IdBanco)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte, IdCbteCble, NumDesde, NumHasta, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
