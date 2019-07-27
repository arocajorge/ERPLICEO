CREATE VIEW web.VWCOMP_001
AS
SELECT        d.IdEmpresa, d.IdSucursal, d.IdOrdenCompra, d.Secuencia, c.Tipo, c.SecuenciaTipo, d.IdProducto, su.Su_Descripcion, c.oc_fecha, c.oc_observacion, c.Estado, term.Descripcion AS NombreTerminoPago, 
                         CAST(c.oc_plazo AS varchar(20)) + ' días' AS oc_plazo, c.IdProveedor, per.pe_nombreCompleto AS NombreProveedor, CASE WHEN prov.pr_telefonos IS NULL 
                         THEN '' ELSE prov.pr_telefonos END + CASE WHEN prov.pr_telefonos IS NOT NULL AND prov.pr_celular IS NOT NULL THEN '-' ELSE '' END + CASE WHEN prov.pr_celular IS NULL 
                         THEN '' ELSE prov.pr_celular END AS TelefonosProveedor, prov.pr_direccion AS DireccionProveedor, per.pe_cedulaRuc AS RucProveedor, com.Descripcion AS NombreComprador, pro.pr_descripcion AS NombreProducto, 
                         uni.Descripcion AS NomUnidadMedida, d.do_Cantidad, d.do_precioCompra, d.do_porc_des, d.do_descuento, d.do_precioFinal, d.do_subtotal, d.do_iva, d.do_subtotal + d.do_iva AS do_total, d.Por_Iva, 
                         CASE WHEN d .Por_Iva > 0 THEN d .do_Cantidad * d .do_precioCompra ELSE 0 END AS SubtotalIVA, CASE WHEN d .Por_Iva = 0 THEN d .do_Cantidad * d .do_precioCompra ELSE 0 END AS Subtotal0, 
                         d.do_Cantidad * d.do_descuento AS DescuentoTotal
FROM            dbo.com_ordencompra_local AS c INNER JOIN
                         dbo.com_ordencompra_local_det AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdSucursal = d.IdSucursal AND c.IdOrdenCompra = d.IdOrdenCompra INNER JOIN
                         dbo.com_comprador AS com ON c.IdEmpresa = com.IdEmpresa AND c.IdComprador = com.IdComprador INNER JOIN
                         dbo.tb_sucursal AS su ON c.IdEmpresa = su.IdEmpresa AND c.IdSucursal = su.IdSucursal INNER JOIN
                         dbo.cp_proveedor AS prov ON c.IdEmpresa = prov.IdEmpresa AND c.IdProveedor = prov.IdProveedor INNER JOIN
                         dbo.tb_persona AS per ON prov.IdPersona = per.IdPersona INNER JOIN
                         dbo.in_UnidadMedida AS uni ON d.IdUnidadMedida = uni.IdUnidadMedida INNER JOIN
                         dbo.com_TerminoPago AS term ON term.IdEmpresa = c.IdEmpresa AND c.IdTerminoPago = term.IdTerminoPago LEFT OUTER JOIN
                         dbo.in_Producto AS pro ON d.IdEmpresa = pro.IdEmpresa AND d.IdProducto = pro.IdProducto
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCOMP_001';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    End
         Begin Table = "term"
            Begin Extent = 
               Top = 666
               Left = 255
               Bottom = 796
               Right = 434
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pro"
            Begin Extent = 
               Top = 685
               Left = 544
               Bottom = 815
               Right = 778
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
      Begin ColumnWidths = 34
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCOMP_001';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[65] 4[5] 2[8] 3) )"
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
         Top = -672
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 24
               Bottom = 137
               Right = 241
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 133
               Left = 420
               Bottom = 306
               Right = 683
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "com"
            Begin Extent = 
               Top = 0
               Left = 606
               Bottom = 192
               Right = 785
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "su"
            Begin Extent = 
               Top = 12
               Left = 705
               Bottom = 142
               Right = 935
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "prov"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "uni"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 0
     ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWCOMP_001';





