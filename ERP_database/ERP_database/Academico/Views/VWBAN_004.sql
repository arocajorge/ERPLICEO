CREATE VIEW Academico.VWBAN_004
AS
SELECT dbo.ba_ArchivoRecaudacionDet.IdEmpresa, dbo.ba_ArchivoRecaudacionDet.IdArchivo, dbo.ba_ArchivoRecaudacionDet.Secuencia, dbo.ba_Banco_Cuenta.ba_Num_Cuenta, dbo.ba_Banco_Cuenta.ba_descripcion, 
                  dbo.tb_banco_procesos_bancarios_x_empresa.NombreProceso, dbo.ba_ArchivoRecaudacionDet.IdAlumno, dbo.aca_Alumno.Codigo, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, 
                  dbo.ba_ArchivoRecaudacionDet.Valor, dbo.ba_ArchivoRecaudacionDet.ValorProntoPago, dbo.ba_ArchivoRecaudacion.Fecha, dbo.ba_ArchivoRecaudacion.Observacion, dbo.ba_ArchivoRecaudacion.Estado
FROM     dbo.ba_ArchivoRecaudacionDet INNER JOIN
                  dbo.ba_ArchivoRecaudacion ON dbo.ba_ArchivoRecaudacionDet.IdEmpresa = dbo.ba_ArchivoRecaudacion.IdEmpresa AND dbo.ba_ArchivoRecaudacionDet.IdArchivo = dbo.ba_ArchivoRecaudacion.IdArchivo INNER JOIN
                  dbo.aca_Alumno ON dbo.ba_ArchivoRecaudacionDet.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.ba_ArchivoRecaudacionDet.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona LEFT OUTER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_ArchivoRecaudacion.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_ArchivoRecaudacion.IdBanco = dbo.ba_Banco_Cuenta.IdBanco LEFT OUTER JOIN
                  dbo.tb_banco_procesos_bancarios_x_empresa ON dbo.ba_ArchivoRecaudacion.IdEmpresa = dbo.tb_banco_procesos_bancarios_x_empresa.IdEmpresa AND 
                  dbo.ba_ArchivoRecaudacion.IdProceso_bancario = dbo.tb_banco_procesos_bancarios_x_empresa.IdProceso
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_004';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_004';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[27] 2[20] 3) )"
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
         Begin Table = "ba_ArchivoRecaudacionDet"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 270
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_ArchivoRecaudacion"
            Begin Extent = 
               Top = 23
               Left = 439
               Bottom = 186
               Right = 684
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_Banco_Cuenta"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_banco_procesos_bancarios_x_empresa"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 305
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWBAN_004';



