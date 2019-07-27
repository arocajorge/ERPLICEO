using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_retencion_det_Bus
    {

        cp_retencion_det_Data oData = new cp_retencion_det_Data();
        public List<cp_retencion_det_Info> get_list(int IdEmpresa, decimal IdCuota)
        {
            try
            {
                return oData.get_list(IdEmpresa, IdCuota);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, decimal IdCuota)
        {
            try
            {
                return oData.eliminarDB(IdEmpresa, IdCuota);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cp_retencion_Info info)
        {
            try
            {
                return oData.guardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
