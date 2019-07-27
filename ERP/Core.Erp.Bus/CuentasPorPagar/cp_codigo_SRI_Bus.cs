using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_codigo_SRI_Bus
    {
        cp_codigo_SRI_Data odata = new cp_codigo_SRI_Data();

        public List<cp_codigo_SRI_Info> get_list(string IdTipoSRI, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdTipoSRI, true);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_codigo_SRI_Info> get_list_cod_ret( bool mostrar_anulados, int IdEmpresa)
        {
            try
            {
                return odata.get_list_cod_ret( mostrar_anulados, IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public cp_codigo_SRI_Info get_info(int IdCodigoSRI)
        {
            try
            {
                return odata.get_info(IdCodigoSRI);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cp_codigo_SRI_Info get_info(int IdEmpresa, int IdCodigoSRI)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCodigoSRI);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_codigo_SRI_Info info)
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

        public bool modificarDB(cp_codigo_SRI_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cp_codigo_SRI_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
