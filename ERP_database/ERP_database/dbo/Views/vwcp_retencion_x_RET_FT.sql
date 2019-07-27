CREATE view [dbo].[vwcp_retencion_x_RET_FT]
as
SELECT        cp_retencion.IdEmpresa, cp_retencion.IdRetencion, cp_retencion.serie1 + '-' +  cp_retencion.serie2 serie, cp_retencion.NumRetencion, cp_retencion.NAutorizacion, 
                         cp_retencion_det.re_tipoRet AS re_tipoRet_RF, SUM(cp_retencion_det.re_baseRetencion) AS re_baseRetencion_RF, SUM(cp_retencion_det.re_Porcen_retencion) 
                         AS re_Porcen_retencion_RF, SUM(cp_retencion_det.re_valor_retencion) AS re_valor_retencion_RF, cp_retencion.IdEmpresa_Ogiro, cp_retencion.IdCbteCble_Ogiro, 
                         cp_retencion.IdTipoCbte_Ogiro
FROM            cp_retencion INNER JOIN
                         cp_retencion_det ON cp_retencion.IdEmpresa = cp_retencion_det.IdEmpresa AND cp_retencion.IdRetencion = cp_retencion_det.IdRetencion
GROUP BY cp_retencion.IdEmpresa, cp_retencion.IdRetencion, cp_retencion.serie1 + '-' +  cp_retencion.serie2, cp_retencion.NumRetencion, cp_retencion.NAutorizacion, cp_retencion_det.re_tipoRet, 
                         cp_retencion.IdEmpresa_Ogiro, cp_retencion.IdCbteCble_Ogiro, cp_retencion.IdTipoCbte_Ogiro
HAVING        (cp_retencion_det.re_tipoRet = 'RTF')