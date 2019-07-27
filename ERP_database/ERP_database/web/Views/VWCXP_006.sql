CREATE VIEW [web].[VWCXP_006]
AS
SELECT dbo.cp_retencion.IdEmpresa, dbo.cp_retencion.IdRetencion, dbo.cp_retencion.serie1 + '-' + dbo.cp_retencion.serie2 +'-'+ dbo.cp_retencion.NumRetencion as NumRetencion, 
dbo.cp_retencion.NAutorizacion, dbo.cp_retencion.fecha, 
                  dbo.cp_retencion.observacion, dbo.cp_retencion.IdEmpresa_Ogiro, dbo.cp_retencion.IdCbteCble_Ogiro, dbo.cp_retencion.IdTipoCbte_Ogiro, dbo.cp_retencion_det.Idsecuencia, dbo.cp_retencion_det.re_tipoRet, 
                  dbo.cp_retencion_det.re_baseRetencion, dbo.cp_retencion_det.IdCodigo_SRI, dbo.cp_retencion_det.re_Codigo_impuesto, dbo.cp_retencion_det.re_Porcen_retencion, dbo.cp_retencion_det.re_valor_retencion, dbo.cp_orden_giro.co_serie+'-'+ dbo.cp_orden_giro.co_factura as NumFactura, 
				  dbo.cp_orden_giro.co_baseImponible, dbo.cp_orden_giro.co_valoriva, dbo.tb_persona.pe_nombreCompleto
FROM     dbo.cp_retencion INNER JOIN
                  dbo.cp_retencion_det ON dbo.cp_retencion.IdEmpresa = dbo.cp_retencion_det.IdEmpresa AND dbo.cp_retencion.IdRetencion = dbo.cp_retencion_det.IdRetencion INNER JOIN
                  dbo.cp_orden_giro ON dbo.cp_retencion.IdEmpresa_Ogiro = dbo.cp_orden_giro.IdEmpresa AND dbo.cp_retencion.IdCbteCble_Ogiro = dbo.cp_orden_giro.IdCbteCble_Ogiro AND 
                  dbo.cp_retencion.IdTipoCbte_Ogiro = dbo.cp_orden_giro.IdTipoCbte_Ogiro INNER JOIN
                  dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                  dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona
