CREATE VIEW Academico.VWACA_003
AS
SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, Representante.pe_nombreCompleto AS NomRepresentante, Representante.pe_cedulaRuc AS CedulaRepresentante, Factura.pe_nombreCompleto AS NomSeFactura, 
                  Factura.pe_cedulaRuc AS CedulaSeFactura, p.pe_nombreCompleto AS NomAlumno, p.pe_nombre AS CedulaAlumno, jc.NomCurso, AnioActual.Descripcion AS DescripcionActual, AnioAnterior.Descripcion AS DescripcionAnterior, 
                  fa.Direccion, MesInicio.smes + ' de ' + CAST(YEAR(AnioActual.FechaDesde) AS varchar) + ' a ' + MesFin.smes + ' de ' + CAST(YEAR(AnioActual.FechaHasta) AS varchar) AS DescripcionPensiones, FFactura.Correo AS CorreoSeFactura, 
                  FRepresentante.Correo AS CorreoRepresentante, Representante.pe_direccion AS DireccionRepresentante, pa.Nacionalidad AS NacionalidadRepresentante, FRepresentante.Sector AS SectorRepresentante, 
                  FRepresentante.Celular AS CelularRepresentante
FROM     dbo.aca_Familia AS FRepresentante WITH (nolock) RIGHT OUTER JOIN
                  dbo.tb_pais AS pa WITH (nolock) ON FRepresentante.IdPais = pa.IdPais RIGHT OUTER JOIN
                  dbo.tb_persona AS p WITH (nolock) INNER JOIN
                  dbo.aca_Matricula AS m WITH (nolock) INNER JOIN
                  dbo.aca_Alumno AS a WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.aca_AnioLectivo AS AnioActual WITH (nolock) ON m.IdEmpresa = AnioActual.IdEmpresa AND m.IdAnio = AnioActual.IdAnio ON p.IdPersona = a.IdPersona INNER JOIN
                  dbo.tb_persona AS Factura WITH (nolock) ON m.IdPersonaF = Factura.IdPersona INNER JOIN
                  dbo.tb_persona AS Representante WITH (nolock) ON m.IdPersonaR = Representante.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND 
                  m.IdCurso = jc.IdCurso INNER JOIN
                  dbo.aca_Familia AS fa WITH (nolock) ON a.IdEmpresa = fa.IdEmpresa AND a.IdAlumno = fa.IdAlumno AND Representante.IdPersona = fa.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AnioAnterior WITH (nolock) ON AnioActual.IdEmpresa = AnioAnterior.IdEmpresa AND AnioActual.IdAnioLectivoAnterior = AnioAnterior.IdAnio INNER JOIN
                  dbo.tb_mes AS MesInicio WITH (nolock) ON MONTH(AnioActual.FechaDesde) = MesInicio.idMes INNER JOIN
                  dbo.tb_mes AS MesFin WITH (nolock) ON MONTH(AnioActual.FechaHasta) = MesFin.idMes ON FRepresentante.IdPersona = m.IdPersonaR AND FRepresentante.IdEmpresa = m.IdEmpresa AND 
                  FRepresentante.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                  dbo.aca_Familia AS FFactura WITH (nolock) ON m.IdEmpresa = FFactura.IdEmpresa AND m.IdAlumno = FFactura.IdAlumno AND m.IdPersonaR = FFactura.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    TopColumn = 3
         End
         Begin Table = "Representante"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "jc"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AnioAnterior"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MesInicio"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "MesFin"
            Begin Extent = 
               Top = 1687
               Left = 48
               Bottom = 1850
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "FFactura"
            Begin Extent = 
               Top = 43
               Left = 1038
               Bottom = 330
               Right = 1283
            End
            DisplayFlags = 280
            TopColumn = 22
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 21
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_003';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[51] 4[3] 2[32] 3) )"
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
         Top = -720
         Left = 0
      End
      Begin Tables = 
         Begin Table = "FRepresentante"
            Begin Extent = 
               Top = 345
               Left = 1032
               Bottom = 622
               Right = 1277
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pa"
            Begin Extent = 
               Top = 7
               Left = 370
               Bottom = 170
               Right = 614
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 393
               Left = 618
               Bottom = 671
               Right = 863
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 215
               Left = 611
               Bottom = 378
               Right = 856
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AnioActual"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Factura"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 322
            End
            DisplayFlags = 280
        ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_003';







