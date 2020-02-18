CREATE VIEW web.vwcxc_cobro
AS
SELECT dbo.cxc_cobro.IdEmpresa, dbo.cxc_cobro.IdSucursal, dbo.cxc_cobro.IdCobro, dbo.cxc_cobro.IdCliente, dbo.tb_persona.pe_nombreCompleto, dbo.cxc_cobro.IdCobro_tipo, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro.cr_TotalCobro, 
                  dbo.cxc_cobro.cr_estado, dbo.tb_sucursal.Su_Descripcion, dbo.cxc_cobro.cr_observacion, dbo.cxc_cobro.cr_NumDocumento, ISNULL(Tipo.nom_Motivo_tipo_cobro, t2.nom_Motivo_tipo_cobro) AS nom_Motivo_tipo_cobro, 
                  dbo.cxc_cobro.IdAlumno, tb_persona_1.pe_nombreCompleto AS NomAlumno, dbo.cxc_cobro.cr_Saldo, dbo.cxc_cobro.IdUsuario
FROM     dbo.tb_persona AS tb_persona_1 INNER JOIN
                  dbo.aca_Alumno ON tb_persona_1.IdPersona = dbo.aca_Alumno.IdPersona RIGHT OUTER JOIN
                  dbo.cxc_cobro INNER JOIN
                  dbo.fa_cliente ON dbo.cxc_cobro.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.cxc_cobro.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                  dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                  dbo.tb_sucursal ON dbo.cxc_cobro.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.cxc_cobro.IdSucursal = dbo.tb_sucursal.IdSucursal ON dbo.aca_Alumno.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND 
                  dbo.aca_Alumno.IdAlumno = dbo.cxc_cobro.IdAlumno LEFT OUTER JOIN
                      (SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_tipo_motivo.nom_Motivo_tipo_cobro
                       FROM      dbo.cxc_cobro_det INNER JOIN
                                         dbo.cxc_cobro_tipo ON dbo.cxc_cobro_det.IdCobro_tipo = dbo.cxc_cobro_tipo.IdCobro_tipo INNER JOIN
                                         dbo.cxc_cobro_tipo_motivo ON dbo.cxc_cobro_tipo.IdMotivo_tipo_cobro = dbo.cxc_cobro_tipo_motivo.IdMotivo_tipo_cobro
                       GROUP BY dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_tipo_motivo.nom_Motivo_tipo_cobro) AS Tipo ON Tipo.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND 
                  Tipo.IdSucursal = dbo.cxc_cobro.IdSucursal AND Tipo.IdCobro = dbo.cxc_cobro.IdCobro LEFT OUTER JOIN
                  dbo.cxc_cobro_tipo AS t1 ON dbo.cxc_cobro.IdCobro_tipo = t1.IdCobro_tipo LEFT OUTER JOIN
                  dbo.cxc_cobro_tipo_motivo AS t2 ON t1.IdMotivo_tipo_cobro = t2.IdMotivo_tipo_cobro


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[71] 4[3] 2[3] 3) )"
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
         Begin Table = "tb_persona_1"
            Begin Extent = 
               Top = 336
               Left = 914
               Bottom = 499
               Right = 1188
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 240
               Left = 198
               Bottom = 403
               Right = 443
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro"
            Begin Extent = 
               Top = 6
               Left = 594
               Bottom = 305
               Right = 788
            End
            DisplayFlags = 280
            TopColumn = 20
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 29
               Left = 64
               Bottom = 159
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 178
               Left = 85
               Bottom = 308
               Right = 317
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 186
               Left = 981
               Bottom = 316
               Right = 1211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Tipo"
            Begin Extent = 
               Top = 335
               Left = 88
               Bottom = 498
               Right = 344
            End
            ', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcxc_cobro';










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t1"
            Begin Extent = 
               Top = 504
               Left = 48
               Bottom = 667
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t2"
            Begin Extent = 
               Top = 672
               Left = 48
               Bottom = 791
               Right = 304
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcxc_cobro';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcxc_cobro';

