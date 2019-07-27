﻿CREATE VIEW web.vwcaj_Caja_Movimiento
AS
SELECT        dbo.caj_Caja_Movimiento.IdEmpresa, dbo.caj_Caja_Movimiento.IdTipocbte, dbo.caj_Caja_Movimiento.IdCbteCble, dbo.caj_Caja_Movimiento.cm_Signo, dbo.caj_Caja_Movimiento.cm_valor, 
                         dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento.cm_fecha, dbo.caj_Caja_Movimiento.IdPersona, dbo.tb_persona.pe_nombreCompleto, dbo.caj_Caja_Movimiento.Estado, 
                         dbo.caj_Caja_Movimiento_Tipo.tm_descripcion, dbo.caj_Caja_Movimiento.IdCaja, dbo.caj_Caja.ca_Descripcion, dbo.caj_Caja_Movimiento.SecuenciaCaja
FROM            dbo.caj_Caja_Movimiento INNER JOIN
                         dbo.tb_persona ON dbo.caj_Caja_Movimiento.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_Tipo.IdEmpresa AND dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                         dbo.caj_Caja ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.caj_Caja_Movimiento.IdCaja = dbo.caj_Caja.IdCaja
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcaj_Caja_Movimiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcaj_Caja_Movimiento';


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
         Begin Table = "caj_Caja_Movimiento"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 219
            End
            DisplayFlags = 280
            TopColumn = 20
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento_Tipo"
            Begin Extent = 
               Top = 6
               Left = 257
               Bottom = 136
               Right = 438
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 248
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
      Begin ColumnWidths = 14
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
         SortType = 1350', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcaj_Caja_Movimiento';

