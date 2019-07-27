using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Banco
{
    public class ba_CatalogoTipo_Bus
    {
        ba_CatalogoTipo_Data odata = new ba_CatalogoTipo_Data();
    
        public List<ba_CatalogoTipo_Info> get_list()
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
        public ba_CatalogoTipo_Info get_info(string IdTipoCatalogo)
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
        public bool validar_existe_IdTipoCatalogo(string IdTipoCatalogo)
        {
            try
            {
                return odata.validar_existe_IdTipoCatalogo(IdTipoCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ba_CatalogoTipo_Info info)
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
        public bool modificarDB(ba_CatalogoTipo_Info info)
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
