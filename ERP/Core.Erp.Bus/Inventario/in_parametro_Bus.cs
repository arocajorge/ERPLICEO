using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;

namespace Core.Erp.Bus.Inventario
{
    public class in_parametro_Bus
    {
        in_parametro_Data odata = new in_parametro_Data();

        public in_parametro_Info get_info(int IdEmpresa)
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

        public bool guardarDB(in_parametro_Info info)
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
