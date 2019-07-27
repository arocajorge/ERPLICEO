using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_CatalogoTipo_Bus
    {
        Af_CatalogoTipo_Data odata = new Af_CatalogoTipo_Data();
    
        public List<Af_CatalogoTipo_Info> get_list()
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

        public Af_CatalogoTipo_Info get_info(string IdTipoCatalogo)
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

        public bool guardarDB(Af_CatalogoTipo_Info info)
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

        public bool modificarDB(Af_CatalogoTipo_Info info)
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
