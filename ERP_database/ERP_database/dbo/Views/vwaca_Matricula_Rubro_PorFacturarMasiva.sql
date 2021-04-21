CREATE VIEW dbo.vwaca_Matricula_Rubro_PorFacturarMasiva
AS
SELECT a.IdEmpresa, a.IdMatricula, a.IdAnio, a.IdPeriodo, a.IdPlantilla, a.IdRubro, a.IdProducto, a.Subtotal, a.IdCod_Impuesto_Iva, a.Porcentaje, a.ValorIVA, a.Total, CASE WHEN j.AplicaProntoPago = 1 AND d .FechaProntoPago > CAST(getdate() 
                  AS date) THEN CAST(k.Subtotal AS float) - (CASE WHEN l.AplicaParaTodo = 1 THEN (CASE WHEN l.TipoDescuento = '$' THEN CAST(l.Valor AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) * (l.Valor / 100), 2) END) 
                  ELSE (CASE WHEN k.TipoDescuento_descuentoDet = '$' THEN CAST(k.Valor_descuentoDet AS FLOAT) ELSE ROUND(CAST(k.Subtotal AS float) * (k.Valor_descuentoDet / 100), 2) END) END) ELSE CAST(k.Subtotal AS float) 
                  + CAST(k.ValorIVA AS FLOAT) END + k.ValorIVA AS ValorProntoPago, c.NomRubro + CASE WHEN c.NumeroCuotas > 1 THEN + ' ' + CAST(e.Secuencia AS varchar) + '/' + CAST(c.NumeroCuotas AS varchar) 
                  ELSE '' END + ' ' + f.smes + ' ' + CAST(YEAR(d.FechaDesde) AS varchar) AS Observacion, h.IdAlumno, d.FechaDesde, d.FechaProntoPago, b.IdTerminoPago, i.IdCliente, m.Codigo, n.pe_nombreCompleto AS Alumno, h.IdEmpresa_rol, 
                  h.IdEmpleado
FROM     dbo.aca_Matricula_Rubro AS a WITH (nolock) INNER JOIN
                  dbo.aca_MecanismoDePago AS b WITH (nolock) ON a.IdEmpresa = b.IdEmpresa AND a.IdMecanismo = b.IdMecanismo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Rubro AS c WITH (nolock) ON a.IdAnio = c.IdAnio AND a.IdEmpresa = c.IdEmpresa AND a.IdRubro = c.IdRubro AND a.IdRubro = c.IdRubro LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Periodo AS d WITH (nolock) ON a.IdEmpresa = d.IdEmpresa AND a.IdAnio = d.IdAnio AND a.IdPeriodo = d.IdPeriodo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Rubro_Periodo AS e WITH (nolock) ON a.IdEmpresa = e.IdEmpresa AND a.IdRubro = e.IdRubro AND a.IdPeriodo = e.IdPeriodo AND a.IdAnio = e.IdAnio LEFT OUTER JOIN
                  dbo.tb_mes AS f WITH (nolock) ON d.IdMes = f.idMes LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS g WITH (nolock) ON a.IdEmpresa = g.IdEmpresa AND a.IdAnio = g.IdAnio INNER JOIN
                  dbo.aca_Matricula AS h WITH (nolock) ON a.IdEmpresa = h.IdEmpresa AND a.IdMatricula = h.IdMatricula LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdPersona, MAX(IdCliente) AS IdCliente
                       FROM      dbo.fa_cliente
                       GROUP BY IdEmpresa, IdPersona) AS i ON h.IdPersonaF = i.IdPersona AND h.IdEmpresa = i.IdEmpresa LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Rubro AS j WITH (nolock) ON j.IdEmpresa = a.IdEmpresa AND j.IdAnio = a.IdAnio AND j.IdRubro = a.IdRubro LEFT OUTER JOIN
                  dbo.aca_Plantilla_Rubro AS k WITH (nolock) ON a.IdEmpresa = k.IdEmpresa AND a.IdAnio = k.IdAnio AND a.IdPlantilla = k.IdPlantilla AND a.IdRubro = k.IdRubro LEFT OUTER JOIN
                  dbo.aca_Plantilla AS l WITH (nolock) ON l.IdEmpresa = k.IdEmpresa AND l.IdAnio = k.IdAnio AND l.IdPlantilla = k.IdPlantilla LEFT OUTER JOIN
                  dbo.aca_Alumno AS m WITH (nolock) ON h.IdEmpresa = m.IdEmpresa AND h.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                  dbo.tb_persona AS n WITH (nolock) ON m.IdPersona = n.IdPersona
WHERE  (a.FechaFacturacion IS NULL) AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.aca_AlumnoRetiro AS ret WITH (nolock)
                       WHERE   (IdEmpresa = a.IdEmpresa) AND (IdMatricula = a.IdMatricula) AND (Estado = 1)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturarMasiva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         Begin Table = "h"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "j"
            Begin Extent = 
               Top = 1498
               Left = 48
               Bottom = 1661
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "k"
            Begin Extent = 
               Top = 1666
               Left = 48
               Bottom = 1829
               Right = 341
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "l"
            Begin Extent = 
               Top = 1834
               Left = 48
               Bottom = 1997
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 2002
               Left = 48
               Bottom = 2165
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "n"
            Begin Extent = 
               Top = 2170
               Left = 48
               Bottom = 2333
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1492
               Right = 292
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturarMasiva';


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
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 333
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 331
            End
            DisplayFlags = 280
            TopColumn = 0
         End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Matricula_Rubro_PorFacturarMasiva';

