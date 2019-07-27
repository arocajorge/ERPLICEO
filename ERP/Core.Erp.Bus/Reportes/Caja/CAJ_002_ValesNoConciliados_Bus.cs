using Core.Erp.Data.Reportes.Caja;
using Core.Erp.Info.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.Caja
{
   public class CAJ_002_ValesNoConciliados_Bus
    {
        CAJ_002_ValesNoConciliados_Data odata = new CAJ_002_ValesNoConciliados_Data();
        public List<CAJ_002_ValesNoConciliados_Info> GetList(int IdEmpresa, decimal IdConciliacion_Caja)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdConciliacion_Caja);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
