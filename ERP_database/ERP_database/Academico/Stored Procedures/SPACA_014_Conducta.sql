create PROCEDURE [Academico].[SPACA_014_Conducta]
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
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv1 on equiv1.IdEmpresa=mco.IdEmpresa and equiv1.IdAnio=m.IdAnio and equiv1.Secuencia = 
CASE WHEN mco.SecuenciaPromedioFinalP1 IS NULL THEN mco.SecuenciaPromedioP1 ELSE mco.SecuenciaPromedioFinalP1 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv2 on equiv2.IdEmpresa=mco.IdEmpresa and equiv2.IdAnio=m.IdAnio and equiv2.Secuencia = 
CASE WHEN mco.SecuenciaPromedioFinalP2 IS NULL THEN mco.SecuenciaPromedioP2 ELSE mco.SecuenciaPromedioFinalP2 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv3 on equiv3.IdEmpresa=mco.IdEmpresa and equiv3.IdAnio=m.IdAnio and equiv3.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinalP3 IS NULL THEN mco.SecuenciaPromedioP3 ELSE mco.SecuenciaPromedioFinalP3 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq1 on equivq1.IdEmpresa=mco.IdEmpresa and equivq1.IdAnio=m.IdAnio and equivq1.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinalQ1 IS NULL THEN mco.SecuenciaPromedioQ1 ELSE mco.SecuenciaPromedioFinalQ1 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv4 on equiv4.IdEmpresa=mco.IdEmpresa and equiv4.IdAnio=m.IdAnio and equiv4.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinalP4 IS NULL THEN mco.SecuenciaPromedioP4 ELSE mco.SecuenciaPromedioFinalP4 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv5 on equiv5.IdEmpresa=mco.IdEmpresa and equiv5.IdAnio=m.IdAnio and equiv5.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinalP5 IS NULL THEN mco.SecuenciaPromedioP5 ELSE mco.SecuenciaPromedioFinalP5 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv6 on equiv6.IdEmpresa=mco.IdEmpresa and equiv6.IdAnio=m.IdAnio and equiv6.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinalP6 IS NULL THEN mco.SecuenciaPromedioP6 ELSE mco.SecuenciaPromedioFinalP6 END 
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq2 on equivq2.IdEmpresa=mco.IdEmpresa and equivq2.IdAnio=m.IdAnio and equivq2.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinalQ2 IS NULL THEN mco.SecuenciaPromedioQ2 ELSE mco.SecuenciaPromedioFinalQ2 END
LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivf on equivf.IdEmpresa=mco.IdEmpresa and equivf.IdAnio=m.IdAnio and equivf.Secuencia =
CASE WHEN mco.SecuenciaPromedioFinal IS NULL THEN mco.SecuenciaPromedioGeneral ELSE mco.SecuenciaPromedioFinal END

where mco.IdEmpresa = @IdEmpresa 
and m.IdMatricula = @IdMatricula