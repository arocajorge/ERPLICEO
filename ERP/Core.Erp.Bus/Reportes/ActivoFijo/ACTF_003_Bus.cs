using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
    public class ACTF_003_Bus
    {
        ACTF_003_Data odata = new ACTF_003_Data();

        public List<ACTF_003_Info> get_list(int IdEmpresa, decimal IdRetiroActivo)

        {
            try
            {
                return odata.get_list(IdEmpresa, IdRetiroActivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
