using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_provincia_Bus
    {
        tb_provincia_Data odata = new tb_provincia_Data();
        public List<tb_provincia_Info> get_list(string IdPais, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdPais, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_provincia_Info get_info( string IdProvincia)
        {
            try
            {
                return odata.get_info( IdProvincia);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public bool guardarDB(tb_provincia_Info info)
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

        public bool modificarDB(tb_provincia_Info info)
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

        public bool anularDB(tb_provincia_Info info)
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
