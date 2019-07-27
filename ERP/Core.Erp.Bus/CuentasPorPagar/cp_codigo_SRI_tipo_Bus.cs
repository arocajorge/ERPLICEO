using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_codigo_SRI_tipo_Bus
    {
        cp_codigo_SRI_tipo_Data odata = new cp_codigo_SRI_tipo_Data();
        public List<cp_codigo_SRI_tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list( true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public cp_codigo_SRI_tipo_Info get_info(string IdTipoSRI)
        {
            try
            {
                return odata.get_info(IdTipoSRI);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cp_codigo_SRI_tipo_Info info)
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

        public bool modificarDB(cp_codigo_SRI_tipo_Info info)
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

        public bool anularDB(cp_codigo_SRI_tipo_Info info)
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

        public bool validar_existe_codigo_tipo(string IdTipoSRI)
        {
            try
            {
                return odata.validar_existe_codigo_tipo(IdTipoSRI);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
