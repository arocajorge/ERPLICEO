using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_mes_Bus
    {
        tb_mes_Data odata = new tb_mes_Data();
        public List<tb_mes_Info> get_list()
        {
            return odata.get_list();
        }
    }
}
