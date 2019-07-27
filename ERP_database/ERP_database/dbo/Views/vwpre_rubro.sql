create view vwpre_Rubro as
SELECT        dbo.pre_Rubro.IdEmpresa, dbo.pre_Rubro.IdRubro, dbo.pre_Rubro.IdRubroTipo, dbo.pre_RubroTipo.Descripcion as Descripcion_RubroTipo, dbo.pre_Rubro.Descripcion, dbo.pre_Rubro.IdCtaCble, dbo.ct_plancta.pc_Cuenta, 
                         dbo.pre_Rubro.Estado
FROM            dbo.pre_RubroTipo INNER JOIN
                         dbo.pre_Rubro ON dbo.pre_RubroTipo.IdEmpresa = dbo.pre_Rubro.IdEmpresa AND dbo.pre_RubroTipo.IdRubroTipo = dbo.pre_Rubro.IdRubroTipo LEFT OUTER JOIN
                         dbo.ct_plancta ON dbo.pre_Rubro.IdEmpresa = dbo.ct_plancta.IdEmpresa AND dbo.pre_Rubro.IdCtaCble = dbo.ct_plancta.IdCtaCble