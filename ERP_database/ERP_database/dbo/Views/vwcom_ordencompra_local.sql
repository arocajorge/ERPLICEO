CREATE VIEW dbo.vwcom_ordencompra_local
AS
SELECT        c.IdEmpresa, c.IdSucursal, c.IdOrdenCompra, c.Tipo, c.SecuenciaTipo, s.codigo + '-' + CAST(c.IdOrdenCompra AS VARCHAR(18)) AS Codigo, s.Su_Descripcion, c.oc_fecha, per.pe_nombreCompleto, d.Total, 
                         c.IdEstadoAprobacion_cat, CASE WHEN c.IdEstadoAprobacion_cat = 'APRO' THEN 'Aprobado' WHEN c.IdEstadoAprobacion_cat = 'XAPRO' THEN 'Por Aprobar' ELSE 'Anulado' END AS EstadoAprobacion, c.oc_observacion, 
                         com.Descripcion, c.IdEstado_cierre, CASE WHEN c.IdEstado_cierre = 'ABI' THEN 'Abierta' WHEN c.IdEstado_cierre = 'CERR' THEN 'Cerrada' ELSE 'Pendiente' END AS EstadoCierre, c.Estado, tp.Descripcion AS TerminoPago, 
                         c.oc_plazo
FROM            dbo.com_ordencompra_local AS c LEFT OUTER JOIN
                         dbo.tb_sucursal AS s ON c.IdEmpresa = s.IdEmpresa AND c.IdSucursal = s.IdSucursal LEFT OUTER JOIN
                         dbo.cp_proveedor AS pro ON c.IdEmpresa = pro.IdEmpresa AND c.IdProveedor = pro.IdProveedor INNER JOIN
                         dbo.tb_persona AS per ON pro.IdPersona = per.IdPersona LEFT OUTER JOIN
                             (SELECT        IdEmpresa, IdSucursal, IdOrdenCompra, SUM(do_total) AS Total
                               FROM            dbo.com_ordencompra_local_det AS det
                               GROUP BY IdEmpresa, IdSucursal, IdOrdenCompra) AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdSucursal = d.IdSucursal AND c.IdOrdenCompra = d.IdOrdenCompra LEFT OUTER JOIN
                         dbo.com_comprador AS com ON c.IdEmpresa = com.IdEmpresa AND c.IdComprador = com.IdComprador LEFT OUTER JOIN
                         dbo.com_TerminoPago AS tp ON c.IdEmpresa = tp.IdEmpresa AND c.IdTerminoPago = tp.IdTerminoPago
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[55] 4[17] 2[12] 3) )"
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
         Left = -474
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 0
               Left = 755
               Bottom = 649
               Right = 988
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 0
               Left = 0
               Bottom = 129
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "pro"
            Begin Extent = 
               Top = 0
               Left = 0
               Bottom = 129
               Right = 248
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 1350
               Left = 38
               Bottom = 1480
               Right = 286
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 6
               Left = 286
               Bottom = 218
               Right = 477
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "com"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 927
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tp"
            Begin Extent = 
               Top = 1326
               Left = 38
               Bottom = 1455
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcom_ordencompra_local';










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'  End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 20
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcom_ordencompra_local';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcom_ordencompra_local';

