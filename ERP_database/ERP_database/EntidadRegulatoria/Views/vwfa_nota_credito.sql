CREATE VIEW EntidadRegulatoria.vwfa_nota_credito
AS
SELECT        nota_credito.IdEmpresa, nota_credito.IdSucursal, nota_credito.IdBodega, nota_credito.IdNota, nota_credito.dev_IdEmpresa, nota_credito.dev_IdDev_Inven, nota_credito.CodNota, nota_credito.CreDeb, 
                         nota_credito.CodDocumentoTipo, nota_credito.Serie1, nota_credito.Serie2, nota_credito.NumNota_Impresa, nota_credito.NumAutorizacion, nota_credito.Fecha_Autorizacion, nota_credito.Nombres, nota_credito.Telefono, 
                         nota_credito.Celular, nota_credito.Correo, nota_credito.pe_Naturaleza, nota_credito.pe_cedulaRuc, nota_credito.IdTipoDocumento, nota_credito.pe_nombreCompleto, nota_credito.pe_razonSocial, nota_credito.no_fecha, 
                         nota_credito.sc_observacion, nota_credito.em_direccion, nota_credito.em_ruc, nota_credito.RazonSocial, nota_credito.NombreComercial, nota_credito.Telefono AS Expr1, nota_credito.ContribuyenteEspecial, 
                         'SI' AS ObligadoAllevarConta, nota_credito.vt_serie1, nota_credito.vt_serie2, nota_credito.vt_NumFactura, nota_credito.vt_fecha, nota_credito_detalle.importeTotal, nota_credito_detalle.impuesto, nota_credito.em_telefonos, 
                         nota_credito_detalle.total_sin_impuesto, nota_credito_detalle.totalDescuento
FROM            (SELECT        dbo.fa_notaCreDeb.IdEmpresa, dbo.fa_notaCreDeb.IdSucursal, dbo.fa_notaCreDeb.IdBodega, dbo.fa_notaCreDeb.IdNota, dbo.fa_notaCreDeb.dev_IdEmpresa, dbo.fa_notaCreDeb.dev_IdDev_Inven, 
                                                    dbo.fa_notaCreDeb.CodNota, dbo.fa_notaCreDeb.CreDeb, dbo.fa_notaCreDeb.CodDocumentoTipo, dbo.fa_notaCreDeb.Serie1, dbo.fa_notaCreDeb.Serie2, dbo.fa_notaCreDeb.NumNota_Impresa, 
                                                    dbo.fa_notaCreDeb.NumAutorizacion, dbo.fa_notaCreDeb.Fecha_Autorizacion, dbo.fa_cliente_contactos.Nombres, dbo.fa_cliente_contactos.Telefono, dbo.fa_cliente_contactos.Celular, 
                                                    dbo.fa_cliente_contactos.Correo, dbo.tb_persona.pe_Naturaleza, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.pe_razonSocial, 
                                                    dbo.fa_notaCreDeb.no_fecha, dbo.fa_notaCreDeb.sc_observacion, dbo.tb_empresa.em_nombre, dbo.tb_empresa.RazonSocial, dbo.tb_empresa.NombreComercial, dbo.tb_empresa.ContribuyenteEspecial, 
                                                    'SI' AS ObligadoAllevarConta, dbo.tb_empresa.em_ruc, dbo.tb_empresa.em_direccion, dbo.tb_empresa.em_Email, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, 
                                                    dbo.fa_factura.vt_fecha, dbo.tb_empresa.em_telefonos, dbo.tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico
                          FROM            dbo.fa_notaCreDeb INNER JOIN
                                                    dbo.fa_cliente_contactos ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa AND dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente_contactos.IdCliente INNER JOIN
                                                    dbo.fa_cliente ON dbo.fa_cliente_contactos.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_cliente_contactos.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                                                    dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                                                    dbo.tb_empresa ON dbo.fa_cliente.IdEmpresa = dbo.tb_empresa.IdEmpresa INNER JOIN
                                                    dbo.fa_notaCreDeb_x_fa_factura_NotaDeb ON dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_nt AND 
                                                    dbo.fa_notaCreDeb.IdSucursal = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_nt AND dbo.fa_notaCreDeb.IdBodega = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_nt AND 
                                                    dbo.fa_notaCreDeb.IdNota = dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdNota_nt INNER JOIN
                                                    dbo.fa_factura ON dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdEmpresa_fac_nd_doc_mod = dbo.fa_factura.IdEmpresa AND 
                                                    dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdSucursal_fac_nd_doc_mod = dbo.fa_factura.IdSucursal AND dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdBodega_fac_nd_doc_mod = dbo.fa_factura.IdBodega AND 
                                                    dbo.fa_notaCreDeb_x_fa_factura_NotaDeb.IdCbteVta_fac_nd_doc_mod = dbo.fa_factura.IdCbteVta INNER JOIN
                                                    dbo.tb_sis_Documento_Tipo_Talonario ON dbo.fa_notaCreDeb.IdEmpresa = dbo.tb_sis_Documento_Tipo_Talonario.IdEmpresa AND 
                                                    dbo.fa_notaCreDeb.CodDocumentoTipo = dbo.tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo AND dbo.fa_notaCreDeb.Serie2 = dbo.tb_sis_Documento_Tipo_Talonario.PuntoEmision AND 
                                                    dbo.fa_notaCreDeb.Serie1 = dbo.tb_sis_Documento_Tipo_Talonario.Establecimiento AND dbo.fa_notaCreDeb.NumNota_Impresa = dbo.tb_sis_Documento_Tipo_Talonario.NumDocumento AND 
                                                    dbo.fa_factura.IdEmpresa = dbo.tb_sis_Documento_Tipo_Talonario.IdEmpresa AND dbo.fa_factura.vt_tipoDoc = dbo.tb_sis_Documento_Tipo_Talonario.CodDocumentoTipo AND 
                                                    dbo.fa_factura.vt_serie2 = dbo.tb_sis_Documento_Tipo_Talonario.PuntoEmision AND dbo.fa_factura.vt_serie1 = dbo.tb_sis_Documento_Tipo_Talonario.Establecimiento AND 
                                                    dbo.fa_factura.vt_NumFactura = dbo.tb_sis_Documento_Tipo_Talonario.NumDocumento
                          WHERE        (dbo.fa_notaCreDeb.NaturalezaNota = 'SRI') AND (dbo.fa_notaCreDeb.Estado = 'A') AND (dbo.fa_notaCreDeb.aprobada_enviar_sri = 1) AND (dbo.fa_notaCreDeb.CreDeb = 'C') AND 
                                                    (dbo.tb_sis_Documento_Tipo_Talonario.es_Documento_Electronico = 1) AND (dbo.fa_notaCreDeb.NumAutorizacion IS NULL)) AS nota_credito INNER JOIN
                             (SELECT        IdEmpresa, IdSucursal, IdBodega, IdNota, CAST(SUM(sc_subtotal) AS numeric(10, 2)) AS Base_imponible, CAST(SUM(sc_iva) AS numeric(10, 2)) AS impuesto, CAST(SUM(sc_Precio) AS numeric(10, 2)) 
                                                         AS totalDescuento, CAST(SUM(sc_subtotal) AS numeric(10, 2)) AS total_sin_impuesto, CAST(SUM(sc_total) AS numeric(10, 2)) AS importeTotal
                               FROM            dbo.fa_notaCreDeb_det
                               GROUP BY IdEmpresa, IdSucursal, IdBodega, IdNota) AS nota_credito_detalle ON nota_credito.IdEmpresa = nota_credito_detalle.IdEmpresa AND nota_credito.IdSucursal = nota_credito_detalle.IdSucursal AND 
                         nota_credito.IdBodega = nota_credito_detalle.IdBodega AND nota_credito.IdNota = nota_credito_detalle.IdNota
GO



GO







GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_nota_credito';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[11] 4[5] 2[67] 3) )"
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
         Begin Table = "nota_credito_detalle"
            Begin Extent = 
               Top = 6
               Left = 283
               Bottom = 136
               Right = 471
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nota_credito"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 270
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
', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_nota_credito';

