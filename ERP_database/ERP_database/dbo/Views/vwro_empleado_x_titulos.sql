CREATE VIEW dbo.vwro_empleado_x_titulos
AS
SELECT        dbo.ro_catalogo.CodCatalogo, dbo.ro_empleado_x_titulos.IdEmpresa, dbo.ro_empleado_x_titulos.IdEmpleado, dbo.ro_empleado_x_titulos.Secuencia, dbo.ro_empleado_x_titulos.fecha, dbo.ro_empleado_x_titulos.Observacion, 
                         dbo.ro_empleado.em_codigo, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto, institucion.ca_descripcion AS institucion, 
                         dbo.ro_catalogo.ca_descripcion AS titulo, dbo.ro_empleado_x_titulos.IdInstitucion, dbo.ro_empleado_x_titulos.IdTitulo, dbo.ro_empleado_x_titulos.estado
FROM            dbo.ro_empleado INNER JOIN
                         dbo.ro_empleado_x_titulos ON dbo.ro_empleado.IdEmpresa = dbo.ro_empleado_x_titulos.IdEmpresa AND dbo.ro_empleado.IdEmpleado = dbo.ro_empleado_x_titulos.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_catalogo AS institucion ON dbo.ro_empleado_x_titulos.IdInstitucion = institucion.CodCatalogo INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_empleado_x_titulos.IdTitulo = dbo.ro_catalogo.CodCatalogo

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[67] 4[5] 2[11] 3) )"
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
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 332
               Right = 327
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado_x_titulos"
            Begin Extent = 
               Top = 6
               Left = 348
               Bottom = 308
               Right = 546
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 9
               Left = 746
               Bottom = 307
               Right = 960
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "institucion"
            Begin Extent = 
               Top = 30
               Left = 690
               Bottom = 242
               Right = 871
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_catalogo"
            Begin Extent = 
               Top = 98
               Left = 996
               Bottom = 228
               Right = 1177
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
      Begin ColumnWidths = 16
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Wi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_x_titulos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'dth = 1500
         Width = 1500
         Width = 1500
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_x_titulos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_empleado_x_titulos';

