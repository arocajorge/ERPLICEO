create view vwct_cbtecble
as
SELECT        dbo.ct_cbtecble.IdEmpresa, dbo.ct_cbtecble.IdTipoCbte, dbo.ct_cbtecble.IdCbteCble, dbo.ct_cbtecble_tipo.tc_Interno, dbo.ct_cbtecble.cb_Fecha, dbo.ct_cbtecble.cb_Valor, dbo.ct_cbtecble.CodCbteCble, 
                         dbo.ct_cbtecble.IdSucursal, dbo.ct_cbtecble.cb_Observacion, dbo.ct_cbtecble.cb_Estado, dbo.tb_sucursal.Su_Descripcion, dbo.ct_cbtecble_tipo.tc_TipoCbte
FROM            dbo.ct_cbtecble INNER JOIN
                         dbo.ct_cbtecble_tipo ON dbo.ct_cbtecble.IdEmpresa = dbo.ct_cbtecble_tipo.IdEmpresa AND dbo.ct_cbtecble.IdTipoCbte = dbo.ct_cbtecble_tipo.IdTipoCbte INNER JOIN
                         dbo.tb_sucursal ON dbo.ct_cbtecble.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ct_cbtecble.IdSucursal = dbo.tb_sucursal.IdSucursal