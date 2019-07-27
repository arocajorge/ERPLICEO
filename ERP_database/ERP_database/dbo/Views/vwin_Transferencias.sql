CREATE VIEW [dbo].[vwin_Transferencias]
AS
SELECT SucuOrig.Su_Descripcion AS SucuOrigen, bodegaOri.bo_Descripcion AS BodegaORIG, sucuDest.Su_Descripcion AS SucuDEST, bodegaDes.bo_Descripcion AS BodegDest, dbo.in_transferencia.IdEmpresa, 
                  dbo.in_transferencia.IdSucursalOrigen, dbo.in_transferencia.IdBodegaOrigen, dbo.in_transferencia.IdTransferencia, dbo.in_transferencia.IdSucursalDest, dbo.in_transferencia.IdBodegaDest, dbo.in_transferencia.tr_Observacion, 
                  dbo.in_transferencia.tr_fecha, dbo.in_transferencia.Estado, dbo.in_transferencia.IdUsuario, dbo.in_transferencia.IdEmpresa_Ing_Egr_Inven_Origen, dbo.in_transferencia.IdSucursal_Ing_Egr_Inven_Origen, 
                  dbo.in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen, dbo.in_transferencia.IdEmpresa_Ing_Egr_Inven_Destino, dbo.in_transferencia.IdSucursal_Ing_Egr_Inven_Destino, dbo.in_transferencia.IdNumMovi_Ing_Egr_Inven_Destino, 
                  dbo.in_transferencia.tr_fechaAnulacion, dbo.in_transferencia.tr_userAnulo, dbo.in_transferencia.Codigo, dbo.in_transferencia.IdMovi_inven_tipo_SucuOrig, dbo.in_transferencia.IdMovi_inven_tipo_SucuDest, 
                  dbo.in_transferencia.IdEstadoAprobacion_cat, dbo.in_Catalogo.Nombre AS EstadoAprobacion_cat, Ingreso.IdEstadoAproba AS IdEstadoAproba_ing, Egreso.IdEstadoAproba AS IdEstadoAproba_egr
FROM     dbo.in_Ing_Egr_Inven AS Egreso RIGHT OUTER JOIN
                  dbo.in_Ing_Egr_Inven AS Ingreso RIGHT OUTER JOIN
                  dbo.tb_bodega AS bodegaDes INNER JOIN
                  dbo.tb_sucursal AS sucuDest ON bodegaDes.IdEmpresa = sucuDest.IdEmpresa AND bodegaDes.IdSucursal = sucuDest.IdSucursal INNER JOIN
                  dbo.in_transferencia INNER JOIN
                  dbo.tb_bodega AS bodegaOri ON dbo.in_transferencia.IdEmpresa = bodegaOri.IdEmpresa AND dbo.in_transferencia.IdBodegaOrigen = bodegaOri.IdBodega AND 
                  dbo.in_transferencia.IdSucursalOrigen = bodegaOri.IdSucursal INNER JOIN
                  dbo.tb_sucursal AS SucuOrig ON bodegaOri.IdEmpresa = SucuOrig.IdEmpresa AND bodegaOri.IdSucursal = SucuOrig.IdSucursal ON bodegaDes.IdEmpresa = dbo.in_transferencia.IdEmpresa AND 
                  bodegaDes.IdBodega = dbo.in_transferencia.IdBodegaDest AND bodegaDes.IdSucursal = dbo.in_transferencia.IdSucursalDest INNER JOIN
                  dbo.in_Catalogo ON dbo.in_transferencia.IdEstadoAprobacion_cat = dbo.in_Catalogo.IdCatalogo ON Ingreso.IdEmpresa = dbo.in_transferencia.IdEmpresa_Ing_Egr_Inven_Destino AND 
                  Ingreso.IdSucursal = dbo.in_transferencia.IdSucursal_Ing_Egr_Inven_Destino AND Ingreso.IdMovi_inven_tipo = dbo.in_transferencia.IdMovi_inven_tipo_SucuDest AND 
                  Ingreso.IdNumMovi = dbo.in_transferencia.IdNumMovi_Ing_Egr_Inven_Destino ON Egreso.IdEmpresa = dbo.in_transferencia.IdEmpresa_Ing_Egr_Inven_Origen AND 
                  Egreso.IdSucursal = dbo.in_transferencia.IdSucursal_Ing_Egr_Inven_Origen AND Egreso.IdMovi_inven_tipo = dbo.in_transferencia.IdMovi_inven_tipo_SucuOrig AND 
                  Egreso.IdNumMovi = dbo.in_transferencia.IdNumMovi_Ing_Egr_Inven_Origen
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[75] 4[5] 2[3] 3) )"
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
         Begin Table = "bodegaDes"
            Begin Extent = 
               Top = 0
               Left = 297
               Bottom = 214
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sucuDest"
            Begin Extent = 
               Top = 0
               Left = 0
               Bottom = 251
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "in_transferencia"
            Begin Extent = 
               Top = 0
               Left = 621
               Bottom = 295
               Right = 897
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "bodegaOri"
            Begin Extent = 
               Top = 14
               Left = 967
               Bottom = 143
               Right = 1228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SucuOrig"
            Begin Extent = 
               Top = 167
               Left = 1033
               Bottom = 296
               Right = 1263
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
         Alias = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Transferencias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'900
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Transferencias';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwin_Transferencias';

