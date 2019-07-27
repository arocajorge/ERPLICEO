CREATE VIEW [web].[VWROL_020]
AS
SELECT ROW_NUMBER() OVER (ORDER BY archiv.IdEmpresa DESC) AS Secuancia, archiv.IdEmpresa, archiv.IdArchivo, archiv.IdNomina, archiv.IdNominaTipo, archiv.IdPeriodo, emp.IdDivision, emp.IdArea, archiv.IdCuentaBancaria, 
archiv.IdProceso, ROUND(archv_det.Valor,2)Valor, CASE WHEN emp.em_tipoCta = 'AHO' THEN 'A' WHEN emp.em_tipoCta = 'COR' THEN 'C' ELSE 'E' END AS em_tipoCta, emp.em_NumCta, per.pe_apellido + ' ' + per.pe_nombre AS Nombres, per.pe_cedulaRuc, 
emp.em_codigo, nom_tip.DescripcionProcesoNomina, nom.Descripcion, periodo.pe_FechaIni, periodo.pe_FechaFin, archv_det.IdSucursal, emp.em_tipoCta AS TipoCuenta, dbo.ro_area.Descripcion AS Area, 
dbo.ro_Division.Descripcion AS Division
FROM     dbo.ro_area INNER JOIN
                  dbo.tb_persona AS per INNER JOIN
                  dbo.ro_empleado AS emp ON per.IdPersona = emp.IdPersona ON dbo.ro_area.IdEmpresa = emp.IdEmpresa AND dbo.ro_area.IdArea = emp.IdArea INNER JOIN
                  dbo.ro_Division ON emp.IdEmpresa = dbo.ro_Division.IdEmpresa AND emp.IdDivision = dbo.ro_Division.IdDivision RIGHT OUTER JOIN
                  dbo.ro_periodo_x_ro_Nomina_TipoLiqui AS pe_x_nom INNER JOIN
                  dbo.ro_archivos_bancos_generacion AS archiv INNER JOIN
                  dbo.ro_archivos_bancos_generacion_x_empleado AS archv_det ON archiv.IdEmpresa = archv_det.IdEmpresa AND archiv.IdArchivo = archv_det.IdArchivo ON pe_x_nom.IdEmpresa = archiv.IdEmpresa AND 
                  pe_x_nom.IdNomina_Tipo = archiv.IdNomina AND pe_x_nom.IdNomina_TipoLiqui = archiv.IdNominaTipo AND pe_x_nom.IdPeriodo = archiv.IdPeriodo INNER JOIN
                  dbo.ro_periodo AS periodo ON pe_x_nom.IdEmpresa = periodo.IdEmpresa AND pe_x_nom.IdPeriodo = periodo.IdPeriodo INNER JOIN
                  dbo.ro_Nomina_Tipoliqui AS nom_tip ON pe_x_nom.IdEmpresa = nom_tip.IdEmpresa AND pe_x_nom.IdNomina_Tipo = nom_tip.IdNomina_Tipo AND pe_x_nom.IdNomina_TipoLiqui = nom_tip.IdNomina_TipoLiqui INNER JOIN
                  dbo.ro_Nomina_Tipo AS nom ON nom_tip.IdEmpresa = nom.IdEmpresa AND nom_tip.IdNomina_Tipo = nom.IdNomina_Tipo ON emp.IdEmpresa = archv_det.IdEmpresa AND emp.IdEmpleado = archv_det.IdEmpleado
GO



GO


