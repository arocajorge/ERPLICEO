CREATE VIEW vwba_Caja_Movimiento_x_Cbte_Ban_x_Deposito
AS
SELECT        dbo.caj_Caja_Movimiento_det.IdEmpresa, dbo.caj_Caja_Movimiento_det.IdTipocbte, dbo.caj_Caja_Movimiento_det.IdCbteCble, dbo.caj_Caja_Movimiento_det.Secuencia, dbo.cxc_cobro_tipo.tc_descripcion, 
                         dbo.caj_Caja_Movimiento_det.cr_Valor, dbo.caj_Caja_Movimiento.cm_fecha, dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento_Tipo.tm_descripcion, dbo.caj_Caja_Movimiento.Estado, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.cr_NumDocumento, dbo.caj_Caja.IdCtaCble, dbo.caj_Caja.IdCaja, dbo.caj_Caja.ca_Descripcion, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdEmpresa, 
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdTipocbte, dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mba_IdCbteCble
FROM            dbo.caj_Caja_Movimiento_det INNER JOIN
                         dbo.cxc_cobro_tipo ON dbo.caj_Caja_Movimiento_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                         dbo.caj_Caja_Movimiento ON dbo.caj_Caja_Movimiento_det.IdEmpresa = dbo.caj_Caja_Movimiento.IdEmpresa AND dbo.caj_Caja_Movimiento_det.IdCbteCble = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                         dbo.caj_Caja_Movimiento_det.IdTipocbte = dbo.caj_Caja_Movimiento.IdTipocbte INNER JOIN
                         dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_Tipo.IdEmpresa AND dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                         dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.caj_Caja ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCaja = dbo.caj_Caja.IdCaja INNER JOIN
                         dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdEmpresa AND 
                         dbo.caj_Caja_Movimiento.IdCbteCble = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdCbteCble AND 
                         dbo.caj_Caja_Movimiento.IdTipocbte = dbo.ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito.mcj_IdTipocbte LEFT OUTER JOIN
                         dbo.cxc_cobro_x_ct_cbtecble INNER JOIN
                         dbo.cxc_cobro ON dbo.cxc_cobro_x_ct_cbtecble.cbr_IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.cxc_cobro_x_ct_cbtecble.cbr_IdSucursal = dbo.cxc_cobro.IdSucursal AND 
                         dbo.cxc_cobro_x_ct_cbtecble.cbr_IdCobro = dbo.cxc_cobro.IdCobro ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.cxc_cobro_x_ct_cbtecble.ct_IdEmpresa AND 
                         dbo.caj_Caja_Movimiento.IdTipocbte = dbo.cxc_cobro_x_ct_cbtecble.ct_IdTipoCbte AND dbo.caj_Caja_Movimiento.IdCbteCble = dbo.cxc_cobro_x_ct_cbtecble.ct_IdCbteCble
WHERE        (dbo.caj_Caja_Movimiento_Tipo.SeDeposita = 1) AND (dbo.caj_Caja_Movimiento.Estado = 'A')