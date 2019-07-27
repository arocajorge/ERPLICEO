CREATE VIEW dbo.vwin_Ing_Egr_Inven
AS
SELECT        dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_movi_inven_tipo.tm_descripcion, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_Ing_Egr_Inven.IdBodega, 
                         dbo.tb_bodega.bo_Descripcion, dbo.in_Ing_Egr_Inven.signo, dbo.in_Ing_Egr_Inven.CodMoviInven, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.Estado, 
                         dbo.in_Ing_Egr_Inven.IdMotivo_Inv, dbo.in_Motivo_Inven.Desc_mov_inv, dbo.in_Ing_Egr_Inven.IdResponsable, dbo.in_Ing_Egr_Inven.IdEstadoAproba, dbo.in_Ing_Egr_Inven.FechaDespacho, 
                         dbo.in_Ing_Egr_Inven.IdUsuarioAR, dbo.in_Ing_Egr_Inven.FechaAR, dbo.in_Ing_Egr_Inven.IdUsuarioDespacho, dbo.in_Catalogo.Nombre AS EstadoAprobacion
FROM            dbo.in_Ing_Egr_Inven INNER JOIN
                         dbo.tb_bodega ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_bodega.IdSucursal AND dbo.in_Ing_Egr_Inven.IdBodega = dbo.tb_bodega.IdBodega INNER JOIN
                         dbo.in_Motivo_Inven ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Motivo_Inven.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMotivo_Inv = dbo.in_Motivo_Inven.IdMotivo_Inv INNER JOIN
                         dbo.in_Catalogo ON dbo.in_Ing_Egr_Inven.IdEstadoAproba = dbo.in_Catalogo.IdCatalogo LEFT OUTER JOIN
                         dbo.in_movi_inven_tipo ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_movi_inven_tipo.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_movi_inven_tipo.IdMovi_inven_tipo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[9] 2[21] 3) )"
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
         Begin Table = "in_Ing_Egr_Inven"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 207
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 16
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 97
               Left = 441
               Bottom = 227
               Right = 702
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "in_Motivo_Inven"
            Begin Extent = 
               Top = 0
               Left = 738
               Bottom = 130
               Right = 930
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "in_movi_inven_tipo"
            Begin Extent = 
               Top = 2
               Left = 379
               Bottom = 132
               Right = 594
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Catalogo"
            Begin Extent = 
               Top = 113
               Left = 271
               Bottom = 243
               Right = 450
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
      Begin ColumnWidths = 22
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
     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_Inven';



