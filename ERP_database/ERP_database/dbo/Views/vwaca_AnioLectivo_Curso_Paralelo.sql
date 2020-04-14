CREATE VIEW dbo.vwaca_AnioLectivo_Curso_Paralelo
AS
SELECT dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa, dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio, dbo.aca_AnioLectivo.Descripcion, dbo.aca_AnioLectivo_Curso_Paralelo.IdSede, dbo.aca_Sede.NomSede, 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel, dbo.aca_NivelAcademico.NomNivel, dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada, dbo.aca_Jornada.NomJornada, dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso, 
                  dbo.aca_Curso.NomCurso, dbo.aca_AnioLectivo_Curso_Paralelo.IdParalelo, dbo.aca_AnioLectivo_Curso_Paralelo.CodigoParalelo, dbo.aca_AnioLectivo_Curso_Paralelo.NomParalelo, dbo.aca_AnioLectivo_Curso_Paralelo.OrdenParalelo, 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdProfesorTutor, dbo.aca_AnioLectivo_Curso_Paralelo.IdProfesorInspector, dbo.aca_Profesor.IdPersona AS IdPersonaTutor, aca_Profesor_1.IdPersona AS IdPersonaInpector, 
                  dbo.tb_persona.pe_nombreCompleto AS NomTutor, tb_persona_1.pe_nombreCompleto AS NomInspector, dbo.aca_NivelAcademico.Orden AS OrdenNivel, dbo.aca_Jornada.OrdenJornada, dbo.aca_Curso.OrdenCurso
FROM     dbo.tb_persona AS tb_persona_1 INNER JOIN
                  dbo.aca_Profesor AS aca_Profesor_1 ON tb_persona_1.IdPersona = aca_Profesor_1.IdPersona RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo INNER JOIN
                  dbo.aca_AnioLectivo ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_AnioLectivo.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Paralelo.IdAnio = dbo.aca_AnioLectivo.IdAnio INNER JOIN
                  dbo.aca_Sede ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_Sede.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Paralelo.IdSede = dbo.aca_Sede.IdSede INNER JOIN
                  dbo.aca_Jornada ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_Jornada.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Paralelo.IdJornada = dbo.aca_Jornada.IdJornada INNER JOIN
                  dbo.aca_Curso ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_Curso.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Paralelo.IdCurso = dbo.aca_Curso.IdCurso INNER JOIN
                  dbo.aca_NivelAcademico ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_NivelAcademico.IdEmpresa AND dbo.aca_AnioLectivo_Curso_Paralelo.IdNivel = dbo.aca_NivelAcademico.IdNivel ON 
                  aca_Profesor_1.IdEmpresa = dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa AND aca_Profesor_1.IdProfesor = dbo.aca_AnioLectivo_Curso_Paralelo.IdProfesorInspector LEFT OUTER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.aca_Profesor ON dbo.tb_persona.IdPersona = dbo.aca_Profesor.IdPersona ON dbo.aca_AnioLectivo_Curso_Paralelo.IdEmpresa = dbo.aca_Profesor.IdEmpresa AND 
                  dbo.aca_AnioLectivo_Curso_Paralelo.IdProfesorTutor = dbo.aca_Profesor.IdProfesor
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Paralelo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'390
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "aca_NivelAcademico"
            Begin Extent = 
               Top = 0
               Left = 877
               Bottom = 163
               Right = 1122
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 162
               Left = 882
               Bottom = 325
               Right = 1156
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Profesor"
            Begin Extent = 
               Top = 206
               Left = 423
               Bottom = 369
               Right = 668
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
      Begin ColumnWidths = 25
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Paralelo';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[23] 2[14] 3) )"
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
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 341
               Left = 880
               Bottom = 504
               Right = 1154
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Profesor_1"
            Begin Extent = 
               Top = 365
               Left = 425
               Bottom = 528
               Right = 670
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "aca_AnioLectivo_Curso_Paralelo"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 353
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_AnioLectivo"
            Begin Extent = 
               Top = 7
               Left = 340
               Bottom = 170
               Right = 585
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Sede"
            Begin Extent = 
               Top = 7
               Left = 633
               Bottom = 170
               Right = 878
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "aca_Jornada"
            Begin Extent = 
               Top = 118
               Left = 1254
               Bottom = 281
               Right = 1499
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Curso"
            Begin Extent = 
               Top = 0
               Left = 1145
               Bottom = 163
               Right = 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_AnioLectivo_Curso_Paralelo';





