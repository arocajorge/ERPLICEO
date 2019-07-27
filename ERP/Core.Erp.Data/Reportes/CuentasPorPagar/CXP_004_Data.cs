using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_004_Data
    {
        public List<CXP_004_Info> get_list(int IdEmpresa, decimal IdOrdenPago)
        {
            try
            {
                List<CXP_004_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_004
                             where q.IdEmpresa == IdEmpresa
                             && q.IdOrdenPago == IdOrdenPago


                             select new CXP_004_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdOrdenPago = q.IdOrdenPago,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 secuencia = q.secuencia,
                                 Estado = q.Estado,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 dc_Observacion = q.dc_Observacion,
                                 tc_TipoCbte = q.tc_TipoCbte,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Fecha =q.Fecha,
                                 Observacion = q.Observacion,
                                 IdTipo_op = q.IdTipo_op,
                                 Valor_a_pagar = q.Valor_a_pagar,
                                 co_factura = q.co_factura,
                                 Descripcion = q.Descripcion,
                                 GeneraDiario = q.GeneraDiario,
                                 IdEstadoAprobacion = q.IdEstadoAprobacion,
                                 estado_apro = q.estado_apro,
                                 NombreUsuario = q.NombreUsuario,
                                 Su_Descripcion = q.Su_Descripcion
                                 
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
