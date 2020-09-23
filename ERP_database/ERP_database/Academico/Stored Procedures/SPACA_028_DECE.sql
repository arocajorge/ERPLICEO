
create PROCEDURE [Academico].[SPACA_028_DECE]
(
@IdEmpresa int,
@IdAnio int,
@IdSede int,
@IdNivel int,
@IdJornada int,
@IdCurso int,
@IdParalelo int,
@IdCatalogoParcialTipo int
)
AS
SELECT IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, 
case when @IdCatalogoParcialTipo = 6 then ObservacionQ1 else
case when @IdCatalogoParcialTipo = 7 then ObservacionQ2 else null end
end as ObservacionDECE
FROM     dbo.aca_AnioLectivo_Curso_Paralelo_Dece AS d
where d.IdEmpresa= @IdEmpresa 
and d.IdAnio = @IdAnio
and d.IdSede = @IdSede
and d.IdNivel = @IdNivel
and d.IdJornada = @IdJornada
and d.IdCurso = @IdCurso
and d.IdParalelo = @IdParalelo