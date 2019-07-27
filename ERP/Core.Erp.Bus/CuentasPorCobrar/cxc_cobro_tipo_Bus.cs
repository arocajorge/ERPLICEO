using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using Core.Erp.Info.Helps;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_cobro_tipo_Bus
    {
        cxc_cobro_tipo_Data odata = new cxc_cobro_tipo_Data();

        public List<cxc_cobro_tipo_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_tipo_Info> get_list_retenciones(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list_retenciones(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public cxc_cobro_tipo_Info get_info(string IdCobro_tipo)
        {
            try
            {
                return odata.get_info(IdCobro_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_IdCobro_tipo(string IdCobro_tipo)
        {
            try
            {
                return odata.validar_existe_IdCobro_tipo(IdCobro_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(cxc_cobro_tipo_Info info)
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
        public bool modificarDB(cxc_cobro_tipo_Info info)
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
        public bool anularDB(cxc_cobro_tipo_Info info)
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
