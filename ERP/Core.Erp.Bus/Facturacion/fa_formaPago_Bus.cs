using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_formaPago_Bus
    {
        fa_formaPago_Data odata = new fa_formaPago_Data();
    
        public List<fa_formaPago_Info> get_list(bool mostrar_anulados)
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
        public fa_formaPago_Info GetInfo(string IdFormaPago)
        {
            try
            {
                return odata.GetInfo(IdFormaPago);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ValidarIdFormaPago(string IdFormaPago)
        {
            try
            {
                return odata.ValidarIdFormaPago(IdFormaPago);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(fa_formaPago_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(fa_formaPago_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(fa_formaPago_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
