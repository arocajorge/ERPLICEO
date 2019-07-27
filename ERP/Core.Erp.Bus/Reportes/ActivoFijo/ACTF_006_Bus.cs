using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Erp.Info.Reportes.ActivoFijo;
using Core.Erp.Bus.Reportes.ActivoFijo;
using Core.Erp.Data.Reportes.ActivoFijo;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
   public class ACTF_006_Bus
    {
        ACTF_006_Data odata = new ACTF_006_Data();
        public List<ACTF_006_Info> get_list(int IdEmpresa, int IdActivoFijo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdActivoFijo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
