﻿CREATE VIEW [dbo].[vwcxc_cartera_x_cobrar]
AS
SELECT cabfac.IdEmpresa, cabfac.IdSucursal, cabfac.IdBodega, cabfac.vt_tipoDoc, cabfac.vt_tipoDoc + '-' + CAST(CAST(cabfac.vt_NumFactura AS int) AS varchar(20)) AS vt_NunDocumento, cabfac.vt_Observacion AS Referencia, 
                  cabfac.IdCbteVta AS IdComprobante, cabfac.CodCbteVta AS CodComprobante, Sucu.Su_Descripcion, cabfac.IdCliente, cabfac.IdAlumno, cabfac.vt_fecha, CAST(detfac.Total AS FLOAT) AS vt_total, 
                  ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) AS Saldo, ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0) AS TotalxCobrado, Bod.bo_Descripcion AS Bodega, 
                  CAST(detfac.SubtotalConDscto AS FLOAT) AS vt_Subtotal, CAST(detfac.ValorIVA AS FLOAT) AS vt_iva, cabfac.vt_fech_venc, ROUND(0, 2) AS dc_ValorRetFu, ROUND( 
                  0, 2) AS dc_ValorRetIva, Cli.Codigo AS CodCliente, tb_persona.pe_nombreCompleto AS NomCliente, tb_empresa.em_nombre, cabfac.Estado, CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND 
                  aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() AS DATE) THEN detfac.ValorProntoPago ELSE detfac.Total END AS ValorProntoPago, aca_AnioLectivo_Periodo.FechaProntoPago AS FechaProntoPago,
				  detfac.IdAnio, detfac.IdPlantilla, cabfac.IdPuntoVta
FROM     fa_factura_resumen AS detfac INNER JOIN
                  fa_factura AS cabfac ON detfac.IdBodega = cabfac.IdBodega AND detfac.IdSucursal = cabfac.IdSucursal AND detfac.IdEmpresa = cabfac.IdEmpresa AND detfac.IdCbteVta = cabfac.IdCbteVta INNER JOIN
                  tb_sucursal AS Sucu ON cabfac.IdEmpresa = Sucu.IdEmpresa AND cabfac.IdSucursal = Sucu.IdSucursal INNER JOIN
                  tb_bodega AS Bod ON cabfac.IdEmpresa = Bod.IdEmpresa AND cabfac.IdSucursal = Bod.IdSucursal AND cabfac.IdBodega = Bod.IdBodega AND Sucu.IdEmpresa = Bod.IdEmpresa AND Sucu.IdSucursal = Bod.IdSucursal INNER JOIN
                  fa_cliente AS Cli ON cabfac.IdEmpresa = Cli.IdEmpresa AND cabfac.IdCliente = Cli.IdCliente INNER JOIN
                  tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN
                  tb_empresa ON cabfac.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN
                  aca_AnioLectivo_Rubro ON detfac.IdEmpresa = aca_AnioLectivo_Rubro.IdEmpresa AND detfac.IdAnio = aca_AnioLectivo_Rubro.IdAnio AND detfac.IdRubro = aca_AnioLectivo_Rubro.IdRubro LEFT OUTER JOIN
                  aca_AnioLectivo_Periodo ON detfac.IdPeriodo = aca_AnioLectivo_Periodo.IdPeriodo AND detfac.IdEmpresa = aca_AnioLectivo_Periodo.IdEmpresa LEFT OUTER JOIN
                  vwcxc_total_cobros_x_Docu ON cabfac.IdEmpresa = vwcxc_total_cobros_x_Docu.IdEmpresa AND cabfac.IdSucursal = vwcxc_total_cobros_x_Docu.IdSucursal AND cabfac.IdBodega = vwcxc_total_cobros_x_Docu.IdBodega_Cbte AND 
                  cabfac.vt_tipoDoc = vwcxc_total_cobros_x_Docu.dc_TipoDocumento AND cabfac.IdCbteVta = vwcxc_total_cobros_x_Docu.IdCbte_vta_nota
WHERE  (cabfac.Estado = 'A')
UNION
SELECT A.IdEmpresa, A.IdSucursal, A.IdBodega, 'NTDB' AS CreDeb, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/D#' + CAST(A.IdNota AS varchar(20)) ELSE 'N/D#' + A.Serie1 + '-' + A.Serie2 + '' + A.NumNota_Impresa END AS Documento, 
                  A.sc_observacion, A.IdNota, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, A.no_fecha, ROUND(SUM(B.sc_total), 2) AS sc_total, ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) AS Saldo, 
                  ISNULL(SUM(CB.dc_ValorPago), 0) AS totalCobrado, Bo.bo_Descripcion, ROUND(SUM(B.sc_subtotal), 2) AS sc_subtotal, ROUND(SUM(B.sc_iva), 2) AS sc_iva, A.no_fecha_venc, 
                  cast(0 as float) AS RtFT, cast(0 as float) AS RtIVA, Cli.Codigo AS CodCliente, tb_persona.pe_nombreCompleto, 
                  tb_empresa.em_nombre, A.Estado, ROUND(SUM(B.sc_total), 2), NULL,
				  null IdAnio, null IdPlantilla, A.IdPuntoVta
FROM     fa_notaCreDeb AS A INNER JOIN
                  fa_notaCreDeb_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN
                  tb_bodega AS Bo ON A.IdEmpresa = Bo.IdEmpresa AND A.IdSucursal = Bo.IdSucursal AND A.IdBodega = Bo.IdBodega INNER JOIN
                  tb_sucursal AS su ON Bo.IdEmpresa = su.IdEmpresa AND Bo.IdSucursal = su.IdSucursal INNER JOIN
                  fa_cliente AS Cli ON A.IdEmpresa = Cli.IdEmpresa AND A.IdCliente = Cli.IdCliente INNER JOIN
                  tb_persona ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN
                  tb_empresa ON A.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN
                  vwcxc_total_cobros_x_Docu AS CB ON A.IdEmpresa = CB.IdEmpresa AND A.IdSucursal = CB.IdSucursal AND A.IdBodega = CB.IdBodega_Cbte AND A.IdNota = CB.IdCbte_vta_nota AND 
                  A.CodDocumentoTipo = CB.dc_TipoDocumento
WHERE  A.Estado = 'A' AND NOT EXISTS
                      (SELECT *
                       FROM      fa_notaCreDeb_x_fa_factura_NotaDeb Cruce
                       WHERE   Cruce.IdEmpresa_nt = A.IdEmpresa AND Cruce.IdSucursal_nt = A.IdSucursal AND Cruce.IdBodega_nt = A.IdBodega AND Cruce.IdNota_nt = A.IdNota AND Cruce.Valor_Aplicado <> 0)
GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.no_fecha, A.CreDeb, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.sc_observacion, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, Bo.bo_Descripcion, A.no_fecha_venc, 
                  Cli.Codigo, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre, A.Estado,A.IdPuntoVta
HAVING (A.CreDeb = 'D')
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[5] 4[4] 2[64] 3) )"
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
         Top = -768
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
      Begin ColumnWidths = 25
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
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3120
         Alias = 1548
         Table = 3216
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cartera_x_cobrar';




GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwcxc_cartera_x_cobrar';

