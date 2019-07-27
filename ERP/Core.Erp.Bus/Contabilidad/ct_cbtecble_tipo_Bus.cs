using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_cbtecble_tipo_Bus
    {
        ct_cbtecble_tipo_Data odata = new ct_cbtecble_tipo_Data();
        public List<ct_cbtecble_tipo_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ct_cbtecble_tipo_Info get_info(int IdTipoCbte)
        {
            try
            {
                return odata.get_info(IdTipoCbte);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ct_cbtecble_tipo_Info info)
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


        public bool modificarDB(ct_cbtecble_tipo_Info info)
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

        public bool anularDB(ct_cbtecble_tipo_Info info)
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
