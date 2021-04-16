CREATE VIEW Academico.VWACA_010
AS
SELECT ROW_NUMBER() OVER (PARTITION BY m.IdEmpresa, m.IdSede, m.IdAnio, m.IdNivel, m.IdJornada, m.IdCurso, 
                  m.IdParalelo, mc.IdCatalogoParcial, ma.IdMateria
ORDER BY p.pe_nombreCompleto) AS RowNumber, m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, mc.IdMateria, ma.NomMateria, mc.IdCatalogoParcial, c.NomCatalogo, mc.IdProfesor, pe1.pe_nombreCompleto AS Profesor, m.IdAlumno, 
                  alu.IdPersona, p.pe_nombreCompleto AS Alumno, mc.Calificacion1, mc.Calificacion2, mc.Calificacion3, mc.Calificacion4, mc.Evaluacion, mc.Remedial1, mc.Remedial2, mc.Conducta, mc.MotivoCalificacion, mc.MotivoConducta, 
                  mc.AccionRemedial, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, alu.Codigo
FROM     dbo.aca_Matricula AS m WITH (nolock) LEFT OUTER JOIN
                  dbo.aca_Alumno AS alu WITH (nolock) ON alu.IdEmpresa = m.IdEmpresa AND alu.IdAlumno = m.IdAlumno LEFT OUTER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON p.IdPersona = alu.IdPersona LEFT OUTER JOIN
                  dbo.aca_AlumnoRetiro AS ar WITH (nolock) ON m.IdEmpresa = ar.IdEmpresa AND m.IdMatricula = ar.IdMatricula AND ar.Estado = 1 INNER JOIN
                  dbo.aca_MatriculaCalificacionParcial AS mc WITH (nolock) ON m.IdEmpresa = mc.IdEmpresa AND m.IdMatricula = mc.IdMatricula LEFT OUTER JOIN
                  dbo.aca_Materia AS ma WITH (nolock) ON ma.IdEmpresa = mc.IdEmpresa AND ma.IdMateria = mc.IdMateria LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND 
                  m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND 
                  m.IdCurso = jc.IdCurso LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio LEFT OUTER JOIN
                  dbo.aca_Catalogo AS c WITH (nolock) ON c.IdCatalogo = mc.IdCatalogoParcial LEFT OUTER JOIN
                  dbo.aca_Profesor AS pro WITH (nolock) ON mc.IdEmpresa = pro.IdEmpresa AND mc.IdProfesor = pro.IdProfesor LEFT OUTER JOIN
                  dbo.tb_persona AS pe1 WITH (nolock) ON pro.IdPersona = pe1.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_010';




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[25] 2[20] 3) )"
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
      Begin ColumnWidths = 34
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







