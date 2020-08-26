CREATE VIEW Academico.VWFAC_009
AS
select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, C.IdSucursal_fac_nd_doc_mod, C.IdBodega_fac_nd_doc_mod, C.IdCbteVta_fac_nd_doc_mod, C.vt_tipoDoc,
a.no_fecha, a.sc_observacion, case when c.vt_tipoDoc = 'NTDB' then 'NOTA DE DEBITO ' +CAST(YEAR(A.no_fecha) AS VARCHAR) else ar.NomRubro +' '+ (case when ar.NumeroCuotas > 1 then right('00'+cast(arp.Secuencia as varchar),2)+'/'+cast(ar.NumeroCuotas as varchar) else '' end) end as vt_Observacion,
a.IdTipoNota, d.No_Descripcion, c.fecha_cruce, b.Total, c.Valor_Aplicado, case when c.IdNota_nt is null then null else (case when conci.IdNota is null then 'V. APLICADO' ELSE 'V. CONCILIADO' END) end AS Tipo, case when c.vt_tipoDoc = 'FACT' THEN f.vt_serie1+'-'+f.vt_serie2+'-'+ f.vt_NumFactura  ELSE (CASE WHEN ND.NaturalezaNota = 'SRI' THEN ND.Serie1+'-'+ND.Serie2+'-'+ND.NumNota_Impresa ELSE CAST(C.IdCbteVta_fac_nd_doc_mod AS VARCHAR) END) END vt_NumFactura,
a.IdAlumno, p.pe_nombreCompleto, al.Codigo as CodigoAlumno, isnull(nd.no_fecha, f.vt_fecha) as vt_fecha, CONCI.IdConciliacion
from fa_notaCreDeb as a inner join
fa_notaCreDeb_resumen as b on a.IdEmpresa =b.IdEmpresa and a.IdSucursal = b.IdSucursal and a.IdBodega = b.IdBodega and a.IdNota = b.IdNota inner join
fa_notaCreDeb_x_fa_factura_NotaDeb as c on a.IdEmpresa= c.IdEmpresa_nt and a.IdSucursal = c.IdSucursal_nt and a.IdBodega = c.IdBodega_nt and a.IdNota = c.IdNota_nt left JOIN
fa_TipoNota as d on a.IdEmpresa = d.IdEmpresa and a.IdTipoNota = d.IdTipoNota left join
aca_Matricula_Rubro as mr on c.IdEmpresa_fac_nd_doc_mod = mr.IdEmpresa and c.IdSucursal_fac_nd_doc_mod = mr.IdSucursal and c.IdBodega_fac_nd_doc_mod = mr.IdBodega and c.IdCbteVta_fac_nd_doc_mod = mr.IdCbteVta and c.vt_tipoDoc = 'FACT' LEFT JOIN
aca_AnioLectivo_Rubro as ar on mr.IdEmpresa = ar.IdEmpresa and mr.IdAnio = ar.IdAnio and mr.IdRubro = ar.IdRubro left join
aca_AnioLectivo_Rubro_Periodo as arp on arp.IdEmpresa = mr.IdEmpresa and arp.IdAnio = mr.IdAnio and arp.IdRubro = mr.IdRubro and arp.IdPeriodo = mr.IdPeriodo left join
(
select x1.IdEmpresa, x1.IdSucursal, x1.IdBodega, x1.IdNota, x2.secuencia_nt, X1.IdConciliacion
from cxc_ConciliacionNotaCredito as x1 inner join
cxc_ConciliacionNotaCreditoDet as x2 on x1.IdEmpresa = x2.IdEmpresa and x1.IdConciliacion = x2.IdConciliacion
where x1.Estado = 1
) as conci on c.IdEmpresa_nt = conci.IdEmpresa and c.IdSucursal_nt = conci.IdSucursal and c.IdBodega_nt = conci.IdBodega and c.IdNota_nt = conci.IdNota and c.secuencia = conci.secuencia_nt left join
fa_factura as f on c.IdEmpresa_fac_nd_doc_mod = f.IdEmpresa and c.IdSucursal_fac_nd_doc_mod = f.IdSucursal and c.IdBodega_fac_nd_doc_mod = f.IdBodega and c.IdCbteVta_fac_nd_doc_mod = f.IdCbteVta and c.vt_tipoDoc = f.vt_tipoDoc LEFT JOIN
fa_notaCreDeb AS nd on c.IdEmpresa_fac_nd_doc_mod = nd.IdEmpresa and c.IdSucursal_fac_nd_doc_mod = nd.IdSucursal and c.IdBodega_fac_nd_doc_mod = nd.IdBodega and c.IdCbteVta_fac_nd_doc_mod = nd.IdNota and c.vt_tipoDoc = nd.CodDocumentoTipo LEFT JOIN
aca_Alumno AS al on a.IdEmpresa = al.IdEmpresa and a.IdAlumno = al.IdAlumno left join
tb_persona as p on al.IdPersona = p.IdPersona
where a.Estado = 'A'  AND A.CreDeb= 'C'