CREATE VIEW vwct_plancta_nivel
AS
SELECT        ct_plancta_nivel.IdEmpresa, ct_plancta_nivel.IdNivelCta, ct_plancta_nivel.nv_NumDigitos, ct_plancta_nivel.nv_Descripcion, ct_plancta_nivel.Estado,
				sum(a.nv_NumDigitos) as nv_NumDigitos_total
FROM            ct_plancta_nivel inner join 
(
select IdEmpresa, IdNivelCta, nv_NumDigitos from ct_plancta_nivel
) A on a.IdEmpresa = ct_plancta_nivel.IdEmpresa
and a.IdNivelCta <= ct_plancta_nivel.IdNivelCta
group by ct_plancta_nivel.IdEmpresa, ct_plancta_nivel.IdNivelCta, ct_plancta_nivel.nv_NumDigitos, ct_plancta_nivel.nv_Descripcion, ct_plancta_nivel.Estado