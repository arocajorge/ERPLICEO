using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.RRHH
{
   public class ro_FormulaHorasRecargo_Bus
    {
        ro_FormulaHorasRecargo_Data odata = new ro_FormulaHorasRecargo_Data();
        public ro_FormulaHorasRecargo_Info get_info(int IdEmpresa)
        {
            try
            {
                return odata.get_info(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
