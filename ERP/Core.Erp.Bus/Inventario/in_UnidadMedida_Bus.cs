using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_UnidadMedida_Bus
    {
        in_UnidadMedida_Data odata = new in_UnidadMedida_Data();

        public List<in_UnidadMedida_Info> get_list(bool mostrar_anulados)
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

        public in_UnidadMedida_Info get_info(string IdUnidadMedida)
        {
            try
            {
                return odata.get_info(IdUnidadMedida);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdUnidadMedida(string IdUnidadMedida)
        {
            try
            {
                return odata.validar_existe_IdUnidadMedida(IdUnidadMedida);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(in_UnidadMedida_Info info)
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

        public bool modificarDB(in_UnidadMedida_Info info)
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

        public bool anularDB(in_UnidadMedida_Info info)
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
