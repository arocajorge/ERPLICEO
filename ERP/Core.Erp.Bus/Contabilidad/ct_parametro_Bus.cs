using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_parametro_Bus
    {
        ct_parametro_Data odata = new ct_parametro_Data();
        public ct_parametro_Info get_info(int IdEmpresa)
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

        public bool guardarDB(ct_parametro_Info info)
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
