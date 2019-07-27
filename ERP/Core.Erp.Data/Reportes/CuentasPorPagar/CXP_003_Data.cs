using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_003_Data
    {
        public List<CXP_003_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                List<CXP_003_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_003
                             where q.IdEmpresa == IdEmpresa
                             && q.IdTipoCbte == IdTipoCbte
                             && q.IdCbteCble == IdCbteCble
                             select new CXP_003_Info
                             {
                                 IdEmpresa =q.IdEmpresa,
                                 IdTipoCbte = q.IdTipoCbte,
                                 IdCbteCble = q.IdCbteCble,
                                 secuencia = q.secuencia,
                                 cn_fecha = q.cn_fecha,
                                 cn_Fecha_vcto = q.cn_Fecha_vcto,
                                 cb_Observacion = q.cb_Observacion,
                                 Estado = q.Estado,
                                 cn_subtotal_iva = q.cn_subtotal_iva,
                                 cn_valoriva = q.cn_valoriva,
                                 cn_total = q.cn_total,
                                 IdCtaCble = q.IdCtaCble,
                                 pc_Cuenta = q.pc_Cuenta,
                                 dc_Valor = q.dc_Valor,
                                 dc_Valor_Debe = q.dc_Valor_Debe,
                                 dc_Valor_Haber = q.dc_Valor_Haber,
                                 dc_Observacion = q.dc_Observacion,
                                 DebCre = q.DebCre,
                                 tc_TipoCbte = q.tc_TipoCbte,
                                 IdProveedor = q.IdProveedor,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Tipo_doc = q.Tipo_doc,
                                 cn_subtotal_siniva = q.cn_subtotal_siniva,
                                 num_documento = q.num_documento,
                                 Su_Descripcion =q.Su_Descripcion,
                                 NomTipoNota = q.NomTipoNota

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
