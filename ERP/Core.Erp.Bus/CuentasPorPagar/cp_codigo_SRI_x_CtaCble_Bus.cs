using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_codigo_SRI_x_CtaCble_Bus
    {
        cp_codigo_SRI_x_CtaCble_Data odata = new cp_codigo_SRI_x_CtaCble_Data();
        public List<cp_codigo_SRI_Info> get_list(int IdEmpresa)
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
        public cp_codigo_SRI_x_CtaCble_Info get_info(int idCodigo_SRI, int IdEmpresa)
        {
            try
            {
                return odata.get_info(idCodigo_SRI, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cp_codigo_SRI_x_CtaCble_Info info)
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

        public bool eliminarDB(int idCodigo_SRI, int IdEmpresa)
        {
            try
            {
                return odata.eliminarDB(idCodigo_SRI, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
