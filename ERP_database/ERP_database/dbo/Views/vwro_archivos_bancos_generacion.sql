CREATE VIEW dbo.vwro_archivos_bancos_generacion
AS
SELECT        dbo.ro_archivos_bancos_generacion.IdEmpresa, dbo.ro_archivos_bancos_generacion.IdArchivo, dbo.ro_archivos_bancos_generacion.IdNomina, dbo.ro_archivos_bancos_generacion.IdNominaTipo, 
                         dbo.ro_archivos_bancos_generacion.IdPeriodo, dbo.ro_archivos_bancos_generacion.IdCuentaBancaria, dbo.ro_archivos_bancos_generacion.IdProceso, dbo.ro_Nomina_Tipo.Descripcion, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.tb_banco_procesos_bancarios_x_empresa.NombreProceso, dbo.ro_archivos_bancos_generacion.estado, 
                         dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso_bancario_tipo, dbo.tb_sucursal.IdSucursal, dbo.tb_sucursal.Su_Descripcion
FROM            dbo.ro_Nomina_Tipoliqui INNER JOIN
                         dbo.ro_Nomina_Tipo ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_Nomina_Tipo.IdEmpresa AND dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_Nomina_Tipo.IdNomina_Tipo INNER JOIN
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui ON dbo.ro_Nomina_Tipoliqui.IdEmpresa = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa AND 
                         dbo.ro_Nomina_Tipoliqui.IdNomina_Tipo = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo AND dbo.ro_Nomina_Tipoliqui.IdNomina_TipoLiqui = dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui INNER JOIN
                         dbo.ro_archivos_bancos_generacion ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_archivos_bancos_generacion.IdEmpresa AND 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_Tipo = dbo.ro_archivos_bancos_generacion.IdNomina AND 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdNomina_TipoLiqui = dbo.ro_archivos_bancos_generacion.IdNominaTipo AND 
                         dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_archivos_bancos_generacion.IdPeriodo INNER JOIN
                         dbo.ro_periodo ON dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdEmpresa = dbo.ro_periodo.IdEmpresa AND dbo.ro_periodo_x_ro_Nomina_TipoLiqui.IdPeriodo = dbo.ro_periodo.IdPeriodo INNER JOIN
                         dbo.tb_banco_procesos_bancarios_x_empresa ON dbo.ro_archivos_bancos_generacion.IdProceso = dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso AND 
                         dbo.ro_archivos_bancos_generacion.IdEmpresa = dbo.tb_banco_procesos_bancarios_x_empresa.IdEmpresa LEFT OUTER JOIN
                         dbo.tb_sucursal ON dbo.ro_archivos_bancos_generacion.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.ro_archivos_bancos_generacion.IdSucursal = dbo.tb_sucursal.IdSucursal
GROUP BY dbo.ro_archivos_bancos_generacion.IdEmpresa, dbo.ro_archivos_bancos_generacion.IdArchivo, dbo.ro_archivos_bancos_generacion.IdNomina, dbo.ro_archivos_bancos_generacion.IdNominaTipo, 
                         dbo.ro_archivos_bancos_generacion.IdPeriodo, dbo.ro_archivos_bancos_generacion.IdCuentaBancaria, dbo.ro_archivos_bancos_generacion.IdProceso, dbo.ro_Nomina_Tipo.Descripcion, 
                         dbo.ro_Nomina_Tipoliqui.DescripcionProcesoNomina, dbo.ro_periodo.pe_FechaIni, dbo.ro_periodo.pe_FechaFin, dbo.tb_banco_procesos_bancarios_x_empresa.NombreProceso, dbo.ro_archivos_bancos_generacion.estado, 
                         dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso_bancario_tipo, dbo.tb_sucursal.IdSucursal, dbo.tb_sucursal.Su_Descripcion
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_archivos_bancos_generacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'            Bottom = 401
               Right = 1004
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
      Begin ColumnWidths = 17
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_archivos_bancos_generacion';










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[18] 4[5] 2[5] 3) )"
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
         Begin Table = "ro_Nomina_Tipoliqui"
            Begin Extent = 
               Top = 314
               Left = 661
               Bottom = 537
               Right = 972
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "ro_Nomina_Tipo"
            Begin Extent = 
               Top = 26
               Left = 824
               Bottom = 230
               Right = 1006
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_periodo_x_ro_Nomina_TipoLiqui"
            Begin Extent = 
               Top = 112
               Left = 722
               Bottom = 356
               Right = 919
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_archivos_bancos_generacion"
            Begin Extent = 
               Top = 0
               Left = 318
               Bottom = 377
               Right = 522
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_periodo"
            Begin Extent = 
               Top = 231
               Left = 846
               Bottom = 561
               Right = 1067
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco_procesos_bancarios_x_empresa"
            Begin Extent = 
               Top = 0
               Left = 0
               Bottom = 315
               Right = 303
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 34
               Left = 758
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_archivos_bancos_generacion';









