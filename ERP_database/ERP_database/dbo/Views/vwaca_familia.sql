CREATE VIEW dbo.vwaca_Familia
AS
SELECT f.IdEmpresa, f.IdAlumno, f.IdCatalogoPAREN, c.NomCatalogo, f.IdPersona, p.pe_Naturaleza, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_apellido, p.pe_nombre, p.pe_nombreCompleto, f.Direccion, f.Celular, f.Correo, f.SeFactura, p.pe_sexo, 
                  p.IdEstadoCivil, p.pe_fechaNacimiento, p.CodCatalogoSangre, p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.pe_telfono_Contacto, f.Secuencia, f.EsRepresentante, p.pe_razonSocial, p.IdProfesion, 
                  f.IdCatalogoFichaInst, f.EmpresaTrabajo, f.DireccionTrabajo, f.TelefonoTrabajo, f.CargoTrabajo, f.AniosServicio, f.IngresoMensual, f.VehiculoPropio, f.Marca, f.Modelo, f.CasaPropia, f.AnioVehiculo, p.IdReligion, p.AsisteCentroCristiano, 
                  f.EstaFallecido, p.IdGrupoEtnico, f.IdPais, f.Cod_Region, f.IdProvincia, f.IdCiudad, f.IdParroquia, f.Sector, f.Estado, f.Telefono
FROM     dbo.tb_persona AS p WITH (nolock) INNER JOIN
                  dbo.aca_Familia AS f WITH (nolock) ON p.IdPersona = f.IdPersona LEFT OUTER JOIN
                  dbo.aca_Catalogo AS c WITH (nolock) ON f.IdCatalogoPAREN = c.IdCatalogo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Familia';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[9] 2[29] 3) )"
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
         Begin Table = "p"
            Begin Extent = 
               Top = 140
               Left = 724
               Bottom = 398
               Right = 998
            End
            DisplayFlags = 280
            TopColumn = 26
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 418
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 0
               Left = 568
               Bottom = 163
               Right = 813
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
      Begin ColumnWidths = 44
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
     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Familia';




















GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Width = 1200
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Familia';









