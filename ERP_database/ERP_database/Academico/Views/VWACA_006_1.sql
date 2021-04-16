/*where aca_Matricula.IdEmpresa = 1 
and aca_Matricula.IdAnio between 0 and 50
and aca_Matricula.IdSede between 0 and 50 
and aca_Matricula.IdNivel between 0 and 50 
and aca_Matricula.IdJornada between 0 and 50
and aca_Matricula.IdCurso between 0 and 50*/
CREATE VIEW Academico.VWACA_006
AS
SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.OrdenCurso, jc.NomCurso, cp.CodigoParalelo, 
                  cp.NomParalelo, cp.OrdenParalelo, p.pe_sexo, CASE WHEN p.pe_sexo = 'SEXO_MAS' THEN 'H' ELSE 'M' END AS NomSexo, 1 AS Cantidad, m.Fecha, a.Descripcion, CASE WHEN Ret.IdEmpresa IS NULL THEN CAST(0 AS BIT) 
                  ELSE CAST(1 AS BIT) END AS EsRetirado, CASE WHEN Ret.IdEmpresa IS NULL THEN '' ELSE 'RETIRADO' END AS EsRetiradoString
FROM     dbo.aca_Alumno AS alu WITH (nolock) INNER JOIN
                  dbo.aca_Matricula AS m WITH (nolock) ON alu.IdEmpresa = m.IdEmpresa AND alu.IdAlumno = m.IdAlumno INNER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON alu.IdPersona = p.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS a WITH (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON a.IdEmpresa = sn.IdEmpresa AND a.IdAnio = sn.IdAnio RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                      (SELECT IdEmpresa, IdMatricula
                       FROM      dbo.aca_AlumnoRetiro AS f WITH (nolock)
                       WHERE   (Estado = 1)) AS Ret ON m.IdEmpresa = Ret.IdEmpresa AND m.IdMatricula = Ret.IdMatricula
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_006';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'      End
         Begin Table = "cp"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Ret"
            Begin Extent = 
               Top = 487
               Left = 48
               Bottom = 606
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
      Begin ColumnWidths = 26
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_006';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[8] 2[35] 3) )"
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
         Top = -480
         Left = 0
      End
      Begin Tables = 
         Begin Table = "alu"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sn"
            Begin Extent = 
               Top = 6
               Left = 634
               Bottom = 234
               Right = 828
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 0
               Left = 953
               Bottom = 332
               Right = 1147
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "jc"
            Begin Extent = 
               Top = 34
               Left = 1313
               Bottom = 197
               Right = 1507
            End
            DisplayFlags = 280
            TopColumn = 0
   ', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_006';





