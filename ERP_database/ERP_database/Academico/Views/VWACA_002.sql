CREATE VIEW Academico.VWACA_002
AS
SELECT dbo.aca_Matricula.IdEmpresa, dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.IdAlumno, dbo.aca_Matricula.IdAnio, dbo.vwaca_AnioLectivo_Curso_Paralelo.Descripcion, dbo.aca_AnioLectivo.EnCurso, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomSede, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, 
                  dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.aca_Alumno.Codigo AS CodigoAlumno, dbo.tb_persona.pe_nombreCompleto AS NombreAlumno, tb_persona_1.pe_cedulaRuc AS CedulaRep, 
                  tb_persona_1.pe_nombreCompleto AS NombreRep, dbo.aca_Plantilla.NomPlantilla
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  dbo.aca_Matricula.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON dbo.aca_Matricula.IdPersonaR = tb_persona_1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_Matricula.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivo.IdAnio INNER JOIN
                  dbo.aca_Plantilla ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_Plantilla.IdAnio AND dbo.aca_Matricula.IdPlantilla = dbo.aca_Plantilla.IdPlantilla
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_002';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' 969
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
      Begin ColumnWidths = 17
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_002';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[24] 2[16] 3) )"
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
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "vwaca_AnioLectivo_Curso_Paralelo"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 292
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
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Plantilla"
            Begin Extent = 
               Top = 0
               Left = 724
               Bottom = 163
               Right =', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_002';



