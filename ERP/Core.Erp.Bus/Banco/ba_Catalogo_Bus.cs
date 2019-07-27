using Core.Erp.Data.Banco;
using Core.Erp.Info.Banco;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Banco
{
    public class ba_Catalogo_Bus
    {
        ba_Catalogo_Data odata = new ba_Catalogo_Data();
    
        public List<ba_Catalogo_Info> get_list(string IdTipoCatalogo, bool mostrar_anulados)
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
        public ba_Catalogo_Info get_info(string IdTipoCatalogo, string IdCatalogo)
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
        public bool guardarDB(ba_Catalogo_Info info)
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
        public bool modificarDB(ba_Catalogo_Info info)
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
        public bool anularDB(ba_Catalogo_Info info)
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
