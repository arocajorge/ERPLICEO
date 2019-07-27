CREATE VIEW web.VWFAC_009
AS
SELECT fa_guia_remision.IdEmpresa, fa_guia_remision.IdSucursal, fa_guia_remision.IdBodega, fa_guia_remision.IdGuiaRemision, vt_tipoDoc, vt_NumFactura, vt_autorizacion,
fa_guia_remision.gi_FechaInicioTraslado, fa_guia_remision.gi_FechaFinTraslado, c.tr_Descripcion as MotivoTraslado, fa_guia_remision.Direccion_Origen,
fa_guia_remision.Direccion_Destino, per.pe_cedulaRuc CedulaCliente, per.pe_nombreCompleto as NomCliente, tb_transportista.Cedula CedulaTransportista, tb_transportista.Nombre NombreTransportista,
fa_guia_remision.placa, em.em_ruc as RucEmpresa, fa_guia_remision_det.gi_cantidad, in_Producto.pr_descripcion pr_descripcion
FROM     fa_guia_remision INNER JOIN
                  fa_guia_remision_det ON fa_guia_remision.IdEmpresa = fa_guia_remision_det.IdEmpresa AND fa_guia_remision.IdSucursal = fa_guia_remision_det.IdSucursal AND fa_guia_remision.IdBodega = fa_guia_remision_det.IdBodega AND 
                  fa_guia_remision.IdGuiaRemision = fa_guia_remision_det.IdGuiaRemision INNER JOIN
                  in_Producto ON fa_guia_remision_det.IdEmpresa = in_Producto.IdEmpresa AND fa_guia_remision_det.IdProducto = in_Producto.IdProducto INNER JOIN
                  in_presentacion ON in_Producto.IdEmpresa = in_presentacion.IdEmpresa AND in_Producto.IdPresentacion = in_presentacion.IdPresentacion INNER JOIN
                  tb_transportista ON fa_guia_remision.IdEmpresa = tb_transportista.IdEmpresa AND fa_guia_remision.IdTransportista = tb_transportista.IdTransportista 
				  left join (
					SELECT gi_IdEmpresa, gi_IdSucursal, gi_IdBodega, gi_IdGuiaRemision, vt_tipoDoc, vt_NumFactura, vt_autorizacion
					FROM (SELECT gi_IdEmpresa, gi_IdSucursal, gi_IdBodega, gi_IdGuiaRemision, vt_tipoDoc, vt_NumFactura, vt_autorizacion 
					FROM (
					SELECT  ROW_NUMBER() OVER(PARTITION BY fa_factura_x_fa_guia_remision.gi_IdEmpresa, fa_factura_x_fa_guia_remision.gi_IdSucursal, fa_factura_x_fa_guia_remision.gi_IdBodega, fa_factura_x_fa_guia_remision.gi_IdGuiaRemision ORDER BY fa_factura_x_fa_guia_remision.gi_IdEmpresa, fa_factura_x_fa_guia_remision.gi_IdSucursal, fa_factura_x_fa_guia_remision.gi_IdBodega, fa_factura_x_fa_guia_remision.gi_IdGuiaRemision) AS IdRow,
					fa_factura_x_fa_guia_remision.gi_IdEmpresa, fa_factura_x_fa_guia_remision.gi_IdSucursal, fa_factura_x_fa_guia_remision.gi_IdBodega, fa_factura_x_fa_guia_remision.gi_IdGuiaRemision, fa_factura.vt_tipoDoc, fa_factura.vt_serie1 +'-'+
					fa_factura.vt_serie2+'-'+ fa_factura.vt_NumFactura vt_NumFactura, fa_factura.vt_autorizacion
					FROM     fa_factura_x_fa_guia_remision INNER JOIN
					fa_factura ON fa_factura_x_fa_guia_remision.fa_IdEmpresa = fa_factura.IdEmpresa AND fa_factura_x_fa_guia_remision.fa_IdSucursal = fa_factura.IdSucursal AND fa_factura_x_fa_guia_remision.fa_IdBodega = fa_factura.IdBodega AND 
					fa_factura_x_fa_guia_remision.fa_IdCbteVta = fa_factura.IdCbteVta
					) A WHERE A.IdRow = 1) b
				  ) fac on fa_guia_remision.IdEmpresa = fac.gi_IdEmpresa and fa_guia_remision.IdSucursal = fac.gi_IdSucursal and fa_guia_remision.IdBodega = fac.gi_IdBodega
				  and fa_guia_remision.IdGuiaRemision = fac.gi_IdGuiaRemision
				  left join fa_MotivoTraslado as c on c.IdMotivoTraslado = fa_guia_remision.IdMotivoTraslado and c.IdEmpresa = fa_guia_remision.IdEmpresa
				  left join fa_cliente as cli on fa_guia_remision.IdEmpresa = cli.IdEmpresa and fa_guia_remision.IdCliente = cli.IdCliente
				  inner join tb_persona as per on cli.IdPersona = per.IdPersona
				  inner join tb_empresa as em on fa_guia_remision.IdEmpresa = em.IdEmpresa