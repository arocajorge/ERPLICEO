CREATE PROCEDURE [web].[SPFAC_011]
(
@IdEmpresa int, @IdClienteIni numeric, @IdClienteFin numeric, @FechaIni date, @FechaFin date, @MostrarAnulados bit
)
AS

SELECT REP.IdEmpresa, REP.IdCliente, REP.pe_nombreCompleto, REP.Fecha, REP.Estado, REP.Tipo, REP.Referencia, REP.Observacion, REP.Debitos, REP.Creditos, 
SUM(REP.VALOR) OVER(PARTITION BY REP.IdCliente ORDER BY Rep.IdEmpresa, Rep.IdCliente, Rep.Fecha,Rep.Secuencia, Rep.Referencia, Rep.Debitos, REP.Creditos) as Saldo, 
SUM(REP.VALOR) OVER(ORDER BY Rep.IdEmpresa, Rep.IdCliente, Rep.Fecha,Rep.Secuencia, Rep.Referencia, Rep.Debitos, REP.Creditos) as SaldoModulo, 
SECUENCIA as Secuencia
FROM(

SELECT A.IdEmpresa, A.IdCliente, A.pe_nombreCompleto, A.Fecha, A.Estado, 'S.I.' Tipo,'' Referencia,'SALDO INICIAL' Observacion,0 Debitos,0 Creditos,SUM(A.Valor) Valor, -1 AS SECUENCIA 
FROM (
	SELECT        fa_factura.IdEmpresa, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto, DATEADD(DAY,-1, @FechaIni) AS Fecha, fa_factura.Estado, 'Factura' AS Tipo, fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura AS Referencia,
							  fa_factura.vt_Observacion, 0 AS Debito, 0 AS Credito, case when fa_factura.Estado = 'A' THEN ISNULL(det.Total, 0) ELSE 0 END AS Valor, 1 as secuencia
	FROM            fa_factura INNER JOIN
							 fa_cliente ON fa_factura.IdEmpresa = fa_cliente.IdEmpresa AND fa_factura.IdCliente = fa_cliente.IdCliente INNER JOIN
							 tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona LEFT OUTER JOIN
								 (SELECT        d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCbteVta, SUM(d.vt_total) AS Total
								   FROM            fa_factura_det AS d INNER JOIN
															 fa_factura AS c ON d.IdEmpresa = c.IdEmpresa AND d.IdSucursal = c.IdSucursal AND d.IdBodega = c.IdBodega AND d.IdCbteVta = c.IdCbteVta
							WHERE c.IdEmpresa = @IdEmpresa and c.IdCliente between @IdClienteIni and @IdClienteFin and c.vt_fecha < @FechaIni and c.Estado = 'A'
								   GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCbteVta) AS det ON fa_factura.IdCbteVta = det.IdCbteVta AND fa_factura.IdBodega = det.IdBodega AND fa_factura.IdSucursal = det.IdSucursal AND 
							 fa_factura.IdEmpresa = det.IdEmpresa
	WHERE fa_factura.IdEmpresa = @IdEmpresa and fa_factura.IdCliente between @IdClienteIni and @IdClienteFin and fa_factura.vt_fecha < @FechaIni and fa_factura.Estado = 'A'
	UNION ALL
	SELECT        fa_notaCreDeb.IdEmpresa, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto, DATEADD(DAY,-1, @FechaIni), fa_notaCreDeb.Estado, case when fa_notaCreDeb.CreDeb = 'C' THEN 'Nota de crédito' else 'Nota de débito' end AS Tipo, 
	case when fa_notaCreDeb.NaturalezaNota = 'INT' THEN 'INT-'+ ISNULL(fa_notaCreDeb.CodNota, cast(fa_notaCreDeb.IdNota as varchar(20))) else  fa_notaCreDeb.Serie1+ '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa end AS Referencia,
							  fa_notaCreDeb.sc_observacion, 
						  
							  0 AS Debito, 
							  0 AS Credito, 						  
							  case when fa_notaCreDeb.Estado = 'A' THEN 						  
							  case when fa_notaCreDeb.CreDeb = 'D' THEN ISNULL(det.Total, 0) ELSE ISNULL(det.Total, 0) *-1 END						  
							  ELSE 0 END AS Valor, 1 as secuencia
	FROM            fa_notaCreDeb INNER JOIN
							 fa_cliente ON fa_notaCreDeb.IdEmpresa = fa_cliente.IdEmpresa AND fa_notaCreDeb.IdCliente = fa_cliente.IdCliente INNER JOIN
							 tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona LEFT OUTER JOIN
								 (SELECT        d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdNota, SUM(d.sc_total) AS Total
								   FROM            fa_notaCreDeb_det AS d INNER JOIN
															 fa_notaCreDeb AS c ON d.IdEmpresa = c.IdEmpresa AND d.IdSucursal = c.IdSucursal AND d.IdBodega = c.IdBodega AND d.IdNota = c.IdNota
							WHERE c.IdEmpresa = @IdEmpresa and c.IdCliente between @IdClienteIni and @IdClienteFin and c.no_fecha < @FechaIni and c.Estado = 'A'
								   GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdNota) AS det ON fa_notaCreDeb.IdNota = det.IdNota AND fa_notaCreDeb.IdBodega = det.IdBodega AND fa_notaCreDeb.IdSucursal = det.IdSucursal AND 
							 fa_notaCreDeb.IdEmpresa = det.IdEmpresa
	WHERE fa_notaCreDeb.IdEmpresa = @IdEmpresa and fa_notaCreDeb.IdCliente between @IdClienteIni and @IdClienteFin and fa_notaCreDeb.no_fecha < @FechaIni and fa_notaCreDeb.Estado = 'A'
	UNION ALL
	SELECT        cxc_cobro.IdEmpresa, cxc_cobro.IdCliente, tb_persona.pe_nombreCompleto, DATEADD(DAY,-1, @FechaIni), cxc_cobro.cr_estado, cxc_cobro_tipo.tc_descripcion, cxc_cobro.cr_NumDocumento, cxc_cobro.cr_observacion, 
							 0 as Debito, 0 as Credito, case when cxc_cobro.cr_estado = 'A' then cxc_cobro_det.dc_ValorPago*-1 else 0 end, cxc_cobro_det.secuencial
	FROM            tb_persona INNER JOIN
							 fa_cliente ON tb_persona.IdPersona = fa_cliente.IdPersona RIGHT OUTER JOIN
							 cxc_cobro_tipo RIGHT OUTER JOIN
							 cxc_cobro_det ON cxc_cobro_tipo.IdCobro_tipo = cxc_cobro_det.IdCobro_tipo RIGHT OUTER JOIN
							 cxc_cobro ON cxc_cobro_det.IdEmpresa = cxc_cobro.IdEmpresa AND cxc_cobro_det.IdSucursal = cxc_cobro.IdSucursal AND cxc_cobro_det.IdCobro = cxc_cobro.IdCobro ON fa_cliente.IdEmpresa = cxc_cobro.IdEmpresa AND 
							 fa_cliente.IdCliente = cxc_cobro.IdCliente
	WHERE cxc_cobro.IdEmpresa = @IdEmpresa and cxc_cobro.IdCliente between @IdClienteIni and @IdClienteFin and cxc_cobro.cr_fecha < @FechaIni  and cxc_cobro.cr_estado = 'A'
	and cxc_cobro_tipo.IdMotivo_tipo_cobro <> 'NTCR'
	) A
	group by A.IdEmpresa, A.IdCliente, A.pe_nombreCompleto, A.Fecha, A.Estado
	
UNION ALL
	SELECT        fa_factura.IdEmpresa, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto, fa_factura.vt_fecha, fa_factura.Estado, 'Factura' AS Tipo, fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura AS Referencia,
							fa_factura.vt_Observacion,ISNULL(det.Total, 0) AS Debito, 0 AS Credito, case when fa_factura.Estado = 'A' THEN ISNULL(det.Total, 0) ELSE 0 END AS Valor, 0 as secuencia
	FROM            fa_factura INNER JOIN
							fa_cliente ON fa_factura.IdEmpresa = fa_cliente.IdEmpresa AND fa_factura.IdCliente = fa_cliente.IdCliente INNER JOIN
							tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona LEFT OUTER JOIN
								(SELECT        d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCbteVta, SUM(d.vt_total) AS Total
								FROM            fa_factura_det AS d INNER JOIN
															fa_factura AS c ON d.IdEmpresa = c.IdEmpresa AND d.IdSucursal = c.IdSucursal AND d.IdBodega = c.IdBodega AND d.IdCbteVta = c.IdCbteVta
						WHERE c.IdEmpresa = @IdEmpresa and c.IdCliente between @IdClienteIni and @IdClienteFin and c.vt_fecha between @FechaIni and @FechaFin and c.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
								GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdCbteVta) AS det ON fa_factura.IdCbteVta = det.IdCbteVta AND fa_factura.IdBodega = det.IdBodega AND fa_factura.IdSucursal = det.IdSucursal AND 
							fa_factura.IdEmpresa = det.IdEmpresa
	WHERE fa_factura.IdEmpresa = @IdEmpresa and fa_factura.IdCliente between @IdClienteIni and @IdClienteFin and fa_factura.vt_fecha between @FechaIni and @FechaFin and fa_factura.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
	UNION ALL
	SELECT        fa_notaCreDeb.IdEmpresa, fa_cliente.IdCliente, tb_persona.pe_nombreCompleto, fa_notaCreDeb.no_fecha, fa_notaCreDeb.Estado, case when fa_notaCreDeb.CreDeb = 'C' THEN 'Nota de crédito' else 'Nota de débito' end AS Tipo, 
	case when fa_notaCreDeb.NaturalezaNota = 'INT' THEN 'INT-'+ ISNULL(fa_notaCreDeb.CodNota, cast(fa_notaCreDeb.IdNota as varchar(20))) else  fa_notaCreDeb.Serie1+ '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa end AS Referencia,
							fa_notaCreDeb.sc_observacion, 
						  
							case when fa_notaCreDeb.CreDeb = 'D' THEN ISNULL(det.Total, 0) ELSE 0 END AS Debito, 
							case when fa_notaCreDeb.CreDeb = 'C' THEN ISNULL(det.Total, 0) ELSE 0 END AS Credito, 						  
							case when fa_notaCreDeb.Estado = 'A' THEN 						  
							case when fa_notaCreDeb.CreDeb = 'D' THEN ISNULL(det.Total, 0) ELSE ISNULL(det.Total, 0) *-1 END						  
							ELSE 0 END AS Valor, 0 as secuencia
	FROM            fa_notaCreDeb INNER JOIN
							fa_cliente ON fa_notaCreDeb.IdEmpresa = fa_cliente.IdEmpresa AND fa_notaCreDeb.IdCliente = fa_cliente.IdCliente INNER JOIN
							tb_persona ON fa_cliente.IdPersona = tb_persona.IdPersona LEFT OUTER JOIN
								(SELECT        d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdNota, SUM(d.sc_total) AS Total
								FROM            fa_notaCreDeb_det AS d INNER JOIN
															fa_notaCreDeb AS c ON d.IdEmpresa = c.IdEmpresa AND d.IdSucursal = c.IdSucursal AND d.IdBodega = c.IdBodega AND d.IdNota = c.IdNota
						WHERE c.IdEmpresa = @IdEmpresa and c.IdCliente between @IdClienteIni and @IdClienteFin and c.no_fecha between @FechaIni and @FechaFin and c.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
								GROUP BY d.IdEmpresa, d.IdSucursal, d.IdBodega, d.IdNota) AS det ON fa_notaCreDeb.IdNota = det.IdNota AND fa_notaCreDeb.IdBodega = det.IdBodega AND fa_notaCreDeb.IdSucursal = det.IdSucursal AND 
							fa_notaCreDeb.IdEmpresa = det.IdEmpresa
	WHERE fa_notaCreDeb.IdEmpresa = @IdEmpresa and fa_notaCreDeb.IdCliente between @IdClienteIni and @IdClienteFin and fa_notaCreDeb.no_fecha between @FechaIni and @FechaFin and fa_notaCreDeb.Estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
	UNION ALL
	SELECT        cxc_cobro.IdEmpresa, cxc_cobro.IdCliente, tb_persona.pe_nombreCompleto, cxc_cobro.cr_fecha, cxc_cobro.cr_estado, cxc_cobro_tipo.tc_descripcion, cxc_cobro.cr_NumDocumento, cxc_cobro.cr_observacion, 
							0 as Debito, cxc_cobro_det.dc_ValorPago as Credito, case when cxc_cobro.cr_estado = 'A' then cxc_cobro_det.dc_ValorPago*-1 else 0 end, cxc_cobro_det.secuencial
	FROM            tb_persona INNER JOIN
							fa_cliente ON tb_persona.IdPersona = fa_cliente.IdPersona RIGHT OUTER JOIN
							cxc_cobro_tipo RIGHT OUTER JOIN
							cxc_cobro_det ON cxc_cobro_tipo.IdCobro_tipo = cxc_cobro_det.IdCobro_tipo RIGHT OUTER JOIN
							cxc_cobro ON cxc_cobro_det.IdEmpresa = cxc_cobro.IdEmpresa AND cxc_cobro_det.IdSucursal = cxc_cobro.IdSucursal AND cxc_cobro_det.IdCobro = cxc_cobro.IdCobro ON fa_cliente.IdEmpresa = cxc_cobro.IdEmpresa AND 
							fa_cliente.IdCliente = cxc_cobro.IdCliente
	WHERE cxc_cobro.IdEmpresa = @IdEmpresa and cxc_cobro.IdCliente between @IdClienteIni and @IdClienteFin and cxc_cobro.cr_fecha between @FechaIni and @FechaFin and cxc_cobro.cr_estado like '%'+case when @MostrarAnulados = 1 then '' else 'A' end+'%'
	and cxc_cobro_tipo.IdMotivo_tipo_cobro <> 'NTCR'
	) REP
ORDER BY Rep.IdEmpresa, Rep.IdCliente, Rep.Fecha,Rep.Secuencia, Rep.Referencia, Rep.Debitos, REP.Creditos