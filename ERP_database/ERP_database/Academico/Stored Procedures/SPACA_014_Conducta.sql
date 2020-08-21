CREATE PROCEDURE [Academico].[SPACA_014_Conducta]
(
@IdEmpresa int,
@IdMatricula numeric
)
AS
SELECT mco.IdEmpresa, mco.IdMatricula, 
equiv1.Secuencia  as SecuenciaP1,equiv1.Letra as LetraP1,
equiv2.Secuencia  as SecuenciaP2,equiv2.Letra as LetraP2,
equiv3.Secuencia  as SecuenciaP3,equiv3.Letra as LetraP3,
equivq1.Secuencia  as SecuenciaQ1,equivq1.Letra as LetraQ1,
equiv4.Secuencia  as SecuenciaP4,equiv4.Letra as LetraP4,
equiv5.Secuencia  as SecuenciaP5,equiv5.Letra as LetraP5,
equiv6.Secuencia  as SecuenciaP6,equiv6.Letra as LetraP6,
equivq2.Secuencia  as SecuenciaQ2,equivq2.Letra as LetraQ2,
equivf.Secuencia  as SecuenciaPF,equivf.Letra as LetraPF
FROM     dbo.aca_MatriculaConducta AS mco 
LEFT OUTER JOIN dbo.aca_Matricula AS m ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv1 on equiv1.IdEmpresa=mco.IdEmpresa and equiv1.IdAnio=m.IdAnio and equiv1.Secuencia = mco.SecuenciaPromedioFinalP1
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv2 on equiv2.IdEmpresa=mco.IdEmpresa and equiv2.IdAnio=m.IdAnio and equiv2.Secuencia = mco.SecuenciaPromedioFinalP2
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv3 on equiv3.IdEmpresa=mco.IdEmpresa and equiv3.IdAnio=m.IdAnio and equiv3.Secuencia = mco.SecuenciaPromedioFinalP3
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq1 on equivq1.IdEmpresa=mco.IdEmpresa and equivq1.IdAnio=m.IdAnio and equivq1.Secuencia = mco.SecuenciaPromedioFinalQ1
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv4 on equiv4.IdEmpresa=mco.IdEmpresa and equiv4.IdAnio=m.IdAnio and equiv4.Secuencia = mco.SecuenciaPromedioFinalP4
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv5 on equiv5.IdEmpresa=mco.IdEmpresa and equiv5.IdAnio=m.IdAnio and equiv5.Secuencia = mco.SecuenciaPromedioFinalP5
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv6 on equiv6.IdEmpresa=mco.IdEmpresa and equiv6.IdAnio=m.IdAnio and equiv6.Secuencia = mco.SecuenciaPromedioFinalP6
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq2 on equivq2.IdEmpresa=mco.IdEmpresa and equivq2.IdAnio=m.IdAnio and equivq2.Secuencia = mco.SecuenciaPromedioFinalQ2
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivf on equivf.IdEmpresa=mco.IdEmpresa and equivf.IdAnio=m.IdAnio and equivf.Secuencia = mco.SecuenciaPromedioFinal
where mco.IdEmpresa = @IdEmpresa 
and m.IdMatricula = @IdMatricula