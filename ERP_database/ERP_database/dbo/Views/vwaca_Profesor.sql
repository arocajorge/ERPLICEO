CREATE VIEW dbo.vwaca_Profesor
AS
SELECT dbo.aca_Profesor.IdEmpresa, dbo.aca_Profesor.IdProfesor, dbo.aca_Profesor.IdPersona, dbo.aca_Profesor.Codigo, dbo.aca_Profesor.Estado, dbo.tb_persona.CodPersona, dbo.tb_persona.pe_Naturaleza, 
                  dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_razonSocial, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_direccion, 
                  dbo.tb_persona.pe_telfono_Contacto, dbo.tb_persona.pe_celular, dbo.tb_persona.pe_sexo, dbo.tb_persona.IdEstadoCivil, dbo.tb_persona.pe_fechaNacimiento, dbo.tb_persona.pe_estado, dbo.tb_persona.pe_correo, 
                  dbo.aca_Profesor.Correo, dbo.aca_Profesor.Direccion, dbo.aca_Profesor.Telefonos, dbo.aca_Profesor.EsProfesor, dbo.aca_Profesor.EsInspector, dbo.tb_persona.CodCatalogoSangre, dbo.tb_persona.CodCatalogoCONADIS, 
                  dbo.tb_persona.PorcentajeDiscapacidad, dbo.tb_persona.NumeroCarnetConadis, dbo.tb_persona.IdProfesion
FROM     dbo.aca_Profesor INNER JOIN
                  dbo.tb_persona ON dbo.aca_Profesor.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Profesor';


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
         Begin Table = "aca_Profesor"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 311
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 341
               Bottom = 311
               Right = 615
            End
            DisplayFlags = 280
            TopColumn = 24
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwaca_Profesor';







