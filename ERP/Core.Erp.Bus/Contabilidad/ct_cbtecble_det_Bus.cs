using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_cbtecble_det_Bus
    {
        ct_cbtecble_det_Data odata = new ct_cbtecble_det_Data();
    
        public List<ct_cbtecble_det_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa,IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ct_cbtecble_det_Info> get_list_para_cierre(int IdEmpresa, int IdSucursal, int IdanioFiscal)
        {
            try
            {
                return odata.get_list_para_cierre(IdEmpresa, IdSucursal, IdanioFiscal);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
