using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_Parametro_Bus
    {
        cxc_Parametro_Data odata = new cxc_Parametro_Data();
    
        public cxc_Parametro_Info get_info(int IdEmpresa)
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

        public bool guardarDB(cxc_Parametro_Info info)
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
