CREATE view vwro_horario_planificacion_det as
SELECT        ROW_NUMBER() OVER (ORDER BY dbo.ro_empleado.IdEmpresa) AS Secuencia, dbo.ro_empleado.IdEmpresa, dbo.ro_empleado.IdEmpleado, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto, dbo.ro_cargo.ca_descripcion, 
                         dbo.ro_area.Descripcion AS ar_descripcion, dbo.ro_Division.Descripcion AS di_descripcion, dbo.ro_Departamento.de_descripcion, dbo.tb_sucursal.Su_Descripcion, dbo.ro_contrato.IdNomina, dbo.tb_sucursal.IdSucursal, 
                         dbo.ro_Departamento.IdDepartamento, dbo.ro_area.IdArea, dbo.ro_Division.IdDivision, dbo.ro_cargo.IdCargo, dbo.ro_horario_planificacion_det.IdPlanificacion, dbo.ro_horario_planificacion_det.IdHorario, 
                         dbo.ro_horario_planificacion_det.fecha, dbo.ro_horario_planificacion_det.IdCalendario
FROM            dbo.ro_horario_planificacion INNER JOIN
                         dbo.ro_horario_planificacion_det ON dbo.ro_horario_planificacion.IdEmpresa = dbo.ro_horario_planificacion_det.IdEmpresa AND 
                         dbo.ro_horario_planificacion.IdPlanificacion = dbo.ro_horario_planificacion_det.IdPlanificacion RIGHT OUTER JOIN
                         dbo.ro_cargo INNER JOIN
                         dbo.tb_persona INNER JOIN
                         dbo.ro_empleado ON dbo.tb_persona.IdPersona = dbo.ro_empleado.IdPersona INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_empleado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_empleado.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.ro_cargo.IdEmpresa = dbo.ro_empleado.IdEmpresa AND 
                         dbo.ro_cargo.IdCargo = dbo.ro_empleado.IdCargo INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.ro_area INNER JOIN
                         dbo.ro_Division ON dbo.ro_area.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_area.IdDivision = dbo.ro_Division.IdDivision ON dbo.ro_empleado.IdDivision = dbo.ro_area.IdDivision AND 
                         dbo.ro_empleado.IdArea = dbo.ro_area.IdArea AND dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado ON 
                         dbo.ro_horario_planificacion_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_horario_planificacion_det.IdEmpleado = dbo.ro_empleado.IdEmpleado

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[5] 2[26] 3) )"
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
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ro_horario_planificacion"
            Begin Extent = 
               Top = 165
               Left = 866
               Bottom = 339
               Right = 1047
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_horario_planificacion_det"
            Begin Extent = 
               Top = 157
               Left = 86
               Bottom = 339
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_cargo"
            Begin Extent = 
               Top = 116
               Left = 191
               Bottom = 246
               Right = 408
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 299
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 19
               Left = 475
               Bottom = 420
               Right = 764
            End
            DisplayFlags = 280
            TopColumn = 13
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 10
               Left = 833
               Bottom = 140
               Right = 1063
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Departamento"
            Begin Extent = 
               Top = 247
               Left = 1032
               Bottom = 377
            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_horario_planificacion_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   Right = 1211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_area"
            Begin Extent = 
               Top = 67
               Left = 877
               Bottom = 250
               Right = 1056
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Division"
            Begin Extent = 
               Top = 20
               Left = 961
               Bottom = 150
               Right = 1140
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_contrato"
            Begin Extent = 
               Top = 93
               Left = 1088
               Bottom = 342
               Right = 1267
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_horario_planificacion_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_horario_planificacion_det';

