CREATE view [dbo].[vwfa_notaCreDeb]
as 
SELECT d.IdEmpresa, d.IdSucursal, d.IdBodega, CASE CreDeb WHEN 'C' THEN 'NTCR' WHEN 'D' THEN 'NTDB' END AS Tipo, d.IdNota, CodNota, CreDeb, Serie1, Serie2, NumNota_Impresa, NumAutorizacion, d.IdCliente, no_fecha, 
                  no_fecha_venc,  d.IdTipoNota, sc_observacion, d.Estado, IdPuntoVta, isnull(det.sc_subtotal,0) AS sc_subtotal,isnull(det.sc_iva,0) AS sc_iva, isnull(det.sc_total,0) as sc_total, isnull(cobro.valor_pago,0) valor_aplicado,
				  round(isnull(det.sc_total,0) - isnull(cobro.valor_pago,0),2) as saldo, ltrim(rtrim(per.pe_nombreCompleto)) as nom_cliente, su.Su_Descripcion, bo.bo_Descripcion bo_descripcion, d.NaturalezaNota, tn.IdCtaCble
FROM     dbo.fa_notaCreDeb d 

left join (
select IdEmpresa,IdSucursal,IdBodega_Cbte,IdCbte_vta_nota,dc_TipoDocumento,sum(dc_ValorPago) as valor_pago from cxc_cobro_det
group by IdEmpresa,IdSucursal,IdBodega_Cbte,IdCbte_vta_nota,dc_TipoDocumento
) as cobro on cobro.IdEmpresa = d.IdEmpresa
and cobro.IdSucursal = d.IdSucursal
and cobro.IdBodega_Cbte = d.IdBodega
and cobro.IdCbte_vta_nota = d.IdNota
and cobro.dc_TipoDocumento = CASE CreDeb WHEN 'C' THEN 'NTCR' WHEN 'D' THEN 'NTDB' END 

left join(
select IdEmpresa,IdSucursal,IdBodega,IdNota,sum(fa_notaCreDeb_det.sc_iva) as sc_iva, sum(fa_notaCreDeb_det.sc_subtotal) as sc_subtotal, sum(fa_notaCreDeb_det.sc_total) sc_total
from fa_notaCreDeb_det
group by IdEmpresa,IdSucursal,IdBodega,IdNota
) as det on det.IdEmpresa = d.IdEmpresa
and det.IdSucursal = d.IdSucursal
and det.IdBodega = d.IdBodega
and det.IdNota = d.IdNota

inner join fa_cliente cli on d.IdEmpresa = cli.IdEmpresa AND cli.IdCliente = d.IdCliente
inner join tb_persona per on cli.IdPersona = per.IdPersona
left join tb_bodega bo on d.IdEmpresa = bo.IdEmpresa and d.IdSucursal = bo.IdSucursal and d.IdBodega = bo.IdBodega 
inner join tb_sucursal su on bo.IdEmpresa = su.IdEmpresa and bo.IdSucursal = su.IdSucursal
left join fa_TipoNota_x_Empresa_x_Sucursal tn on d.IdEmpresa = tn.IdEmpresa
and d.IdSucursal = tn.IdSucursal and d.IdTipoNota = tn.IdTipoNota
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
         Begin Table = "fa_notaCreDeb"
            Begin Extent = 
               Top = 18
               Left = 296
               Bottom = 206
               Right = 474
            End
            DisplayFlags = 280
            TopColumn = 29
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb';

