using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.General
{
    public class tb_ciudad_Bus
    {
        tb_ciudad_Data odata = new tb_ciudad_Data();
        public List<tb_ciudad_Info> get_list(string IdProvincia, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdProvincia, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_ciudad_Info get_info(string IdCiudad)
        {
            try
            {
                return odata.get_info( IdCiudad);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_ciudad_Info info)
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

        public bool modificarDB(tb_ciudad_Info info)
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

        public bool anularDB(tb_ciudad_Info info)
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
