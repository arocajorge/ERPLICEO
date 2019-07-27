using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;

namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_orden_giro_det_Bus
    {
        cp_orden_giro_det_Data odata = new cp_orden_giro_det_Data();

        public List<cp_orden_giro_det_Info> get_list(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte_Ogiro, IdCbteCble_Ogiro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<cp_orden_giro_det_Info> GetListPorIngresar(int IdEmpresa, int IdSucursal, decimal IdProveedor)
        //{
        //    try
        //    {
        //        return odata.GetListPorIngresar(IdEmpresa, IdSucursal, IdProveedor);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
