CREATE VIEW web.vwcp_retencion_det
AS
SELECT        cp_retencion_det.IdEmpresa, cp_retencion_det.IdRetencion, cp_retencion_det.Idsecuencia, cp_retencion_det.re_tipoRet, cp_retencion_det.re_baseRetencion, cp_retencion_det.IdCodigo_SRI, 
                         cp_retencion_det.re_Codigo_impuesto, cp_retencion_det.re_Porcen_retencion, cp_retencion_det.re_valor_retencion, cp_retencion_det.re_estado, cp_codigo_SRI_x_CtaCble.IdCtaCble
FROM            cp_retencion_det LEFT OUTER JOIN
                         cp_codigo_SRI_x_CtaCble ON cp_retencion_det.IdCodigo_SRI = cp_codigo_SRI_x_CtaCble.idCodigo_SRI AND cp_retencion_det.IdEmpresa = cp_codigo_SRI_x_CtaCble.IdEmpresa