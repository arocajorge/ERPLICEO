using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_retencion_x_ct_cbtecble_Bus
    {
        cp_retencion_x_ct_cbtecble_Data odata = new cp_retencion_x_ct_cbtecble_Data();
        public cp_retencion_x_ct_cbtecble_Info get_info(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdRetencion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cp_retencion_x_ct_cbtecble_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
