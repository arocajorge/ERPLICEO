
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
                  f.vt_serie1 + '-' + f.vt_serie2 + '-' + f.vt_NumFactura AS vt_NumFactura, ISNULL(ParamF.IdCtaCbleDebe, tn.IdCtaCble) AS IdCtaCbleHaber,  tn.IdCtaCbleCXC AS IdCtaCbleDebe, ndnc.no_fecha, 
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
                  ParamF.IdJornada = m.IdJornada AND ParamF.IdCurso = m.IdCurso ON ndnc.IdEmpresa = cruce.IdEmpresa_nt AND ndnc.IdSucursal = cruce.IdSucursal_nt AND ndnc.IdBodega = cruce.IdBodega_nt AND ndnc.IdNota = cruce.IdNota_nt
where ndnc.IdEmpresa = @IdEmpresa and ndnc.IdSucursal = @IdSucursal and ndnc.IdBodega = @IdBodega and ndnc.IdNota = @IdNota