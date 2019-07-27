CREATE VIEW [dbo].[vwfa_Nota_Credito] AS
SELECT 
 notaCreDeb_agrupadas.IdEmpresa							,notaCreDeb_agrupadas.IdSucursal			,notaCreDeb_agrupadas.IdBodega				,notaCreDeb_agrupadas.IdNota				  
,notaCreDeb_agrupadas.CreDeb							,notaCreDeb_agrupadas.IdCliente				
,notaCreDeb_agrupadas.no_fecha							,notaCreDeb_agrupadas.IdTipoNota			,notaCreDeb_agrupadas.sc_observacion	
,notaCreDeb_agrupadas.IdUsuarioUltMod					,notaCreDeb_agrupadas.Fecha_UltMod          ,notaCreDeb_agrupadas.IdUsuarioUltAnu		,notaCreDeb_agrupadas.Fecha_UltAnu
,notaCreDeb_agrupadas.MotiAnula				,notaCreDeb_agrupadas.bo_Descripcion
,notaCreDeb_agrupadas.Su_Descripcion								,notaCreDeb_agrupadas.pe_apellido			,notaCreDeb_agrupadas.pe_nombre
,notaCreDeb_agrupadas.CodNota							,notaCreDeb_agrupadas.no_fecha_venc			,notaCreDeb_agrupadas.Estado
,notaCreDeb_agrupadas.IdUsuario
,notaCreDeb_agrupadas.Serie1							,notaCreDeb_agrupadas.Serie2				,notaCreDeb_agrupadas.NumNota_Impresa	
,notaCreDeb_agrupadas.NaturalezaNota									,notaCreDeb_agrupadas.IdTipoDocumento		,notaCreDeb_agrupadas.pe_cedulaRuc
								


,notaCreDeb_agrupadas.IdCtaCble_TipoNota	
,sum(notaCreDeb_agrupadas.sc_subtotal) sc_subtotal		
,sum(notaCreDeb_agrupadas.sc_total) sc_total
,sum(notaCreDeb_agrupadas.sc_iva) sc_iva
,sum(notaCreDeb_agrupadas.vt_Subtotal0) vt_subtotal0	
,sum(notaCreDeb_agrupadas.vt_SubtotalIva) vt_subtotalIva
,notaCreDeb_agrupadas.IdPuntoVta
FROM 
(
SELECT        dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CreDeb, dbo.fa_notaCreDeb.IdCliente,
                         dbo.fa_notaCreDeb.no_fecha, dbo.fa_notaCreDeb.IdTipoNota, dbo.fa_notaCreDeb.sc_observacion,  dbo.fa_notaCreDeb.IdUsuarioUltMod, dbo.fa_notaCreDeb.Fecha_UltMod, 
                         dbo.fa_notaCreDeb.IdUsuarioUltAnu, dbo.fa_notaCreDeb.Fecha_UltAnu,  dbo.fa_notaCreDeb.MotiAnula, dbo.tb_bodega.bo_Descripcion, 
                         dbo.tb_sucursal.Su_Descripcion,  dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.fa_notaCreDeb.CodNota, 
                         dbo.fa_notaCreDeb.no_fecha_venc, dbo.fa_notaCreDeb.Estado,  dbo.fa_notaCreDeb.IdUsuario, dbo.fa_notaCreDeb.Serie1, 
                         dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.NumNota_Impresa,  dbo.fa_notaCreDeb.NaturalezaNota, dbo.tb_persona.IdTipoDocumento, 
                         dbo.tb_persona.pe_cedulaRuc, 
						  
						  dbo.fa_notaCreDeb.IdCtaCble_TipoNota,
						 SUM(dbo.fa_notaCreDeb_det.sc_subtotal) 
                         AS sc_subtotal, SUM(dbo.fa_notaCreDeb_det.sc_iva) AS sc_iva,
						 SUM(dbo.fa_notaCreDeb_det.sc_total) AS sc_total,
						  CASE 
								 WHEN dbo.fa_notaCreDeb_det.IdCod_Impuesto_Iva= 'IVA0' THEN sum(dbo.fa_notaCreDeb_det.sc_subtotal)
								 else 0
								END AS vt_Subtotal0, 
							CASE 
								 WHEN dbo.fa_notaCreDeb_det.IdCod_Impuesto_Iva<> 'IVA0' THEN sum(dbo.fa_notaCreDeb_det.sc_subtotal)
								ELSE 	0
								END AS vt_SubtotalIva 
					  ,dbo.fa_notaCreDeb.IdPuntoVta
FROM            dbo.fa_notaCreDeb INNER JOIN
                         dbo.fa_notaCreDeb_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_det.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_det.IdSucursal AND 
                         dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_det.IdBodega AND dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_det.IdNota INNER JOIN
                         dbo.fa_cliente ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.tb_bodega ON dbo.fa_notaCreDeb.IdEmpresa = dbo.tb_bodega.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.tb_bodega.IdSucursal AND 
                         dbo.fa_notaCreDeb.IdBodega = dbo.tb_bodega.IdBodega INNER JOIN
                         dbo.tb_sucursal ON dbo.tb_bodega.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.tb_bodega.IdSucursal = dbo.tb_sucursal.IdSucursal 
GROUP BY dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CreDeb, dbo.fa_notaCreDeb.IdCliente, 
                         dbo.fa_notaCreDeb.no_fecha, dbo.fa_notaCreDeb.IdTipoNota, dbo.fa_notaCreDeb.sc_observacion,  dbo.fa_notaCreDeb.IdUsuarioUltMod, dbo.fa_notaCreDeb.Fecha_UltMod, 
                         dbo.fa_notaCreDeb.IdUsuarioUltAnu, dbo.fa_notaCreDeb.Fecha_UltAnu, dbo.fa_notaCreDeb.MotiAnula, dbo.tb_bodega.bo_Descripcion, 
                         dbo.tb_sucursal.Su_Descripcion,  dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.fa_notaCreDeb.CodNota, 
                         dbo.fa_notaCreDeb.no_fecha_venc, dbo.fa_notaCreDeb.Estado,  dbo.fa_notaCreDeb.IdUsuario, dbo.fa_notaCreDeb.Serie1, 
                         dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.NumNota_Impresa,  dbo.fa_notaCreDeb.NaturalezaNota, dbo.tb_persona.IdTipoDocumento, 
                         dbo.tb_persona.pe_cedulaRuc, dbo.fa_notaCreDeb.IdCtaCble_TipoNota
						 ,dbo.fa_notaCreDeb_det.IdCod_Impuesto_Iva,dbo.fa_notaCreDeb.IdPuntoVta
)as  notaCreDeb_agrupadas
group by  
notaCreDeb_agrupadas.IdEmpresa							,notaCreDeb_agrupadas.IdSucursal			,notaCreDeb_agrupadas.IdBodega				,notaCreDeb_agrupadas.IdNota				  
,notaCreDeb_agrupadas.CreDeb							,notaCreDeb_agrupadas.IdCliente				
,notaCreDeb_agrupadas.no_fecha							,notaCreDeb_agrupadas.IdTipoNota			,notaCreDeb_agrupadas.sc_observacion	
,notaCreDeb_agrupadas.IdUsuarioUltMod					,notaCreDeb_agrupadas.Fecha_UltMod          ,notaCreDeb_agrupadas.IdUsuarioUltAnu		,notaCreDeb_agrupadas.Fecha_UltAnu
									,notaCreDeb_agrupadas.MotiAnula				,notaCreDeb_agrupadas.bo_Descripcion
,notaCreDeb_agrupadas.Su_Descripcion					,notaCreDeb_agrupadas.pe_apellido			,notaCreDeb_agrupadas.pe_nombre
,notaCreDeb_agrupadas.CodNota			,notaCreDeb_agrupadas.no_fecha_venc			,notaCreDeb_agrupadas.Estado
,notaCreDeb_agrupadas.IdUsuario
,notaCreDeb_agrupadas.Serie1							,notaCreDeb_agrupadas.Serie2				,notaCreDeb_agrupadas.NumNota_Impresa	
,notaCreDeb_agrupadas.NaturalezaNota								,notaCreDeb_agrupadas.IdTipoDocumento		,notaCreDeb_agrupadas.pe_cedulaRuc
			
			,notaCreDeb_agrupadas.IdCtaCble_TipoNota			
				,notaCreDeb_agrupadas.IdPuntoVta
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[5] 4[4] 2[4] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[14] 3) )"
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
               Top = 38
               Left = 515
               Bottom = 399
               Right = 693
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_notaCreDeb_det"
            Begin Extent = 
               Top = 0
               Left = 65
               Bottom = 420
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "fa_Vendedor"
            Begin Extent = 
               Top = 32
               Left = 788
               Bottom = 151
               Right = 956
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 135
               Left = 386
               Bottom = 254
               Right = 596
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 154
               Left = 454
               Bottom = 273
               Right = 646
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "tb_bodega"
            Begin Extent = 
               Top = 259
               Left = 448
               Bottom = 378
               Right = 646
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_sucursal"
            Begin Extent = 
               Top = 450
               Left = 307
               Bottom = 569
               Right = 521
            End
 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_Nota_Credito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'           DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 25
               Left = 931
               Bottom = 392
               Right = 1133
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
      Begin ColumnWidths = 55
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
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 5010
         Alias = 3450
         Table = 3975
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_Nota_Credito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_Nota_Credito';

