
CREATE view EntidadRegulatoria.vwfe_documentos_peniente_enviar_sri as
select IdEmpresa,IdSucursal,IdBodega,IdCbteVta, 'FACT' TipoDocumento, Fecha_Transaccion, vt_fecha from fa_factura where aprobada_enviar_sri=1 and Generado is null
union all
select IdEmpresa,IdSucursal,IdBodega,IdGuiaRemision,'GUIA' TipoDocumento, FechaCreacion, gi_fecha from fa_guia_remision where aprobada_enviar_sri=1 and Generado is null
union all
select IdEmpresa,IdSucursal,IdBodega,IdNota,'NTCR' TipoDocumento, no_fecha, no_fecha from fa_notaCreDeb where aprobada_enviar_sri=1 AND CreDeb='C' and Generado is null
union all
select IdEmpresa,IdSucursal,IdBodega,IdNota,'NTDB' TipoDocumento, no_fecha, no_fecha from fa_notaCreDeb where aprobada_enviar_sri=1 AND CreDeb='D' and Generado is null
union all
select IdEmpresa,0 IdSucursal, 0 IdBodega, IdRetencion, 'RETEN' TipoDocumento, Fecha_Transac, fecha from cp_retencion where aprobada_enviar_sri=1 and Generado is null