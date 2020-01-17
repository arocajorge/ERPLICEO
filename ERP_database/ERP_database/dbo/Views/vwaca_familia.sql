CREATE VIEW dbo.vwaca_Familia
AS
SELECT dbo.aca_Familia.IdEmpresa, dbo.aca_Familia.IdAlumno, dbo.aca_Familia.IdCatalogoPAREN, dbo.aca_Catalogo.NomCatalogo, dbo.aca_Familia.IdPersona, dbo.tb_persona.pe_Naturaleza, dbo.tb_persona.IdTipoDocumento, 
                  dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto, dbo.aca_Familia.Direccion, dbo.aca_Familia.Celular, dbo.aca_Familia.Correo, dbo.aca_Familia.SeFactura, 
                  dbo.tb_persona.pe_sexo, dbo.tb_persona.IdEstadoCivil, dbo.tb_persona.pe_fechaNacimiento, dbo.tb_persona.CodCatalogoSangre, dbo.tb_persona.CodCatalogoCONADIS, dbo.tb_persona.PorcentajeDiscapacidad, 
                  dbo.tb_persona.NumeroCarnetConadis, dbo.tb_persona.pe_telfono_Contacto, dbo.aca_Familia.Secuencia, dbo.aca_Familia.EsRepresentante, dbo.tb_persona.pe_razonSocial, dbo.tb_persona.IdProfesion, 
                  dbo.aca_Familia.IdCatalogoFichaInst, dbo.aca_Familia.EmpresaTrabajo, dbo.aca_Familia.DireccionTrabajo, dbo.aca_Familia.TelefonoTrabajo, dbo.aca_Familia.CargoTrabajo, dbo.aca_Familia.AniosServicio, 
                  dbo.aca_Familia.IngresoMensual, dbo.aca_Familia.VehiculoPropio, dbo.aca_Familia.Marca, dbo.aca_Familia.Modelo, dbo.aca_Familia.CasaPropia, dbo.aca_Familia.Estado, dbo.aca_Familia.AnioVehiculo
FROM     dbo.tb_persona INNER JOIN
                  dbo.aca_Familia ON dbo.tb_persona.IdPersona = dbo.aca_Familia.IdPersona LEFT OUTER JOIN
                  dbo.aca_Catalogo ON dbo.aca_Familia.IdCatalogoPAREN = dbo.aca_Catalogo.IdCatalogo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_familia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[9] 2[20] 3) )"
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
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 140
               Left = 724
               Bottom = 398
               Right = 998
            End
            DisplayFlags = 280
            TopColumn = 23
         End
         Begin Table = "aca_Familia"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 418
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "aca_Catalogo"
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
      Begin ColumnWidths = 10
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









