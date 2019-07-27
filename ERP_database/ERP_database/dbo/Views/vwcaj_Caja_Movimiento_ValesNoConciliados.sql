CREATE view [dbo].[vwcaj_Caja_Movimiento_ValesNoConciliados]
as
SELECT IdEmpresa, IdTipocbte, IdCbteCble, IdCaja, cm_valor
FROM     dbo.caj_Caja_Movimiento AS c
WHERE  (cm_Signo = '-') AND (Estado = 'A')
and not exists(
select f.IdEmpresa from cp_conciliacion_Caja_det_x_ValeCaja as f
where c.IdEmpresa= f.IdEmpresa_movcaja
and c.IdTipocbte = f.IdTipocbte_movcaja
and c.IdCbteCble = f.IdCbteCble_movcaja
)