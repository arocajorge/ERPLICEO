CREATE VIEW [Academico].[VWACA_012]
AS
SELECT a.IdEmpresa, a.IdMatricula, b.IdPeriodo, b.IdRubro,c.IdSucursal, c.IdBodega, c.IdCbteVta,  a.IdAlumno, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, c.vt_serie1 + '-' + c.vt_serie2 + '-' + c.vt_NumFactura AS vt_NumFactura, c.vt_fecha, 
                  c.vt_Observacion, dbo.cxc_cobro.cr_fecha, dbo.cxc_cobro_det.dc_ValorPago, sn.NomSede, sn.OrdenNivel, sn.NomNivel, nj.OrdenJornada, nj.NomJornada, jc.OrdenCurso, jc.NomCurso, cp.OrdenParalelo, cp.NomParalelo, 
                  d.Codigo AS CodigoAlumno, e.pe_cedulaRuc, e.pe_nombreCompleto, e.pe_fechaNacimiento, h.Nacionalidad, i.NomRubro, j.Descripcion AS DescripcionAnio, case when dbo.cxc_cobro_det.dc_ValorPago is null then 'NO' ELSE 'SI' END AS EstadoPago
FROM     dbo.tb_persona AS e INNER JOIN
                  dbo.aca_Alumno AS d INNER JOIN
                  dbo.aca_Matricula AS a INNER JOIN
                  dbo.aca_Matricula_Rubro AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdMatricula = b.IdMatricula ON d.IdEmpresa = a.IdEmpresa AND d.IdAlumno = a.IdAlumno ON e.IdPersona = d.IdPersona LEFT OUTER JOIN
                  dbo.cxc_cobro_det INNER JOIN
                  dbo.cxc_cobro ON dbo.cxc_cobro_det.IdEmpresa = dbo.cxc_cobro.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = dbo.cxc_cobro.IdSucursal AND dbo.cxc_cobro_det.IdCobro = dbo.cxc_cobro.IdCobro RIGHT OUTER JOIN
                  dbo.fa_factura AS c ON dbo.cxc_cobro_det.IdEmpresa = c.IdEmpresa AND dbo.cxc_cobro_det.IdSucursal = c.IdSucursal AND dbo.cxc_cobro_det.IdBodega_Cbte = c.IdBodega AND dbo.cxc_cobro_det.IdCbte_vta_nota = c.IdCbteVta AND 
                  dbo.cxc_cobro_det.dc_TipoDocumento = c.vt_tipoDoc ON b.IdEmpresa = c.IdEmpresa AND b.IdSucursal = c.IdSucursal AND b.IdBodega = c.IdBodega AND b.IdCbteVta = c.IdCbteVta LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON 
                  a.IdEmpresa = cp.IdEmpresa AND a.IdAnio = cp.IdAnio AND a.IdSede = cp.IdSede AND a.IdNivel = cp.IdNivel AND a.IdJornada = cp.IdJornada AND a.IdCurso = cp.IdCurso AND a.IdParalelo = cp.IdParalelo LEFT OUTER JOIN
                  dbo.tb_pais AS h ON d.IdPais = h.IdPais LEFT OUTER JOIN
                  dbo.aca_AnioLectivo_Rubro AS i ON b.IdEmpresa = i.IdEmpresa AND b.IdAnio = i.IdAnio AND b.IdRubro = i.IdRubro LEFT OUTER JOIN
                  dbo.aca_AnioLectivo AS j ON a.IdEmpresa = j.IdEmpresa AND a.IdAnio = j.IdAnio
WHERE  (ISNULL(dbo.cxc_cobro.cr_estado, N'A') = 'A') --and b.IdRubro = 3