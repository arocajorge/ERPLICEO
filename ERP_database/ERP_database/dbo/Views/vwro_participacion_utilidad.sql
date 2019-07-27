CREATE VIEW [dbo].[vwro_participacion_utilidad]
	AS 
SELECT        dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa, dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo, dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui, dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo, 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.Cerrado, dbo.ro_periodo_x_ro_Nomina_TipoLiqui.Procesado, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.ro_Nomina_Tipo.Descripcion, 
                         dbo.ro_participacion_utilidad.UtilidadDerechoIndividual, dbo.ro_participacion_utilidad.UtilidadCargaFamiliar, dbo.ro_participacion_utilidad.IdPeriodo AS Expr1, dbo.ro_participacion_utilidad.IdNominaTipo_liq, 
                         dbo.ro_participacion_utilidad.IdNomina, dbo.ro_participacion_utilidad.IdUtilidad, dbo.ro_participacion_utilidad.Estado
FROM            dbo.ro_participacion_utilidad INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_participacion_utilidad.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND 
                         dbo.ro_participacion_utilidad.IdNomina = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND dbo.ro_participacion_utilidad.IdNominaTipo_liq = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui AND 
                         dbo.ro_participacion_utilidad.IdPeriodo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo
