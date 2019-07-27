using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_region_Bus
    {
        tb_region_Data odata = new tb_region_Data();
        public List<tb_region_Info> get_list(string IdPais, bool mostrar_anulados)
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

        public tb_region_Info get_info( string CodRegion)
        {
            try
            {
                return odata.get_info(CodRegion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_region_Info info)
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

        public bool modificarDB(tb_region_Info info)
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

        public bool anularDB(tb_region_Info info)
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
