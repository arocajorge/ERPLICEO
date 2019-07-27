
CREATE VIEW [dbo].[vwcxc_cobro_det]
AS
SELECT        A.IdEmpresa, A.IdSucursal, A.IdCobro, A.IdCliente, B.IdBodega_Cbte, B.dc_TipoDocumento AS dc_TipoDocumento_Cobrado, B.IdCbte_vta_nota, B.dc_ValorPago, 
                         A.IdPersona, A.IdCobro_tipo, A.cr_TotalCobro, A.cr_fecha, A.cr_fechaCobro, A.cr_fechaDocu, A.cr_observacion, A.cr_Banco, A.cr_cuenta, A.cr_NumCheque, 
                         A.cr_Tarjeta, A.cr_NumDocumento, A.cr_estado, A.cr_recibo, A.Fecha_Transac, A.IdUsuario, A.IdUsuarioUltMod, A.Fecha_UltMod, A.IdUsuarioUltAnu, A.Fecha_UltAnu, 
                         A.nom_pc, A.nSucursal, A.nCliente, A.ip, A.cr_Codigo, A.IdVendedorCliente, 
                         F.vt_tipoDoc + ' ' + F.vt_serie1 + '-' + F.vt_serie2 + '-' + F.vt_NumFactura + '/' + cast(B.IdCbte_vta_nota AS varchar(50)) AS Documento_Cobrado
						 ,B.secuencial
FROM            dbo.vwcxc_cobro AS A INNER JOIN
                         dbo.cxc_cobro_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdCobro = B.IdCobro INNER JOIN
                         dbo.fa_factura AS F ON B.IdEmpresa = F.IdEmpresa AND B.IdSucursal = F.IdSucursal AND B.IdBodega_Cbte = F.IdBodega AND B.IdCbte_vta_nota = F.IdCbteVta AND 
                         B.dc_TipoDocumento = F.vt_tipoDoc
UNION
SELECT        A.IdEmpresa, A.IdSucursal, A.IdCobro, A.IdCliente, B.IdBodega_Cbte, B.dc_TipoDocumento, B.IdCbte_vta_nota, B.dc_ValorPago, A.IdPersona, A.IdCobro_tipo, 
                         A.cr_TotalCobro, A.cr_fecha, A.cr_fechaCobro, A.cr_fechaDocu, A.cr_observacion, A.cr_Banco, A.cr_cuenta, A.cr_NumCheque, A.cr_Tarjeta, A.cr_NumDocumento, 
                         A.cr_estado, A.cr_recibo, A.Fecha_Transac, A.IdUsuario, A.IdUsuarioUltMod, A.Fecha_UltMod, A.IdUsuarioUltAnu, A.Fecha_UltAnu, A.nom_pc, A.nSucursal, A.nCliente, 
                         A.ip, A.cr_Codigo, A.IdVendedorCliente, DocumentoCobrado = CASE WHEN C.NumNota_Impresa IS NULL THEN C.Tipo + ' # ' + cast(C.IdNota AS varchar(30)) 
                         ELSE C.Tipo + ' ' + C.Serie1 + '-' + C.Serie2 + '-' + C.NumNota_Impresa + '/' + cast(B.IdCbte_vta_nota AS varchar(20)) END
						 ,B.secuencial
FROM            dbo.vwcxc_cobro AS A INNER JOIN
                         dbo.cxc_cobro_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdCobro = B.IdCobro INNER JOIN
                         dbo.vwfa_notaCreDeb AS C ON B.IdEmpresa = C.IdEmpresa AND B.IdSucursal = C.IdSucursal AND B.IdBodega_Cbte = C.IdBodega AND 
                         B.dc_TipoDocumento = C.Tipo AND B.IdCbte_vta_nota = C.IdNota
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[19] 4[5] 2[58] 3) )"
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_det';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cobro_det';

