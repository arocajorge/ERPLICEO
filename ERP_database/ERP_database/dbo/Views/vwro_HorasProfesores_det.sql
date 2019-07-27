CREATE VIEW dbo.vwro_HorasProfesores_det
AS
SELECT        dbo.ro_HorasProfesores_det.IdEmpresa, dbo.ro_HorasProfesores_det.IdCarga, dbo.ro_HorasProfesores_det.Secuencia, dbo.ro_HorasProfesores_det.IdRubro, dbo.ro_HorasProfesores_det.IdEmpresa_nov, 
                         dbo.ro_HorasProfesores_det.IdNovedad, dbo.ro_HorasProfesores_det.Observacion, dbo.ro_HorasProfesores_det.IdEmpleado, dbo.ro_HorasProfesores_det.NumHoras, dbo.ro_rubro_tipo.rub_codigo, 
                         dbo.ro_rubro_tipo.ru_codRolGen, dbo.ro_rubro_tipo.ru_descripcion, dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.ro_HorasProfesores_det.IdSucursal, dbo.ro_HorasProfesores_det.ValorHora, dbo.ro_empleado_novedad_det.Valor
FROM            dbo.ro_empleado_novedad_det INNER JOIN
                         dbo.ro_rubro_tipo ON dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_empleado_novedad_det.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                         dbo.ro_HorasProfesores_det INNER JOIN
                         dbo.ro_empleado ON dbo.ro_HorasProfesores_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_HorasProfesores_det.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_empleado_Novedad ON dbo.ro_HorasProfesores_det.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND dbo.ro_HorasProfesores_det.IdNovedad = dbo.ro_empleado_Novedad.IdNovedad AND 
                         dbo.ro_HorasProfesores_det.IdEmpleado = dbo.ro_empleado_Novedad.IdEmpleado ON dbo.ro_empleado_novedad_det.IdEmpresa = dbo.ro_empleado_Novedad.IdEmpresa AND 
                         dbo.ro_empleado_novedad_det.IdNovedad = dbo.ro_empleado_Novedad.IdNovedad
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_HorasProfesores_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'84
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_HorasProfesores_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[26] 4[5] 2[5] 3) )"
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
         Begin Table = "ro_HorasProfesores_det"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 295
               Right = 262
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 22
               Left = 594
               Bottom = 334
               Right = 883
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 278
               Left = 658
               Bottom = 504
               Right = 897
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 0
               Left = 796
               Bottom = 291
               Right = 1028
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_Novedad"
            Begin Extent = 
               Top = 27
               Left = 383
               Bottom = 233
               Right = 580
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_novedad_det"
            Begin Extent = 
               Top = 191
               Left = 212
               Bottom = 432
               Right = 496
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
      Begin ColumnWidths = 21
         Width = 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_HorasProfesores_det';

