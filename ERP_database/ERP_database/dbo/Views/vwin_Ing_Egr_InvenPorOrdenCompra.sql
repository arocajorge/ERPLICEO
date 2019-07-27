CREATE VIEW dbo.vwin_Ing_Egr_InvenPorOrdenCompra
AS
SELECT        dbo.in_Ing_Egr_Inven.IdEmpresa, dbo.in_Ing_Egr_Inven.IdSucursal, dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven.IdNumMovi, dbo.in_movi_inven_tipo.tm_descripcion, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.in_Ing_Egr_Inven.signo, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.cm_observacion, dbo.in_Ing_Egr_Inven.Estado, dbo.in_Ing_Egr_Inven.IdBodega, dbo.tb_bodega.bo_Descripcion, 
                         dbo.tb_sucursal.Su_Descripcion, dbo.in_Ing_Egr_Inven.CodMoviInven, dbo.in_Motivo_Inven.Desc_mov_inv, dbo.in_Ing_Egr_Inven.IdMotivo_Inv
FROM            dbo.in_Motivo_Inven INNER JOIN
                         dbo.tb_persona INNER JOIN
                         dbo.cp_proveedor ON dbo.tb_persona.IdPersona = dbo.cp_proveedor.IdPersona INNER JOIN
                         dbo.in_Ing_Egr_Inven INNER JOIN
                         dbo.com_parametro ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.com_parametro.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.com_parametro.IdMovi_inven_tipo_OC ON 
                         dbo.cp_proveedor.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND dbo.cp_proveedor.IdProveedor = dbo.in_Ing_Egr_Inven.IdResponsable INNER JOIN
                         dbo.in_movi_inven_tipo ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_movi_inven_tipo.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_movi_inven_tipo.IdMovi_inven_tipo ON 
                         dbo.in_Motivo_Inven.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND dbo.in_Motivo_Inven.IdMotivo_Inv = dbo.in_Ing_Egr_Inven.IdMotivo_Inv LEFT OUTER JOIN
                         dbo.tb_sucursal INNER JOIN
                         dbo.tb_bodega ON dbo.tb_sucursal.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.tb_bodega.IdSucursal ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_bodega.IdEmpresa AND 
                         dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_bodega.IdSucursal AND dbo.in_Ing_Egr_Inven.IdBodega = dbo.tb_bodega.IdBodega
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_InvenPorOrdenCompra';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Motivo_Inven"
            Begin Extent = 
               Top = 198
               Left = 678
               Bottom = 328
               Right = 870
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
      Begin ColumnWidths = 16
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_InvenPorOrdenCompra';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[28] 2[12] 3) )"
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
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 2
               Left = 471
               Bottom = 132
               Right = 732
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 178
               Left = 48
               Bottom = 308
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 303
               Left = 65
               Bottom = 433
               Right = 297
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ing_Egr_Inven"
            Begin Extent = 
               Top = 177
               Left = 372
               Bottom = 413
               Right = 563
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "com_parametro"
            Begin Extent = 
               Top = 598
               Left = 554
               Bottom = 728
               Right = 783
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_movi_inven_tipo"
            Begin Extent = 
               Top = 576
               Left = 81
               Bottom = 745
               Right = 296
      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Ing_Egr_InvenPorOrdenCompra';

