CREATE VIEW dbo.vwtb_banco_procesos_bancarios_x_empresa
AS
SELECT        dbo.tb_banco_procesos_bancarios_x_empresa.IdEmpresa, dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso, dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso_bancario_tipo, 
                         dbo.tb_banco_procesos_bancarios_x_empresa.Codigo_Empresa, dbo.tb_banco.ba_descripcion, dbo.tb_banco.CodigoLegal, dbo.tb_banco_procesos_bancarios_x_empresa.NombreProceso, 
                         dbo.tb_banco_procesos_bancarios_x_empresa.IdTipoNota, dbo.tb_banco_procesos_bancarios_x_empresa.Se_contabiliza, dbo.tb_banco_procesos_bancarios_x_empresa.estado, dbo.tb_banco.IdBanco, 
                         dbo.tb_banco_procesos_bancarios_x_empresa.IdBanco AS Expr1
FROM            dbo.tb_banco INNER JOIN
                         dbo.tb_banco_procesos_bancarios_x_empresa ON dbo.tb_banco.IdBanco = dbo.tb_banco_procesos_bancarios_x_empresa.IdBanco
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_banco_procesos_bancarios_x_empresa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[67] 4[5] 2[5] 3) )"
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
         Begin Table = "tb_banco"
            Begin Extent = 
               Top = 5
               Left = 507
               Bottom = 245
               Right = 744
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco_procesos_bancarios_x_empresa"
            Begin Extent = 
               Top = 0
               Left = 81
               Bottom = 306
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2580
         Width = 1500
         Width = 2640
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_banco_procesos_bancarios_x_empresa';



