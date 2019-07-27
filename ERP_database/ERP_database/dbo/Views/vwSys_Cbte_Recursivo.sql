create view [dbo].[vwSys_Cbte_Recursivo]
as
select H.IdCtaCblePadre,H.IdCtaCble,SUM(H.Valor) as Valor
from 
(
select B.IdCtaCblePadre,B.IdCtaCble,B.pc_Cuenta,SUM(A.dc_Valor) as Valor
from ct_cbtecble_det A,ct_plancta B
where A.IdEmpresa=B.IdEmpresa and 
A.IdCtaCble=B.IdCtaCble
group by B.IdCtaCblePadre,B.IdCtaCble,B.pc_Cuenta
union 
select C.IdCtaCblePadre,C.IdCtaCble,C.pc_Cuenta,0 as Valor
from ct_plancta C
) H
group by H.IdCtaCblePadre,H.IdCtaCble
--order by 2