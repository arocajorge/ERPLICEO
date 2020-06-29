CREATE VIEW dbo.vwba_ArchivoRecaudacion_Archivo
AS
SELECT dbo.ba_ArchivoRecaudacionDet.IdEmpresa, dbo.ba_ArchivoRecaudacionDet.IdArchivo, dbo.ba_ArchivoRecaudacionDet.Secuencia, dbo.ba_ArchivoRecaudacion.Nom_Archivo, dbo.ba_ArchivoRecaudacion.IdBanco, 
                  dbo.ba_Banco_Cuenta.ba_Num_Cuenta, dbo.tb_banco.CodigoLegal, dbo.ba_ArchivoRecaudacionDet.IdMatricula, dbo.ba_ArchivoRecaudacionDet.IdAlumno, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, 
                  dbo.aca_Alumno.Codigo AS CodigoAlumno, dbo.tb_persona.pe_nombreCompleto AS NomAlumno, dbo.ba_ArchivoRecaudacionDet.Valor, dbo.ba_ArchivoRecaudacionDet.ValorProntoPago, 
                  dbo.ba_ArchivoRecaudacion.SecuencialDescarga, dbo.ba_ArchivoRecaudacion.Fecha, dbo.ba_ArchivoRecaudacion.Observacion, dbo.ba_ArchivoRecaudacionDet.FechaProceso, dbo.ba_ArchivoRecaudacionDet.FechaProntoPago
FROM     dbo.ba_ArchivoRecaudacion INNER JOIN
                  dbo.ba_ArchivoRecaudacionDet ON dbo.ba_ArchivoRecaudacion.IdEmpresa = dbo.ba_ArchivoRecaudacionDet.IdEmpresa AND dbo.ba_ArchivoRecaudacion.IdArchivo = dbo.ba_ArchivoRecaudacionDet.IdArchivo INNER JOIN
                  dbo.ba_Banco_Cuenta ON dbo.ba_ArchivoRecaudacion.IdEmpresa = dbo.ba_Banco_Cuenta.IdEmpresa AND dbo.ba_ArchivoRecaudacion.IdBanco = dbo.ba_Banco_Cuenta.IdBanco LEFT OUTER JOIN
                  dbo.tb_banco ON dbo.ba_Banco_Cuenta.IdBanco_Financiero = dbo.tb_banco.IdBanco LEFT OUTER JOIN
                  dbo.tb_persona INNER JOIN
                  dbo.aca_Alumno ON dbo.tb_persona.IdPersona = dbo.aca_Alumno.IdPersona ON dbo.ba_ArchivoRecaudacionDet.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.ba_ArchivoRecaudacionDet.IdAlumno = dbo.aca_Alumno.IdAlumno
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ArchivoRecaudacion_Archivo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'    Width = 1764
         Width = 1200
         Width = 1584
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ArchivoRecaudacion_Archivo';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[62] 4[8] 2[13] 3) )"
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
         Begin Table = "ba_ArchivoRecaudacion"
            Begin Extent = 
               Top = 7
               Left = 26
               Bottom = 170
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ba_ArchivoRecaudacionDet"
            Begin Extent = 
               Top = 244
               Left = 14
               Bottom = 459
               Right = 328
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "ba_Banco_Cuenta"
            Begin Extent = 
               Top = 202
               Left = 428
               Bottom = 365
               Right = 718
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "tb_banco"
            Begin Extent = 
               Top = 53
               Left = 1212
               Bottom = 255
               Right = 1487
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 0
               Left = 718
               Bottom = 303
               Right = 992
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 84
               Left = 401
               Bottom = 247
               Right = 646
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
     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ArchivoRecaudacion_Archivo';





