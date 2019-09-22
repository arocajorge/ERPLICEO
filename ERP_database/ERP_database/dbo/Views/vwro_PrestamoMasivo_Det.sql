CREATE VIEW dbo.vwro_PrestamoMasivo_Det
AS
SELECT dbo.ro_PrestamoMasivo_Det.IdEmpresa, dbo.ro_PrestamoMasivo_Det.IdSucursal, dbo.ro_PrestamoMasivo_Det.IdCarga, dbo.ro_PrestamoMasivo_Det.Secuencia, dbo.ro_PrestamoMasivo_Det.IdPrestamo, 
                  dbo.ro_PrestamoMasivo_Det.IdEmpleado, dbo.ro_empleado.IdPersona, dbo.tb_persona.pe_nombreCompleto, dbo.ro_PrestamoMasivo_Det.IdRubro, dbo.ro_rubro_tipo.ru_descripcion, 
                  dbo.ro_PrestamoMasivo_Det.Monto, dbo.ro_PrestamoMasivo_Det.NumCuotas
FROM     dbo.ro_PrestamoMasivo_Det INNER JOIN
                  dbo.ro_rubro_tipo ON dbo.ro_PrestamoMasivo_Det.IdEmpresa = dbo.ro_rubro_tipo.IdEmpresa AND dbo.ro_PrestamoMasivo_Det.IdRubro = dbo.ro_rubro_tipo.IdRubro INNER JOIN
                  dbo.ro_empleado ON dbo.ro_PrestamoMasivo_Det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_PrestamoMasivo_Det.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                  dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_PrestamoMasivo_Det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'     Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_PrestamoMasivo_Det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "ro_PrestamoMasivo_Det"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "ro_rubro_tipo"
            Begin Extent = 
               Top = 146
               Left = 364
               Bottom = 309
               Right = 617
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 0
               Left = 590
               Bottom = 211
               Right = 937
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 12
               Left = 1010
               Bottom = 175
               Right = 1284
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_PrestamoMasivo_Det';

