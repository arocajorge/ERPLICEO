CREATE VIEW web.vwfa_factura
AS
SELECT dbo.fa_factura.IdEmpresa, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_fecha, dbo.tb_persona.pe_nombreCompleto AS Nombres, 
                  dbo.fa_Vendedor.Ve_Vendedor, det.vt_Subtotal0, det.vt_SubtotalIVA, det.vt_iva, det.vt_total, dbo.fa_factura.Estado, dbo.fa_factura.esta_impresa, dbo.fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_in_eg_x_inv, 
                  dbo.fa_factura_x_in_Ing_Egr_Inven.IdSucursal_in_eg_x_inv, dbo.fa_factura_x_in_Ing_Egr_Inven.IdMovi_inven_tipo_in_eg_x_inv, dbo.fa_factura_x_in_Ing_Egr_Inven.IdNumMovi_in_eg_x_inv, dbo.fa_factura.Fecha_Autorizacion, 
                  dbo.fa_factura.vt_autorizacion, dbo.fa_factura.IdAlumno, tb_persona_1.pe_nombreCompleto AS NombresAlumno, dbo.fa_factura.IdUsuario
FROM     dbo.tb_persona AS tb_persona_1 INNER JOIN
                  dbo.aca_Alumno ON tb_persona_1.IdPersona = dbo.aca_Alumno.IdPersona RIGHT OUTER JOIN
                  dbo.fa_factura INNER JOIN
                  dbo.fa_Vendedor ON dbo.fa_factura.IdEmpresa = dbo.fa_Vendedor.IdEmpresa AND dbo.fa_factura.IdVendedor = dbo.fa_Vendedor.IdVendedor LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdSucursal, IdBodega, IdCbteVta, SUM(vt_Subtotal0) AS vt_Subtotal0, SUM(vt_SubtotalIVA) AS vt_SubtotalIVA, SUM(vt_iva) AS vt_iva, SUM(vt_total) AS vt_total
                       FROM      (SELECT IdEmpresa, IdSucursal, IdBodega, IdCbteVta, CASE WHEN vt_por_iva = 0 THEN vt_Subtotal ELSE 0 END AS vt_Subtotal0, CASE WHEN vt_por_iva > 0 THEN vt_Subtotal ELSE 0 END AS vt_SubtotalIVA, vt_iva, 
                                                            vt_total
                                          FROM      dbo.fa_factura_det) AS A
                       GROUP BY IdEmpresa, IdSucursal, IdBodega, IdCbteVta) AS det ON dbo.fa_factura.IdCbteVta = det.IdCbteVta AND dbo.fa_factura.IdBodega = det.IdBodega AND dbo.fa_factura.IdSucursal = det.IdSucursal AND 
                  dbo.fa_factura.IdEmpresa = det.IdEmpresa LEFT OUTER JOIN
                  dbo.fa_cliente_contactos ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente_contactos.IdCliente LEFT OUTER JOIN
                  dbo.fa_factura_x_in_Ing_Egr_Inven ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_x_in_Ing_Egr_Inven.IdEmpresa_fa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_x_in_Ing_Egr_Inven.IdSucursal_fa AND 
                  dbo.fa_factura.IdBodega = dbo.fa_factura_x_in_Ing_Egr_Inven.IdBodega_fa AND dbo.fa_factura.IdCbteVta = dbo.fa_factura_x_in_Ing_Egr_Inven.IdCbteVta_fa INNER JOIN
                  dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona ON dbo.aca_Alumno.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.aca_Alumno.IdAlumno = dbo.fa_factura.IdAlumno
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_factura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 196
               Left = 672
               Bottom = 359
               Right = 928
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 387
               Left = 667
               Bottom = 550
               Right = 941
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
      Begin ColumnWidths = 24
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
         Alias = 1524
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_factura';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[36] 2[3] 3) )"
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
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 219
               Left = 1097
               Bottom = 382
               Right = 1371
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 0
               Left = 1202
               Bottom = 163
               Right = 1447
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 227
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 19
         End
         Begin Table = "fa_Vendedor"
            Begin Extent = 
               Top = 25
               Left = 499
               Bottom = 188
               Right = 707
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "det"
            Begin Extent = 
               Top = 202
               Left = 361
               Bottom = 365
               Right = 555
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente_contactos"
            Begin Extent = 
               Top = 252
               Left = 82
               Bottom = 415
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_x_in_Ing_Egr_Inven"
            Begin Extent = 
               Top = 391
               Left = 321
               Bottom = 554
               Right = 615', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwfa_factura';





