CREATE VIEW vwcxc_cartera_cobrada_saldo0
	AS
	select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.vt_tipoDoc, a.vt_tipoDoc+'-'+ CAST(cast(a.vt_NumFactura as numeric) AS VARCHAR) vt_NunDocumento, a.vt_Observacion, a.IdCbteVta, a.CodCbteVta, c.Su_Descripcion, a.IdCliente, a.IdAlumno, a.vt_fecha, b.Total, a.vt_fech_venc, e.pe_nombreCompleto, B.IdAnio, B.IdPlantilla, A.IdPuntoVta,
	isnull(sum(f.dc_ValorPago),0) dc_ValorPago
	from fa_factura as a inner join
	fa_factura_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdCbteVta = b.IdCbteVta left join
	tb_sucursal as c on a.IdEmpresa = c.IdEmpresa and a.IdSucursal = c.IdSucursal inner join
	fa_cliente as d on a.IdEmpresa = d.IdEmpresa and a.IdCliente = d.IdCliente inner join
	tb_persona as e on d.IdPersona = e.IdPersona LEFT JOIN
	cxc_cobro_det AS f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdCbteVta = f.IdCbte_vta_nota and a.vt_tipoDoc = f.dc_TipoDocumento and f.estado = 'A'
	where a.Estado = 'A' AND A.vt_fecha < DATEADD(YEAR,-1, GETDATE())
	group by a.IdEmpresa, a.IdSucursal, a.IdBodega, a.vt_tipoDoc, a.vt_tipoDoc, a.vt_NumFactura, a.vt_Observacion, a.IdCbteVta, a.CodCbteVta, c.Su_Descripcion, a.IdCliente, a.IdAlumno, a.vt_fecha, b.Total, a.vt_fech_venc, e.pe_nombreCompleto, B.IdAnio, B.IdPlantilla, A.IdPuntoVta
	HAVING DBO.BankersRounding( B.Total - isnull(sum(f.dc_ValorPago),0),2) = 0
	UNION ALL
	select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.CodDocumentoTipo, a.CodDocumentoTipo+'-'+ 
	case when a.NaturalezaNota= 'SRI' THEN
	CAST(cast(a.NumNota_Impresa as numeric) AS VARCHAR) 
	ELSE ISNULL(A.CodNota,CAST(A.IdNota AS VARCHAR))
	end
	vt_NunDocumento, a.sc_observacion, a.IdNota, a.CodNota, c.Su_Descripcion, a.IdCliente, a.IdAlumno, a.no_fecha, b.Total, a.no_fecha_venc, e.pe_nombreCompleto, B.IdAnio, NULL, A.IdPuntoVta,
	isnull(sum(f.dc_ValorPago),0) dc_ValorPago
	from fa_notaCreDeb as a inner join
	fa_notaCreDeb_resumen as b on a.IdEmpresa = b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota  left join
	tb_sucursal as c on a.IdEmpresa = c.IdEmpresa and a.IdSucursal = c.IdSucursal inner join
	fa_cliente as d on a.IdEmpresa = d.IdEmpresa and a.IdCliente = d.IdCliente inner join
	tb_persona as e on d.IdPersona = e.IdPersona LEFT JOIN
	cxc_cobro_det AS f on a.IdEmpresa = f.IdEmpresa and a.IdSucursal = f.IdSucursal and a.IdBodega = f.IdBodega_Cbte and a.IdNota = f.IdCbte_vta_nota and a.CodDocumentoTipo = f.dc_TipoDocumento and f.estado = 'A'
	where a.Estado = 'A'  and a.CreDeb = 'D' AND A.no_fecha < DATEADD(YEAR,-1, GETDATE())
	group by a.IdEmpresa, a.IdSucursal, a.IdBodega, a.CodDocumentoTipo, a.CodNota, a.NumNota_Impresa, a.sc_observacion, a.IdNota, a.CodNota, c.Su_Descripcion, a.IdCliente, a.IdAlumno, a.no_fecha, b.Total, a.no_fecha_venc, e.pe_nombreCompleto, B.IdAnio, A.IdPuntoVta, a.NaturalezaNota
	HAVING DBO.BankersRounding( B.Total - isnull(sum(f.dc_ValorPago),0),2) = 0