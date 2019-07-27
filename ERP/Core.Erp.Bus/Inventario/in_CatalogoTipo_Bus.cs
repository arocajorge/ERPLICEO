using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_CatalogoTipo_Bus
    {
        in_CatalogoTipo_Data odata = new in_CatalogoTipo_Data();
        public List<in_CatalogoTipo_Info> get_list(bool mostrar_anulados)
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

        public in_CatalogoTipo_Info get_info(int IdCatalogo_tipo)
        {
            try
            {
                return odata.get_info(IdCatalogo_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_CatalogoTipo_Info info)
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

        public bool modificarDB(in_CatalogoTipo_Info info)
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

        public bool anularDB(in_CatalogoTipo_Info info)
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
