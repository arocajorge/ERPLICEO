CREATE view [dbo].[vwcp_conciliacion_Caja_det_x_ValeCaja]
as

SELECT        dbo.cp_conciliacion_Caja.IdEmpresa, dbo.cp_conciliacion_Caja.IdConciliacion_Caja, dbo.cp_conciliacion_Caja.Fecha, dbo.cp_conciliacion_Caja.IdCaja, 
                         dbo.cp_conciliacion_Caja.IdEstadoCierre, dbo.cp_conciliacion_Caja.Observacion, dbo.cp_conciliacion_Caja.IdEmpresa_op, 
                         dbo.cp_conciliacion_Caja.IdOrdenPago_op, dbo.cp_conciliacion_Caja.IdCtaCble, dbo.cp_conciliacion_Caja_det_x_ValeCaja.Secuencia, 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdEmpresa_movcaja, dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdCbteCble_movcaja, 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdTipocbte_movcaja, dbo.caj_Caja_Movimiento.cm_Signo, vwtb_persona_beneficiario.pe_nombreCompleto cm_beneficiario, 
                         dbo.caj_Caja_Movimiento.IdTipoMovi, dbo.caj_Caja_Movimiento.cm_observacion, dbo.caj_Caja_Movimiento.cm_fecha, dbo.caj_Caja_Movimiento.Estado, 
                         dbo.caj_Caja_Movimiento_det.Secuencia AS Secuencia_DetcajMovi, dbo.caj_Caja_Movimiento.cm_valor AS cr_Valor, 
                         dbo.caj_Caja_Movimiento_Tipo.tm_descripcion AS nom_TipoMovi, dbo.caj_Caja.ca_Descripcion AS nom_Caja, dbo.cp_catalogo.Nombre AS nom_EstadoCierre, 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdCtaCble AS IdCtaCble_ValeCaja, dbo.caj_Caja_Movimiento.IdTipo_Persona, dbo.caj_Caja_Movimiento.IdEntidad, 
                         dbo.caj_Caja_Movimiento.IdPersona, dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdCentroCosto, dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdCentroCosto_sub_centro_costo, 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdPunto_cargo, 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdPunto_cargo_grupo, dbo.vwtb_persona_beneficiario.IdBeneficiario
                           
FROM            dbo.cp_catalogo_tipo INNER JOIN
                         dbo.cp_catalogo ON dbo.cp_catalogo_tipo.IdCatalogo_tipo = dbo.cp_catalogo.IdCatalogo_tipo INNER JOIN
                         dbo.cp_conciliacion_Caja INNER JOIN
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja ON dbo.cp_conciliacion_Caja.IdEmpresa = dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdEmpresa AND 
                         dbo.cp_conciliacion_Caja.IdConciliacion_Caja = dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdConciliacion_Caja INNER JOIN
                         dbo.caj_Caja_Movimiento ON dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdEmpresa_movcaja = dbo.caj_Caja_Movimiento.IdEmpresa AND 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdCbteCble_movcaja = dbo.caj_Caja_Movimiento.IdCbteCble AND 
                         dbo.cp_conciliacion_Caja_det_x_ValeCaja.IdTipocbte_movcaja = dbo.caj_Caja_Movimiento.IdTipocbte INNER JOIN
                         dbo.caj_Caja_Movimiento_det ON dbo.caj_Caja_Movimiento.IdEmpresa = dbo.caj_Caja_Movimiento_det.IdEmpresa AND 
                         dbo.caj_Caja_Movimiento.IdCbteCble = dbo.caj_Caja_Movimiento_det.IdCbteCble AND 
                         dbo.caj_Caja_Movimiento.IdTipocbte = dbo.caj_Caja_Movimiento_det.IdTipocbte INNER JOIN
                         dbo.caj_Caja_Movimiento_Tipo ON dbo.caj_Caja_Movimiento.IdTipoMovi = dbo.caj_Caja_Movimiento_Tipo.IdTipoMovi INNER JOIN
                         dbo.caj_Caja ON dbo.cp_conciliacion_Caja.IdEmpresa = dbo.caj_Caja.IdEmpresa AND dbo.cp_conciliacion_Caja.IdCaja = dbo.caj_Caja.IdCaja ON 
                         dbo.cp_catalogo.IdCatalogo = dbo.cp_conciliacion_Caja.IdEstadoCierre INNER JOIN
                         dbo.vwtb_persona_beneficiario ON dbo.caj_Caja_Movimiento.IdTipo_Persona = dbo.vwtb_persona_beneficiario.IdTipo_Persona AND 
                         dbo.caj_Caja_Movimiento.IdEntidad = dbo.vwtb_persona_beneficiario.IdEntidad AND 
                         dbo.caj_Caja_Movimiento.IdPersona = dbo.vwtb_persona_beneficiario.IdPersona AND 
                         dbo.caj_Caja_Movimiento.IdEmpresa = dbo.vwtb_persona_beneficiario.IdEmpresa
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[78] 4[5] 2[5] 3) )"
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
         Begin Table = "cp_catalogo_tipo"
            Begin Extent = 
               Top = 151
               Left = 72
               Bottom = 293
               Right = 281
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_catalogo"
            Begin Extent = 
               Top = 310
               Left = 114
               Bottom = 439
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_conciliacion_Caja"
            Begin Extent = 
               Top = 6
               Left = 0
               Bottom = 262
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp_conciliacion_Caja_det_x_ValeCaja"
            Begin Extent = 
               Top = 104
               Left = 811
               Bottom = 334
               Right = 1060
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "caj_Caja_Movimiento"
            Begin Extent = 
               Top = 64
               Left = 555
               Bottom = 347
               Right = 764
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento_det"
            Begin Extent = 
               Top = 34
               Left = 948
               Bottom = 451
               Right = 1157
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja_Movimiento_Tipo"
            Begin Extent = 
               Top = 0
               Left = 234
        ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_conciliacion_Caja_det_x_ValeCaja';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'       Bottom = 129
               Right = 443
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "caj_Caja"
            Begin Extent = 
               Top = 1
               Left = 470
               Bottom = 89
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vwtb_persona_beneficiario"
            Begin Extent = 
               Top = 146
               Left = 294
               Bottom = 318
               Right = 526
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
      Begin ColumnWidths = 36
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 3120
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
         Width = 1965
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
         Column = 3630
         Alias = 2475
         Table = 4305
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_conciliacion_Caja_det_x_ValeCaja';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcp_conciliacion_Caja_det_x_ValeCaja';

