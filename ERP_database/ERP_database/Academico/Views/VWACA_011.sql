CREATE VIEW Academico.VWACA_011
AS
SELECT ROW_NUMBER() OVER (PARTITION BY dbo.aca_Matricula.IdMatricula
ORDER BY dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria ASC) AS RowNumber, dbo.aca_MatriculaCalificacion.IdEmpresa, dbo.aca_MatriculaCalificacion.IdMatricula, dbo.aca_Matricula.IdAlumno, dbo.tb_persona.pe_nombreCompleto, 
dbo.aca_Alumno.LugarNacimiento, dbo.tb_persona.pe_fechaNacimiento, YEAR(GETDATE()) - YEAR(dbo.tb_persona.pe_fechaNacimiento) AS Edad, dbo.aca_Matricula.IdPersonaR, dbo.aca_Catalogo.NomCatalogo AS Parentezco, 
tb_persona_1.pe_nombreCompleto AS Representante, dbo.aca_Familia.Direccion, dbo.aca_Familia.Celular, dbo.aca_Matricula.IdAnio, dbo.aca_Matricula.IdSede, dbo.aca_Matricula.IdNivel, dbo.aca_Matricula.IdJornada, dbo.aca_Matricula.IdCurso, 
dbo.aca_Matricula.IdParalelo, dbo.aca_MatriculaCalificacion.IdMateria, dbo.aca_AnioLectivo_Curso_Materia.NomMateria, dbo.aca_AnioLectivo_Curso_Materia.OrdenMateria, dbo.aca_MatriculaCalificacion.IdProfesor, 
dbo.aca_MatriculaCalificacion.CalificacionP1, dbo.aca_MatriculaCalificacion.CalificacionP2, dbo.aca_MatriculaCalificacion.CalificacionP3, dbo.aca_MatriculaCalificacion.PromedioQ1, dbo.aca_MatriculaCalificacion.ExamenQ1, 
dbo.aca_MatriculaCalificacion.PromedioFinalQ1, dbo.aca_MatriculaCalificacion.CalificacionP4, dbo.aca_MatriculaCalificacion.CalificacionP5, dbo.aca_MatriculaCalificacion.CalificacionP6, dbo.aca_MatriculaCalificacion.PromedioQ2, 
dbo.aca_MatriculaCalificacion.ExamenQ2, dbo.aca_MatriculaCalificacion.PromedioFinalQ2, dbo.aca_MatriculaCalificacion.ExamenMejoramiento, dbo.aca_MatriculaCalificacion.CampoMejoramiento, 
CASE WHEN (dbo.aca_MatriculaCalificacion.CampoMejoramiento IS NULL OR
dbo.aca_MatriculaCalificacion.CampoMejoramiento = '') THEN ROUND(((dbo.aca_MatriculaCalificacion.PromedioFinalQ1 + dbo.aca_MatriculaCalificacion.PromedioFinalQ2) / 2), 2) 
ELSE CASE WHEN (dbo.aca_MatriculaCalificacion.CampoMejoramiento = 'Q1') THEN ROUND(((dbo.aca_MatriculaCalificacion.ExamenMejoramiento + dbo.aca_MatriculaCalificacion.PromedioFinalQ2) / 2), 2) 
ELSE CASE WHEN (dbo.aca_MatriculaCalificacion.CampoMejoramiento = 'Q2') THEN ROUND(((dbo.aca_MatriculaCalificacion.PromedioFinalQ1 + dbo.aca_MatriculaCalificacion.ExamenMejoramiento) / 2), 2) END END END AS PromedioQuimestral, 
dbo.aca_MatriculaCalificacion.ExamenSupletorio, dbo.aca_MatriculaCalificacion.ExamenRemedial, dbo.aca_MatriculaCalificacion.ExamenGracia, dbo.aca_MatriculaCalificacion.PromedioFinal
FROM     dbo.aca_MatriculaCalificacion WITH (nolock) INNER JOIN
                  dbo.aca_Matricula WITH (nolock) ON dbo.aca_MatriculaCalificacion.IdEmpresa = dbo.aca_Matricula.IdEmpresa AND dbo.aca_MatriculaCalificacion.IdMatricula = dbo.aca_Matricula.IdMatricula INNER JOIN
                  dbo.aca_Alumno WITH (nolock) ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona WITH (nolock) ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.aca_Familia WITH (nolock) ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Familia.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Familia.IdAlumno AND dbo.aca_Matricula.IdPersonaR = dbo.aca_Familia.IdPersona INNER JOIN
                  dbo.aca_Catalogo WITH (nolock) ON dbo.aca_Familia.IdCatalogoPAREN = dbo.aca_Catalogo.IdCatalogo INNER JOIN
                  dbo.tb_persona AS tb_persona_1 ON dbo.aca_Matricula.IdPersonaR = tb_persona_1.IdPersona INNER JOIN
                  dbo.aca_AnioLectivo_Curso_Materia WITH (nolock)  ON dbo.aca_Matricula.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.aca_AnioLectivo_Curso_Materia.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.aca_AnioLectivo_Curso_Materia.IdSede AND dbo.aca_Matricula.IdNivel = dbo.aca_AnioLectivo_Curso_Materia.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.aca_AnioLectivo_Curso_Materia.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.aca_AnioLectivo_Curso_Materia.IdCurso AND 
                  dbo.aca_MatriculaCalificacion.IdMateria = dbo.aca_AnioLectivo_Curso_Materia.IdMateria AND dbo.aca_MatriculaCalificacion.IdEmpresa = dbo.aca_AnioLectivo_Curso_Materia.IdEmpresa
WHERE  (dbo.aca_Familia.EsRepresentante = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_011';




GO





GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[18] 2[25] 3) )"
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
      Begin ColumnWidths = 43
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
         Width = 2100
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
         Or = 1548
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_011';





