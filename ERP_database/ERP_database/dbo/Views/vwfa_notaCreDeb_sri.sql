CREATE VIEW [dbo].[vwfa_notaCreDeb_sri]
AS
SELECT        dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CreDeb, 
                         dbo.fa_notaCreDeb.CodNota, dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.IdCliente, dbo.fa_notaCreDeb.no_fecha, 
                         dbo.fa_notaCreDeb.Estado, dbo.fa_notaCreDeb.NaturalezaNota, dbo.fa_notaCreDeb.NumAutorizacion, dbo.tb_empresa.RazonSocial, 
                         dbo.tb_empresa.NombreComercial, dbo.tb_empresa.ContribuyenteEspecial,  'SI'ObligadoAllevarConta, dbo.tb_empresa.em_ruc, 
                         dbo.tb_empresa.em_direccion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_sucursal.Su_Direccion, dbo.tb_persona.pe_razonSocial AS cl_RazonSocial, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_correo, 
                         dbo.fa_notaCreDeb.NumNota_Impresa, dbo.fa_factura.vt_serie1 + '-' + dbo.fa_factura.vt_serie2 + '-' + dbo.fa_factura.vt_NumFactura AS num_Factura, 
                         dbo.fa_factura.vt_fecha AS fecha_fact, dbo.fa_factura.vt_Observacion AS obser_fact, dbo.fa_notaCreDeb.sc_observacion AS obser_Nota, 
                         dbo.fa_TipoNota.No_Descripcion AS nc_Motivo, fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc AS tipo_doc_aplicado
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb ON dbo.fa_factura.IdEmpresa = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod AND 
                         dbo.fa_factura.IdSucursal = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod AND 
                         dbo.fa_factura.IdBodega = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod AND 
                         dbo.fa_factura.IdCbteVta = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod AND 
                         dbo.fa_factura.vt_tipoDoc = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc INNER JOIN
                         dbo.fa_notaCreDeb INNER JOIN
                         dbo.tb_empresa ON dbo.fa_notaCreDeb.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                         dbo.tb_sucursal ON dbo.fa_notaCreDeb.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.fa_cliente ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona ON 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt = dbo.fa_notaCreDeb.IdEmpresa AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt = dbo.fa_notaCreDeb.IdSucursal AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt = dbo.fa_notaCreDeb.IdBodega AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt = dbo.fa_notaCreDeb.IdNota LEFT OUTER JOIN
                         dbo.fa_TipoNota ON dbo.fa_notaCreDeb.IdTipoNota = dbo.fa_TipoNota.IdTipoNota
WHERE        (dbo.fa_notaCreDeb.NaturalezaNota = 'SRI')
UNION
SELECT        dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.CreDeb, 
                         dbo.fa_notaCreDeb.CodNota, dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.IdCliente, dbo.fa_notaCreDeb.no_fecha, 
                         dbo.fa_notaCreDeb.Estado, dbo.fa_notaCreDeb.NaturalezaNota, dbo.fa_notaCreDeb.NumAutorizacion, dbo.tb_empresa.RazonSocial, 
                         dbo.tb_empresa.NombreComercial, dbo.tb_empresa.ContribuyenteEspecial,  'SI'ObligadoAllevarConta, dbo.tb_empresa.em_ruc, 
                         dbo.tb_empresa.em_direccion, dbo.tb_sucursal.Su_Descripcion, dbo.tb_sucursal.Su_Direccion, dbo.tb_persona.pe_razonSocial AS cl_RazonSocial, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_correo, 
                         dbo.fa_notaCreDeb.NumNota_Impresa, CASE WHEN fa_notaCreDeb_Doc_Modif.NumNota_Impresa IS NULL 
                         THEN fa_notaCreDeb_Doc_Modif.CodNota ELSE fa_notaCreDeb_Doc_Modif.Serie1 + '-' + fa_notaCreDeb_Doc_Modif.Serie2 + '-' + fa_notaCreDeb_Doc_Modif.NumNota_Impresa
                          END AS num_Factura, fa_notaCreDeb_Doc_Modif.no_fecha AS Expr1, fa_notaCreDeb_Doc_Modif.sc_observacion, 
                         dbo.fa_notaCreDeb.sc_observacion AS obser_Nota, dbo.fa_TipoNota.No_Descripcion AS nc_Motivo, fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc AS tipo_doc_aplicado
FROM            dbo.fa_notaCreDeb_x_fa_factura_NotaDeb INNER JOIN
                         dbo.fa_notaCreDeb INNER JOIN
                         dbo.tb_empresa ON dbo.fa_notaCreDeb.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                         dbo.tb_sucursal ON dbo.fa_notaCreDeb.IdEmpresa = dbo.tb_sucursal.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.tb_sucursal.IdSucursal INNER JOIN
                         dbo.fa_cliente ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona ON 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt = dbo.fa_notaCreDeb.IdEmpresa AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt = dbo.fa_notaCreDeb.IdSucursal AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt = dbo.fa_notaCreDeb.IdBodega AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt = dbo.fa_notaCreDeb.IdNota INNER JOIN
                         dbo.fa_notaCreDeb AS fa_notaCreDeb_Doc_Modif ON 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = fa_notaCreDeb_Doc_Modif.IdEmpresa AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = fa_notaCreDeb_Doc_Modif.IdSucursal AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = fa_notaCreDeb_Doc_Modif.IdBodega AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = fa_notaCreDeb_Doc_Modif.IdNota AND 
                         dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.vt_tipoDoc = fa_notaCreDeb_Doc_Modif.CodDocumentoTipo LEFT OUTER JOIN
                         dbo.fa_TipoNota ON dbo.fa_notaCreDeb.IdTipoNota = dbo.fa_TipoNota.IdTipoNota AND fa_notaCreDeb_Doc_Modif.IdTipoNota = dbo.fa_TipoNota.IdTipoNota
WHERE        (dbo.fa_notaCreDeb.NaturalezaNota = 'SRI')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[5] 4[19] 2[26] 3) )"
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
      Begin ColumnWidths = 33
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
         Width = 1905
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
         Width = 1620
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 4110
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 2355
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1380
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_sri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwfa_notaCreDeb_sri';

