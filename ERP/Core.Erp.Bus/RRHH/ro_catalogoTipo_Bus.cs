using Core.Erp.Data.RRHH;
using Core.Erp.Info.RRHH;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.RRHH
{
    public class ro_catalogoTipo_Bus
    {
        ro_catalogoTipo_Data odata = new ro_catalogoTipo_Data();
        public List<ro_catalogoTipo_Info> get_list( bool mostrar_anulados)
        {
            try
            {
                return odata.get_list( mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_catalogoTipo_Info get_info(int IdTipoCatalogo)
        {
            try
            {
                return odata.get_info(IdTipoCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(ro_catalogoTipo_Info info)
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

        public bool modificarDB(ro_catalogoTipo_Info info)
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

        public bool anularDB(ro_catalogoTipo_Info info)
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
