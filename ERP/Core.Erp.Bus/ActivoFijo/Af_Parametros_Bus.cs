using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Parametros_Bus
    {
        Af_Parametros_Data odata = new Af_Parametros_Data();
    
        public Af_Parametros_Info get_info(int IdEmpresa)
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

        public bool guardarDB(Af_Parametros_Info info)
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
