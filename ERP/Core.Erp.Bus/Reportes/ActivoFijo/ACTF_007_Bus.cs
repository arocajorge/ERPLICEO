using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
  public class ACTF_007_Bus
    {
        ACTF_007_Data odata = new ACTF_007_Data();
        public List<ACTF_007_Info> GetList(int IdEmpresa, int IdActivoFijo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdActivoFijo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
