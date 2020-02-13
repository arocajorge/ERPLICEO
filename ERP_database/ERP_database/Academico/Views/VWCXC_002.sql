CREATE VIEW Academico.VWCXC_002
AS
SELECT a.IdEmpresa, a.IdSucursal, a.IdCobro, a.IdAlumno, c.pe_nombreCompleto, CASE WHEN a.cr_estado = 'I' THEN 'ANULADO' ELSE '' END AS cr_estado, a.cr_fecha, d.tc_descripcion, b.Codigo AS CodigoAlumno, a.cr_observacion, e.NomSede, 
                  e.NomNivel, e.NomJornada, e.NomCurso, e.NomParalelo, e.CodigoParalelo, e.NomPlantilla, a.cr_TotalCobro, a.cr_Saldo, g.pe_nombreCompleto AS NomCliente, g.pe_cedulaRuc AS CedulaCliente, a.IdCliente, 
                  CASE WHEN a.IdCobro_tipo = 'TARJ' THEN h.NombreTarjeta ELSE a.cr_Banco END AS cr_Banco, CASE WHEN a.IdCobro_tipo = 'TARJ' THEN a.cr_Tarjeta ELSE a.cr_NumDocumento END AS cr_NumDocumento, a.Fecha_Transac, 
                  a.cr_ObservacionPantalla
FROM     dbo.cxc_cobro AS a INNER JOIN
                  dbo.aca_Alumno AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdAlumno = b.IdAlumno INNER JOIN
                  dbo.tb_persona AS c ON c.IdPersona = b.IdPersona INNER JOIN
                  dbo.cxc_cobro_tipo AS d ON a.IdCobro_tipo = d.IdCobro_tipo LEFT OUTER JOIN
                      (SELECT A1.IdEmpresa, A1.IdAnio, A1.IdAlumno, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, JornadaCurso.NomCurso, CursoParalelo.NomParalelo, CursoParalelo.CodigoParalelo, A3.NomPlantilla
                       FROM      dbo.aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel ON NivelJornada.IdEmpresa = SedeNivel.IdEmpresa AND NivelJornada.IdAnio = SedeNivel.IdAnio AND NivelJornada.IdSede = SedeNivel.IdSede AND 
                                         NivelJornada.IdNivel = SedeNivel.IdNivel INNER JOIN
                                         dbo.aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND 
                                         NivelJornada.IdNivel = JornadaCurso.IdNivel AND NivelJornada.IdJornada = JornadaCurso.IdJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Curso_Paralelo AS CursoParalelo ON JornadaCurso.IdEmpresa = CursoParalelo.IdEmpresa AND JornadaCurso.IdAnio = CursoParalelo.IdAnio AND JornadaCurso.IdSede = CursoParalelo.IdSede AND 
                                         JornadaCurso.IdNivel = CursoParalelo.IdNivel AND JornadaCurso.IdJornada = CursoParalelo.IdJornada AND JornadaCurso.IdCurso = CursoParalelo.IdCurso LEFT OUTER JOIN
                                         dbo.aca_Matricula AS A1 INNER JOIN
                                         dbo.aca_AnioLectivo AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdAnio = A2.IdAnio ON CursoParalelo.IdEmpresa = A1.IdEmpresa AND CursoParalelo.IdAnio = A1.IdAnio AND CursoParalelo.IdSede = A1.IdSede AND 
                                         CursoParalelo.IdNivel = A1.IdNivel AND CursoParalelo.IdJornada = A1.IdJornada AND CursoParalelo.IdCurso = A1.IdCurso AND CursoParalelo.IdParalelo = A1.IdPersonaF LEFT OUTER JOIN
                                         dbo.aca_Plantilla AS A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdAnio = A3.IdAnio AND A1.IdPlantilla = A3.IdPlantilla
                       WHERE   (A2.EnCurso = 1)) AS e ON b.IdEmpresa = e.IdEmpresa AND b.IdAlumno = e.IdAlumno INNER JOIN
                  dbo.fa_cliente AS f ON f.IdEmpresa = a.IdEmpresa AND f.IdCliente = a.IdCliente INNER JOIN
                  dbo.tb_persona AS g ON g.IdPersona = f.IdPersona LEFT OUTER JOIN
                  dbo.tb_TarjetaCredito AS h ON a.IdEmpresa = h.IdEmpresa AND a.IdTarjeta = h.IdTarjeta
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWCXC_002';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         Begin Table = "h"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 256
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWCXC_002';


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
               Right = 296
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 322
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
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 304
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "g"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWCXC_002';

