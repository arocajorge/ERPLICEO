using Core.Erp.Data.Caja;
using Core.Erp.Info.Caja;
using System;

namespace Core.Erp.Bus.Caja
{
    public class caj_parametro_Bus
    {
        caj_parametro_Data odata = new caj_parametro_Data();
    
        public caj_parametro_Info get_info(int IdEmpresa)
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

        public bool guardarDB(caj_parametro_Info info)
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
