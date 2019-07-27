CREATE view vwtb_sis_Documento_Tipo_x_Empresa_Anulados 
as
select isnull(ROW_NUMBER() OVER(ORDER BY A.IdEmpresa),0) as IdRow, A.*  from (
SELECT        cp_retencion.IdEmpresa, cp_retencion.fecha, year(cp_retencion.fecha) AS anio, MONTH(cp_retencion.fecha) AS mes, '07' AS CodDocumentoTipo, cp_retencion.serie1,
                          cp_retencion.serie2, cp_retencion.NumRetencion, cp_retencion.NumRetencion Documento_ini, cp_retencion.NumRetencion Documento_fin, 
                         CASE WHEN tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico = 0 THEN tb_sis_Documento_Tipo_Talonario.NumAutorizacion ELSE cp_retencion.NAutorizacion
                          END AS NumAutorizacion, cp_retencion.Estado
FROM            tb_sis_Documento_Tipo_Talonario INNER JOIN
                         cp_retencion ON tb_sis_Documento_Tipo_Talonario.IdEmpresa = cp_retencion.IdEmpresa AND 
                         tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo = cp_retencion.CodDocumentoTipo AND 
                         tb_sis_Documento_Tipo_Talonario.PuntoEmision = cp_retencion.serie2 AND tb_sis_Documento_Tipo_Talonario.Establecimiento = cp_retencion.serie1 AND 
                         tb_sis_Documento_Tipo_Talonario.NumDocumento = cp_retencion.NumRetencion
WHERE        cp_retencion.Estado = 'I'
UNION
SELECT        fa_notaCreDeb.IdEmpresa, fa_notaCreDeb.no_fecha, year(fa_notaCreDeb.no_fecha) AS anio, MONTH(fa_notaCreDeb.no_fecha) AS mes, 
                         CASE WHEN fa_notaCreDeb.CreDeb = 'C' THEN '04' ELSE '05' END AS CodDocumentoTipo, fa_notaCreDeb.Serie1, fa_notaCreDeb.Serie2, 
                         fa_notaCreDeb.NumNota_Impresa, fa_notaCreDeb.NumNota_Impresa Documento_ini, fa_notaCreDeb.NumNota_Impresa Documento_fin, 
                         CASE WHEN tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico = 0 THEN tb_sis_Documento_Tipo_Talonario.NumAutorizacion ELSE fa_notaCreDeb.NumAutorizacion
                          END AS NumAutorizacion, fa_notaCreDeb.Estado
FROM            tb_sis_Documento_Tipo_Talonario INNER JOIN
                         fa_notaCreDeb ON tb_sis_Documento_Tipo_Talonario.IdEmpresa = fa_notaCreDeb.IdEmpresa AND 
                         tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo = fa_notaCreDeb.CodDocumentoTipo AND 
                         tb_sis_Documento_Tipo_Talonario.PuntoEmision = fa_notaCreDeb.Serie2 AND tb_sis_Documento_Tipo_Talonario.Establecimiento = fa_notaCreDeb.Serie1 AND 
                         tb_sis_Documento_Tipo_Talonario.NumDocumento = fa_notaCreDeb.NumNota_Impresa
WHERE        fa_notaCreDeb.NaturalezaNota = 'SRI' AND fa_notaCreDeb.Estado = 'I'
UNION
SELECT        fa_factura.IdEmpresa, fa_factura.vt_fecha, year(fa_factura.vt_fecha) AS anio, MONTH(fa_factura.vt_fecha) AS mes, '18' AS CodDocumentoTipo, fa_factura.vt_serie1, 
                         fa_factura.vt_serie2, fa_factura.vt_NumFactura, fa_factura.vt_NumFactura Documento_ini, fa_factura.vt_NumFactura Documento_fin, 
                         CASE WHEN tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico = 0 THEN tb_sis_Documento_Tipo_Talonario.NumAutorizacion ELSE fa_factura.vt_autorizacion
                          END AS NumAutorizacion, fa_factura.Estado
FROM            tb_sis_Documento_Tipo_Talonario INNER JOIN
                         fa_factura ON tb_sis_Documento_Tipo_Talonario.IdEmpresa = fa_factura.IdEmpresa AND 
                         tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo = fa_factura.vt_tipoDoc AND tb_sis_Documento_Tipo_Talonario.PuntoEmision = fa_factura.vt_serie2 AND 
                         tb_sis_Documento_Tipo_Talonario.Establecimiento = fa_factura.vt_serie1 AND 
                         tb_sis_Documento_Tipo_Talonario.NumDocumento = fa_factura.vt_NumFactura
WHERE        fa_factura.Estado = 'I') A
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[19] 2[28] 3) )"
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
         Begin Table = "tb_sis_Documento_Tipo_x_Empresa_Anulados"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 197
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "cp_TipoDocumento"
            Begin Extent = 
               Top = 6
               Left = 285
               Bottom = 135
               Right = 554
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_Catalogo"
            Begin Extent = 
               Top = 6
               Left = 592
               Bottom = 135
               Right = 801
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
      Begin ColumnWidths = 15
         Width = 284
         Width = 1500
         Width = 1500
         Width = 2940
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
      Begin ColumnWidths = 11
         Column = 5820
         Alias = 900
         Table = 2490
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_sis_Documento_Tipo_x_Empresa_Anulados';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwtb_sis_Documento_Tipo_x_Empresa_Anulados';

