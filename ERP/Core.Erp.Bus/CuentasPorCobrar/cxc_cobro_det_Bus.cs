using Core.Erp.Data.CuentasPorCobrar;
using Core.Erp.Info.CuentasPorCobrar;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorCobrar
{
    public class cxc_cobro_det_Bus
    {
        cxc_cobro_det_Data odata = new cxc_cobro_det_Data();
        public List<cxc_cobro_det_Info> get_list_cartera(int IdEmpresa, int IdSucursal, decimal IdCliente)
        {
            try
            {
                return odata.get_list_cartera(IdEmpresa, IdSucursal, IdCliente);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCobro);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string dc_TipoDocumento)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, dc_TipoDocumento);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
