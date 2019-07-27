create view [dbo].[vwcp_conciliacion_Caja_det_Ing_Caja_total_aplicado]
as
SELECT        IdEmpresa_movcaj, IdCbteCble_movcaj, IdTipocbte_movcaj, SUM(valor_aplicado) AS Total_aplicado
FROM            cp_conciliacion_Caja_det_Ing_Caja
GROUP BY IdEmpresa_movcaj, IdCbteCble_movcaj, IdTipocbte_movcaj