CREATE VIEW dbo.vwaca_Alumno
AS
SELECT a.IdEmpresa, a.IdAlumno, a.Codigo, a.IdPersona, p.pe_Naturaleza, p.pe_nombreCompleto, p.pe_apellido, p.pe_nombre, p.IdTipoDocumento, p.pe_cedulaRuc, a.Direccion, a.Celular, a.Correo, p.pe_sexo, p.pe_fechaNacimiento, 
                  p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, a.Estado, a.IdCatalogoESTMAT, a.IdCurso, a.IdCatalogoESTALU, p.pe_telfono_Contacto, 
                  aca_Catalogo_1.NomCatalogo AS NomCatalogoESTMAT, c.NomCatalogo AS NomCatalogoESTALU, a.FechaIngreso, a.LugarNacimiento, a.IdPais, a.Cod_Region, a.IdProvincia, a.IdCiudad, a.IdParroquia, a.Sector, p.IdReligion, 
                  p.AsisteCentroCristiano, p.IdGrupoEtnico, a.Dificultad_Lectura, a.Dificultad_Escritura, a.Dificultad_Matematicas
FROM     dbo.aca_Alumno AS a WITH (nolock) INNER JOIN
                  dbo.tb_persona AS p WITH (nolock) ON a.IdPersona = p.IdPersona LEFT OUTER JOIN
                  dbo.aca_Catalogo AS c WITH (nolock) ON a.IdCatalogoESTALU = c.IdCatalogo LEFT OUTER JOIN
                  dbo.aca_Catalogo AS aca_Catalogo_1 WITH (nolock) ON a.IdCatalogoESTMAT = aca_Catalogo_1.IdCatalogo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_alumno';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[6] 2[33] 3) )"
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
               Left = 55
               Bottom = 477
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 0
               Left = 451
               Bottom = 223
               Right = 725
            End
            DisplayFlags = 280
            TopColumn = 28
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 51
               Left = 907
               Bottom = 214
               Right = 1152
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Catalogo_1"
            Begin Extent = 
               Top = 218
               Left = 789
               Bottom = 381
               Right = 1034
            End
            DisplayFlags = 280
            TopColumn = 2
         End
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
         W', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Alumno';
















GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'idth = 1200
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Alumno';







