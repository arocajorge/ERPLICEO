create view vwfa_factura_sin_guia as
SELECT        IdEmpresa, IdCliente, pe_cedulaRuc, pe_apellido, pe_nombre, IdSucursal, IdBodega, IdCbteVta, vt_serie1, vt_serie2, vt_NumFactura, vt_Observacion, vt_fecha
FROM            (SELECT        dbo.fa_cliente.IdEmpresa, dbo.fa_cliente.IdCliente, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_apellido, dbo.tb_persona.pe_nombre, dbo.fa_factura.IdSucursal, dbo.fa_factura.IdBodega, 
                                                    dbo.fa_factura.IdCbteVta, dbo.fa_factura.vt_serie1, dbo.fa_factura.vt_serie2, dbo.fa_factura.vt_NumFactura, dbo.fa_factura_det.Secuencia, dbo.fa_factura.vt_Observacion, dbo.fa_factura.vt_fecha
                          FROM            dbo.fa_factura INNER JOIN
                                                    dbo.fa_factura_det ON dbo.fa_factura.IdEmpresa = dbo.fa_factura_det.IdEmpresa AND dbo.fa_factura.IdSucursal = dbo.fa_factura_det.IdSucursal AND dbo.fa_factura.IdBodega = dbo.fa_factura_det.IdBodega AND 
                                                    dbo.fa_factura.IdCbteVta = dbo.fa_factura_det.IdCbteVta INNER JOIN
                                                    dbo.fa_cliente ON dbo.fa_factura.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_factura.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
                                                    dbo.tb_persona ON dbo.fa_cliente.IdPersona = dbo.tb_persona.IdPersona
                          WHERE        (NOT EXISTS
                                                        (SELECT        IdEmpresa_guia, IdSucursal_guia, IdBodega_guia, IdGuiaRemision_guia, Secuencia_guia, IdEmpresa_fact, IdSucursal_fact, IdBodega_fact, IdCbteVta_fact, Secuencia_fact, observacion
                                                          FROM            dbo.fa_guia_remision_det_x_factura AS Guia
                                                          WHERE        (IdEmpresa_fact = dbo.fa_factura_det.IdEmpresa) AND (IdSucursal_fact = dbo.fa_factura_det.IdSucursal) AND (IdBodega_fact = dbo.fa_factura_det.IdBodega) AND 
                                                                                    (IdCbteVta_fact = dbo.fa_factura_det.IdCbteVta) AND (Secuencia_fact = dbo.fa_factura_det.Secuencia))) AND (dbo.fa_factura.Estado = 'A')) AS query
GROUP BY IdEmpresa, IdCliente, pe_cedulaRuc, pe_apellido, pe_nombre, IdSucursal, IdBodega, IdCbteVta, vt_serie1, vt_serie2, vt_NumFactura, vt_Observacion, vt_fecha
