CREATE VIEW web.VWCXP_002
AS
SELECT dbo.cp_retencion.IdEmpresa, dbo.cp_retencion.IdRetencion, dbo.cp_retencion.serie1 + '-' + dbo.cp_retencion.serie2 AS serie, dbo.cp_retencion.NumRetencion, dbo.cp_retencion.NAutorizacion, dbo.cp_retencion.fecha, 
                  dbo.cp_retencion.observacion, dbo.cp_retencion.IdEmpresa_Ogiro, dbo.cp_retencion.IdCbteCble_Ogiro, dbo.cp_retencion.IdTipoCbte_Ogiro, dbo.cp_retencion_det.Idsecuencia, dbo.cp_retencion_det.re_tipoRet, 
                  dbo.cp_retencion_det.re_baseRetencion, dbo.cp_retencion_det.IdCodigo_SRI, dbo.cp_retencion_det.re_Codigo_impuesto, dbo.cp_retencion_det.re_Porcen_retencion, dbo.cp_retencion_det.re_valor_retencion
FROM     dbo.cp_retencion INNER JOIN
                  dbo.cp_retencion_det ON dbo.cp_retencion.IdEmpresa = dbo.cp_retencion_det.IdEmpresa AND dbo.cp_retencion.IdRetencion = dbo.cp_retencion_det.IdRetencion
