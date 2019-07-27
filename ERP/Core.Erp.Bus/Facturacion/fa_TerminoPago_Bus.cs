using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_TerminoPago_Bus
    {
        fa_TerminoPago_Data odata = new fa_TerminoPago_Data();

        public List<fa_TerminoPago_Info> get_list(bool mostrar_anulados)
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

        public fa_TerminoPago_Info get_info(string IdTerminoPago)
        {
            try
            {
                return odata.get_info(IdTerminoPago);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_IdTerminoPago(string IdTerminoPago)
        {
            try
            {
                return odata.validar_existe_IdTerminoPago(IdTerminoPago);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(fa_TerminoPago_Info info)
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

        public bool modificarDB(fa_TerminoPago_Info info)
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
        public bool anularDB(fa_TerminoPago_Info info)
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
