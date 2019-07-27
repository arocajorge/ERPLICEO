using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
    public class ACTF_002_Bus
    {
        ACTF_002_Data odata = new ACTF_002_Data();
    
        public List<ACTF_002_Info> get_list(int IdEmpresa, decimal IdVtaActivo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdVtaActivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
