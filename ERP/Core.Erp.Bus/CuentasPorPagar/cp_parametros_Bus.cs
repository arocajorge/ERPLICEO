using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_parametros_Bus
    {
        cp_parametros_Data odata = new cp_parametros_Data();
        public Boolean modificarDB(cp_parametros_Info inf)
        {
            try
            {
                return odata.modificarDB(inf);
            }
            catch (Exception )
            {
               
                throw ;
            }
        }
        public cp_parametros_Info get_info(int IdEmpresa)
        {
            try
            {
                return odata.get_info(IdEmpresa);
            }
            catch (Exception )
            {
                throw;
            }
        }

        public List<cp_parametros_Info> get_list(int IdEmpresa)
        {
            try
            {
                return odata.get_list(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
