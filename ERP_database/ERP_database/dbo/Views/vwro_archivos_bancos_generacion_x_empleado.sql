CREATE VIEW dbo.vwro_archivos_bancos_generacion_x_empleado
AS
SELECT        dbo.ro_archivos_bancos_generacion.IdEmpresa, dbo.ro_archivos_bancos_generacion.IdArchivo, dbo.ro_archivos_bancos_generacion.IdNomina, dbo.ro_archivos_bancos_generacion.IdNominaTipo, 
                         dbo.ro_archivos_bancos_generacion.IdPeriodo, dbo.ro_archivos_bancos_generacion_x_empleado.Valor, dbo.ro_archivos_bancos_generacion_x_empleado.IdEmpleado, 
                         dbo.ro_archivos_bancos_generacion_x_empleado.pagacheque, dbo.ro_empleado.em_tipoCta, dbo.ro_empleado.em_NumCta, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.IdTipoDocumento, dbo.ro_archivos_bancos_generacion_x_empleado.IdSucursal, dbo.ro_archivos_bancos_generacion_x_empleado.Secuencia
FROM            dbo.ro_archivos_bancos_generacion INNER JOIN
                         dbo.ro_archivos_bancos_generacion_x_empleado ON dbo.ro_archivos_bancos_generacion.IdEmpresa = dbo.ro_archivos_bancos_generacion_x_empleado.IdEmpresa AND 
                         dbo.ro_archivos_bancos_generacion.IdArchivo = dbo.ro_archivos_bancos_generacion_x_empleado.IdArchivo INNER JOIN
                         dbo.ro_empleado ON dbo.ro_archivos_bancos_generacion_x_empleado.IdEmpresa = dbo.ro_empleado.IdEmpresa AND 
                         dbo.ro_archivos_bancos_generacion_x_empleado.IdEmpleado = dbo.ro_empleado.IdEmpleado INNER JOIN
                         dbo.tb_persona ON dbo.ro_empleado.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[71] 4[5] 2[5] 3) )"
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
         Begin Table = "ro_archivos_bancos_generacion"
            Begin Extent = 
               Top = 0
               Left = 204
               Bottom = 270
               Right = 438
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_archivos_bancos_generacion_x_empleado"
            Begin Extent = 
               Top = 6
               Left = 481
               Bottom = 216
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ro_empleado"
            Begin Extent = 
               Top = 30
               Left = 828
               Bottom = 389
               Right = 1117
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 111
               Left = 38
               Bottom = 456
               Right = 270
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
      Begin ColumnWidths = 18
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
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_archivos_bancos_generacion_x_empleado';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_archivos_bancos_generacion_x_empleado';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Table = 1170
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_archivos_bancos_generacion_x_empleado';



