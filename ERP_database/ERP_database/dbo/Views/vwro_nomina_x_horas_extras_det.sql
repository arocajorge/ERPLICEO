CREATE VIEW dbo.vwro_nomina_x_horas_extras_det
AS
SELECT        he_det.IdEmpresa, he_det.IdHorasExtras, he_det.IdEmpleado, he_det.IdCalendario, he_det.IdTurno, he_det.IdHorario, he_det.FechaRegistro, he_det.time_entrada1, he_det.time_entrada2, he_det.time_salida1, 
                         he_det.time_salida2, he_det.hora_extra25, he_det.hora_extra50, he_det.hora_extra100, he_det.hora_atraso, he_det.hora_temprano, he_det.hora_trabajada, he_det.es_HorasExtrasAutorizadas, horario.Descripcion AS Horario, 
                         per.pe_nombreCompleto, per.pe_apellido, per.pe_nombre, turno.tu_descripcion, car.ca_descripcion, per.pe_cedulaRuc, he_det.Valor25, he_det.Valor50, he_det.Valor100, he_det.Sueldo_base
FROM            dbo.ro_contrato AS cont INNER JOIN
                         dbo.tb_persona AS per INNER JOIN
                         dbo.ro_empleado AS emp ON per.IdPersona = emp.IdPersona ON cont.IdEmpresa = emp.IdEmpresa AND cont.IdEmpleado = emp.IdEmpleado INNER JOIN
                         dbo.ro_horario AS horario INNER JOIN
                         dbo.ro_nomina_x_horas_extras AS he INNER JOIN
                         dbo.ro_nomina_x_horas_extras_det AS he_det ON he.IdEmpresa = he_det.IdEmpresa AND he.IdHorasExtras = he_det.IdHorasExtras ON horario.IdEmpresa = he_det.IdEmpresa AND 
                         horario.IdHorario = he_det.IdHorario INNER JOIN
                         dbo.ro_turno AS turno ON he_det.IdEmpresa = turno.IdEmpresa AND he_det.IdTurno = turno.IdTurno ON emp.IdEmpresa = he_det.IdEmpresa AND emp.IdEmpleado = he_det.IdEmpleado INNER JOIN
                         dbo.ro_cargo AS car ON emp.IdEmpresa = car.IdEmpresa AND emp.IdCargo = car.IdCargo
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[58] 4[5] 2[5] 3) )"
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
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cont"
            Begin Extent = 
               Top = 0
               Left = 542
               Bottom = 302
               Right = 737
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 14
               Left = 866
               Bottom = 231
               Right = 1114
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "emp"
            Begin Extent = 
               Top = 318
               Left = 510
               Bottom = 539
               Right = 815
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "horario"
            Begin Extent = 
               Top = 172
               Left = 100
               Bottom = 370
               Right = 309
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "he"
            Begin Extent = 
               Top = 21
               Left = 51
               Bottom = 202
               Right = 256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "he_det"
            Begin Extent = 
               Top = 35
               Left = 223
               Bottom = 298
               Right = 467
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "turno"
            Begin Extent = 
               Top = 0
               Left = 1068
               Bottom = 130
               Right = 1263
            End
            DisplayFlags = 280
            Top', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_nomina_x_horas_extras_det';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Column = 0
         End
         Begin Table = "car"
            Begin Extent = 
               Top = 104
               Left = 881
               Bottom = 304
               Right = 1114
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
      Begin ColumnWidths = 25
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_nomina_x_horas_extras_det';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwro_nomina_x_horas_extras_det';

