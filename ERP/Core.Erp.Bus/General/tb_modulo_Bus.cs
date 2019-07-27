using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_modulo_Bus
    {
        tb_modulo_Data odata = new tb_modulo_Data();
        public List<tb_modulo_Info> get_list()
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
        public tb_modulo_Info get_info(string CodModulo)
        {
            try
            {
                return odata.get_info(CodModulo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_modulo_Info info)
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
        public bool modificarDB(tb_modulo_Info info)
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
        public bool validar_existe_CodModulo(string CodModulo)
        {
            try
            {
                return odata.validar_existe_CodModulo(CodModulo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
