using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_sis_Documento_Tipo_Bus
    {
        tb_sis_Documento_Tipo_Data odata = new tb_sis_Documento_Tipo_Data();
    
        public List<tb_sis_Documento_Tipo_Info> get_list(bool mostrar_anulados)
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

        public tb_sis_Documento_Tipo_Info get_info(string CodDocumentoTipo)
        {
            try
            {
                return odata.get_info(CodDocumentoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodDocumento(string CodDocumentoTipo)
        {
            try
            {
                return odata.validar_existe_CodDocumento(CodDocumentoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_sis_Documento_Tipo_Info info)
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

        public bool modificarDB(tb_sis_Documento_Tipo_Info info)
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
        public bool anularDB(tb_sis_Documento_Tipo_Info info)
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
