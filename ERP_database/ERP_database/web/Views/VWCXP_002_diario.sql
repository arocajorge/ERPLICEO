
CREATE VIEW [web].[VWCXP_002_diario]
AS
select r.IdEmpresa_Ogiro, r.IdTipoCbte_Ogiro, r.IdCbteCble_Ogiro,
ct.IdEmpresa, ct.IdTipoCbte, ct.IdCbteCble, ct.secuencia, ct.IdCtaCble, pc.pc_Cuenta, ct.dc_Valor, case when ct.dc_Valor > 0 then ct.dc_Valor else 0 end as dc_Valor_Debe,
case when ct.dc_Valor < 0 then ABS(ct.dc_Valor) else 0 end as dc_Valor_Haber
from cp_retencion_x_ct_cbtecble ret
inner join ct_cbtecble_det as ct on ret.ct_IdEmpresa = ct.IdEmpresa
and ret.ct_IdTipoCbte = ct.IdTipoCbte and ret.ct_IdCbteCble = ct.IdCbteCble
inner join ct_plancta as pc on pc.IdEmpresa = ct.IdEmpresa and pc.IdCtaCble = ct.IdCtaCble
inner join cp_retencion as r on ret.rt_IdEmpresa = r.IdEmpresa and ret.rt_IdRetencion = r.IdRetencion
