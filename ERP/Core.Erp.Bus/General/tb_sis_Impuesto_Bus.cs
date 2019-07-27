using Core.Erp.Data.General;
using Core.Erp.Info.General;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.General
{
    public class tb_sis_Impuesto_Bus
    {
        tb_sis_Impuesto_Data odata = new tb_sis_Impuesto_Data();

        public List<tb_sis_Impuesto_Info> get_list(string IdTipoImpuesto, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdTipoImpuesto, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_sis_Impuesto_Info get_info(string IdCod_Impuesto = "")
        {
            try
            {
                return odata.get_info(IdCod_Impuesto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_sis_Impuesto_Info info)
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

        public bool modificarDB(tb_sis_Impuesto_Info info)
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
        public bool anularDB(tb_sis_Impuesto_Info info)
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

        public bool validar_existe_IdCod_Impuesto(string IdCod_Impuesto)
        {
            try
            {
                return odata.validar_existe_IdCod_Impuesto(IdCod_Impuesto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
