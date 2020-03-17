CREATE VIEW Academico.VWFAC_003
AS
SELECT fa_notaCreDeb_det.IdEmpresa, fa_notaCreDeb_det.IdSucursal, fa_notaCreDeb_det.IdBodega, fa_notaCreDeb_det.IdNota, fa_notaCreDeb_det.Secuencia, fa_notaCreDeb_det.IdProducto, in_Producto.pr_descripcion, 
                  in_presentacion.nom_presentacion, in_Producto.lote_num_lote, in_Producto.lote_fecha_vcto, fa_notaCreDeb_det.sc_cantidad, fa_notaCreDeb_det.sc_Precio, fa_notaCreDeb_det.sc_descUni, fa_notaCreDeb_det.sc_PordescUni, 
                  fa_notaCreDeb_det.sc_precioFinal, fa_notaCreDeb_det.sc_descUni * fa_notaCreDeb_det.sc_cantidad AS DescTotal, fa_notaCreDeb_det.sc_subtotal, iif(fa_notaCreDeb_det.vt_por_iva > 0, fa_notaCreDeb_det.sc_subtotal, 0) 
                  AS sc_subtotalIVA, iif(fa_notaCreDeb_det.vt_por_iva = 0, fa_notaCreDeb_det.sc_subtotal, 0) AS sc_subtotal0, fa_notaCreDeb_det.sc_iva, fa_notaCreDeb_det.vt_por_iva, tb_persona.pe_nombreCompleto Nombres, aca_Alumno.Codigo AS CodigoAlumno, Su_Descripcion, 
                  fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa AS NumNota_Impresa, fa_notaCreDeb.no_fecha, fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.CreDeb, fa_notaCreDeb.sc_observacion, 
                  fa_notaCreDeb_det.sc_total, tipo.No_Descripcion, e.NomSede, e.NomNivel, e.NomJornada, e.NomCurso, e.NomParalelo, e.CodigoParalelo, e.NomPlantilla, fa_notaCreDeb.IdUsuario, fa_notaCreDeb.Fecha_UltMod, fa_notaCreDeb.FechaTransaccion
FROM     in_presentacion INNER JOIN
                  in_Producto ON in_presentacion.IdEmpresa = in_Producto.IdEmpresa AND in_presentacion.IdPresentacion = in_Producto.IdPresentacion INNER JOIN
                  fa_notaCreDeb INNER JOIN
                  fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND 
                  fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota ON in_Producto.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND in_Producto.IdProducto = fa_notaCreDeb_det.IdProducto INNER JOIN
                  aca_Alumno ON fa_notaCreDeb.IdEmpresa = aca_Alumno.IdEmpresa AND fa_notaCreDeb.IdCliente = aca_Alumno.IdAlumno INNER JOIN
				  tb_persona on tb_persona.IdPersona = aca_Alumno.IdPersona inner join
                  tb_sucursal ON fa_notaCreDeb.IdEmpresa = tb_sucursal.IdEmpresa AND fa_notaCreDeb.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                  fa_TipoNota AS tipo ON fa_notaCreDeb.IdTipoNota = tipo.IdTipoNota AND fa_notaCreDeb.IdEmpresa = tipo.IdEmpresa LEFT OUTER JOIN
                      (SELECT A1.IdEmpresa, A1.IdAnio, A1.IdAlumno, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, JornadaCurso.NomCurso, CursoParalelo.NomParalelo, CursoParalelo.CodigoParalelo, A3.NomPlantilla
                       FROM      dbo.aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel ON NivelJornada.IdEmpresa = SedeNivel.IdEmpresa AND NivelJornada.IdAnio = SedeNivel.IdAnio AND NivelJornada.IdSede = SedeNivel.IdSede AND 
                                         NivelJornada.IdNivel = SedeNivel.IdNivel INNER JOIN
                                         dbo.aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND 
                                         NivelJornada.IdNivel = JornadaCurso.IdNivel AND NivelJornada.IdJornada = JornadaCurso.IdJornada INNER JOIN
                                         dbo.aca_AnioLectivo_Curso_Paralelo AS CursoParalelo ON JornadaCurso.IdEmpresa = CursoParalelo.IdEmpresa AND JornadaCurso.IdAnio = CursoParalelo.IdAnio AND JornadaCurso.IdSede = CursoParalelo.IdSede AND 
                                         JornadaCurso.IdNivel = CursoParalelo.IdNivel AND JornadaCurso.IdJornada = CursoParalelo.IdJornada AND JornadaCurso.IdCurso = CursoParalelo.IdCurso RIGHT OUTER JOIN
                                         dbo.aca_Matricula AS A1 INNER JOIN
                                         dbo.aca_AnioLectivo AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdAnio = A2.IdAnio ON CursoParalelo.IdParalelo = A1.IdParalelo AND CursoParalelo.IdEmpresa = A1.IdEmpresa AND CursoParalelo.IdAnio = A1.IdAnio AND 
                                         CursoParalelo.IdSede = A1.IdSede AND CursoParalelo.IdNivel = A1.IdNivel AND CursoParalelo.IdJornada = A1.IdJornada AND CursoParalelo.IdCurso = A1.IdCurso LEFT OUTER JOIN
                                         dbo.aca_Plantilla AS A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdAnio = A3.IdAnio AND A1.IdPlantilla = A3.IdPlantilla
                       WHERE   (A2.EnCurso = 1)) AS e ON fa_notaCreDeb.IdEmpresa = e.IdEmpresa AND fa_notaCreDeb.IdAlumno = e.IdAlumno 
WHERE  fa_notaCreDeb.IdAlumno IS NOT NULL