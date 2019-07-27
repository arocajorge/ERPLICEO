CREATE VIEW dbo.vwro_NominasPagosCheques_det
AS
SELECT        dbo.ro_NominasPagosCheques_det.IdEmpresa, dbo.ro_NominasPagosCheques_det.IdTransaccion, dbo.ro_NominasPagosCheques_det.Secuencia, dbo.ro_NominasPagosCheques_det.IdSucursal, 
                         dbo.ro_NominasPagosCheques_det.IdEmpleado, dbo.ro_NominasPagosCheques_det.Observacion, dbo.ro_NominasPagosCheques_det.Valor, dbo.ro_NominasPagosCheques_det.IdEmpresa_op, 
                         dbo.ro_NominasPagosCheques_det.IdOrdenPago, dbo.ro_empleado.em_codigo, dbo.ro_empleado.em_tipoCta, dbo.ro_empleado.em_NumCta, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, 
                         dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.IdPersona, dbo.ro_NominasPagosCheques_det.IdEmpresa_dc, dbo.ro_NominasPagosCheques_det.IdTipoCbte, dbo.ro_NominasPagosCheques_det.IdCbteCble, 
                         dbo.ro_NominasPagosCheques_det.Secuancia_op, dbo.ro_NominasPagosCheques_det.Estado
FROM            dbo.ro_NominasPagosCheques INNER JOIN
                         dbo.ro_NominasPagosCheques_det ON dbo.ro_NominasPagosCheques.IdEmpresa = dbo.ro_NominasPagosCheques_det.IdEmpresa AND 
                         dbo.ro_NominasPagosCheques.IdTransaccion = dbo.ro_NominasPagosCheques_det.IdTransaccion INNER JOIN
                         dbo.ro_empleado ON dbo.ro_NominasPagosCheques_det.IdEmpresa = dbo.ro_empleado.IdEmpresa AND dbo.ro_NominasPagosCheques_det.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_NominasPagosCheques_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_NominasPagosCheques_det';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[72] 4[5] 2[8] 3) )"
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
         Begin Table = "ro_NominasPagosCheques"
            Begin Extent = 
               Top = 4
               Left = 38
               Bottom = 136
               Right = 362
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_NominasPagosCheques_det"
            Begin Extent = 
               Top = 14
               Left = 390
               Bottom = 264
               Right = 560
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 0
               Left = 639
               Bottom = 309
               Right = 928
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 89
               Left = 1005
               Bottom = 348
               Right = 1237
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
         Or =', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_NominasPagosCheques_det';



