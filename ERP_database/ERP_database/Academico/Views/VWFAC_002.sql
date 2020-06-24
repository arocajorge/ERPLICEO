CREATE VIEW Academico.VWFAC_002
AS
SELECT d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCbteVta, d.Secuencia, d.IdProducto, pro.pr_descripcion, d.vt_cantidad, d.vt_Precio, d.vt_cantidad * d.vt_Precio AS SubtotalSinDscto, d.vt_cantidad * d.vt_DescUnitario AS DescuentoTotal, 
                  d.vt_Subtotal AS SubtotalConDscto, d.vt_iva, d.vt_total, d.vt_por_iva, CASE WHEN d .vt_por_iva > 0 THEN vt_cantidad * vt_Precio ELSE 0 END AS SubtotalIVA, 
                  CASE WHEN d .vt_por_iva = 0 THEN vt_cantidad * vt_Precio ELSE 0 END AS SubtotalSinIVA, c.vt_fecha, c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura AS vt_NumFactura, per.pe_nombreCompleto AS cli_Nombre, 
                  per.pe_cedulaRuc AS cli_cedulaRuc, con.Direccion AS cli_direccion, con.Telefono AS cli_Telefonos, con.Correo AS cli_correo, su.Su_Descripcion, su.Su_Telefonos, su.Su_Direccion, cat.Nombre AS FormaDePago, c.IdCatalogo_FormaPago, 
                  c.vt_autorizacion, c.Fecha_Autorizacion, c.vt_Observacion, dbo.fa_factura_resumen.SubtotalIVASinDscto, dbo.fa_factura_resumen.SubtotalSinIVASinDscto, dbo.fa_factura_resumen.SubtotalSinDscto AS T_SubtotalSinDscto, 
                  dbo.fa_factura_resumen.Descuento, dbo.fa_factura_resumen.SubtotalIVAConDscto, dbo.fa_factura_resumen.SubtotalSinIVAConDscto, dbo.fa_factura_resumen.SubtotalConDscto AS T_SubtotalConDscto, 
                  dbo.fa_factura_resumen.ValorIVA, dbo.fa_factura_resumen.Total, dbo.fa_factura_resumen.ValorEfectivo, dbo.fa_factura_resumen.Cambio, e.NomSede, e.NomNivel, e.NomJornada, e.NomCurso, e.NomParalelo, e.NomPlantilla, 
                  p.pe_nombreCompleto AS NomAlumno, c.IdAlumno, c.IdCliente, f.Codigo
FROM     dbo.fa_cliente_contactos AS con INNER JOIN
                  dbo.fa_factura AS c ON con.IdEmpresa = c.IdEmpresa AND con.IdCliente = c.IdCliente INNER JOIN
                  dbo.fa_factura_det AS d ON c.IdEmpresa = d.IdEmpresa AND c.IdSucursal = d.IdSucursal AND c.IdBodega = d.IdBodega AND c.IdCbteVta = d.IdCbteVta INNER JOIN
                  dbo.in_Producto AS pro ON d.IdEmpresa = pro.IdEmpresa AND d.IdProducto = pro.IdProducto INNER JOIN
                  dbo.fa_cliente AS cli ON con.IdEmpresa = cli.IdEmpresa AND con.IdCliente = cli.IdCliente INNER JOIN
                  dbo.tb_persona AS per ON cli.IdPersona = per.IdPersona INNER JOIN
                  dbo.tb_sucursal AS su ON c.IdEmpresa = su.IdEmpresa AND c.IdSucursal = su.IdSucursal LEFT OUTER JOIN
                  dbo.fa_factura_resumen ON c.IdEmpresa = dbo.fa_factura_resumen.IdEmpresa AND c.IdSucursal = dbo.fa_factura_resumen.IdSucursal AND c.IdBodega = dbo.fa_factura_resumen.IdBodega AND 
                  c.IdCbteVta = dbo.fa_factura_resumen.IdCbteVta LEFT OUTER JOIN
                  dbo.fa_catalogo AS cat ON c.IdCatalogo_FormaPago = cat.IdCatalogo LEFT OUTER JOIN
                      (SELECT A1.IdEmpresa, A1.IdAnio, A1.IdAlumno, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, JornadaCurso.NomCurso, CursoParalelo.NomParalelo, CursoParalelo.CodigoParalelo, A3.NomPlantilla
                       FROM      dbo.aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel ON NivelJornada.IdEmpresa = SedeNivel.IdEmpresa AND NivelJornada.IdAnio = SedeNivel.IdAnio AND NivelJornada.IdSede = SedeNivel.IdSede AND 
                                         NivelJornada.IdNivel = SedeNivel.IdNivel INNER JOIN
                                         dbo.aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND 
                                         NivelJornada.IdNivel = JornadaCurso.IdNivel AND NivelJornada.IdJornada = JornadaCurso.IdJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Curso_Paralelo AS CursoParalelo ON JornadaCurso.IdEmpresa = CursoParalelo.IdEmpresa AND JornadaCurso.IdAnio = CursoParalelo.IdAnio AND JornadaCurso.IdSede = CursoParalelo.IdSede AND 
                                         JornadaCurso.IdNivel = CursoParalelo.IdNivel AND JornadaCurso.IdJornada = CursoParalelo.IdJornada AND JornadaCurso.IdCurso = CursoParalelo.IdCurso RIGHT OUTER JOIN
                                         dbo.aca_Matricula AS A1 INNER JOIN
                                         dbo.aca_AnioLectivo AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdAnio = A2.IdAnio ON CursoParalelo.IdParalelo = A1.IdParalelo AND CursoParalelo.IdEmpresa = A1.IdEmpresa AND CursoParalelo.IdAnio = A1.IdAnio AND 
                                         CursoParalelo.IdSede = A1.IdSede AND CursoParalelo.IdNivel = A1.IdNivel AND CursoParalelo.IdJornada = A1.IdJornada AND CursoParalelo.IdCurso = A1.IdCurso LEFT OUTER JOIN
                                         dbo.aca_Plantilla AS A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdAnio = A3.IdAnio AND A1.IdPlantilla = A3.IdPlantilla
                       WHERE   (A2.EnCurso = 1)) AS e ON c.IdEmpresa = e.IdEmpresa AND c.IdAlumno = e.IdAlumno INNER JOIN
                  dbo.aca_Alumno AS f ON f.IdEmpresa = c.IdEmpresa AND f.IdAlumno = c.IdAlumno INNER JOIN
                  dbo.tb_persona AS p ON f.IdPersona = p.IdPersona
WHERE  (c.IdAlumno IS NOT NULL)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWFAC_002';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         End
         Begin Table = "fa_factura_resumen"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 306
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cat"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "e"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "f"
            Begin Extent = 
               Top = 1687
               Left = 48
               Bottom = 1850
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 1855
               Left = 48
               Bottom = 2018
               Right = 322
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
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWFAC_002';






GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[49] 4[12] 2[20] 3) )"
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
         Top = -1587
         Left = 0
      End
      Begin Tables = 
         Begin Table = "con"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pro"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 323
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cli"
            Begin Extent = 
               Top = 591
               Left = 372
               Bottom = 754
               Right = 628
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "per"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 322
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "su"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 320
            End
            DisplayFlags = 280
            TopColumn = 0', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWFAC_002';



