CREATE VIEW dbo.vwfa_notaCreDeb_MasivaDet
AS
SELECT dbo.fa_notaCreDeb_MasivaDet.IdEmpresa, dbo.fa_notaCreDeb_MasivaDet.IdNCMasivo, dbo.fa_notaCreDeb_MasivaDet.Secuencia, dbo.fa_notaCreDeb_MasivaDet.IdAlumno, dbo.fa_notaCreDeb_MasivaDet.Subtotal, 
                  dbo.fa_notaCreDeb_MasivaDet.IVA, dbo.fa_notaCreDeb_MasivaDet.vt_por_iva, dbo.fa_notaCreDeb_MasivaDet.IdCod_Impuesto_Iva, dbo.fa_notaCreDeb_MasivaDet.Total, dbo.fa_notaCreDeb_MasivaDet.ObservacionDet, 
                  dbo.fa_notaCreDeb_MasivaDet.IdSucursal, dbo.fa_notaCreDeb_MasivaDet.IdBodega, dbo.fa_notaCreDeb_MasivaDet.IdNota, dbo.fa_notaCreDeb.IdCliente, dbo.aca_Alumno.Codigo, dbo.tb_persona.pe_nombreCompleto, 
                  dbo.tb_persona.pe_cedulaRuc
FROM     dbo.fa_notaCreDeb_MasivaDet INNER JOIN
                  dbo.fa_notaCreDeb ON dbo.fa_notaCreDeb_MasivaDet.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.fa_notaCreDeb_MasivaDet.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                  dbo.fa_notaCreDeb_MasivaDet.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.fa_notaCreDeb_MasivaDet.IdNota = dbo.fa_notaCreDeb.IdNota INNER JOIN
                  dbo.aca_Alumno ON dbo.fa_notaCreDeb_MasivaDet.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.fa_notaCreDeb_MasivaDet.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_MasivaDet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' Output = 720
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_MasivaDet';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
         Begin Table = "fa_notaCreDeb_MasivaDet"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 255
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_notaCreDeb"
            Begin Extent = 
               Top = 5
               Left = 552
               Bottom = 288
               Right = 796
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "aca_Alumno"
            Begin Extent = 
               Top = 43
               Left = 1020
               Bottom = 206
               Right = 1265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 42
               Left = 1334
               Bottom = 302
               Right = 1608
            End
            DisplayFlags = 280
            TopColumn = 1
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
        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_MasivaDet';



