CREATE VIEW dbo.vwseg_Menu_x_Usuario_x_Empresa
AS
SELECT M.IdMenu, M.IdMenuPadre, M.DescripcionMenu, M.PosicionMenu, M.Habilitado, M.Tiene_FormularioAsociado, M.nom_Formulario, M.nom_Asembly, M.nivel, M_x_E_x_U.IdEmpresa, M_x_E_x_U.IdUsuario, M_x_E_x_U.Nuevo, 
                  M_x_E_x_U.Modificar, M_x_E_x_U.Anular
FROM     dbo.seg_Menu AS M INNER JOIN
                  dbo.seg_Menu_x_Empresa AS M_x_E ON M.IdMenu = M_x_E.IdMenu INNER JOIN
                  dbo.seg_Menu_x_Empresa_x_Usuario AS M_x_E_x_U ON M_x_E.IdMenu = M_x_E_x_U.IdMenu AND M_x_E.IdEmpresa = M_x_E_x_U.IdEmpresa
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwseg_Menu_x_Usuario_x_Empresa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[29] 2[20] 3) )"
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
         Begin Table = "M"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 319
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "M_x_E"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 316
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "M_x_E_x_U"
            Begin Extent = 
               Top = 129
               Left = 636
               Bottom = 292
               Right = 880
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3468
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwseg_Menu_x_Usuario_x_Empresa';

