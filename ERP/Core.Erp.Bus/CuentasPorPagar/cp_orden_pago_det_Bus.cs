using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    class cp_orden_pago_det_Bus
    {
        cp_orden_pago_det_Data oData = new cp_orden_pago_det_Data();
        public List<cp_orden_pago_det_Info> Get_list_cuotas_x_doc_det(int IdEmpresa, decimal IdCuota)
        {
            try
            {
                return oData.Get_list_cuotas_x_doc_det(IdEmpresa, IdCuota);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarDB(int IdEmpresa, decimal IdCuota)
        {
            try
            {
                return oData.EliminarDB(IdEmpresa, IdCuota);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(List<cp_orden_pago_det_Info> Lista)
        {
            try
            {
                return oData.GuardarDB(Lista);
            }
            catch (Exception)
            {
                throw;
            }
        }

      
        public bool calcular_cuotas(int Idempresa, decimal IdOrdenPago, string estado_aprobacion)
        {
            try
            {
                return oData.modificar_estado_aprobacion(Idempresa,IdOrdenPago,estado_aprobacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
