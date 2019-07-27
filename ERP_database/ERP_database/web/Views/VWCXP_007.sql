CREATE VIEW [web].[VWCXP_007]
AS
SELECT isnull(ROW_NUMBER() OVER (ORDER BY A.IdEmpresa), 0) AS IdRow, A.*
FROM     (SELECT dbo.cp_orden_giro.IdEmpresa, dbo.cp_orden_giro.IdTipoCbte_Ogiro, dbo.cp_orden_giro.IdCbteCble_Ogiro, dbo.cp_orden_giro.IdOrden_giro_Tipo, dbo.cp_TipoDocumento.Codigo, dbo.cp_TipoDocumento.Descripcion, 
                                    dbo.cp_proveedor.IdProveedor, tb_persona.pe_nombreCompleto pr_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.cp_orden_giro.co_serie AS serie_fact, dbo.cp_orden_giro.co_factura AS num_factura, 
                                    CASE WHEN cp_retencion.IdRetencion IS NULL THEN dbo.cp_orden_giro.co_FechaFactura ELSE cp_retencion.fecha END AS co_FechaFactura, cp_orden_giro.co_subtotal_iva AS subtotal_iva, 
                                    cp_orden_giro.co_subtotal_siniva AS subtotal_sin_iva, cp_orden_giro.co_valoriva valor_iva, 
                                    CASE WHEN dbo.tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico = 1 THEN dbo.cp_retencion.NAutorizacion ELSE dbo.tb_sis_Documento_Tipo_Talonario.NumAutorizacion END AS NAutorizacion, 
                                    dbo.cp_retencion.serie1 + '-' + dbo.cp_retencion.serie1 AS serie_ret, dbo.cp_retencion.NumRetencion, dbo.cp_retencion_det.re_baseRetencion, dbo.cp_retencion_det.re_Porcen_retencion, 
                                    dbo.cp_retencion_det.re_valor_retencion, dbo.cp_retencion_det.re_Codigo_impuesto, CASE WHEN cp_retencion_det.re_tipoRet = 'IVA' AND 
                                    cp_retencion_det.re_Porcen_retencion = 0 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RIVA_0, CASE WHEN cp_retencion_det.re_tipoRet = 'IVA' AND 
                                    cp_retencion_det.re_Porcen_retencion = 10 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RIVA_10, CASE WHEN cp_retencion_det.re_tipoRet = 'IVA' AND 
                                    cp_retencion_det.re_Porcen_retencion = 20 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RIVA_20, CASE WHEN cp_retencion_det.re_tipoRet = 'IVA' AND 
                                    cp_retencion_det.re_Porcen_retencion = 30 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RIVA_30, CASE WHEN cp_retencion_det.re_tipoRet = 'IVA' AND 
                                    cp_retencion_det.re_Porcen_retencion = 70 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RIVA_70, CASE WHEN cp_retencion_det.re_tipoRet = 'IVA' AND 
                                    cp_retencion_det.re_Porcen_retencion = 100 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RIVA_100, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 0 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_0, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 0.1 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_0_1, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 1 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_1, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 2 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_2, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 8 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_8, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 10 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_10, CASE WHEN cp_retencion_det.re_tipoRet = 'RTF' AND 
                                    cp_retencion_det.re_Porcen_retencion = 100 THEN cp_retencion_det.re_valor_retencion ELSE 0 END AS RTF_100, CASE WHEN cp_retencion_det.re_tipoRet IS NULL 
                                    THEN 'Documento sin retención' ELSE 'Documento con retención' END AS Documento, dbo.cp_codigo_SRI.co_descripcion AS descripcion_cod_sri, dbo.cp_retencion_det.re_tipoRet, 
                                    dbo.cp_orden_giro.Num_Autorizacion AS Num_Autorizacion_OG, cp_orden_giro.IdSucursal, tb_sucursal.Su_Descripcion
                  FROM      dbo.cp_codigo_SRI INNER JOIN
                                    dbo.cp_retencion_det INNER JOIN
                                    dbo.cp_retencion ON dbo.cp_retencion_det.IdEmpresa = dbo.cp_retencion.IdEmpresa AND dbo.cp_retencion_det.IdRetencion = dbo.cp_retencion.IdRetencion ON 
                                    dbo.cp_codigo_SRI.IdCodigo_SRI = dbo.cp_retencion_det.IdCodigo_SRI INNER JOIN
                                    dbo.tb_sis_Documento_Tipo_Talonario ON dbo.cp_retencion.IdEmpresa = dbo.tb_sis_Documento_Tipo_Talonario.IdEmpresa AND 
                                    dbo.cp_retencion.CodDocumentoTipo = dbo.tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo AND dbo.cp_retencion.serie2 = dbo.tb_sis_Documento_Tipo_Talonario.PuntoEmision AND 
                                    dbo.cp_retencion.serie1 = dbo.tb_sis_Documento_Tipo_Talonario.Establecimiento AND dbo.cp_retencion.NumRetencion = dbo.tb_sis_Documento_Tipo_Talonario.NumDocumento RIGHT OUTER JOIN
                                    dbo.cp_TipoDocumento INNER JOIN
                                    dbo.cp_orden_giro INNER JOIN
                                    dbo.cp_proveedor ON dbo.cp_orden_giro.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_orden_giro.IdProveedor = dbo.cp_proveedor.IdProveedor ON 
                                    dbo.cp_TipoDocumento.CodTipoDocumento = dbo.cp_orden_giro.IdOrden_giro_Tipo INNER JOIN
                                    dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona ON dbo.cp_retencion.IdEmpresa_Ogiro = dbo.cp_orden_giro.IdEmpresa AND 
                                    dbo.cp_retencion.IdCbteCble_Ogiro = dbo.cp_orden_giro.IdCbteCble_Ogiro AND dbo.cp_retencion.IdTipoCbte_Ogiro = dbo.cp_orden_giro.IdTipoCbte_Ogiro INNER JOIN
                                    tb_persona AS per ON cp_proveedor.IdPersona = per.IdPersona INNER JOIN
									tb_sucursal ON dbo.cp_orden_giro.IdEmpresa = tb_sucursal.IdEmpresa AND dbo.cp_orden_giro.IdSucursal = tb_sucursal.IdSucursal
                  WHERE   (dbo.cp_orden_giro.Estado = 'A')
                  /*and cp_orden_giro.IdEmpresa = 3 and cp_orden_giro.IdCbteCble_Ogiro = 248*/ UNION
                  SELECT cp_nota_DebCre.IdEmpresa, cp_nota_DebCre.IdTipoCbte_Nota, cp_nota_DebCre.IdCbteCble_Nota, '04', 'N/C', 'N/C Compras' AS Descripcion, cp_proveedor.IdProveedor, tb_persona.pe_nombreCompleto pr_nombre, 
                                    tb_persona.pe_cedulaRuc, cp_nota_DebCre.cn_serie1 + '-' + cp_nota_DebCre.cn_serie2 AS serie_fact, cp_nota_DebCre.cn_Nota AS num_factura, cp_nota_DebCre.cn_fecha, cp_nota_DebCre.cn_subtotal_iva AS subtotal_iva, 
                                    cp_nota_DebCre.cn_subtotal_siniva AS subtotal_sin_iva, cp_nota_DebCre.cn_valoriva AS valor_iva, cp_nota_DebCre.cn_Autorizacion, NULL AS serie_ret, NULL AS NumRetencion, NULL AS re_baseRetencion, NULL 
                                    AS re_Porcen_retencion, NULL AS re_valor_retencion, NULL AS re_Codigo_impuesto, 0 AS RIVA_0, 0 AS RIVA_10, 0 AS RIVA_20, 0 AS RIVA_30, 0 AS RIVA_70, 0 AS RIVA_100, 0 AS RTF_0, 0 AS RTF_0_1, 0 AS RTF_1, 0 AS RTF_2, 
                                    0 AS RTF_8, 0 AS RTF_10, 0 AS RTF_100, 'N/C Compras' AS Documento, 'N/C Compras' AS descripcion_cod_sri, NULL AS re_tipoRet, NULL, cp_nota_DebCre.IdSucursal, tb_sucursal.Su_Descripcion
                  FROM     cp_nota_DebCre INNER JOIN
                                    cp_proveedor ON cp_nota_DebCre.IdEmpresa = cp_proveedor.IdEmpresa AND cp_nota_DebCre.IdProveedor = cp_proveedor.IdProveedor INNER JOIN
                                    tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
									tb_sucursal ON dbo.cp_nota_DebCre.IdEmpresa = tb_sucursal.IdEmpresa AND dbo.cp_nota_DebCre.IdSucursal = tb_sucursal.IdSucursal
                  WHERE  (cp_nota_DebCre.cn_serie2 IS NOT NULL) AND (cp_nota_DebCre.DebCre = 'C') AND cp_nota_DebCre.Estado = 'A'
                  UNION
                  SELECT cp_nota_DebCre.IdEmpresa, cp_nota_DebCre.IdTipoCbte_Nota, cp_nota_DebCre.IdCbteCble_Nota, '05', 'N/D', 'N/D Compras' AS Descripcion, cp_proveedor.IdProveedor, tb_persona.pe_nombreCompleto pr_nombre, 
                                    tb_persona.pe_cedulaRuc, cp_nota_DebCre.cn_serie1 + '-' + cp_nota_DebCre.cn_serie2 AS serie_fact, cp_nota_DebCre.cn_Nota AS num_factura, cp_nota_DebCre.cn_fecha, cp_nota_DebCre.cn_subtotal_iva AS subtotal_iva, 
                                    cp_nota_DebCre.cn_subtotal_siniva AS subtotal_sin_iva, cp_nota_DebCre.cn_valoriva AS valor_iva, cp_nota_DebCre.cn_Autorizacion, NULL AS serie_ret, NULL AS NumRetencion, NULL AS re_baseRetencion, NULL 
                                    AS re_Porcen_retencion, NULL AS re_valor_retencion, NULL AS re_Codigo_impuesto, 0 AS RIVA_0, 0 AS RIVA_10, 0 AS RIVA_20, 0 AS RIVA_30, 0 AS RIVA_70, 0 AS RIVA_100, 0 AS RTF_0, 0 AS RTF_0_1, 0 AS RTF_1, 0 AS RTF_2, 
                                    0 AS RTF_8, 0 AS RTF_10, 0 AS RTF_100, 'N/D Compras' AS Documento, 'N/D Compras' AS descripcion_cod_sri, NULL AS re_tipoRet, NULL, cp_nota_DebCre.IdSucursal, tb_sucursal.Su_Descripcion
                  FROM     cp_nota_DebCre INNER JOIN
                                    cp_proveedor ON cp_nota_DebCre.IdEmpresa = cp_proveedor.IdEmpresa AND cp_nota_DebCre.IdProveedor = cp_proveedor.IdProveedor INNER JOIN
                                    tb_persona ON cp_proveedor.IdPersona = tb_persona.IdPersona INNER JOIN
									tb_sucursal ON dbo.cp_nota_DebCre.IdEmpresa = tb_sucursal.IdEmpresa AND dbo.cp_nota_DebCre.IdSucursal = tb_sucursal.IdSucursal
                  WHERE  (cp_nota_DebCre.cn_serie2 IS NOT NULL) AND (cp_nota_DebCre.DebCre = 'D') AND cp_nota_DebCre.Estado = 'A') A