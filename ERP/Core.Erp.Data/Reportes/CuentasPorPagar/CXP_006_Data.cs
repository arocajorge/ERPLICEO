using Core.Erp.Info.Reportes.CuentasPorPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Erp.Data.Reportes.CuentasPorPagar
{
    public class CXP_006_Data
    {
        public List<CXP_006_Info> get_list(int IdEmpresa, decimal IdRetencion)
        {
            try
            {
                List<CXP_006_Info> Lista;
                using (Entities_reportes Context = new Entities_reportes())
                {
                    Lista = (from q in Context.VWCXP_006
                             where q.IdEmpresa == IdEmpresa
                             && q.IdRetencion == IdRetencion
                             select new CXP_006_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdRetencion = q.IdRetencion,
                                 NumRetencion = q.NumRetencion,
                                 NAutorizacion = q.NAutorizacion,
                                 fecha = q.fecha,
                                 observacion = q.observacion,
                                 IdEmpresa_Ogiro = q.IdEmpresa_Ogiro,
                                 IdCbteCble_Ogiro = q.IdCbteCble_Ogiro,
                                 IdTipoCbte_Ogiro = q.IdTipoCbte_Ogiro,
                                 Idsecuencia = q.Idsecuencia,
                                 re_tipoRet = q.re_tipoRet,
                                 re_baseRetencion = q.re_baseRetencion,
                                 IdCodigo_SRI = q.IdCodigo_SRI,
                                 re_Codigo_impuesto = q.re_Codigo_impuesto,
                                 re_Porcen_retencion = q.re_Porcen_retencion,
                                 re_valor_retencion = q.re_valor_retencion,
                                 NumFactura = q.NumFactura,
                                 co_baseImponible = q.co_baseImponible,
                                 co_valoriva = q.co_valoriva,
                                 pe_nombreCompleto = q.pe_nombreCompleto                             }).ToList();
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
