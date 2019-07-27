CREATE VIEW [web].[vwfa_guia_remision]
	AS 
SELECT        dbo.fa_guia_remision.IdEmpresa, dbo.fa_guia_remision.IdSucursal, dbo.fa_guia_remision.IdBodega, dbo.fa_guia_remision.IdGuiaRemision, dbo.fa_guia_remision.CodGuiaRemision, dbo.fa_guia_remision.CodDocumentoTipo, 
                         dbo.fa_guia_remision.Serie1, dbo.fa_guia_remision.Serie2, dbo.fa_guia_remision.NumGuia_Preimpresa, dbo.fa_guia_remision.NUAutorizacion, dbo.fa_guia_remision.Fecha_Autorizacion, dbo.fa_guia_remision.IdCliente, 
                         dbo.fa_guia_remision.IdTransportista, dbo.fa_guia_remision.gi_fecha, dbo.fa_guia_remision.gi_plazo, dbo.fa_guia_remision.gi_fech_venc, dbo.fa_guia_remision.gi_Observacion,  
                         dbo.fa_guia_remision.gi_FechaFinTraslado, dbo.fa_guia_remision.gi_FechaInicioTraslado, dbo.fa_guia_remision.placa, dbo.fa_guia_remision.Direccion_Origen, 
                         dbo.fa_guia_remision.Direccion_Destino, dbo.tb_persona.pe_cedulaRuc, dbo.tb_persona.pe_nombreCompleto, dbo.fa_guia_remision.Estado
FROM            dbo.fa_guia_remision INNER JOIN
                         dbo.fa_cliente ON dbo.fa_guia_remision.IdEmpresa = dbo.fa_cliente.IdEmpresa AND dbo.fa_guia_remision.IdCliente = dbo.fa_cliente.IdCliente INNER JOIN
						 dbo.tb_persona ON dbo.tb_persona.IdPersona = dbo.fa_cliente.IdPersona