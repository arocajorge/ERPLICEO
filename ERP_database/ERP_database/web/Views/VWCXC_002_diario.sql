CREATE view [web].[VWCXC_002_diario]
as

SELECT        dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, 1 as secuencial, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, 
                         dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa AS IdEmpresa_ct, dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte AS IdTipoCbte_ct, dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble AS IdCbteCble_ct, dbo.ct_cbtecble_det.IdCtaCble, 
                         dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor, CASE WHEN ct_cbtecble_det.dc_Valor > 0 THEN ct_cbtecble_det.dc_Valor ELSE 0 END AS dc_Valor_Debe, 
                         CASE WHEN ct_cbtecble_det.dc_Valor < 0 THEN ABS(ct_cbtecble_det.dc_Valor) ELSE 0 END AS dc_Valor_Haber
FROM            dbo.cxc_cobro AS cxc_cobro_1 INNER JOIN
                         dbo.cxc_cobro_det ON cxc_cobro_1.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND cxc_cobro_1.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND cxc_cobro_1.IdCobro = dbo.cxc_cobro_det.IdCobro INNER JOIN
                         dbo.cxc_cobro_x_ct_cbtecble ON cxc_cobro_1.IdEmpresa = dbo.cxc_cobro_x_ct_cbtecble.cbr_IdEmpresa AND cxc_cobro_1.IdSucursal = dbo.cxc_cobro_x_ct_cbtecble.cbr_IdSucursal AND 
                         cxc_cobro_1.IdCobro = dbo.cxc_cobro_x_ct_cbtecble.cbr_IdCobro INNER JOIN
                         dbo.ct_plancta INNER JOIN
                         dbo.ct_cbtecble_det ON dbo.ct_plancta.IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.ct_plancta.IdCtaCble = dbo.ct_cbtecble_det.IdCtaCble ON 
                         dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa = dbo.ct_cbtecble_det.IdEmpresa AND dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte = dbo.ct_cbtecble_det.IdTipoCbte AND 
                         dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble = dbo.ct_cbtecble_det.IdCbteCble
WHERE        (cxc_cobro_1.IdCobro_tipo IS NULL)
GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, 
                         dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa, dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte, dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble, dbo.ct_cbtecble_det.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_cbtecble_det.dc_Valor
