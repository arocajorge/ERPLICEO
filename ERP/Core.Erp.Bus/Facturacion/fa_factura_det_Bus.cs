using Core.Erp.Data.Facturacion;
using Core.Erp.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.Facturacion
{
    public class fa_factura_det_Bus
    {
        fa_factura_det_Data odata = new fa_factura_det_Data();
        public List<fa_factura_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
              return   odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<fa_factura_det_Info> get_list_proformas_x_facturar(int IdEmpresa, int IdSucursal, decimal IdCliente)
        {
            try
            {
                return odata.get_list_proformas_x_facturar(IdEmpresa, IdSucursal, IdCliente);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<fa_factura_det_Info> get_list_proforma(int IdEmpresa, int IdSucursal, decimal IdCliente, decimal IdProforma)
        {
            try
            {
                return odata.get_list_proforma(IdEmpresa, IdSucursal, IdCliente, IdProforma);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
