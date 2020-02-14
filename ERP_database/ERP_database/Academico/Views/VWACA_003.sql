CREATE VIEW Academico.VWACA_003
AS
SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, Representante.pe_nombreCompleto AS NomRepresentante, Representante.pe_cedulaRuc AS CedulaRepresentante, Factura.pe_nombreCompleto AS NomSeFactura, 
                  Factura.pe_cedulaRuc AS CedulaSeFactura, p.pe_nombreCompleto AS NomAlumno, p.pe_nombre AS CedulaAlumno, dbo.aca_AnioLectivo_Jornada_Curso.NomCurso, AnioActual.Descripcion AS DescripcionActual, 
                  AnioAnterior.Descripcion AS DescripcionAnterior, dbo.aca_Familia.Direccion, MesInicio.smes + ' de ' + CAST(YEAR(AnioActual.FechaDesde) AS varchar) + ' a ' + MesFin.smes + ' de ' + CAST(YEAR(AnioActual.FechaHasta) AS varchar) 
                  AS DescripcionPensiones, FFactura.Correo AS CorreoSeFactura, FRepresentante.Correo AS CorreoRepresentante, Representante.pe_direccion AS DireccionRepresentante, dbo.tb_pais.Nacionalidad AS NacionalidadRepresentante, 
                  FRepresentante.Sector AS SectorRepresentante, FRepresentante.Celular AS CelularRepresentante
FROM     dbo.aca_Familia AS FRepresentante RIGHT OUTER JOIN
                  dbo.tb_pais ON FRepresentante.IdPais = dbo.tb_pais.IdPais RIGHT OUTER JOIN
                  dbo.tb_persona AS p INNER JOIN
                  dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.aca_AnioLectivo AS AnioActual ON m.IdEmpresa = AnioActual.IdEmpresa AND m.IdAnio = AnioActual.IdAnio ON p.IdPersona = a.IdPersona INNER JOIN
                  dbo.tb_persona AS Factura ON m.IdPersonaF = Factura.IdPersona INNER JOIN
                  dbo.tb_persona AS Representante ON m.IdPersonaR = Representante.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso ON m.IdEmpresa = dbo.aca_AnioLectivo_Jornada_Curso.IdEmpresa AND m.IdAnio = dbo.aca_AnioLectivo_Jornada_Curso.IdAnio AND m.IdSede = dbo.aca_AnioLectivo_Jornada_Curso.IdSede AND 
                  m.IdNivel = dbo.aca_AnioLectivo_Jornada_Curso.IdNivel AND m.IdJornada = dbo.aca_AnioLectivo_Jornada_Curso.IdJornada AND m.IdCurso = dbo.aca_AnioLectivo_Jornada_Curso.IdCurso INNER JOIN
                  dbo.aca_Familia ON a.IdEmpresa = dbo.aca_Familia.IdEmpresa AND a.IdAlumno = dbo.aca_Familia.IdAlumno AND Representante.IdPersona = dbo.aca_Familia.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS AnioAnterior ON AnioActual.IdEmpresa = AnioAnterior.IdEmpresa AND AnioActual.IdAnioLectivoAnterior = AnioAnterior.IdAnio INNER JOIN
                  dbo.tb_mes AS MesInicio ON MONTH(AnioActual.FechaDesde) = MesInicio.idMes INNER JOIN
                  dbo.tb_mes AS MesFin ON MONTH(AnioActual.FechaHasta) = MesFin.idMes ON FRepresentante.IdPersona = m.IdPersonaR AND FRepresentante.IdEmpresa = m.IdEmpresa AND FRepresentante.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                  dbo.aca_Familia AS FFactura ON m.IdEmpresa = FFactura.IdEmpresa AND m.IdAlumno = FFactura.IdAlumno AND m.IdPersonaR = FFactura.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'playFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Familia"
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
         Begin Table = "tb_pais"
            Begin Extent = 
               Top = 7
               Left = 370
               Bottom = 170
               Right = 614
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
         Configuration = "(H (1[60] 4[13] 2[12] 3) )"
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
            TopColumn = 6
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
            TopColumn = 0
         End
         Begin Table = "Factura"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
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
         Begin Table = "aca_AnioLectivo_Jornada_Curso"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 242
            End
            Dis', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_003';





