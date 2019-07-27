using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_CambioProductoDet_Bus
    {
        fa_CambioProductoDet_Data odata = new fa_CambioProductoDet_Data();
        public List<fa_CambioProductoDet_Info> GetList(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCambio)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdBodega, IdCambio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_CambioProductoDet_Info> GetListFacturas(int IdEmpresa, int IdSucursal, int IdBodega, decimal NumeroFactura, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetListFacturas(IdEmpresa, IdSucursal, IdBodega, NumeroFactura, FechaIni, FechaFin);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
