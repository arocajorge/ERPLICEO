using Core.Erp.Data.Caja;
using Core.Erp.Info.Caja;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Caja
{
    public class cp_conciliacion_Caja_det_x_ValeCaja_Bus
    {
        cp_conciliacion_Caja_det_x_ValeCaja_Data odata = new cp_conciliacion_Caja_det_x_ValeCaja_Data();

        public List<cp_conciliacion_Caja_det_x_ValeCaja_Info> get_list(int IdEmpresa, decimal IdConciliacion_caja)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdConciliacion_caja);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
