CREATE VIEW Academico.VWACA_011
AS
SELECT TOP (100) PERCENT dbo.aca_MatriculaCalificacion.IdEmpresa, dbo.aca_MatriculaCalificacion.IdMatricula, dbo.aca_Matricula.IdAlumno, dbo.tb_persona.pe_nombreCompleto, dbo.aca_Alumno.LugarNacimiento, 
                  dbo.tb_persona.pe_fechaNacimiento, dbo.aca_Matricula.IdPersonaR, dbo.aca_Catalogo.NomCatalogo AS Parentezco, tb_persona_1.pe_nombreCompleto AS Representante, dbo.aca_Familia.Direccion, dbo.aca_Familia.Celular, 
                  dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacion.IdMateria, 
                  dbo.aca_AnioLectivo_Curso_Materia.NomMateria, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria, dbo.aca_MatriculaCalificacion.IdProfesor, dbo.aca_MatriculaCalificacion.CalificacionP1, dbo.aca_MatriculaCalificacion.CalificacionP2, 
                  dbo.aca_MatriculaCalificacion.CalificacionP3, dbo.aca_MatriculaCalificacion.PromedioQ1, dbo.aca_MatriculaCalificacion.ExamenQ1, dbo.aca_MatriculaCalificacion.PromedioFinalQ1, dbo.aca_MatriculaCalificacion.CalificacionP4, 
                  dbo.aca_MatriculaCalificacion.CalificacionP5, dbo.aca_MatriculaCalificacion.CalificacionP6, dbo.aca_MatriculaCalificacion.PromedioQ2, dbo.aca_MatriculaCalificacion.ExamenQ2, dbo.aca_MatriculaCalificacion.PromedioFinalQ2, 
                  dbo.aca_MatriculaCalificacion.ExamenMejoramiento, dbo.aca_MatriculaCalificacion.ExamenSupletorio, dbo.aca_MatriculaCalificacion.ExamenRemedial, dbo.aca_MatriculaCalificacion.ExamenGracia, 
                  dbo.aca_MatriculaCalificacion.PromedioFinal
FROM     dbo.aca_MatriculaCalificacion INNER JOIN
                  dbo.aca_Matricula ON dbo.aca_MatriculaCalificacion.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_MatriculaCalificacion.IdMatricula = dbo.aca_Matricula.IdMatricula INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.aca_Familia ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Familia.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Familia.IdAlumno AND dbo.aca_Matricula.IdPersonaR = dbo.aca_Familia.IdPersona INNER JOIN
                  dbo.aca_Catalogo ON dbo.aca_Familia.IdCatalogoPAREN = dbo.aca_Catalogo.IdCatalogo INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON dbo.aca_Matricula.IdPersonaR = tb_persona_1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia ON dbo.aca_Matricula.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivo_Curso_Materia.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.aca_AnioLectivo_Curso_Materia.IdSede AND dbo.aca_Matricula.IdNivel = dbo.aca_AnioLectivo_Curso_Materia.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.aca_AnioLectivo_Curso_Materia.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.aca_AnioLectivo_Curso_Materia.IdCurso AND 
                  dbo.aca_MatriculaCalificacion.IdMateria = dbo.aca_AnioLectivo_Curso_Materia.IdMateria AND dbo.aca_MatriculaCalificacion.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa
WHERE  (dbo.aca_Familia.EsRepresentante = 1)
ORDER BY dbo.tb_persona.pe_nombreCompleto, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_011';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'2
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo_Curso_Materia"
            Begin Extent = 
               Top = 19
               Left = 555
               Bottom = 297
               Right = 799
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 40
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
         Or = 1548
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_011';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[13] 2[32] 3) )"
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
         Top = -845
         Left = 0
      End
      Begin Tables = 
         Begin Table = "aca_MatriculaCalificacion"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_Matricula"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 6
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
         Begin Table = "aca_Familia"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 19
         End
         Begin Table = "aca_Catalogo"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 32', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_011';

