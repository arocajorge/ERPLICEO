CREATE VIEW [Academico].[VWCXC_002]
AS
SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.secuencial, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento, 
                  dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro_tipo.tc_descripcion, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto, 
                  dbo.fa_factura.vt_serie1 + '-' + dbo.fa_factura.vt_serie2 + '-' + dbo.fa_factura.vt_NumFactura AS vt_NumFactura, dbo.fa_factura.vt_fecha, dbo.fa_factura.vt_Observacion AS ObservacionFact, 
                  dbo.cxc_cobro.cr_observacion AS ObservacionCobro, dbo.cxc_cobro.cr_fecha, dbo.fa_cliente_contactos.Nombres AS NombreContacto, dbo.fa_cliente_contactos.Direccion, dbo.fa_cliente_contactos.Correo, 
                  dbo.tb_persona.pe_cedulaRuc, dbo.cxc_cobro.cr_estado, dbo.tb_sucursal.Su_Descripcion, dbo.cxc_cobro.cr_Banco AS ba_descripcion, dbo.cxc_cobro.cr_NumDocumento, dbo.cxc_cobro.cr_TotalCobro
FROM     dbo.cxc_cobro_tipo RIGHT OUTER JOIN
                  dbo.tb_sucursal INNER JOIN
                  dbo.cxc_cobro ON dbo.tb_sucursal.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.cxc_cobro.IdSucursal INNER JOIN
                  dbo.aca_Alumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona ON dbo.cxc_cobro.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.cxc_cobro.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.fa_factura INNER JOIN
                  dbo.cxc_cobro_det ON dbo.fa_factura.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND dbo.fa_factura.IdBodega = dbo.cxc_cobro_det.IdBodega_Cbte AND 
                  dbo.fa_factura.IdCbteVta = dbo.cxc_cobro_det.IdCbte_vta_nota AND dbo.fa_factura.vt_tipoDoc = dbo.cxc_cobro_det.dc_TipoDocumento INNER JOIN
                  dbo.fa_cliente_contactos ON dbo.fa_factura.IdCliente = dbo.fa_cliente_contactos.IdCliente AND dbo.fa_factura.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND 
                  dbo.cxc_cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND dbo.cxc_cobro.IdCobro = dbo.cxc_cobro_det.IdCobro ON dbo.cxc_cobro_tipo.IdCobro_tipo = dbo.cxc_cobro_det.IdCobro_tipo
UNION ALL
SELECT dbo.cxc_cobro_det.IdEmpresa, dbo.cxc_cobro_det.IdSucursal, dbo.cxc_cobro_det.IdCobro, dbo.cxc_cobro_det.secuencial, dbo.cxc_cobro_det.IdBodega_Cbte, dbo.cxc_cobro_det.IdCbte_vta_nota, dbo.cxc_cobro_det.dc_TipoDocumento, 
                  dbo.cxc_cobro_det.dc_ValorPago, dbo.cxc_cobro_tipo.tc_descripcion, dbo.tb_persona.IdPersona, dbo.tb_persona.pe_nombreCompleto, 
                  CASE WHEN fa_notaCreDeb.NaturalezaNota = 'SRI' then
				  dbo.fa_notaCreDeb.SERIE1 + '-' + dbo.fa_notaCreDeb.SERIE2 + '-' + dbo.fa_notaCreDeb.NUMNOTA_IMPRESA else dbo.fa_notaCreDeb.CodNota  
				  end AS vt_NumFactura,
				  dbo.fa_notaCreDeb.NO_fecha, dbo.fa_notaCreDeb.sc_observacion AS ObservacionFact, 
                  dbo.cxc_cobro.cr_observacion AS ObservacionCobro, dbo.cxc_cobro.cr_fecha, dbo.fa_cliente_contactos.Nombres AS NombreContacto, dbo.fa_cliente_contactos.Direccion, dbo.fa_cliente_contactos.Correo, 
                  dbo.tb_persona.pe_cedulaRuc, dbo.cxc_cobro.cr_estado, dbo.tb_sucursal.Su_Descripcion, dbo.cxc_cobro.cr_Banco AS ba_descripcion, dbo.cxc_cobro.cr_NumDocumento, dbo.cxc_cobro.cr_TotalCobro
FROM     dbo.cxc_cobro_tipo RIGHT OUTER JOIN
                  dbo.tb_sucursal INNER JOIN
                  dbo.cxc_cobro ON dbo.tb_sucursal.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.tb_sucursal.IdSucursal = dbo.cxc_cobro.IdSucursal INNER JOIN
                  dbo.aca_Alumno INNER JOIN
                  dbo.tb_persona ON dbo.aca_Alumno.IdPersona = dbo.tb_persona.IdPersona ON dbo.cxc_cobro.IdEmpresa = dbo.aca_Alumno.IdEmpresa AND dbo.cxc_cobro.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN
                  dbo.fa_notaCreDeb INNER JOIN
                  dbo.cxc_cobro_det ON dbo.fa_notaCreDeb.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND dbo.fa_notaCreDeb.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND dbo.fa_notaCreDeb.IdBodega = dbo.cxc_cobro_det.IdBodega_Cbte AND 
                  dbo.fa_notaCreDeb.IdNota = dbo.cxc_cobro_det.IdCbte_vta_nota AND dbo.fa_notaCreDeb.CodDocumentoTipo = dbo.cxc_cobro_det.dc_TipoDocumento INNER JOIN
                  dbo.fa_cliente_contactos ON dbo.fa_notaCreDeb.IdCliente = dbo.fa_cliente_contactos.IdCliente AND dbo.fa_notaCreDeb.IdEmpresa = dbo.fa_cliente_contactos.IdEmpresa ON dbo.cxc_cobro.IdEmpresa = dbo.cxc_cobro_det.IdEmpresa AND 
                  dbo.cxc_cobro.IdSucursal = dbo.cxc_cobro_det.IdSucursal AND dbo.cxc_cobro.IdCobro = dbo.cxc_cobro_det.IdCobro ON dbo.cxc_cobro_tipo.IdCobro_tipo = dbo.cxc_cobro_det.IdCobro_tipo