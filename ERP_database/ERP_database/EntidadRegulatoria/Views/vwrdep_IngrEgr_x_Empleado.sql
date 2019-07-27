CREATE VIEW EntidadRegulatoria.vwrdep_IngrEgr_x_Empleado
AS
SELECT        EntidadRegulatoria.ro_rdep.IdEmpresa, EntidadRegulatoria.ro_rdep.Id_Rdep, EntidadRegulatoria.ro_rdep.IdSucursal, EntidadRegulatoria.ro_rdep.pe_anio, EntidadRegulatoria.ro_rdep.IdNomina_Tipo, 
                         EntidadRegulatoria.ro_rdep.Su_CodigoEstablecimiento, EntidadRegulatoria.ro_rdep.Observacion, EntidadRegulatoria.ro_rdep.Estado, EntidadRegulatoria.ro_rdep_det.Secuencia, EntidadRegulatoria.ro_rdep_det.IdEmpleado, 
                         EntidadRegulatoria.ro_rdep_det.pe_cedulaRuc, EntidadRegulatoria.ro_rdep_det.pe_nombre, EntidadRegulatoria.ro_rdep_det.pe_apellido, EntidadRegulatoria.ro_rdep_det.Sueldo, 
                         EntidadRegulatoria.ro_rdep_det.FondosReserva, EntidadRegulatoria.ro_rdep_det.DecimoTercerSueldo, EntidadRegulatoria.ro_rdep_det.DecimoCuartoSueldo, EntidadRegulatoria.ro_rdep_det.Vacaciones, 
                         EntidadRegulatoria.ro_rdep_det.AportePErsonal, EntidadRegulatoria.ro_rdep_det.GastoAlimentacion, EntidadRegulatoria.ro_rdep_det.GastoEucacion, EntidadRegulatoria.ro_rdep_det.GastoSalud, 
                         EntidadRegulatoria.ro_rdep_det.GastoVestimenta, EntidadRegulatoria.ro_rdep_det.GastoVivienda, EntidadRegulatoria.ro_rdep_det.Utilidades, EntidadRegulatoria.ro_rdep_det.IngresoVarios, 
                         EntidadRegulatoria.ro_rdep_det.IngresoPorOtrosEmpleaodres, EntidadRegulatoria.ro_rdep_det.IessPorOtrosEmpleadores, EntidadRegulatoria.ro_rdep_det.ValorImpuestoPorEsteEmplador, 
                         EntidadRegulatoria.ro_rdep_det.ValorImpuestoPorOtroEmplador, EntidadRegulatoria.ro_rdep_det.ExoneraionPorDiscapacidad, EntidadRegulatoria.ro_rdep_det.ExoneracionPorTerceraEdad, 
                         EntidadRegulatoria.ro_rdep_det.OtrosIngresosRelacionDependencia, EntidadRegulatoria.ro_rdep_det.ImpuestoRentaCausado, EntidadRegulatoria.ro_rdep_det.ValorImpuestoRetenidoTrabajador, 
                         EntidadRegulatoria.ro_rdep_det.ImpuestoRentaAsumidoPorEsteEmpleador, EntidadRegulatoria.ro_rdep_det.BaseImponibleGravada, EntidadRegulatoria.ro_rdep_det.IngresosGravadorPorEsteEmpleador
FROM            EntidadRegulatoria.ro_rdep INNER JOIN
                         EntidadRegulatoria.ro_rdep_det ON EntidadRegulatoria.ro_rdep.IdEmpresa = EntidadRegulatoria.ro_rdep_det.IdEmpresa AND EntidadRegulatoria.ro_rdep.Id_Rdep = EntidadRegulatoria.ro_rdep_det.Id_Rdep
WHERE        (EntidadRegulatoria.ro_rdep.Estado = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwrdep_IngrEgr_x_Empleado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'nd = 1400
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
', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwrdep_IngrEgr_x_Empleado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[12] 3) )"
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
         Begin Table = "ro_rdep (EntidadRegulatoria)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 268
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_rdep_det (EntidadRegulatoria)"
            Begin Extent = 
               Top = 6
               Left = 306
               Bottom = 136
               Right = 619
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
      Begin ColumnWidths = 39
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
         Appe', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwrdep_IngrEgr_x_Empleado';

