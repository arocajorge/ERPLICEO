CREATE VIEW web.vwcp_nota_DebCre
AS
SELECT        dbo.cp_nota_DebCre.IdEmpresa, dbo.cp_nota_DebCre.IdCbteCble_Nota, dbo.cp_nota_DebCre.IdTipoCbte_Nota, dbo.cp_nota_DebCre.DebCre, dbo.cp_nota_DebCre.IdTipoNota, dbo.cp_nota_DebCre.IdProveedor, 
                         dbo.cp_nota_DebCre.IdSucursal, dbo.cp_nota_DebCre.cn_fecha, dbo.cp_nota_DebCre.Fecha_contable, dbo.cp_nota_DebCre.cn_Fecha_vcto, dbo.cp_nota_DebCre.cn_serie1, dbo.cp_nota_DebCre.cn_serie2, 
                         dbo.cp_nota_DebCre.cn_Nota, dbo.cp_nota_DebCre.cn_observacion, dbo.cp_nota_DebCre.cn_subtotal_iva, dbo.cp_nota_DebCre.cn_subtotal_siniva, dbo.cp_nota_DebCre.cn_baseImponible, dbo.cp_nota_DebCre.cn_Por_iva, 
                         dbo.cp_nota_DebCre.cn_valoriva, dbo.cp_nota_DebCre.cn_Ice_base, dbo.cp_nota_DebCre.cn_Ice_por, dbo.cp_nota_DebCre.cn_Ice_valor, dbo.cp_nota_DebCre.cn_Serv_por, dbo.cp_nota_DebCre.cn_Serv_valor, 
                         dbo.cp_nota_DebCre.cn_BaseSeguro, dbo.cp_nota_DebCre.cn_total, dbo.cp_nota_DebCre.cn_vaCoa, dbo.cp_nota_DebCre.cn_Autorizacion, 
                         dbo.cp_nota_DebCre.cn_num_doc_modificado, dbo.cp_nota_DebCre.IdCod_ICE, dbo.cp_nota_DebCre.IdTipoServicio, dbo.cp_nota_DebCre.IdIden_credito,  dbo.cp_nota_DebCre.Estado, 
						 dbo.cp_nota_DebCre.cn_tipoLocacion, dbo.cp_nota_DebCre.cod_nota, 
                         dbo.cp_nota_DebCre.fecha_autorizacion, dbo.cp_nota_DebCre.PagoSujetoRetencion, dbo.cp_nota_DebCre.ConvenioTributacion, dbo.cp_nota_DebCre.PaisPago, dbo.cp_nota_DebCre.PagoLocExt, 
                         dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, 
                         dbo.tb_persona.pe_razonSocial
FROM            dbo.cp_nota_DebCre INNER JOIN
                         dbo.cp_proveedor ON dbo.cp_nota_DebCre.IdEmpresa = dbo.cp_proveedor.IdEmpresa AND dbo.cp_nota_DebCre.IdProveedor = dbo.cp_proveedor.IdProveedor INNER JOIN
                         dbo.tb_persona ON dbo.cp_proveedor.IdPersona = dbo.tb_persona.IdPersona

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[82] 4[5] 2[5] 3) )"
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
         Begin Table = "cp_nota_DebCre"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 311
               Right = 265
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_proveedor"
            Begin Extent = 
               Top = 0
               Left = 428
               Bottom = 284
               Right = 660
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 13
               Left = 736
               Bottom = 336
               Right = 968
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
      Begin ColumnWidths = 10
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
', @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_nota_DebCre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'web', @level1type = N'VIEW', @level1name = N'vwcp_nota_DebCre';

