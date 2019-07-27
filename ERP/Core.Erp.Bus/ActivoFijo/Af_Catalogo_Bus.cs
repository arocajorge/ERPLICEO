using Core.Erp.Data.ActivoFijo;
using Core.Erp.Info.ActivoFijo;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.ActivoFijo
{
    public class Af_Catalogo_Bus
    {
        Af_Catalogo_Data odata = new Af_Catalogo_Data();
    
        public List<Af_Catalogo_Info> get_list(string IdTipoCatalogo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdTipoCatalogo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Af_Catalogo_Info get_info(string IdTipoCatalogo, string IdCatalogo)
        {
            try
            {
                return odata.get_info(IdTipoCatalogo, IdCatalogo);
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

        public bool guardarDB(Af_Catalogo_Info info)
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
        public bool modificarDB(Af_Catalogo_Info info)
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

        public bool anularDB(Af_Catalogo_Info info)
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
