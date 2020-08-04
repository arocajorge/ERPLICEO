CREATE VIEW Academico.VWFAC_003
AS
SELECT     fa_notaCreDeb_det.IdEmpresa, fa_notaCreDeb_det.IdSucursal, fa_notaCreDeb_det.IdBodega, fa_notaCreDeb_det.IdNota, fa_notaCreDeb_det.Secuencia, 
                      fa_notaCreDeb_det.IdProducto, in_Producto.pr_descripcion, in_presentacion.nom_presentacion, in_Producto.lote_num_lote, in_Producto.lote_fecha_vcto, 
                      fa_notaCreDeb_det.sc_cantidad, fa_notaCreDeb_det.sc_Precio, fa_notaCreDeb_det.sc_descUni, fa_notaCreDeb_det.sc_PordescUni, 
                      fa_notaCreDeb_det.sc_precioFinal, fa_notaCreDeb_det.sc_descUni * fa_notaCreDeb_det.sc_cantidad AS DescTotal, fa_notaCreDeb_det.sc_subtotal, 
                      iif(fa_notaCreDeb_det.vt_por_iva > 0, fa_notaCreDeb_det.sc_subtotal, 0) AS sc_subtotalIVA, iif(fa_notaCreDeb_det.vt_por_iva = 0, fa_notaCreDeb_det.sc_subtotal, 0) 
                      AS sc_subtotal0, fa_notaCreDeb_det.sc_iva, fa_notaCreDeb_det.vt_por_iva, tb_persona.pe_nombreCompleto Nombres, aca_Alumno.Codigo AS CodigoAlumno, 
                      Su_Descripcion, fa_notaCreDeb.Serie1 + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa AS NumNota_Impresa, fa_notaCreDeb.no_fecha, 
                      fa_notaCreDeb.no_fecha_venc, fa_notaCreDeb.CreDeb, fa_notaCreDeb.sc_observacion, fa_notaCreDeb_det.sc_total, tipo.No_Descripcion, e.NomSede, e.NomNivel, 
                      e.NomJornada, e.NomCurso, e.NomParalelo, e.CodigoParalelo, e.NomPlantilla, fa_notaCreDeb.IdUsuario, fa_notaCreDeb.Fecha_UltMod, 
                      fa_notaCreDeb.FechaTransaccion
FROM         in_presentacion INNER JOIN
                      in_Producto ON in_presentacion.IdEmpresa = in_Producto.IdEmpresa AND in_presentacion.IdPresentacion = in_Producto.IdPresentacion INNER JOIN
                      fa_notaCreDeb INNER JOIN
                      fa_notaCreDeb_det ON fa_notaCreDeb.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND fa_notaCreDeb.IdSucursal = fa_notaCreDeb_det.IdSucursal AND 
                      fa_notaCreDeb.IdBodega = fa_notaCreDeb_det.IdBodega AND fa_notaCreDeb.IdNota = fa_notaCreDeb_det.IdNota ON 
                      in_Producto.IdEmpresa = fa_notaCreDeb_det.IdEmpresa AND in_Producto.IdProducto = fa_notaCreDeb_det.IdProducto INNER JOIN
                      aca_Alumno ON fa_notaCreDeb.IdEmpresa = aca_Alumno.IdEmpresa AND fa_notaCreDeb.IdAlumno = aca_Alumno.IdAlumno INNER JOIN
                      tb_persona ON tb_persona.IdPersona = aca_Alumno.IdPersona INNER JOIN
                      tb_sucursal ON fa_notaCreDeb.IdEmpresa = tb_sucursal.IdEmpresa AND fa_notaCreDeb.IdSucursal = tb_sucursal.IdSucursal INNER JOIN
                      fa_TipoNota AS tipo ON fa_notaCreDeb.IdTipoNota = tipo.IdTipoNota AND fa_notaCreDeb.IdEmpresa = tipo.IdEmpresa LEFT OUTER JOIN
                          (SELECT     A1.IdEmpresa, A1.IdAnio, A1.IdAlumno, SedeNivel.NomSede, SedeNivel.NomNivel, NivelJornada.NomJornada, JornadaCurso.NomCurso, 
                                                   CursoParalelo.NomParalelo, CursoParalelo.CodigoParalelo, A3.NomPlantilla
                            FROM          dbo.aca_AnioLectivo_NivelAcademico_Jornada AS NivelJornada INNER JOIN
                                                   dbo.aca_AnioLectivo_Sede_NivelAcademico AS SedeNivel ON NivelJornada.IdEmpresa = SedeNivel.IdEmpresa AND 
                                                   NivelJornada.IdAnio = SedeNivel.IdAnio AND NivelJornada.IdSede = SedeNivel.IdSede AND NivelJornada.IdNivel = SedeNivel.IdNivel INNER JOIN
                                                   dbo.aca_AnioLectivo_Jornada_Curso AS JornadaCurso ON NivelJornada.IdEmpresa = JornadaCurso.IdEmpresa AND 
                                                   NivelJornada.IdAnio = JornadaCurso.IdAnio AND NivelJornada.IdSede = JornadaCurso.IdSede AND NivelJornada.IdNivel = JornadaCurso.IdNivel AND 
                                                   NivelJornada.IdJornada = JornadaCurso.IdJornada INNER JOIN
                                                   dbo.aca_AnioLectivo_Curso_Paralelo AS CursoParalelo ON JornadaCurso.IdEmpresa = CursoParalelo.IdEmpresa AND 
                                                   JornadaCurso.IdAnio = CursoParalelo.IdAnio AND JornadaCurso.IdSede = CursoParalelo.IdSede AND JornadaCurso.IdNivel = CursoParalelo.IdNivel AND 
                                                   JornadaCurso.IdJornada = CursoParalelo.IdJornada AND JornadaCurso.IdCurso = CursoParalelo.IdCurso RIGHT OUTER JOIN
                                                   dbo.aca_Matricula AS A1 INNER JOIN
                                                   dbo.aca_AnioLectivo AS A2 ON A1.IdEmpresa = A2.IdEmpresa AND A1.IdAnio = A2.IdAnio ON CursoParalelo.IdParalelo = A1.IdParalelo AND 
                                                   CursoParalelo.IdEmpresa = A1.IdEmpresa AND CursoParalelo.IdAnio = A1.IdAnio AND CursoParalelo.IdSede = A1.IdSede AND 
                                                   CursoParalelo.IdNivel = A1.IdNivel AND CursoParalelo.IdJornada = A1.IdJornada AND CursoParalelo.IdCurso = A1.IdCurso LEFT OUTER JOIN
                                                   dbo.aca_Plantilla AS A3 ON A1.IdEmpresa = A3.IdEmpresa AND A1.IdAnio = A3.IdAnio AND A1.IdPlantilla = A3.IdPlantilla
                            WHERE      (A2.EnCurso = 1)) AS e ON fa_notaCreDeb.IdEmpresa = e.IdEmpresa AND fa_notaCreDeb.IdAlumno = e.IdAlumno
WHERE     fa_notaCreDeb.IdAlumno IS NOT NULL
GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWFAC_003';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[24] 4[19] 2[25] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'Academico', @level1type = N'VIEW', @level1name = N'VWFAC_003';



