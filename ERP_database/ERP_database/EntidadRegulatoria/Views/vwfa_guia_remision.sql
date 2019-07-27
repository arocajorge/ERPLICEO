
CREATE view EntidadRegulatoria.vwfa_guia_remision as

SELECT        dbo.fa_guia_remision.IdEmpresa, dbo.fa_guia_remision.IdSucursal, dbo.fa_guia_remision.IdBodega, dbo.fa_guia_remision.IdGuiaRemision, dbo.fa_guia_remision.CodGuiaRemision, dbo.fa_guia_remision.CodDocumentoTipo, 
                         dbo.fa_guia_remision.Serie1, dbo.fa_guia_remision.Serie2, dbo.fa_guia_remision.NumGuia_Preimpresa, dbo.fa_guia_remision.NUAutorizacion, dbo.fa_guia_remision.Fecha_Autorizacion, dbo.fa_guia_remision.gi_fecha, 
                         dbo.fa_guia_remision.gi_plazo, dbo.fa_guia_remision.gi_fech_venc, dbo.fa_guia_remision.gi_Observacion, dbo.fa_guia_remision.gi_FechaFinTraslado, dbo.fa_guia_remision.gi_FechaInicioTraslado, 
                         dbo.fa_guia_remision.placa, '' ruta, dbo.fa_guia_remision.Direccion_Origen, dbo.fa_guia_remision.Direccion_Destino, dbo.tb_transportista.Cedula, dbo.tb_transportista.Nombre, 
                         dbo.fa_cliente_contactos.Nombres, dbo.fa_cliente_contactos.Telefono, dbo.fa_cliente_contactos.Celular, dbo.fa_cliente_contactos.Correo, dbo.fa_cliente_contactos.Direccion, dbo.tb_persona.pe_cedulaRuc, 
                         dbo.tb_persona.pe_nombreCompleto, dbo.tb_persona.IdTipoDocumento, dbo.tb_persona.pe_Naturaleza, dbo.tb_empresa.em_nombre, dbo.tb_empresa.RazonSocial, dbo.tb_empresa.NombreComercial, dbo.tb_empresa.em_ruc, 
                         dbo.tb_empresa.em_telefonos, dbo.tb_empresa.ContribuyenteEspecial,  'SI'ObligadoAllevarConta, dbo.tb_empresa.em_Email, dbo.tb_empresa.em_direccion, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, 
                         dbo.fa_factura.vt_NumFactura, dbo.fa_factura.vt_fecha
FROM            dbo.tb_transportista INNER JOIN
                         dbo.fa_guia_remision ON dbo.tb_transportista.IdEmpresa = dbo.fa_guia_remision.IdEmpresa AND dbo.tb_transportista.IdTransportista = dbo.fa_guia_remision.IdTransportista INNER JOIN
                         dbo.fa_cliente_contactos ON dbo.fa_guia_remision.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa AND dbo.fa_guia_remision.IdCliente = dbo.fa_cliente_contactos.IdCliente AND 
                         dbo.fa_cliente_contactos.IdContacto = 1 INNER JOIN
                         dbo.tb_persona INNER JOIN
                         dbo.fa_cliente ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona ON dbo.fa_cliente_contactos.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_cliente_contactos.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_empresa ON dbo.fa_cliente.IdEmpresa = dbo.tb_empresa.IdEmpresa AND dbo.fa_guia_remision.Estado = 'A' AND dbo.fa_guia_remision.aprobada_enviar_sri = 1 AND NOT EXISTS
                             (SELECT        ID_REGISTRO, FECHA_CARGA, ESTADO
                               FROM            EntidadRegulatoria.fa_elec_registros_generados
                               WHERE        (ID_REGISTRO = SUBSTRING(dbo.tb_empresa.em_nombre, 0, 4) + '-' + 'GUI' + '-' + dbo.fa_guia_remision.Serie1 + '-' + dbo.fa_guia_remision.Serie2 + '-' + dbo.fa_guia_remision.NumGuia_Preimpresa)) INNER JOIN
                         dbo.fa_factura_x_fa_guia_remision ON dbo.fa_guia_remision.IdEmpresa = dbo.fa_factura_x_fa_guia_remision.gi_IdEmpresa AND dbo.fa_guia_remision.IdSucursal = dbo.fa_factura_x_fa_guia_remision.gi_IdSucursal AND 
                         dbo.fa_guia_remision.IdBodega = dbo.fa_factura_x_fa_guia_remision.gi_IdBodega AND dbo.fa_guia_remision.IdGuiaRemision = dbo.fa_factura_x_fa_guia_remision.gi_IdGuiaRemision INNER JOIN
                         dbo.fa_factura ON dbo.fa_factura_x_fa_guia_remision.fa_IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_factura_x_fa_guia_remision.fa_IdSucursal = dbo.fa_factura.IdSucursal AND 
                         dbo.fa_factura_x_fa_guia_remision.fa_IdBodega = dbo.fa_factura.IdBodega AND dbo.fa_factura_x_fa_guia_remision.fa_IdCbteVta = dbo.fa_factura.IdCbteVta
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'ght = 535
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura"
            Begin Extent = 
               Top = 4
               Left = 582
               Bottom = 428
               Right = 781
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
      Begin ColumnWidths = 45
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
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
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
', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[28] 4[5] 2[65] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[77] 2[4] 3) )"
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
         Configuration = "(H (1[75] 4) )"
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
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tb_transportista"
            Begin Extent = 
               Top = 37
               Left = 0
               Bottom = 299
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_guia_remision"
            Begin Extent = 
               Top = 30
               Left = 68
               Bottom = 374
               Right = 304
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente_contactos"
            Begin Extent = 
               Top = 0
               Left = 972
               Bottom = 302
               Right = 1142
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_persona"
            Begin Extent = 
               Top = 6
               Left = 1024
               Bottom = 319
               Right = 1256
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_cliente"
            Begin Extent = 
               Top = 229
               Left = 814
               Bottom = 519
               Right = 1030
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_empresa"
            Begin Extent = 
               Top = 0
               Left = 854
               Bottom = 454
               Right = 1073
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa_factura_x_fa_guia_remision"
            Begin Extent = 
               Top = 30
               Left = 348
               Bottom = 160
               Ri', @level0type = N'SCHEMA', @level0name = N'EntidadRegulatoria', @level1type = N'VIEW', @level1name = N'vwfa_guia_remision';







