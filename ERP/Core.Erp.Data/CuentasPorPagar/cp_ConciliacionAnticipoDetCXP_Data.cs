using Core.Erp.Data.General;
using Core.Erp.Info.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.CuentasPorPagar
{
    public class cp_ConciliacionAnticipoDetCXP_Data
    {
        cp_proveedor_Data data_proveedor = new cp_proveedor_Data();
        tb_persona_Data data_persona = new tb_persona_Data();
        public List<cp_ConciliacionAnticipoDetCXP_Info> getlist(int IdEmpresa, int IdConciliacion)
        {
            try
            {
                List<cp_ConciliacionAnticipoDetCXP_Info> Lista = new List<cp_ConciliacionAnticipoDetCXP_Info>();

                using (Entities_cuentas_por_pagar db = new Entities_cuentas_por_pagar())
                {
                    Lista = (from q in db.vwcp_ConciliacionAnticipoDetCXP
                             where q.IdEmpresa == IdEmpresa
                             && q.IdConciliacion == IdConciliacion
                             select new cp_ConciliacionAnticipoDetCXP_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdConciliacion = q.IdConciliacion,
                                 Secuencia = q.Secuencia,
                                 IdOrdenPago = q.IdOrdenPago,
                                 IdEmpresa_cxp = q.IdEmpresa_cxp,
                                 IdTipoCbte_cxp = q.IdTipoCbte_cxp,
                                 IdCbteCble_cxp = q.IdCbteCble_cxp,
                                 MontoAplicado = q.MontoAplicado,
                                 tc_TipoCbte = q.tc_TipoCbte,
                                 Fecha_cxp = q.Fecha_cxp,
                                 Observacion_cxp = q.Observacion_cxp                               
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cp_ConciliacionAnticipoDetCXP_Info> get_list_facturas_x_cruzar(int IdEmpresa, int IdSucursal, decimal IdProveedor, string Usuario)
        {
            try
            {
                List<cp_ConciliacionAnticipoDetCXP_Info> Lista = new List<cp_ConciliacionAnticipoDetCXP_Info>();

                using (Entities_cuentas_por_pagar Context = new Entities_cuentas_por_pagar())
                {
                    Lista = Context.spcp_Get_Data_orden_pago_con_cancelacion_data(IdEmpresa, 1, 999999999, "PROVEE", IdProveedor, IdProveedor, "APRO", Usuario, IdSucursal, false, false).Select(q=> new cp_ConciliacionAnticipoDetCXP_Info
                    {
                            IdEmpresa = q.IdEmpresa,
                            IdOrdenPago = q.IdOrdenPago,
                            IdEmpresa_cxp = q.IdEmpresa_cxp??0,
                            IdTipoCbte_cxp = q.IdTipoCbte_cxp??0,
                            IdCbteCble_cxp = q.IdCbteCble_cxp??0,
                            Fecha_cxp = q.Fecha_Fa_Prov,
                            MontoAplicado = q.Saldo_x_Pagar_OP,
                            Observacion_cxp = q.Observacion
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
