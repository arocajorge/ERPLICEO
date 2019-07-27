
create view [dbo].[vwcxc_cobro_x_movi_caja_x_cbtecble]
as
SELECT    E.IdEmpresa, E.IdCobro, E.IdCobro_tipo, E.cr_TotalCobro, E.cr_fecha, E.cr_Banco,
 E.cr_cuenta, E.cr_NumDocumento, H.tc_TipoCbte, a.IdCbteCble as Num_CbteCble, I.IdCaja, I.ca_Descripcion, 
                      G.IdCbteCble AS Movi_Caja, J.tm_descripcion
FROM         caj_Caja AS I INNER JOIN
                      caj_Caja_Movimiento AS G INNER JOIN
                      cxc_cobro_x_caj_Caja_Movimiento AS F ON G.IdEmpresa = F.mcj_IdEmpresa AND G.IdCbteCble = F.mcj_IdCbteCble AND G.IdTipocbte = F.mcj_IdTipocbte INNER JOIN
                      caj_Caja_Movimiento_Tipo AS J ON G.IdTipoMovi = J.IdTipoMovi ON I.IdEmpresa = G.IdEmpresa AND I.IdCaja = G.IdCaja RIGHT OUTER JOIN
                      cxc_cobro AS E RIGHT OUTER JOIN
                      ct_cbtecble AS a INNER JOIN
                      cxc_cobro_x_ct_cbtecble AS b ON a.IdEmpresa = b.ct_IdEmpresa AND a.IdTipoCbte = b.ct_IdTipoCbte AND a.IdCbteCble = b.ct_IdCbteCble INNER JOIN
                      ct_cbtecble_tipo AS H ON a.IdTipoCbte = H.IdTipoCbte ON E.IdEmpresa = b.cbr_IdEmpresa AND E.IdSucursal = b.cbr_IdSucursal AND E.IdCobro = b.cbr_IdCobro ON 
                      F.cbr_IdEmpresa = E.IdEmpresa AND F.cbr_IdSucursal = E.IdSucursal AND F.cbr_IdCobro = E.IdCobro