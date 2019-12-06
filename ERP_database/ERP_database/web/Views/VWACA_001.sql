﻿CREATE VIEW web.VWACA_001
AS
SELECT dbo.aca_Matricula.IdEmpresa, dbo.aca_Matricula.IdMatricula, dbo.aca_Matricula.Fecha AS FechaMatricula, dbo.aca_Matricula.Codigo AS CodMatricula, dbo.aca_Alumno.Codigo AS CodAlumno, dbo.aca_Matricula.IdAlumno, 
                  dbo.aca_Alumno.IdPersona, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.vwaca_AnioLectivo_Jornada_Curso.Descripcion AS Anio, dbo.vwaca_AnioLectivo_Jornada_Curso.NomSede, 
                  dbo.vwaca_AnioLectivo_Jornada_Curso.NomNivel, dbo.vwaca_AnioLectivo_Jornada_Curso.NomJornada, dbo.vwaca_AnioLectivo_Jornada_Curso.NomCurso, dbo.aca_Paralelo.NomParalelo, dbo.tb_Catalogo.ca_descripcion AS Sexo, 
                  dbo.aca_Alumno.LugarNacimiento, dbo.tb_persona.pe_fechaNacimiento, dbo.aca_Alumno.Direccion, dbo.aca_Alumno.Celular, dbo.aca_SocioEconomico.TieneElectricidad, dbo.aca_SocioEconomico.TieneHermanos, 
                  dbo.aca_SocioEconomico.NombreHermanos, dbo.aca_CatalogoFicha.NomCatalogoFicha AS TipoVivienda, aca_CatalogoFicha_1.NomCatalogoFicha AS TenenciaVivienda, dbo.tb_persona.CodCatalogoCONADIS, 
                  dbo.tb_persona.PorcentajeDiscapacidad, dbo.tb_persona.NumeroCarnetConadis
FROM     dbo.aca_Matricula INNER JOIN
                  dbo.aca_Alumno ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.aca_Matricula.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.vwaca_AnioLectivo_Jornada_Curso ON dbo.aca_Matricula.IdEmpresa = dbo.vwaca_AnioLectivo_Jornada_Curso.IdEmpresa AND dbo.aca_Matricula.IdAnio = dbo.vwaca_AnioLectivo_Jornada_Curso.IdAnio AND 
                  dbo.aca_Matricula.IdSede = dbo.vwaca_AnioLectivo_Jornada_Curso.IdSede AND dbo.aca_Matricula.IdNivel = dbo.vwaca_AnioLectivo_Jornada_Curso.IdNivel AND 
                  dbo.aca_Matricula.IdJornada = dbo.vwaca_AnioLectivo_Jornada_Curso.IdJornada AND dbo.aca_Matricula.IdCurso = dbo.vwaca_AnioLectivo_Jornada_Curso.IdCurso INNER JOIN
                  dbo.aca_Paralelo ON dbo.aca_Matricula.IdEmpresa = dbo.aca_Paralelo.IdEmpresa AND dbo.aca_Matricula.IdParalelo = dbo.aca_Paralelo.IdParalelo INNER JOIN
                  dbo.tb_Catalogo ON dbo.tb_persona.pe_sexo = dbo.tb_Catalogo.CodCatalogo INNER JOIN
                  dbo.aca_SocioEconomico ON dbo.aca_Alumno.IdEmpresa = dbo.aca_SocioEconomico.IdEmpresa AND dbo.aca_Alumno.IdAlumno = dbo.aca_SocioEconomico.IdAlumno INNER JOIN
                  dbo.aca_CatalogoFicha ON dbo.aca_SocioEconomico.IdCatalogoFichaVi = dbo.aca_CatalogoFicha.IdCatalogoFicha INNER JOIN
                  dbo.aca_CatalogoFicha AS aca_CatalogoFicha_1 ON dbo.aca_SocioEconomico.IdCatalogoFichaTVi = aca_CatalogoFicha_1.IdCatalogoFicha
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACA_001';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 1056
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "aca_CatalogoFicha"
            Begin Extent = 
               Top = 19
               Left = 1188
               Bottom = 182
               Right = 1433
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_CatalogoFicha_1"
            Begin Extent = 
               Top = 230
               Left = 1158
               Bottom = 393
               Right = 1403
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
      Begin ColumnWidths = 30
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
         Width = 1968
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACA_001';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[55] 2[3] 3) )"
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
               Left = 345
               Bottom = 250
               Right = 586
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 446
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 22
         End
         Begin Table = "vwaca_AnioLectivo_Jornada_Curso"
            Begin Extent = 
               Top = 7
               Left = 634
               Bottom = 170
               Right = 878
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "aca_Paralelo"
            Begin Extent = 
               Top = 7
               Left = 926
               Bottom = 170
               Right = 1171
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_Catalogo"
            Begin Extent = 
               Top = 194
               Left = 519
               Bottom = 414
               Right = 763
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_SocioEconomico"
            Begin Extent = 
               Top = 175
               Left = 811
               Bottom = 431
               Right', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'VWACA_001';

