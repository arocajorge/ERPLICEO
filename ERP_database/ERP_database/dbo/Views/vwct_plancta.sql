
CREATE VIEW [dbo].[vwct_plancta]
AS
SELECT        dbo.ct_plancta.IdEmpresa, dbo.ct_plancta.IdCtaCble, dbo.ct_plancta.pc_Cuenta, dbo.ct_plancta.IdCtaCblePadre, 
                         dbo.ct_plancta.pc_Naturaleza, dbo.ct_plancta.IdNivelCta, dbo.ct_plancta.IdGrupoCble, dbo.ct_plancta.pc_Estado, dbo.ct_plancta.pc_EsMovimiento, 
                         null pc_es_flujo_efectivo, dbo.ct_plancta.pc_clave_corta, ISNULL(ct_plancta_1.pc_Cuenta, '') AS CuentaPadre, null IdTipoCtaCble
FROM            dbo.ct_plancta LEFT OUTER JOIN
                         dbo.ct_plancta AS ct_plancta_1 ON dbo.ct_plancta.IdEmpresa = ct_plancta_1.IdEmpresa AND dbo.ct_plancta.IdCtaCblePadre = ct_plancta_1.IdCtaCble