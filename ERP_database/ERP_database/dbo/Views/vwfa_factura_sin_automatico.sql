CREATE view [dbo].[vwfa_factura_sin_automatico]
as
SELECT        dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.estado, 
                         dbo.fa_factura_x_cxc_cobro.IdCobro
FROM            dbo.fa_factura_x_cxc_cobro RIGHT OUTER JOIN
                         dbo.cxc_cobro_det ON dbo.fa_factura_x_cxc_cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.fa_factura_x_cxc_cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                         dbo.fa_factura_x_cxc_cobro.IdBodega = dbo.cxc_cobro_det.IdBodega_Cbte AND dbo.fa_factura_x_cxc_cobro.IdCbteVta = dbo.cxc_cobro_det.IdCbte_vta_nota
WHERE        (dbo.cxc_cobro_det.dc_TipoDocumento = 'FACT') AND (dbo.cxc_cobro_det.estado = 'A') AND (dbo.fa_factura_x_cxc_cobro.IdCobro IS NULL)
union all
SELECT        dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento, dbo.cxc_cobro_det.estado, 
                         dbo.fa_factura_x_cxc_cobro.IdCobro
FROM            dbo.cxc_cobro_x_ct_cbtecble INNER JOIN
                         dbo.cxc_cobro_det ON dbo.cxc_cobro_x_ct_cbtecble.cbr_IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.cxc_cobro_x_ct_cbtecble.cbr_IdSucursal = dbo.cxc_cobro_det.IdSucursal AND 
                         dbo.cxc_cobro_x_ct_cbtecble.cbr_IdCobro = dbo.cxc_cobro_det.IdCobro INNER JOIN
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito ON dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdEmpresa AND 
                         dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdCbteCble AND 
                         dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdTipocbte INNER JOIN
                         dbo.fa_factura_x_cxc_cobro ON dbo.cxc_cobro_det.IdEmpresa = dbo.fa_factura_x_cxc_cobro.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.fa_factura_x_cxc_cobro.IdSucursal AND 
                         dbo.cxc_cobro_det.IdBodega_Cbte = dbo.fa_factura_x_cxc_cobro.IdBodega AND dbo.cxc_cobro_det.IdCbte_vta_nota = dbo.fa_factura_x_cxc_cobro.IdCbteVta
WHERE        (dbo.cxc_cobro_det.dc_TipoDocumento = 'FACT') AND (dbo.cxc_cobro_det.estado = 'A')