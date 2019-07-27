/*order by dbo.tb_persona.pe_apellido asc*/
CREATE VIEW web.VWROL_014
AS
SELECT        dbo.ro_Departamento.IdDepartamento, dbo.ro_Departamento.de_descripcion, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre,
                             (SELECT        RUBRO
                               FROM            dbo.vwro_rubros_acumulados_x_empleados AS D
                               WHERE        (IdEmpleado = dbo.ro_empleado.IdEmpleado) AND (IdEmpresa = dbo.ro_empleado.IdEmpresa) AND (IdRubro = 15)) AS Decimo_Cuarto,
                             (SELECT        RUBRO
                               FROM            dbo.vwro_rubros_acumulados_x_empleados AS D
                               WHERE        (IdEmpleado = dbo.ro_empleado.IdEmpleado) AND (IdEmpresa = dbo.ro_empleado.IdEmpresa) AND (IdRubro = 16)) AS Decimo_Tercero,
                             (SELECT        RUBRO
                               FROM            dbo.vwro_rubros_acumulados_x_empleados AS D
                               WHERE        (IdEmpleado = dbo.ro_empleado.IdEmpleado) AND (IdEmpresa = dbo.ro_empleado.IdEmpresa) AND (IdRubro = 19)) AS Fondos_Reservas, dbo.ro_empleado.IdDivision, dbo.ro_empleado.IdEmpresa, 
                         dbo.ro_empleado.IdEmpleado, dbo.ro_contrato.IdNomina AS IdTipoNomina, dbo.ro_contrato.EstadoContrato, dbo.ro_empleado.IdArea, dbo.ro_area.Descripcion, dbo.ro_Division.Descripcion AS Division_Descripcion, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.ro_empleado.IdSucursal
FROM            dbo.ro_empleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_Departamento ON dbo.ro_empleado.IdEmpresa = dbo.ro_Departamento.IdEmpresa AND dbo.ro_empleado.IdDepartamento = dbo.ro_Departamento.IdDepartamento INNER JOIN
                         dbo.ro_contrato ON dbo.ro_empleado.IdEmpresa = dbo.ro_contrato.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_contrato.IdEmpleado INNER JOIN
                         dbo.ro_area ON dbo.ro_empleado.IdEmpresa = dbo.ro_area.IdEmpresa AND dbo.ro_empleado.IdArea = dbo.ro_area.IdArea INNER JOIN
                         dbo.ro_Division ON dbo.ro_empleado.IdEmpresa = dbo.ro_Division.IdEmpresa AND dbo.ro_empleado.IdDivision = dbo.ro_Division.IdDivision AND dbo.ro_area.IdEmpresa = dbo.ro_Division.IdEmpresa AND 
                         dbo.ro_area.IdDivision = dbo.ro_Division.IdDivision INNER JOIN
                         dbo.tb_sucursal ON dbo.ro_empleado.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_empleado.IdSucursal = dbo.tb_sucursal.IdSucursal
WHERE        (dbo.ro_empleado.em_status <> 'EST_LIQ') AND (dbo.ro_contrato.EstadoContrato = 'ECT_ACT')


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_014';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_014';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[57] 4[5] 2[19] 3) )"
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
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Departamento"
            Begin Extent = 
               Top = 220
               Left = 254
               Bottom = 350
               Right = 459
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_contrato"
            Begin Extent = 
               Top = 5
               Left = 705
               Bottom = 280
               Right = 884
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "ro_area"
            Begin Extent = 
               Top = 98
               Left = 534
               Bottom = 417
               Right = 713
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_Division"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 332
               Left = 476
               Bottom = 462
               Right = 706
            End
      ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWROL_014';







