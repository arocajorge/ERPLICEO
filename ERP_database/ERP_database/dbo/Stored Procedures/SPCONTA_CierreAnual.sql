--exec SPCONTA_CierreAnual 2,8,2019
CREATE PROCEDURE [dbo].[SPCONTA_CierreAnual]
(
@IdEmpresa int,
@IdSucursal int,
@IdAnio int
)
AS

select d.IdEmpresa, d.IdCtaCble, ISNULL(ROW_NUMBER() over(order by d.IdEmpresa),0) as Secuencia, d.IdCtaCble + ' - '+ pc.pc_Cuenta as pc_Cuenta, round(sum(d.dc_Valor),2) dc_Valor, case when round(sum(d.dc_Valor),2) > 0 then round(sum(d.dc_Valor),2) else 0 end as dc_Valor_Debe,
case when round(sum(d.dc_Valor),2) < 0 then abs(round(sum(d.dc_Valor),2)) else 0 end as dc_Valor_Haber, d.IdCentroCosto, cc.cc_Descripcion, d.IdPunto_cargo_grupo, pg.nom_punto_cargo_grupo,
d.IdPunto_cargo, pp.nom_punto_cargo
from ct_cbtecble as c inner join 
ct_cbtecble_det as d on c.IdEmpresa = d.IdEmpresa and c.IdTipoCbte = d.IdTipoCbte and c.IdCbteCble = d.IdCbteCble inner join 
ct_plancta as pc on pc.IdEmpresa = d.IdEmpresa and pc.IdCtaCble = d.IdCtaCble inner join
ct_grupocble as g on pc.IdGrupoCble = g.IdGrupoCble left join 
ct_CentroCosto cc on cc.IdEmpresa = d.IdEmpresa and cc.IdCentroCosto = d.IdCentroCosto left join
ct_punto_cargo as pp on pp.IdEmpresa = d.IdEmpresa and pp.IdPunto_cargo = d.IdPunto_cargo left join
ct_punto_cargo_grupo as pg on pg.IdEmpresa = d.IdEmpresa and pg.IdPunto_cargo_grupo = d.IdPunto_cargo_grupo 
where d.IdEmpresa = @IdEmpresa and year(c.cb_Fecha) = @IdAnio and g.gc_estado_financiero = 'ER' and c.IdSucursal = @IdSucursal
group by d.IdEmpresa, d.IdCtaCble, pc.pc_Cuenta,d.IdCentroCosto, cc.cc_Descripcion, d.IdPunto_cargo_grupo, pg.nom_punto_cargo_grupo,
d.IdPunto_cargo, pp.nom_punto_cargo
having round(sum(dc_Valor),2) <> 0
UNION ALL
SELECT d.IdEmpresa, ISNULL(A.IdCtaCbleCierre, '') AS Expr1, 9999999 AS Secuencia, A.IdCtaCbleCierre + ' - ' + cta.pc_Cuenta AS Expr2, ROUND(SUM(d.dc_Valor)*-1, 2) AS dc_Valor, CASE WHEN round(SUM(d .dc_Valor)*-1, 2) 
                  > 0 THEN round(SUM(d .dc_Valor)*-1, 2) ELSE 0 END AS dc_Valor_Debe, CASE WHEN round(SUM(d .dc_Valor)*-1, 2) < 0 THEN abs(round(SUM(d .dc_Valor)*-1, 2)) ELSE 0 END AS dc_Valor_Haber, NULL AS IdCentroCosto, NULL 
                  AS cc_Descripcion, NULL AS IdPunto_cargo_grupo, NULL AS nom_punto_cargo_grupo, NULL AS IdPunto_cargo, NULL AS nom_punto_cargo
FROM     ct_cbtecble AS c INNER JOIN
                  ct_cbtecble_det AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdTipoCbte = d.IdTipoCbte AND c.IdCbteCble = d.IdCbteCble INNER JOIN
                  ct_plancta AS pc ON pc.IdEmpresa = d.IdEmpresa AND pc.IdCtaCble = d.IdCtaCble INNER JOIN
                  ct_grupocble AS g ON pc.IdGrupoCble = g.IdGrupoCble LEFT OUTER JOIN
                  ct_anio_fiscal_x_cuenta_utilidad AS A ON c.IdEmpresa = A.IdEmpresa AND A.IdanioFiscal = YEAR(c.cb_Fecha) LEFT OUTER JOIN
                  ct_plancta AS cta ON cta.IdEmpresa = A.IdEmpresa AND A.IdCtaCbleCierre = cta.IdCtaCble
where d.IdEmpresa = @IdEmpresa and year(c.cb_Fecha) = @IdAnio and g.gc_estado_financiero = 'ER' and c.IdSucursal = @IdSucursal
group by d.IdEmpresa, A.IdCtaCbleCierre, cta.pc_Cuenta