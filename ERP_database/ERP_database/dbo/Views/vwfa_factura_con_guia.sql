create view vwfa_factura_con_guia as

SELECT        IdEmpresa, IdCliente, pe_cedulaRuc, pe_apellido, pe_nombre, IdSucursal, IdBodega, IdCbteVta, vt_serie1, vt_serie2, vt_NumFactura, vt_Observacion, vt_fecha,IdGuiaRemision_guia
FROM            (


SELECT        dbo.fa_cliente.IdEmpresa, dbo.fa_cliente.IdCliente, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, dbo.fa_factura.IdCbteVta, 
                         dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura_det.Secuencia, dbo.fa_factura.vt_Observacion, dbo.fa_factura.vt_fecha,fa_guia_remision_det_x_factura.IdGuiaRemision_guia
FROM            dbo.fa_factura INNER JOIN
                         dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND 
                         dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta INNER JOIN
                         dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                         dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona INNER JOIN
                         dbo.fa_guia_remision_det_x_factura ON dbo.fa_factura_det.IdEmpresa = dbo.fa_guia_remision_det_x_factura.IdEmpresa_fact AND dbo.fa_factura_det.IdSucursal = dbo.fa_guia_remision_det_x_factura.IdSucursal_fact AND 
                         dbo.fa_factura_det.IdBodega = dbo.fa_guia_remision_det_x_factura.IdBodega_fact AND dbo.fa_factura_det.IdCbteVta = dbo.fa_guia_remision_det_x_factura.IdCbteVta_fact AND 
                         dbo.fa_factura_det.Secuencia = dbo.fa_guia_remision_det_x_factura.Secuencia_fact



) AS query
GROUP BY IdEmpresa, IdCliente, pe_cedulaRuc, pe_apellido, pe_nombre, IdSucursal, IdBodega, IdCbteVta, vt_serie1, vt_serie2, vt_NumFactura, vt_Observacion, vt_fecha,IdGuiaRemision_guia
