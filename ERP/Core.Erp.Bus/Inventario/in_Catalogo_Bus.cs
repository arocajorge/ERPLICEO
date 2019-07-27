using Core.Erp.Data.Inventario;
using Core.Erp.Info.Inventario;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Inventario
{
    public class in_Catalogo_Bus
    {
        in_Catalogo_Data odata = new in_Catalogo_Data();
        public List<in_Catalogo_Info> get_list(int IdCatalogo_tipo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdCatalogo_tipo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public in_Catalogo_Info get_info(string IdCatalogo)
        {
            try
            {
                return odata.get_info(IdCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool validar_existe_IdCatalogo(string IdCatalogo)
        {
            try
            {
                return odata.validar_existe_IdCatalogo(IdCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(in_Catalogo_Info info)
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
        public bool modificarDB(in_Catalogo_Info info)
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
        public bool anularDB(in_Catalogo_Info info)
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
