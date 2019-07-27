CREATE view web.VWROL_009 as
SELECT
	dbo.tb_persona.pe_cedulaRuc AS CedulaRuc,
	dbo.tb_persona.pe_nombreCompleto AS NombreCompleto,
	dbo.ro_empleado_novedad_det.IdRubro,
	dbo.ro_empleado_novedad_det.FechaPago,
	dbo.ro_empleado_novedad_det.Valor,
	dbo.ro_empleado_novedad_det.EstadoCobro,
	dbo.ro_rubro_tipo.ru_descripcion AS RubroDescripcion,
	dbo.ro_Division.Descripcion AS Division,
	dbo.ro_Departamento.de_descripcion AS Departamento,
	dbo.ro_empleado_Novedad.IdEmpresa,
	dbo.ro_empleado_Novedad.IdEmpleado,
	dbo.ro_empleado.IdDivision,
	dbo.ro_empleado.em_codigo AS CodigoEmpleado,
	dbo.ro_Departamento.IdDepartamento,
	dbo.tb_persona.pe_apellido,
	dbo.tb_persona.pe_nombre,
	dbo.ro_cargo.ca_descripcion,
	ro_empleado.IdArea,
	ro_rubro_tipo.ru_tipo,
	dbo.ro_empleado_Novedad.IdSucursal,
	dbo.tb_sucursal.Su_Descripcion,
	dbo.ro_empleado_Novedad.IdNomina_Tipo,
	dbo.ro_Nomina_Tipo.Descripcion AS Descripcion_Nomina_Tipo
FROM
	dbo.ro_empleado_novedad_det
INNER JOIN dbo.ro_rubro_tipo ON dbo.ro_empleado_novedad_det.IdRubro = dbo.ro_rubro_tipo.IdRubro
AND dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa
INNER JOIN dbo.ro_empleado ON dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_empleado.IdEmpresa
INNER JOIN dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
INNER JOIN dbo.ro_empleado_Novedad ON dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa
AND dbo.ro_empleado_novedad_det.IdNovedad = dbo.ro_empleado_Novedad.IdNovedad
AND dbo.ro_empleado_Novedad.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado
AND dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa
AND dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado
LEFT JOIN dbo.tb_sucursal ON dbo.tb_sucursal.IdEmpresa = dbo.ro_empleado_novedad_det.IdEmpresa
AND dbo.tb_sucursal.IdSucursal = dbo.ro_empleado_Novedad.IdSucursal
INNER JOIN dbo.ro_Nomina_Tipo ON ro_Nomina_Tipo.IdEmpresa = dbo.ro_empleado_novedad_det.IdEmpresa
AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = dbo.ro_empleado_Novedad.IdNomina_Tipo
INNER JOIN dbo.ro_Division ON dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision
AND dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa
INNER JOIN dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa
AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento
INNER JOIN dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa
AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo
WHERE
	ro_empleado_novedad.Estado = 'A'
UNION ALL
	SELECT
		dbo.tb_persona.pe_cedulaRuc,
		dbo.tb_persona.pe_nombreCompleto,
		dbo.ro_rubro_tipo.IdRubro,
		GETDATE() AS FechaPago,
		dbo.ro_empleado_x_ro_rubro.Valor,
		'PEN' AS EstadoCobro,
		dbo.ro_rubro_tipo.ru_descripcion,
		dbo.ro_Division.Descripcion,
		dbo.ro_Departamento.de_descripcion,
		dbo.ro_empleado.IdEmpresa,
		dbo.ro_empleado.IdEmpleado,
		dbo.ro_empleado.IdDivision,
		dbo.ro_empleado.em_codigo,
		dbo.ro_empleado.IdDepartamento,
		dbo.tb_persona.pe_apellido,
		dbo.tb_persona.pe_nombre,
		dbo.ro_cargo.ca_descripcion,
		ro_empleado.IdArea,
		ro_rubro_tipo.ru_tipo,
		dbo.ro_empleado.IdSucursal,
		dbo.tb_sucursal.Su_Descripcion,
		ro_empleado_x_ro_rubro.IdNomina_Tipo,
		dbo.ro_Nomina_Tipo.Descripcion
	FROM
		dbo.ro_rubro_tipo
	INNER JOIN dbo.ro_empleado_x_ro_rubro ON dbo.ro_rubro_tipo.IdEmpresa = dbo.ro_empleado_x_ro_rubro.IdEmpresa
	AND dbo.ro_rubro_tipo.IdRubro = dbo.ro_empleado_x_ro_rubro.IdRubro
	INNER JOIN dbo.ro_empleado ON dbo.ro_empleado_x_ro_rubro.IdEmpresa = dbo.ro_empleado.IdEmpresa
	AND dbo.ro_empleado_x_ro_rubro.IdEmpleado = dbo.ro_empleado.IdEmpleado
	INNER JOIN dbo.tb_sucursal ON dbo.tb_sucursal.IdEmpresa = dbo.ro_empleado_x_ro_rubro.IdEmpresa
	AND dbo.tb_sucursal.IdSucursal = dbo.ro_empleado.IdSucursal
	INNER JOIN dbo.ro_Nomina_Tipo ON ro_Nomina_Tipo.IdEmpresa = dbo.ro_empleado_x_ro_rubro.IdEmpresa
	AND dbo.ro_Nomina_Tipo.IdNomina_Tipo = ro_empleado_x_ro_rubro.IdNomina_Tipo
	INNER JOIN dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
	INNER JOIN dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa
	AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision
	INNER JOIN dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa
	AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento
	INNER JOIN dbo.ro_cargo ON dbo.ro_empleado.IdEmpresa = dbo.ro_cargo.IdEmpresa
	AND dbo.ro_empleado.IdCargo = dbo.ro_cargo.IdCargo

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_009';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[13] 4[10] 2[73] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_009';

