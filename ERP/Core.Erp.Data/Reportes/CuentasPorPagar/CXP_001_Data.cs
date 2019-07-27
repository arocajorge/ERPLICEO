using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_001_Data
    {
        public List<CXP_001_Info> get_list(int IdEmpresa, int IdTipoCbte_Ogiro, decimal IdCbteCble_Ogiro)
        {
            try
            {
                List<CXP_001_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_001
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipoCbte_Ogiro == IdTipoCbte_Ogiro
                             && q.IdCbteCble_Ogiro == IdCbteCble_Ogiro
                             select new CXP_001_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 Codigo = q.Codigo,
                                 Descripcion = q.Descripcion,
                                 codigoSRI = q.codigoSRI,
                                 co_descripcion = q.co_descripcion,
                                 em_nombre = q.em_nombre,
                                 Su_Descripcion = q.Su_Descripcion,
                                 pr_nombre = q.pr_nombre,
                                 IdIden_credito = q.IdIden_credito,
                                 IdOrden_giro_Tipo = q.IdOrden_giro_Tipo,
                                 IdProveedor = q.IdProveedor,
                                 co_fechaOg = q.co_fechaOg,
                                 co_serie = q.co_serie,
                                 co_factura = q.co_factura,
                                 co_FechaFactura = q.co_FechaFactura,
                                 co_FechaFactura_vct = q.co_FechaFactura_vct,
                                 co_observacion = q.co_observacion,
                                 co_baseImponible = q.co_baseImponible,
                                 co_subtotal_iva = q.co_subtotal_iva,
                                 co_subtotal_siniva = q.co_subtotal_siniva,
                                 co_total = q.co_total,
                                 co_valorpagar = q.co_valorpagar,
                                 secuencia = q.secuencia,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 dc_Observacion = q.dc_Observacion,
                                 NombreUsuario = q.NombreUsuario
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
