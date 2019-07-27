using Core.Erp.Data.Reportes.ActivoFijo;
using Core.Erp.Info.Reportes.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Reportes.ActivoFijo
{
    public class ACTF_001_Bus
    {
        ACTF_001_Data odata = new ACTF_001_Data();
        public List<ACTF_001_Info> get_list(int IdEmpresa, decimal Id_Mejora_Baja_Activo, string Id_Tipo)
        {
            try
            {
                return odata.get_list(IdEmpresa , Id_Mejora_Baja_Activo, Id_Tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
