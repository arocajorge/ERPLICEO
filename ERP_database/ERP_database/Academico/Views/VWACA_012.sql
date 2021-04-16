CREATE VIEW Academico.VWACA_012
AS
SELECT a.IdEmpresa, a.IdMatricula, b.IdPeriodo, b.IdRubro, c.IdSucursal, c.IdBodega, c.IdCbteVta, a.IdAlumno, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, 
                  c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura AS vt_NumFactura, c.vt_fecha, c.vt_Observacion, MAX(dbo.cxc_cobro.cr_fecha) AS cr_fecha, SUM(dbo.cxc_cobro_det.dc_ValorPago) AS dc_ValorPago, sn.NomSede, sn.OrdenNivel, 
                  sn.NomNivel, nj.OrdenJornada, nj.NomJornada, jc.OrdenCurso, jc.NomCurso, cp.OrdenParalelo, cp.NomParalelo, d.Codigo AS CodigoAlumno, e.pe_cedulaRuc, e.pe_nombreCompleto, e.pe_fechaNacimiento, h.Nacionalidad, i.NomRubro, 
                  j.Descripcion AS DescripcionAnio, CASE WHEN dbo.cxc_cobro_det.IdCbte_vta_nota IS NULL THEN 'NO' ELSE 'SI' END AS EstadoPago
FROM     dbo.tb_persona AS e WITH (nolock) INNER JOIN
                  dbo.aca_Alumno AS d WITH (nolock) INNER JOIN
                  dbo.aca_Matricula AS a WITH (nolock) INNER JOIN
                  dbo.aca_Matricula_Rubro AS b WITH (nolock) ON a.IdEmpresa = b.IdEmpresa AND a.IdMatricula = b.IdMatricula ON d.IdEmpresa = a.IdEmpresa AND d.IdAlumno = a.IdAlumno ON e.IdPersona = d.IdPersona LEFT OUTER JOIN
                  dbo.cxc_cobro_det WITH (nolock) INNER JOIN
                  dbo.cxc_cobro WITH (nolock) ON dbo.cxc_cobro_det.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.cxc_cobro.IdSucursal AND dbo.cxc_cobro_det.IdCobro = dbo.cxc_cobro.IdCobro RIGHT OUTER JOIN
                  dbo.fa_factura AS c WITH (nolock) ON dbo.cxc_cobro_det.IdEmpresa = c.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = c.IdSucursal AND dbo.cxc_cobro_det.IdBodega_Cbte = c.IdBodega AND 
                  dbo.cxc_cobro_det.IdCbte_vta_nota = c.IdCbteVta AND dbo.cxc_cobro_det.dc_TipoDocumento = c.vt_tipoDoc ON b.IdEmpresa = c.IdEmpresa AND b.IdSucursal = c.IdSucursal AND b.IdBodega = c.IdBodega AND 
                  b.IdCbteVta = c.IdCbteVta LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  a.IdEmpresa = cp.IdEmpresa AND a.IdAnio = cp.IdAnio AND a.IdSede = cp.IdSede AND a.IdNivel = cp.IdNivel AND a.IdJornada = cp.IdJornada AND a.IdCurso = cp.IdCurso AND a.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.tb_pais AS h ON d.IdPais = h.IdPais LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Rubro AS i WITH (nolock) ON b.IdEmpresa = i.IdEmpresa AND b.IdAnio = i.IdAnio AND b.IdRubro = i.IdRubro LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS j WITH (nolock) ON a.IdEmpresa = j.IdEmpresa AND a.IdAnio = j.IdAnio
WHERE  (ISNULL(dbo.cxc_cobro.cr_estado, N'A') = 'A')
GROUP BY a.IdEmpresa, a.IdMatricula, b.IdPeriodo, b.IdRubro, c.IdSucursal, c.IdBodega, c.IdCbteVta, a.IdAlumno, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura, c.vt_fecha, 
                  c.vt_Observacion, sn.NomSede, sn.OrdenNivel, sn.NomNivel, nj.OrdenJornada, nj.NomJornada, jc.OrdenCurso, jc.NomCurso, cp.OrdenParalelo, cp.NomParalelo, d.Codigo, e.pe_cedulaRuc, e.pe_nombreCompleto, e.pe_fechaNacimiento, 
                  h.Nacionalidad, i.NomRubro, j.Descripcion, dbo.cxc_cobro_det.IdCbte_vta_nota
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_012';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'n = 0
         End
         Begin Table = "sn"
            Begin Extent = 
               Top = 1183
               Left = 48
               Bottom = 1346
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "nj"
            Begin Extent = 
               Top = 1351
               Left = 48
               Bottom = 1514
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "jc"
            Begin Extent = 
               Top = 1519
               Left = 48
               Bottom = 1682
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 1687
               Left = 48
               Bottom = 1850
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "h"
            Begin Extent = 
               Top = 1855
               Left = 48
               Bottom = 2018
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "i"
            Begin Extent = 
               Top = 2023
               Left = 48
               Bottom = 2186
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "j"
            Begin Extent = 
               Top = 2191
               Left = 48
               Bottom = 2354
               Right = 347
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
      Begin ColumnWidths = 37
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
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
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_012';


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
         Begin Table = "e"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 338
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 315
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "a"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 309
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 511
               Left = 48
               Bottom = 674
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro_det"
            Begin Extent = 
               Top = 679
               Left = 48
               Bottom = 842
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cxc_cobro"
            Begin Extent = 
               Top = 847
               Left = 48
               Bottom = 1010
               Right = 312
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 1015
               Left = 48
               Bottom = 1178
               Right = 315
            End
            DisplayFlags = 280
            TopColum', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWACA_012';

