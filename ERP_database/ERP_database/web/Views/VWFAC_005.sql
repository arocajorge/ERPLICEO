CREATE VIEW WEB.VWFAC_005
AS
SELECT dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, dbo.fa_factura.vt_tipoDoc, dbo.fa_factura.IdCliente,  
                  LTRIM(RTRIM(dbo.tb_persona.pe_nombreCompleto)) AS NomCliente, dbo.fa_factura.vt_fecha, ISNULL(RTIVA.VRetenIVA, 0) AS VRetenIVA, ISNULL(RTFTE.VRetenFTE, 0) AS VRetenFTE, 
                  dbo.fa_factura_resumen.Total - ISNULL(RTIVA.VRetenIVA, 0) - ISNULL(RTFTE.VRetenFTE, 0) AS ValorACobrar, ISNULL(COBR.VCobrado, 0) AS VCobrado, dbo.fa_factura_resumen.Total - ISNULL(COBR.VCobrado, 0) AS Saldo, 
                  'FACTURA' AS TipoDocumento, CASE WHEN tb_persona.IdTipoDocumento = 'PAS' AND fa_factura_resumen.SubtotalSinIVASinDscto > 0 AND fa_factura_resumen.SubtotalIVASinDscto = 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) 
                  END AS EsExportacion, su.Su_Descripcion, su.Su_CodigoEstablecimiento, dbo.fa_factura_resumen.SubtotalIVASinDscto, dbo.fa_factura_resumen.SubtotalSinIVASinDscto, dbo.fa_factura_resumen.SubtotalSinDscto, 
                  dbo.fa_factura_resumen.Descuento, dbo.fa_factura_resumen.SubtotalIVAConDscto, dbo.fa_factura_resumen.SubtotalSinIVAConDscto, dbo.fa_factura_resumen.SubtotalConDscto, dbo.fa_factura_resumen.ValorIVA, 
                  dbo.fa_factura_resumen.Total
FROM     dbo.fa_cliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.fa_factura ON dbo.fa_cliente.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_cliente.IdCliente = dbo.fa_factura.IdCliente INNER JOIN
                  dbo.fa_factura_resumen ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_resumen.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_resumen.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_resumen.IdBodega AND 
                  dbo.fa_factura.IdCbteVta = dbo.fa_factura_resumen.IdCbteVta LEFT OUTER JOIN
                      (SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento, SUM(dbo.cxc_cobro_det.dc_ValorPago) 
                                         AS VRetenIVA
                       FROM      dbo.cxc_cobro_det INNER JOIN
                                         dbo.cxc_cobro ON dbo.cxc_cobro_det.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.cxc_cobro.IdSucursal AND dbo.cxc_cobro_det.IdCobro = dbo.cxc_cobro.IdCobro INNER JOIN
                                         dbo.cxc_cobro_tipo ON dbo.cxc_cobro_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo
                       WHERE   (dbo.cxc_cobro.cr_estado = N'A') AND (dbo.cxc_cobro_det.estado = 'A') AND (dbo.cxc_cobro_tipo.ESRetenIVA = 'S') AND (dbo.cxc_cobro_det.dc_TipoDocumento = 'FACT')
                       GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento) AS RTIVA ON 
                  dbo.fa_factura.IdEmpresa = RTIVA.IdEmpresa AND dbo.fa_factura.IdSucursal = RTIVA.IdSucursal AND dbo.fa_factura.IdBodega = RTIVA.IdBodega_Cbte AND dbo.fa_factura.IdCbteVta = RTIVA.IdCbte_vta_nota AND 
                  dbo.fa_factura.vt_tipoDoc = RTIVA.dc_TipoDocumento LEFT OUTER JOIN
                      (SELECT cxc_cobro_det_2.IdEmpresa, cxc_cobro_det_2.IdSucursal, cxc_cobro_det_2.IdBodega_Cbte, cxc_cobro_det_2.IdCbte_vta_nota, cxc_cobro_det_2.dc_TipoDocumento, SUM(cxc_cobro_det_2.dc_ValorPago) AS VRetenFTE
                       FROM      dbo.cxc_cobro_det AS cxc_cobro_det_2 INNER JOIN
                                         dbo.cxc_cobro AS cxc_cobro_2 ON cxc_cobro_det_2.IdEmpresa = cxc_cobro_2.IdEmpresa AND cxc_cobro_det_2.IdSucursal = cxc_cobro_2.IdSucursal AND cxc_cobro_det_2.IdCobro = cxc_cobro_2.IdCobro INNER JOIN
                                         dbo.cxc_cobro_tipo AS cxc_cobro_tipo_2 ON cxc_cobro_det_2.IdCobro_tipo = cxc_cobro_tipo_2.IdCobro_tipo
                       WHERE   (cxc_cobro_2.cr_estado = N'A') AND (cxc_cobro_det_2.estado = 'A') AND (cxc_cobro_tipo_2.ESRetenFTE = 'S') AND (cxc_cobro_det_2.dc_TipoDocumento = 'FACT')
                       GROUP BY cxc_cobro_det_2.IdEmpresa, cxc_cobro_det_2.IdSucursal, cxc_cobro_det_2.IdBodega_Cbte, cxc_cobro_det_2.IdCbte_vta_nota, cxc_cobro_det_2.dc_TipoDocumento) AS RTFTE ON 
                  dbo.fa_factura.IdEmpresa = RTFTE.IdEmpresa AND dbo.fa_factura.IdSucursal = RTFTE.IdSucursal AND dbo.fa_factura.IdBodega = RTFTE.IdBodega_Cbte AND dbo.fa_factura.IdCbteVta = RTFTE.IdCbte_vta_nota AND 
                  dbo.fa_factura.vt_tipoDoc = RTFTE.dc_TipoDocumento LEFT OUTER JOIN
                      (SELECT cxc_cobro_det_1.IdEmpresa, cxc_cobro_det_1.IdSucursal, cxc_cobro_det_1.IdBodega_Cbte, cxc_cobro_det_1.IdCbte_vta_nota, cxc_cobro_det_1.dc_TipoDocumento, SUM(cxc_cobro_det_1.dc_ValorPago) AS VCobrado
                       FROM      dbo.cxc_cobro_det AS cxc_cobro_det_1 INNER JOIN
                                         dbo.cxc_cobro AS cxc_cobro_1 ON cxc_cobro_det_1.IdEmpresa = cxc_cobro_1.IdEmpresa AND cxc_cobro_det_1.IdSucursal = cxc_cobro_1.IdSucursal AND cxc_cobro_det_1.IdCobro = cxc_cobro_1.IdCobro INNER JOIN
                                         dbo.cxc_cobro_tipo AS cxc_cobro_tipo_1 ON cxc_cobro_det_1.IdCobro_tipo = cxc_cobro_tipo_1.IdCobro_tipo
                       WHERE   (cxc_cobro_1.cr_estado = N'A') AND (cxc_cobro_det_1.estado = 'A') AND (cxc_cobro_det_1.dc_TipoDocumento = 'FACT')
                       GROUP BY cxc_cobro_det_1.IdEmpresa, cxc_cobro_det_1.IdSucursal, cxc_cobro_det_1.IdBodega_Cbte, cxc_cobro_det_1.IdCbte_vta_nota, cxc_cobro_det_1.dc_TipoDocumento) AS COBR ON 
                  dbo.fa_factura.IdEmpresa = COBR.IdEmpresa AND dbo.fa_factura.IdSucursal = COBR.IdSucursal AND dbo.fa_factura.IdBodega = COBR.IdBodega_Cbte AND dbo.fa_factura.IdCbteVta = COBR.IdCbte_vta_nota AND 
                  dbo.fa_factura.vt_tipoDoc = COBR.dc_TipoDocumento LEFT OUTER JOIN
                  dbo.tb_sucursal AS su ON dbo.fa_factura.IdEmpresa = su.IdEmpresa AND dbo.fa_factura.IdSucursal = su.IdSucursal
WHERE  (dbo.fa_factura.Estado = 'A')