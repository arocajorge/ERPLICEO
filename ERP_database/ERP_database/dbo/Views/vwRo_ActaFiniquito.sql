CREATE VIEW dbo.vwRo_ActaFiniquito
AS
SELECT        dbo.tb_persona.pe_nombreCompleto AS NombreCompleto, dbo.tb_persona.pe_cedulaRuc AS Identificacion, dbo.ro_Acta_Finiquito.IdEmpresa, dbo.ro_Acta_Finiquito.IdActaFiniquito, dbo.ro_Acta_Finiquito.IdEmpleado, 
                         dbo.ro_Acta_Finiquito.IdCausaTerminacion, dbo.ro_Acta_Finiquito.FechaIngreso, dbo.ro_Acta_Finiquito.FechaSalida, dbo.ro_Acta_Finiquito.UltimaRemuneracion, dbo.ro_Acta_Finiquito.Observacion AS ObservacionFiniquito, 
                         dbo.ro_Acta_Finiquito.Ingresos, dbo.ro_Acta_Finiquito.Egresos, dbo.ro_Acta_Finiquito.Estado AS EstadoActa, dbo.ro_Acta_Finiquito.IdCodSectorial, dbo.ro_Acta_Finiquito.EsMujerEmbarazada, 
                         dbo.ro_Acta_Finiquito.EsDirigenteSindical, dbo.ro_Acta_Finiquito.EsPorDiscapacidad, dbo.ro_Acta_Finiquito.EsPorEnfermedadNoProfesional, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, 
                         dbo.ro_empleado.em_codigo, dbo.ro_Acta_Finiquito.IdTipoCbte, dbo.ro_Acta_Finiquito.IdOrdenPago, dbo.ro_Acta_Finiquito.IdCbteCble, dbo.ro_catalogo.ca_descripcion, ro_catalogo_1.ca_descripcion AS Contrato, 
                         ro_catalogo_1.CodCatalogo, dbo.ro_contrato.EstadoContrato
FROM            dbo.ro_catalogo AS ro_catalogo_1 INNER JOIN
                         dbo.ro_contrato ON ro_catalogo_1.CodCatalogo = dbo.ro_contrato.IdContrato_Tipo INNER JOIN
                         dbo.ro_Acta_Finiquito INNER JOIN
                         dbo.ro_empleado ON dbo.ro_Acta_Finiquito.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_Acta_Finiquito.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.ro_catalogo ON dbo.ro_Acta_Finiquito.IdCausaTerminacion = dbo.ro_catalogo.CodCatalogo ON dbo.ro_contrato.IdEmpresa = dbo.ro_Acta_Finiquito.IdEmpresa AND 
                         dbo.ro_contrato.IdEmpleado = dbo.ro_Acta_Finiquito.IdEmpleado AND dbo.ro_contrato.IdContrato = dbo.ro_Acta_Finiquito.IdContrato

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[60] 4[5] 2[5] 3) )"
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
         Begin Table = "ro_catalogo_1"
            Begin Extent = 
               Top = 341
               Left = 794
               Bottom = 522
               Right = 980
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_contrato"
            Begin Extent = 
               Top = 40
               Left = 109
               Bottom = 219
               Right = 288
            End
            DisplayFlags = 280
            TopColumn = 12
         End
         Begin Table = "ro_Acta_Finiquito"
            Begin Extent = 
               Top = 47
               Left = 563
               Bottom = 280
               Right = 820
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 0
               Left = 665
               Bottom = 260
               Right = 954
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 0
               Left = 889
               Bottom = 228
               Right = 1121
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "ro_catalogo"
            Begin Extent = 
               Top = 319
               Left = 44
               Bottom = 472
               Right = 225
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
      Begin ColumnWidths = 28
         Width = 284
         Width = 150', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_ActaFiniquito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'0
         Width = 1500
         Width = 1500
         Width = 2010
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_ActaFiniquito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwRo_ActaFiniquito';

