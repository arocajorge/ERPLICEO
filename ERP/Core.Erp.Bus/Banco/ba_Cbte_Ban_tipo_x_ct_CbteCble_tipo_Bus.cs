using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Banco
{
    public class ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Bus
    {
        ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Data odata = new ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Data();
        public List<ba_Cbte_Ban_tipo_x_ct_CbteCble_tipo_Info> GetList(int IdEmpresa)
        {
            try
            {
                return odata.GetList(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
