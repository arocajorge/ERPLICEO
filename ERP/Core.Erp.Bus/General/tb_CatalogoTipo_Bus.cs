using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_CatalogoTipo_Bus
    {
        tb_CatalogoTipo_Data odata = new tb_CatalogoTipo_Data();

        public List<tb_CatalogoTipo_Info> get_list()
        {
            try
            {
                return odata.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_CatalogoTipo_Info get_info(int IdTipoCatalogo)
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
  

        public bool guardarDB(tb_CatalogoTipo_Info info)
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



        public bool modificarDB(tb_CatalogoTipo_Info info)
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
    }
}
