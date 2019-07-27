CREATE VIEW [web].[VWBAN_002_cancelaciones]
AS
SELECT        dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte, 
                         dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, ISNULL(dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.caj_Caja_Movimiento.IdCbteCble) AS IdCbte_vta_nota, 
                         dbo.cxc_cobro_det.dc_TipoDocumento, CASE WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NULL THEN 'ING ' + CAST(caj_Caja_Movimiento.IdCbteCble AS VARCHAR(20)) WHEN fa_factura.IdCbteVta IS NULL 
                         AND fa_notaCreDeb.IdNota IS NOT NULL THEN 'NTDB ' + fa_notaCreDeb.CodNota ELSE 'FACT ' + fa_factura.vt_NumFactura END AS Referencia, CASE WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NULL 
                         THEN caj_Caja_Movimiento.cm_observacion WHEN fa_factura.IdCbteVta IS NULL AND fa_notaCreDeb.IdNota IS NOT NULL THEN fa_notaCreDeb.sc_observacion ELSE fa_factura.vt_Observacion END AS Observacion, 
                         ISNULL(dbo.caj_Caja_Movimiento_det.cr_Valor,dbo.cxc_cobro_det.dc_ValorPago) AS monto
FROM            dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito LEFT OUTER JOIN
                         dbo.cxc_cobro_x_caj_Caja_Movimiento INNER JOIN
                         dbo.cxc_cobro_det ON dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                         dbo.cxc_cobro_x_caj_Caja_Movimiento.cbr_IdCobro = dbo.cxc_cobro_det.IdCobro RIGHT OUTER JOIN
                         dbo.caj_Caja_Movimiento_det INNER JOIN
                         dbo.caj_Caja_Movimiento ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                         dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte LEFT OUTER JOIN
                         dbo.cxc_cobro_det_x_ct_cbtecble_det ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdEmpresa_ct AND 
                         dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdCbteCble_ct AND dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdTipoCbte_ct AND 
                         dbo.caj_Caja_Movimiento_det.Secuencia = dbo.cxc_cobro_det_x_ct_cbtecble_det.secuencia_ct ON dbo.cxc_cobro_det.secuencial = dbo.cxc_cobro_det_x_ct_cbtecble_det.secuencial_cb AND 
                         dbo.cxc_cobro_det.IdCobro = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdCobro_cb AND dbo.cxc_cobro_det.IdSucursal = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdSucursal_cb AND 
                         dbo.cxc_cobro_det.IdEmpresa = dbo.cxc_cobro_det_x_ct_cbtecble_det.IdEmpresa_cb AND dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND 
                         dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND dbo.cxc_cobro_x_caj_Caja_Movimiento.mcj_IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte ON 
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte LEFT OUTER JOIN
                         dbo.fa_factura ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_factura.IdSucursal AND dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_factura.IdBodega AND 
                         dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_factura.IdCbteVta AND dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_factura.vt_tipoDoc LEFT OUTER JOIN
                         dbo.fa_notaCreDeb ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                         dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_notaCreDeb.IdBodega AND dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_notaCreDeb.IdNota AND 
                         dbo.cxc_cobro_det.dc_TipoDocumento = dbo.fa_notaCreDeb.CodDocumentoTipo
GROUP BY dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte, 
fa_factura.IdCbteVta,fa_notaCreDeb.IdNota,caj_Caja_Movimiento.IdCbteCble,fa_factura.IdCbteVta,fa_notaCreDeb.CodNota,fa_factura.vt_NumFactura,
caj_Caja_Movimiento.cm_observacion,fa_notaCreDeb.sc_observacion,fa_factura.vt_Observacion,
                         dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, ISNULL(dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.caj_Caja_Movimiento.IdCbteCble), dbo.cxc_cobro_det.dc_TipoDocumento, 
                         ISNULL(dbo.caj_Caja_Movimiento_det.cr_Valor,dbo.cxc_cobro_det.dc_ValorPago)
