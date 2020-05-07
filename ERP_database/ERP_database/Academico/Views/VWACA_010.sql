CREATE VIEW Academico.VWACA_010
AS
SELECT ROW_NUMBER() OVER (PARTITION BY dbo.aca_Matricula.IdEmpresa, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, 
                  dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacionParcial.IdCatalogoParcial, dbo.aca_Materia.IdMateria
ORDER BY dbo.tb_persona.pe_nombreCompleto) AS RowNumber, dbo.aca_MatriculaCalificacionParcial.IdEmpresa, dbo.aca_MatriculaCalificacionParcial.IdMatricula, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, 
dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacionParcial.IdMateria, dbo.aca_Materia.NomMateria, dbo.aca_MatriculaCalificacionParcial.IdCatalogoParcial, 
dbo.aca_Catalogo.NomCatalogo, dbo.aca_MatriculaCalificacionParcial.IdProfesor, tb_persona_1.pe_nombreCompleto AS Profesor, dbo.aca_Matricula.IdAlumno, dbo.aca_Alumno.IdPersona, dbo.tb_persona.pe_nombreCompleto AS Alumno, 
dbo.aca_MatriculaCalificacionParcial.Calificacion1, dbo.aca_MatriculaCalificacionParcial.Calificacion2, dbo.aca_MatriculaCalificacionParcial.Calificacion3, dbo.aca_MatriculaCalificacionParcial.Calificacion4, 
dbo.aca_MatriculaCalificacionParcial.Evaluacion, dbo.aca_MatriculaCalificacionParcial.Remedial1, dbo.aca_MatriculaCalificacionParcial.Remedial2, dbo.aca_MatriculaCalificacionParcial.Conducta, 
dbo.aca_MatriculaCalificacionParcial.MotivoCalificacion, dbo.aca_MatriculaCalificacionParcial.MotivoConducta, dbo.aca_MatriculaCalificacionParcial.AccionRemedial, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomNivel, 
dbo.vwaca_AnioLectivo_Curso_Paralelo.NomJornada, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomCurso, dbo.vwaca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.aca_Alumno.Codigo
FROM     dbo.aca_Materia INNER JOIN
                  dbo.aca_Catalogo INNER JOIN
                  dbo.aca_MatriculaCalificacionParcial ON dbo.aca_Catalogo.IdCatalogo = dbo.aca_MatriculaCalificacionParcial.IdCatalogoParcial ON dbo.aca_Materia.IdEmpresa = dbo.aca_MatriculaCalificacionParcial.IdEmpresa AND 
                  dbo.aca_Materia.IdMateria = dbo.aca_MatriculaCalificacionParcial.IdMateria INNER JOIN
                  dbo.aca_Matricula ON dbo.aca_MatriculaCalificacionParcial.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_MatriculaCalificacionParcial.IdMatricula = dbo.aca_Matricula.IdMatricula INNER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.aca_Alumno ON dbo.tb_persona.IdPersona = dbo.aca_Alumno.IdPersona ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.aca_Profesor ON dbo.aca_MatriculaCalificacionParcial.IdEmpresa = dbo.aca_Profesor.IdEmpresa AND dbo.aca_MatriculaCalificacionParcial.IdProfesor = dbo.aca_Profesor.IdProfesor INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON dbo.aca_Profesor.IdPersona = tb_persona_1.IdPersona INNER JOIN
                  dbo.vwaca_AnioLectivo_Curso_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdCurso AND 
                  dbo.aca_Matricula.IdParalelo = dbo.vwaca_AnioLectivo_Curso_Paralelo.IdParalelo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_010';




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[20] 2[12] 3) )"
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
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 41
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_010';





