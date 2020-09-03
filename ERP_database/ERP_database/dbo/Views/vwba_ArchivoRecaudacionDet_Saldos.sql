CREATE VIEW dbo.vwba_ArchivoRecaudacionDet_Saldos
AS
SELECT IdEmpresa, IdAlumno, IdMatricula, CodigoAlumno, pe_nombreCompleto, SUM(Saldo) AS Saldo, SUM(SaldoProntoPago) AS SaldoProntoPago, MAX(FechaProntoPago) AS FechaProntoPago
FROM     (SELECT a.IdEmpresa, a.IdAlumno, d.IdMatricula, c.Codigo AS CodigoAlumno, f.pe_nombreCompleto, dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) AS Saldo, 
                                    CASE WHEN dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) > 0 THEN CASE WHEN ap.FechaProntoPago >= CAST(getdate() AS date) THEN dbo.BankersRounding(B.ValorProntoPago - ISNULL(g.dc_ValorPago, 0), 2) 
                                    ELSE dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) END ELSE 0 END AS SaldoProntoPago, CASE WHEN dbo.BankersRounding(b.Total - b.ValorProntoPago, 2) 
                                    <> 0 THEN ap.FechaProntoPago ELSE a.vt_fecha END AS FechaProntoPago
                  FROM      (SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS dc_ValorPago
                                     FROM      dbo.cxc_cobro_det AS a
                                     WHERE   (dc_TipoDocumento = 'FACT') AND (estado = 'A')
                                     GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS g RIGHT OUTER JOIN
                                    dbo.fa_factura AS a INNER JOIN
                                    dbo.fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN
                                    dbo.aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN
                                    dbo.aca_Matricula AS d ON d.IdEmpresa = c.IdEmpresa AND d.IdAlumno = c.IdAlumno INNER JOIN
                                    dbo.aca_AnioLectivo AS e ON d.IdEmpresa = e.IdEmpresa AND d.IdAnio = e.IdAnio INNER JOIN
                                    dbo.tb_persona AS f ON c.IdPersona = f.IdPersona LEFT OUTER JOIN
                                    dbo.aca_AnioLectivo_Periodo AS ap ON b.IdPeriodo = ap.IdPeriodo AND b.IdEmpresa = ap.IdEmpresa ON g.IdEmpresa = a.IdEmpresa AND g.IdSucursal = a.IdSucursal AND g.IdBodega_Cbte = a.IdBodega AND 
                                    g.IdCbte_vta_nota = a.IdCbteVta AND g.dc_TipoDocumento = a.vt_tipoDoc
                  WHERE   (e.EnCurso = 1) AND (dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) > 0) AND (a.Estado = 'A')
                  UNION ALL
                  SELECT A.IdEmpresa, A.IdAlumno, G.IdMatricula, C.Codigo, D.pe_nombreCompleto, dbo.BankersRounding(B.Total - ISNULL(E.dc_ValorPago, 0), 2) AS Expr1, dbo.BankersRounding(B.Total - ISNULL(E.dc_ValorPago, 0), 2) AS Expr2, 
                                    A.no_fecha
                  FROM     dbo.fa_notaCreDeb AS A INNER JOIN
                                    dbo.fa_notaCreDeb_resumen AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN
                                    dbo.aca_Alumno AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdAlumno = C.IdAlumno INNER JOIN
                                    dbo.tb_persona AS D ON C.IdPersona = D.IdPersona LEFT OUTER JOIN
                                        (SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS dc_ValorPago
                                         FROM      dbo.cxc_cobro_det AS a
                                         WHERE   (dc_TipoDocumento = 'NTDB') AND (estado = 'A')
                                         GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS E ON E.IdEmpresa = A.IdEmpresa AND A.IdSucursal = E.IdSucursal AND A.IdBodega = E.IdBodega_Cbte AND 
                                    A.IdNota = E.IdCbte_vta_nota AND A.CodDocumentoTipo = E.dc_TipoDocumento INNER JOIN
                                    dbo.aca_Matricula AS G ON A.IdEmpresa = G.IdEmpresa AND A.IdAlumno = G.IdAlumno INNER JOIN
                                    dbo.aca_AnioLectivo AS H ON G.IdEmpresa = H.IdEmpresa AND G.IdAnio = H.IdAnio
                  WHERE  (H.EnCurso = 1) AND (dbo.BankersRounding(B.Total - ISNULL(E.dc_ValorPago, 0), 2) > 0) AND (A.Estado = 'A') AND (A.CreDeb = 'D')) AS A_1
GROUP BY IdEmpresa, IdAlumno, IdMatricula, CodigoAlumno, pe_nombreCompleto
HAVING (dbo.BankersRounding(SUM(SaldoProntoPago), 2) > 0) AND (NOT EXISTS
                      (SELECT IdEmpresa
                       FROM      dbo.aca_AlumnoRetiro AS f
                       WHERE   (IdEmpresa = A_1.IdEmpresa) AND (IdMatricula = A_1.IdMatricula) AND (Estado = 1)))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ArchivoRecaudacionDet_Saldos';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[33] 4[16] 2[32] 3) )"
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
         Begin Table = "A_1"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 308
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwba_ArchivoRecaudacionDet_Saldos';

