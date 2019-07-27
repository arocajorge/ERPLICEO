using Core.Erp.Data.Reportes.Banco;
using Core.Erp.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Banco
{
    public class BAN_005_Bus
    {
        BAN_005_Data odata = new BAN_005_Data();
        public List<BAN_005_Info> get_list(int IdEmpresa, int IdTipocbte, decimal IdCbteCble,int NumDesde, int NumHasta, int IdBanco)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipocbte, IdCbteCble, NumDesde, NumHasta, IdBanco);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
