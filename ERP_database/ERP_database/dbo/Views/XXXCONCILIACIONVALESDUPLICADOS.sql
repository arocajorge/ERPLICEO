CREATE VIEW XXXCONCILIACIONVALESDUPLICADOS
AS
select IdEmpresa, IdConciliacion_Caja, IdEmpresa_movcaja, IdTipocbte_movcaja, IdCbteCble_movcaja, count(*)REpetido 
from cp_conciliacion_Caja_det_x_ValeCaja
group by IdEmpresa, IdConciliacion_Caja, IdEmpresa_movcaja, IdTipocbte_movcaja, IdCbteCble_movcaja
having count(*) > 1