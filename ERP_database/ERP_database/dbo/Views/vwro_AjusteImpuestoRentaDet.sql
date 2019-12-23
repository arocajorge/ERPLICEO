CREATE VIEW dbo.vwro_AjusteImpuestoRentaDet
AS
SELECT dbo.ro_AjusteImpuestoRentaDet.IdEmpresa, dbo.ro_AjusteImpuestoRentaDet.IdAjuste, dbo.ro_AjusteImpuestoRentaDet.Secuencia, dbo.ro_AjusteImpuestoRentaDet.IdEmpleado, dbo.ro_AjusteImpuestoRentaDet.SueldoFechaCorte, 
                  dbo.ro_AjusteImpuestoRentaDet.SueldoProyectado, dbo.ro_AjusteImpuestoRentaDet.OtrosIngresos, dbo.ro_AjusteImpuestoRentaDet.IngresosLiquidos, dbo.ro_AjusteImpuestoRentaDet.GastosPersonales, 
                  dbo.ro_AjusteImpuestoRentaDet.AporteFechaCorte, dbo.ro_AjusteImpuestoRentaDet.BaseImponible, dbo.ro_AjusteImpuestoRentaDet.FraccionBasica, dbo.ro_AjusteImpuestoRentaDet.Excedente, 
                  dbo.ro_AjusteImpuestoRentaDet.ImpuestoFraccionBasica, dbo.ro_AjusteImpuestoRentaDet.ImpuestoRentaCausado, dbo.ro_AjusteImpuestoRentaDet.DescontadoFechaCorte, dbo.ro_AjusteImpuestoRentaDet.LiquidacionFinal, 
                  dbo.tb_persona.pe_nombreCompleto
FROM     dbo.ro_AjusteImpuestoRentaDet INNER JOIN
                  dbo.ro_empleado ON dbo.ro_AjusteImpuestoRentaDet.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_AjusteImpuestoRentaDet.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                  dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_AjusteImpuestoRentaDet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[35] 2[6] 3) )"
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
         Begin Table = "ro_AjusteImpuestoRentaDet"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 311
               Right = 302
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 7
               Left = 350
               Bottom = 170
               Right = 697
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 7
               Left = 745
               Bottom = 170
               Right = 1019
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
      Begin ColumnWidths = 9
         Width = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_AjusteImpuestoRentaDet';



