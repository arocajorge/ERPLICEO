--[dbo].[spfa_notaCreDeb_ParaContabilizarAcademico] 1,1,1,412
CREATE PROCEDURE [dbo].[spfa_notaCreDeb_ParaContabilizarAcademico]
(
@IdEmpresa int,
@IdSucursal int,
@IdBodega int,
@IdNota numeric
)
AS
SELECT isnull(ROW_NUMBER() OVER(ORDER BY ndnc.IdEmpresa),0) IdRow, ndnc.IdEmpresa, ndnc.IdSucursal, ndnc.IdBodega, ndnc.IdNota, cruce.secuencia, cruce.Valor_Aplicado, cruce.IdEmpresa_fac_nd_doc_mod, cruce.IdSucursal_fac_nd_doc_mod, cruce.IdBodega_fac_nd_doc_mod, 
                  cruce.IdCbteVta_fac_nd_doc_mod, cruce.vt_tipoDoc, ndnc.Serie1 + '-' + ndnc.Serie2 + '-' + ndnc.NumNota_Impresa AS NumNota_Impresa, ndnc.CreDeb, ncnd_resumen.SubtotalConDscto, ncnd_resumen.ValorIVA, ncnd_resumen.Total, 
                  f.vt_serie1 + '-' + f.vt_serie2 + '-' + f.vt_NumFactura AS vt_NumFactura, ISNULL(ParamF.IdCtaCbleDebe, tn.IdCtaCble) AS IdCtaCbleHaber,  
				  CASE WHEN NDNC.CreDeb = 'C' THEN
				  isnull(TAR.IdCtaCble,tn.IdCtaCbleCXC)
				  ELSE NDNC.IdCtaCble_TipoNota END AS IdCtaCbleDebe, 				  
				  ndnc.no_fecha, 
                  ndnc.sc_observacion, cp.pe_nombreCompleto AS NomCliente, ap.pe_nombreCompleto AS NomAlumno
FROM     dbo.fa_TipoNota AS tn INNER JOIN
                  dbo.fa_notaCreDeb_resumen AS ncnd_resumen INNER JOIN
                  dbo.fa_notaCreDeb AS ndnc ON ncnd_resumen.IdEmpresa = ndnc.IdEmpresa AND ncnd_resumen.IdSucursal = ndnc.IdSucursal AND ncnd_resumen.IdBodega = ndnc.IdBodega AND ncnd_resumen.IdNota = ndnc.IdNota ON 
                  tn.IdEmpresa = ndnc.IdEmpresa AND tn.IdTipoNota = ndnc.IdTipoNota INNER JOIN
                  dbo.fa_cliente AS c ON ndnc.IdCliente = c.IdCliente AND ndnc.IdEmpresa = c.IdEmpresa INNER JOIN
                  dbo.tb_persona AS cp ON c.IdPersona = cp.IdPersona INNER JOIN
                  dbo.aca_Alumno AS a ON ndnc.IdEmpresa = a.IdEmpresa AND ndnc.IdAlumno = a.IdAlumno INNER JOIN
                  dbo.tb_persona AS ap ON a.IdPersona = ap.IdPersona LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Plantilla_Parametrizacion AS ParamF INNER JOIN
                  dbo.fa_factura AS f INNER JOIN
                  dbo.fa_notaCreDeb_x_fa_factura_NotaDeb AS cruce ON f.IdEmpresa = cruce.IdEmpresa_fac_nd_doc_mod AND f.IdSucursal = cruce.IdSucursal_fac_nd_doc_mod AND f.IdBodega = cruce.IdBodega_fac_nd_doc_mod AND 
                  f.IdCbteVta = cruce.IdCbteVta_fac_nd_doc_mod INNER JOIN
                  dbo.aca_Matricula AS m INNER JOIN
                  dbo.aca_Matricula_Rubro AS mr ON m.IdEmpresa = mr.IdEmpresa AND m.IdMatricula = mr.IdMatricula ON f.IdEmpresa = mr.IdEmpresa AND f.IdSucursal = mr.IdSucursal AND f.IdBodega = mr.IdBodega AND f.IdCbteVta = mr.IdCbteVta ON 
                  ParamF.IdPlantilla = mr.IdPlantilla AND ParamF.IdRubro = mr.IdRubro AND ParamF.IdEmpresa = m.IdEmpresa AND ParamF.IdAnio = m.IdAnio AND ParamF.IdSede = m.IdSede AND ParamF.IdNivel = m.IdNivel AND 
                  ParamF.IdJornada = m.IdJornada AND ParamF.IdCurso = m.IdCurso ON ndnc.IdEmpresa = cruce.IdEmpresa_nt AND ndnc.IdSucursal = cruce.IdSucursal_nt AND ndnc.IdBodega = cruce.IdBodega_nt AND ndnc.IdNota = cruce.IdNota_nt LEFT JOIN
				  (
						select a.IdEmpresa, a.IdSucursal, a.IdBodega, a.IdNota, cs.IdCtaCble from fa_notaCreDeb as a join
						fa_notaCreDeb_x_cxc_cobro as b on a.IdEmpresa = b.IdEmpresa_nt and a.IdSucursal = b.IdSucursal_nt and a.IdBodega = b.IdBodega_nt and a.IdNota = b.IdNota_nt join
						cxc_cobro as c on b.IdEmpresa_cbr = c.IdEmpresa and b.IdSucursal_cbr = c.IdSucursal and b.IdCobro_cbr = c.IdCobro left join
						cxc_cobro_det as d on c.IdEmpresa = d.IdEmpresa and c.IdSucursal = d.IdSucursal and c.IdCobro = d.IdCobro join
						cxc_cobro_tipo_Param_conta_x_sucursal as cs on cs.IdEmpresa = c.IdEmpresa and cs.IdCobro_tipo = c.IdCobro_tipo
						where a.IdEmpresa = @IdEmpresa and a.IdSucursal = @IdSucursal and a.IdBodega = @IdBodega and a.IdNota = @IdNota and d.IdCobro is null 
				  ) as tar on tar.IdEmpresa = ndnc.IdEmpresa and tar.IdSucursal = ndnc.IdSucursal and tar.IdBodega = ndnc.IdBodega and tar.IdNota = ndnc.IdNota
where ndnc.IdEmpresa = @IdEmpresa and ndnc.IdSucursal = @IdSucursal and ndnc.IdBodega = @IdBodega and ndnc.IdNota = @IdNota