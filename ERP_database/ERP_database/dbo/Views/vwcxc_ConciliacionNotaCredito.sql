CREATE VIEW dbo.vwcxc_ConciliacionNotaCredito
AS
SELECT dbo.cxc_ConciliacionNotaCredito.IdEmpresa, dbo.cxc_ConciliacionNotaCredito.IdConciliacion, dbo.cxc_ConciliacionNotaCredito.IdSucursal, dbo.cxc_ConciliacionNotaCredito.IdBodega, dbo.cxc_ConciliacionNotaCredito.IdNota, 
                  dbo.cxc_ConciliacionNotaCredito.IdCobro, dbo.cxc_ConciliacionNotaCredito.Fecha, dbo.cxc_ConciliacionNotaCredito.Valor, dbo.cxc_ConciliacionNotaCredito.Observacion, dbo.cxc_ConciliacionNotaCredito.Estado, 
                  CASE WHEN fa_notaCreDeb.NaturalezaNota = 'SRI' THEN fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa ELSE fa_notaCreDeb.CodNota END AS Referencia, 
                  dbo.tb_persona.pe_nombreCompleto, dbo.cxc_ConciliacionNotaCredito.IdTipoCbte, dbo.cxc_ConciliacionNotaCredito.IdCbteCble
FROM     dbo.cxc_ConciliacionNotaCredito INNER JOIN
                  dbo.fa_notaCreDeb ON dbo.cxc_ConciliacionNotaCredito.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.cxc_ConciliacionNotaCredito.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                  dbo.cxc_ConciliacionNotaCredito.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.cxc_ConciliacionNotaCredito.IdNota = dbo.fa_notaCreDeb.IdNota INNER JOIN
                  dbo.aca_Alumno ON dbo.fa_notaCreDeb.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.fa_notaCreDeb.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_ConciliacionNotaCredito';


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
         Begin Table = "cxc_ConciliacionNotaCredito"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 311
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "fa_notaCreDeb"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 322
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_ConciliacionNotaCredito';

