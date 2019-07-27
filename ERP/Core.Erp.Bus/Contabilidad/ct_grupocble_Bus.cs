using Core.Erp.Data.Contabilidad;
using Core.Erp.Info.Contabilidad;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Contabilidad
{
    public class ct_grupocble_Bus
    {
        ct_grupocble_Data odata = new ct_grupocble_Data();
        public List<ct_grupocble_Info> get_list(bool mostrar_anulados)
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

        public ct_grupocble_Info get_info(string IdGrupoCble)
        {
            try
            {
                return odata.get_info(IdGrupoCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ct_grupocble_Info info)
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

        public bool modificarDB(ct_grupocble_Info info)
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

        public bool anularDB(ct_grupocble_Info info)
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
