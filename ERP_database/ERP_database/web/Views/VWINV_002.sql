CREATE VIEW web.VWINV_002
AS
SELECT dbo.in_Ing_Egr_Inven_det.IdEmpresa, dbo.in_Ing_Egr_Inven_det.IdSucursal, dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo, dbo.in_Ing_Egr_Inven_det.IdNumMovi, dbo.in_Ing_Egr_Inven_det.Secuencia, 
                  dbo.in_Ing_Egr_Inven_det.IdProducto, dbo.in_Producto.pr_descripcion, dbo.in_Producto.pr_codigo, dbo.tb_sucursal.Su_Descripcion, dbo.tb_bodega.bo_Descripcion, dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion, 
                  dbo.in_UnidadMedida.Descripcion, ABS(dbo.in_Ing_Egr_Inven_det.dm_cantidad_sinConversion) AS dm_cantidad_sinConversion, dbo.in_Ing_Egr_Inven_det.mv_costo AS mv_costo_sinConversion, dbo.in_Ing_Egr_Inven.cm_observacion, 
                  dbo.in_Ing_Egr_Inven.CodMoviInven, dbo.in_Ing_Egr_Inven.cm_fecha, dbo.in_Ing_Egr_Inven.Estado, dbo.in_Motivo_Inven.IdMotivo_Inv, dbo.in_Motivo_Inven.Desc_mov_inv, dbo.in_Producto.lote_num_lote, 
                  dbo.in_Producto.lote_fecha_vcto, dbo.in_presentacion.nom_presentacion, dbo.in_Ing_Egr_Inven.signo, dbo.in_movi_inven_tipo.tm_descripcion, dbo.seg_usuario.Nombre AS NomUsuario, dbo.in_Ing_Egr_Inven.IdResponsable, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.in_movi_inven_tipo INNER JOIN
                  dbo.tb_bodega INNER JOIN
                  dbo.in_Ing_Egr_Inven INNER JOIN
                  dbo.tb_sucursal ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.tb_bodega.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND 
                  dbo.tb_bodega.IdSucursal = dbo.in_Ing_Egr_Inven.IdSucursal AND dbo.tb_bodega.IdBodega = dbo.in_Ing_Egr_Inven.IdBodega ON dbo.in_movi_inven_tipo.IdEmpresa = dbo.in_Ing_Egr_Inven.IdEmpresa AND 
                  dbo.in_movi_inven_tipo.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo INNER JOIN
                  dbo.in_Ing_Egr_Inven_det ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Ing_Egr_Inven_det.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdSucursal = dbo.in_Ing_Egr_Inven_det.IdSucursal AND 
                  dbo.in_Ing_Egr_Inven.IdMovi_inven_tipo = dbo.in_Ing_Egr_Inven_det.IdMovi_inven_tipo AND dbo.in_Ing_Egr_Inven.IdNumMovi = dbo.in_Ing_Egr_Inven_det.IdNumMovi INNER JOIN
                  dbo.in_Producto ON dbo.in_Ing_Egr_Inven_det.IdProducto = dbo.in_Producto.IdProducto AND dbo.in_Ing_Egr_Inven_det.IdEmpresa = dbo.in_Producto.IdEmpresa INNER JOIN
                  dbo.in_UnidadMedida ON dbo.in_Ing_Egr_Inven_det.IdUnidadMedida_sinConversion = dbo.in_UnidadMedida.IdUnidadMedida INNER JOIN
                  dbo.in_Motivo_Inven ON dbo.in_Ing_Egr_Inven.IdEmpresa = dbo.in_Motivo_Inven.IdEmpresa AND dbo.in_Ing_Egr_Inven.IdMotivo_Inv = dbo.in_Motivo_Inven.IdMotivo_Inv INNER JOIN
                  dbo.in_presentacion ON dbo.in_Producto.IdEmpresa = dbo.in_presentacion.IdEmpresa AND dbo.in_Producto.IdPresentacion = dbo.in_presentacion.IdPresentacion LEFT OUTER JOIN
                  dbo.tb_persona ON dbo.in_Ing_Egr_Inven.IdResponsable = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                  dbo.seg_usuario ON dbo.in_Ing_Egr_Inven.IdUsuario = dbo.seg_usuario.IdUsuario
WHERE  (dbo.in_Ing_Egr_Inven.signo = '-')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[90] 4[3] 2[3] 3) )"
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
         Begin Table = "in_movi_inven_tipo"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 303
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 360
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ing_Egr_Inven"
            Begin Extent = 
               Top = 167
               Left = 700
               Bottom = 521
               Right = 915
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Ing_Egr_Inven_det"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 357
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Producto"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_UnidadMedida"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 25', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWINV_002';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'6
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_Motivo_Inven"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_presentacion"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "seg_usuario"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 342
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 142
               Left = 1005
               Bottom = 536
               Right = 1279
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
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWINV_002';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWINV_002';

