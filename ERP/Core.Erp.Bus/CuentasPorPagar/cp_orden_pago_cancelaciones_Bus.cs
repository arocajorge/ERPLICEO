using Core.Erp.Data.CuentasPorPagar;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
namespace Core.Erp.Bus.CuentasPorPagar
{
    public class cp_orden_pago_cancelaciones_Bus
    {
        cp_orden_pago_cancelaciones_Data odata = new cp_orden_pago_cancelaciones_Data();
        public Boolean guardarDB(cp_orden_pago_cancelaciones_Info Info)
        {
            try
            {
                return odata.guardarDB(Info);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<cp_orden_pago_cancelaciones_Info> get_list_con_saldo(int IdEmpresa, decimal IdPersona, string IdTipo_Persona, decimal IdEntidad, string IdEstado_Aprobacion, string IdUsuario, bool mostrar_saldo_0, int IdSucursal)
        {
            try
            {
                return odata.get_list_con_saldo(IdEmpresa, IdPersona, IdTipo_Persona, IdEntidad, IdEstado_Aprobacion, IdUsuario, mostrar_saldo_0, IdSucursal);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_orden_pago_cancelaciones_Info> get_list_x_pago(int IdEmpresa_pago, int IdTipoCbte_pago, decimal IdCbteCble_pago, string IdUsuario)
        {
            try
            {
                return odata.get_list_x_pago(IdEmpresa_pago,IdTipoCbte_pago,IdCbteCble_pago,IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<cp_orden_pago_det_Info> Get_list_Cancelacion_x_CXP(int IdEmpresa_cxp, int IdTipoCbte_cxp, decimal IdCbteCble_cxp)
        {
            try
            {
                return odata.Get_list_Cancelacion_x_CXP(IdEmpresa_cxp, IdTipoCbte_cxp, IdCbteCble_cxp);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool si_existe_cancelacion(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                return odata.si_existe_cancelacion(IdEmpresa, IdOrdenPago);
            }
            catch (Exception)
            {

                throw;
            }
        }

        }
    }
