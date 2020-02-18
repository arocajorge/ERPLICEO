CREATE VIEW [dbo].[vwfa_notaCreDeb_ParaConciliarNC]
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, c.Total, dbo.BankersRounding(ISNULL(SUM(b.Valor_Aplicado), 0), 2) AS Valor_Aplicado, 
                  CASE WHEN a.NaturalezaNota = 'SRI' THEN a.Serie1 + '-' + a.Serie2 + '-' + a.NumNota_Impresa ELSE a.CodNota END AS NumNota, a.no_fecha, a.sc_observacion, f.No_Descripcion, a.IdAlumno, a.IdCliente, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.fa_notaCreDeb AS a LEFT OUTER JOIN
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb AS b ON a.IdEmpresa = b.IdEmpresa_nt AND a.IdSucursal = b.IdSucursal_nt AND a.IdBodega = b.IdBodega_nt AND a.IdNota = b.IdNota_nt INNER JOIN
                  dbo.fa_notaCreDeb_resumen AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdSucursal = c.IdSucursal AND a.IdBodega = c.IdBodega AND a.IdNota = c.IdNota INNER JOIN
                  dbo.aca_Alumno AS d ON a.IdEmpresa = d.IdEmpresa AND a.IdAlumno = d.IdAlumno INNER JOIN
                  dbo.tb_persona AS e ON e.IdPersona = d.IdPersona INNER JOIN
                  dbo.fa_TipoNota AS f ON a.IdEmpresa = f.IdEmpresa AND a.IdTipoNota = f.IdTipoNota INNER JOIN
                  dbo.aca_Alumno ON a.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND a.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona
WHERE  (a.CreDeb = 'C') AND (a.Estado = 'A')
GROUP BY a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, c.Total, a.NaturalezaNota, a.Serie1, a.Serie2, a.NumNota_Impresa, a.CodNota, a.no_fecha, a.sc_observacion, f.No_Descripcion, a.IdAlumno, a.IdCliente, 
                  dbo.tb_persona.pe_nombreCompleto
HAVING (c.Total - dbo.BankersRounding(ISNULL(SUM(b.Valor_Aplicado), 0), 2) > 0)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_ParaConciliarNC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'   End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 1140
               Bottom = 170
               Right = 1430
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
      Begin ColumnWidths = 13
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_ParaConciliarNC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[8] 3) )"
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
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 294
            End
            DisplayFlags = 280
            TopColumn = 13
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 253
               Left = 649
               Bottom = 564
               Right = 947
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 309
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 0
               Left = 708
               Bottom = 163
               Right = 969
            End
            DisplayFlags = 280
            TopColumn = 0
      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_ParaConciliarNC';

