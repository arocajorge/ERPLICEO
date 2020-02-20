CREATE VIEW [dbo].[vwcxc_ConciliacionNotaCreditoDet]
AS
SELECT dbo.cxc_ConciliacionNotaCreditoDet.IdEmpresa, dbo.cxc_ConciliacionNotaCreditoDet.IdConciliacion, dbo.cxc_ConciliacionNotaCreditoDet.Secuencia, dbo.cxc_ConciliacionNotaCreditoDet.IdSucursal, 
                  dbo.cxc_ConciliacionNotaCreditoDet.IdBodega, dbo.cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota, dbo.cxc_ConciliacionNotaCreditoDet.vt_TipoDoc, dbo.cxc_ConciliacionNotaCreditoDet.Valor, 
                  CASE WHEN cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = 'FACT' THEN fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura ELSE CASE WHEN fa_notaCreDeb.NaturalezaNota = 'SRI' THEN fa_notaCreDeb.Serie1
                   + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa ELSE fa_notaCreDeb.CodNota END END AS ReferenciaDet, dbo.cxc_ConciliacionNotaCreditoDet.secuencia_nt, isnull(fa_factura.vt_fecha,fa_notaCreDeb.no_fecha) vt_fecha,
				   fa_factura_resumen.IdAnio, fa_factura_resumen.IdPlantilla, isnull(fa_factura.IdPuntoVta, fa_notacredeb.IdPuntoVta)IdPuntoVta, isnull(fa_factura.IdAlumno, fa_notaCreDeb.IdAlumno)IdAlumno, isnull(fa_factura.IdCliente, fa_notaCreDeb.IdCliente) IdCliente, 
				   cxc_ConciliacionNotaCreditoDet.ValorProntoPago, isnull(fa_factura.vt_Observacion, fa_notaCreDeb.sc_observacion) vt_Observacion
FROM     dbo.cxc_ConciliacionNotaCreditoDet LEFT OUTER JOIN
                  dbo.fa_notaCreDeb ON dbo.cxc_ConciliacionNotaCreditoDet.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.cxc_ConciliacionNotaCreditoDet.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND 
                  dbo.cxc_ConciliacionNotaCreditoDet.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota = dbo.fa_notaCreDeb.IdNota AND 
                  dbo.cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = dbo.fa_notaCreDeb.CodDocumentoTipo LEFT OUTER JOIN
                  dbo.fa_factura ON dbo.cxc_ConciliacionNotaCreditoDet.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_ConciliacionNotaCreditoDet.IdSucursal = dbo.fa_factura.IdSucursal AND 
                  dbo.cxc_ConciliacionNotaCreditoDet.IdBodega = dbo.fa_factura.IdBodega AND dbo.cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota = dbo.fa_factura.IdCbteVta AND 
                  dbo.cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = dbo.fa_factura.vt_tipoDoc left join 
				  fa_factura_resumen on fa_factura_resumen.IdEmpresa = fa_factura.IdEmpresa and fa_factura_resumen.IdSucursal = fa_factura.IdSucursal and fa_factura_resumen.IdBodega = fa_factura.IdBodega and fa_factura_resumen.IdCbteVta = fa_factura.IdCbteVta left join
				  fa_notaCreDeb_resumen on fa_notaCreDeb_resumen.IdEmpresa = fa_notaCreDeb.IdEmpresa and fa_notaCreDeb_resumen.IdSucursal = fa_notaCreDeb.IdSucursal and fa_notaCreDeb_resumen.IdBodega = fa_notaCreDeb.IdBodega and fa_notaCreDeb_resumen.IdNota = fa_notaCreDeb.IdNota
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_ConciliacionNotaCreditoDet';


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
         Top = -120
         Left = 0
      End
      Begin Tables = 
         Begin Table = "cxc_ConciliacionNotaCreditoDet"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 118
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_notaCreDeb"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 299
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_ConciliacionNotaCreditoDet';

