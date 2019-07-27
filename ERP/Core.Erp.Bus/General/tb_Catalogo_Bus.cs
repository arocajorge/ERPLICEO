using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_Catalogo_Bus
    {
        tb_Catalogo_Data odata = new tb_Catalogo_Data();
        public List<tb_Catalogo_Info> get_list(int IdTipoCatalogo, bool mostrar_anulados)
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

        public tb_Catalogo_Info get_info(string CodCatalogo)
        {
            try
            {
                return odata.get_info(CodCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodCatalogo(string CodCatalogo)
        {
            try
            {
                return odata.validar_existe_CodCatalogo(CodCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_Catalogo_Info info)
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

        public bool modificarDB(tb_Catalogo_Info info)
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

        public bool anularDB(tb_Catalogo_Info info)
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
