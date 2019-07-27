using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_CambioProducto_Bus
    {
        fa_CambioProducto_Data odata = new fa_CambioProducto_Data();

        public List<fa_CambioProducto_Info> GetList(int IdEmpresa, int IdSucursal, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(fa_CambioProducto_Info info)
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

        public bool ModificarDB(fa_CambioProducto_Info info)
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

        public bool AnularDB(fa_CambioProducto_Info info)
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

        public fa_CambioProducto_Info GetInfo(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCambio)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdSucursal, IdBodega, IdCambio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
