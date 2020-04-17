CREATE VIEW Academico.VWACA_012
AS
SELECT a.IdEmpresa, c.IdSucursal, c.IdBodega, c.IdCbteVta, b.IdPeriodo, b.IdRubro, a.IdAlumno, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, c.vt_serie1+'-'+ c.vt_serie2+'-'+ c.vt_NumFactura vt_NumFactura, c.vt_fecha, c.vt_Observacion, 
                  cxc_cobro.cr_fecha, cxc_cobro_det.dc_ValorPago, sn.NomSede, sn.OrdenNivel, sn.NomNivel, nj.OrdenJornada, nj.NomJornada, jc.OrdenCurso, jc.NomCurso, cp.OrdenParalelo, cp.NomParalelo, d.Codigo as CodigoAlumno,
				  e.pe_cedulaRuc, e.pe_nombreCompleto, e.pe_fechaNacimiento, h.Nacionalidad, i.NomRubro, j.Descripcion as DescripcionAnio
FROM     aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN
                  aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN
                  aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN
                  aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso RIGHT OUTER JOIN
                  cxc_cobro_det INNER JOIN
                  cxc_cobro ON cxc_cobro_det.IdEmpresa = cxc_cobro.IdEmpresa AND cxc_cobro_det.IdSucursal = cxc_cobro.IdSucursal AND cxc_cobro_det.IdCobro = cxc_cobro.IdCobro INNER JOIN
                  fa_factura AS c ON cxc_cobro_det.IdEmpresa = c.IdEmpresa AND cxc_cobro_det.IdSucursal = c.IdSucursal AND cxc_cobro_det.IdBodega_Cbte = c.IdBodega AND cxc_cobro_det.IdCbte_vta_nota = c.IdCbteVta AND 
                  cxc_cobro_det.dc_TipoDocumento = c.vt_tipoDoc RIGHT OUTER JOIN
                  aca_Matricula AS a INNER JOIN
                  aca_Matricula_Rubro AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdMatricula = b.IdMatricula ON c.IdEmpresa = b.IdEmpresa AND c.IdSucursal = b.IdSucursal AND c.IdBodega = b.IdBodega AND c.IdCbteVta = b.IdCbteVta ON 
                  cp.IdEmpresa = a.IdEmpresa AND cp.IdAnio = a.IdAnio AND cp.IdSede = a.IdSede AND cp.IdNivel = a.IdNivel AND cp.IdJornada = a.IdJornada AND cp.IdCurso = a.IdCurso AND cp.IdParalelo = a.IdParalelo inner join
				  aca_Alumno as d on d.IdEmpresa = a.IdEmpresa and d.IdAlumno = a.IdAlumno inner join
				  tb_persona as e on d.IdPersona = e.IdPersona left join
				  tb_pais as h on d.IdPais = h.IdPais left join 
				  aca_AnioLectivo_Rubro as i on b.IdEmpresa = i.IdEmpresa and b.IdAnio = i.IdAnio and b.IdRubro = i.IdRubro left join
				  aca_AnioLectivo as j on a.IdEmpresa = j.IdEmpresa and a.IdAnio = j.IdAnio
where cxc_cobro.cr_estado = 'A'