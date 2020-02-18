CREATE VIEW [Academico].[VWCXC_003]
AS
SELECT B.IdEmpresa, B.IdSucursal, B.IdCobro, B.secuencial, A.IdUsuario, c.Nombre AS NombreUsuario, B.IdBodega_Cbte, B.IdCbte_vta_nota, B.dc_TipoDocumento, 
                  CASE WHEN b.IdCobro_tipo <> 'NTCR' THEN b.dc_ValorPago ELSE 0 END AS dc_ValorPago, CASE WHEN b.IdCobro_tipo = 'NTCR' THEN b.dc_ValorPago ELSE 0 END AS dc_ValorPagoNC, f.Codigo AS CodigAlumno, g.pe_nombreCompleto, 
                  CASE WHEN b.dc_TipoDocumento = 'FACT' THEN d .vt_NumFactura ELSE CASE WHEN e.NaturalezaNota = 'SRI' THEN e.NumNota_Impresa ELSE e.CodNota END END AS NumFactura, LEFT(B.IdCobro_tipo, 2) AS IdCobro_tipo, A.cr_fecha, 
                  A.IdTarjeta, ISNULL(h.NombreTarjeta, A.cr_Banco) AS NombreTarjeta, A.cr_Banco, YEAR(j.FechaDesde) AS AnioFactura, A.IdAlumno, CASE WHEN k.IdMotivo_tipo_cobro IN ('CHEQ', 'EFEC') 
                  THEN 'EFECTIVO' WHEN k.IdMotivo_tipo_cobro IN ('TARJ') THEN 'TARJETA DE CREDITO' WHEN k.IdMotivo_tipo_cobro IN ('NTCR') THEN 'NOTA DE CREDITO' END AS GrupoTipoCobro, k.tc_descripcion AS NombreTipoCobro, 
                  B.dc_ValorPago AS TotalPago
FROM     dbo.cxc_cobro AS A INNER JOIN
                  dbo.cxc_cobro_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdCobro = B.IdCobro LEFT OUTER JOIN
                  dbo.seg_usuario AS c ON A.IdUsuario = c.IdUsuario LEFT OUTER JOIN
                  dbo.fa_factura AS d ON B.IdEmpresa = d .IdEmpresa AND B.IdSucursal = d .IdSucursal AND B.IdBodega_Cbte = d .IdBodega AND B.IdCbte_vta_nota = d .IdCbteVta AND B.dc_TipoDocumento = d .vt_tipoDoc LEFT OUTER JOIN
                  dbo.fa_notaCreDeb AS e ON B.IdEmpresa = e.IdEmpresa AND B.IdSucursal = e.IdSucursal AND B.IdBodega_Cbte = e.IdBodega AND B.IdCbte_vta_nota = e.IdNota AND B.dc_TipoDocumento = e.CodDocumentoTipo INNER JOIN
                  dbo.aca_Alumno AS f ON A.IdEmpresa = f.IdEmpresa AND A.IdAlumno = f.IdAlumno INNER JOIN
                  dbo.tb_persona AS g ON f.IdPersona = g.IdPersona LEFT OUTER JOIN
                  dbo.tb_TarjetaCredito AS h ON A.IdEmpresa = h.IdEmpresa AND A.IdTarjeta = h.IdTarjeta LEFT OUTER JOIN
                  dbo.fa_factura_resumen AS i ON d .IdEmpresa = i.IdEmpresa AND d .IdSucursal = i.IdSucursal AND d .IdBodega = i.IdBodega AND d .IdCbteVta = i.IdCbteVta LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS j ON i.IdEmpresa = j.IdEmpresa AND i.IdAnio = j.IdAnio LEFT OUTER JOIN
                  dbo.cxc_cobro_tipo AS k ON B.IdCobro_tipo = k.IdCobro_tipo
WHERE (A.cr_estado = 'A')
UNION ALL
SELECT A.IdEmpresa, A.IdSucursal, A.IdCobro, 0, A.IdUsuario, C.Nombre, 0, 0, 'ANTI', A.cr_TotalCobro, 0, D .Codigo, E.pe_nombreCompleto, 'ANTI', LEFT(A.IdCobro_tipo, 2), A.CR_FECHA, A.IdTarjeta, ISNULL(G.NombreTarjeta, A.cr_Banco), 
                  A.cr_Banco, YEAR(A.cr_fecha), A.IdAlumno, CASE WHEN f.IdMotivo_tipo_cobro IN ('CHEQ', 'EFEC') THEN 'EFECTIVO' WHEN f.IdMotivo_tipo_cobro IN ('TARJ') THEN 'TARJETA DE CREDITO' WHEN f.IdMotivo_tipo_cobro IN ('NTCR') 
                  THEN 'NOTA DE CREDITO' END AS GrupoTipoCobro, f.tc_descripcion AS NombreTipoCobro, a.cr_TotalCobro AS TotalPago
FROM     cxc_cobro AS A LEFT JOIN
                  seg_usuario AS C ON A.IdUsuario = C.IdUsuario INNER JOIN
                  aca_Alumno AS D ON D .IdEmpresa = A.IdEmpresa AND D .IdAlumno = A.IdAlumno INNER JOIN
                  tb_persona AS E ON E.IdPersona = D .IdPersona INNER JOIN
                  cxc_cobro_tipo AS F ON F.IdCobro_tipo = A.IdCobro_tipo LEFT JOIN
                  tb_TarjetaCredito AS G ON A.IdEmpresa = G.IdEmpresa AND A.IdTarjeta = G.IdTarjeta
WHERE  NOT EXISTS
                      (SELECT B.IdEmpresa
                       FROM      cxc_cobro_det AS B
                       WHERE   A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdCobro = B.IdCobro) AND (A.cr_estado = 'A')